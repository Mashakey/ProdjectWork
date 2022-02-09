using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using static DataTypes;

public class FeedbackSender : MonoBehaviour
{
    private string subject = "Обратная связь";
    public GameObject sendMessage;
    public Sprite activeSend;
    public InputField Name;
    public InputField Email;
    public InputField Phone;
    public InputField Message;

    public GameObject UIFeedback;
    public GameObject menu;
    

    public void SendMessageFeedback()
    {
        string message = Message.text;
        string name = Name.text;
        string email = Email.text;
        string phone = Phone.text;

        Debug.LogError("message: "+ message + ", name: " + name + ", email: " + email + ", phone: " + phone );

        if (sendMessage.GetComponent<Image>().sprite == activeSend)
        {
            SendFeedback(message,name, email,phone);
            GameObject.Find("FeedbackThanks").SetActive(true);
        }
    }

    public void SendFeedback(string message, string name, string email, string phone)
	{
        string body = "";
        body += "name=" + System.Web.HttpUtility.UrlEncode(name);
        body += "&email=" + System.Web.HttpUtility.UrlEncode(email);
        body += "&phone=" + System.Web.HttpUtility.UrlEncode(phone);
        body += "&message=" + System.Web.HttpUtility.UrlEncode(message);
        body += "&subject=" + System.Web.HttpUtility.UrlEncode(subject);
        StartCoroutine(SendFeedback(body));

        GameObject.Find("NameFeedback").GetComponent<MessageFeedback>().ResetOutlineBack();
    }

    public IEnumerator SendFeedback(string body)
    {
        string link = ServerDownloaderSettings.FeedbackURI + ServerDownloaderSettings.send_feedback + body;
        string postData = "{}";

        using (var postWebRequest = UnityWebRequest.Post(link, postData))
        {
            postWebRequest.SetRequestHeader("Authorization", "Token ezkiz-jwt-secret");
            postWebRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(postData));
            postWebRequest.SetRequestHeader("Accept", "application/json");
            postWebRequest.SetRequestHeader("Content-Type", "application/json");
            yield return postWebRequest.SendWebRequest();
            while (!postWebRequest.isDone) yield return null;
            if (postWebRequest.isNetworkError || postWebRequest.isHttpError)
            {
                OnSendFeedbackError(postWebRequest.error, postWebRequest.downloadHandler.text);
                yield break;
            }
            string debugString = "Loading to " + link + " completed.\nData sent:\n" + postData + "\nResponse:\n" + postWebRequest.responseCode + "\nHeaders:\n";
            Dictionary<string, string> headers = postWebRequest.GetResponseHeaders();
            foreach (var header in headers)
            {
                debugString += header.Key + ": " + header.Value + "\n";
            }
            OnSendFeedbackSucces(debugString);
        }
        yield break;
    }

    public void OnSendFeedbackSucces(string message)
	{
        Debug.LogWarning(message);
        menu.SetActive(true);
        UIFeedback.SetActive(false);
	}

    public void OnSendFeedbackError(string errorLink, string errorData)
    {
        //Debug.LogErrorFormat($"Can't send feedback on link '{errorLink}' {errorData}");
        Debug.LogErrorFormat($"Can't send feedback on link '{errorLink}'");
    }

}
