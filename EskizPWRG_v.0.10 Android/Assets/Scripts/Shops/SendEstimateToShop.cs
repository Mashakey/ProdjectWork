using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SendEstimateToShop : MonoBehaviour
{
    [SerializeField]
    InputField nameInputField;
    [SerializeField]
    InputField emailInputField;
    [SerializeField]
    InputField phoneInputField;
    [SerializeField]
    InputField messageInputField;

    [SerializeField]
    GameObject succesWindow;
    [SerializeField]
    GameObject errorWindow;

    [SerializeField]
    Button proceedToSendToShopPageButton;
    [SerializeField]
    Image proceedToSendToShopPageButtonImage;
    [SerializeField]
    Sprite activeButtonSprite;
    [SerializeField]
    Sprite unactiveButtonSprite;

    [SerializeField]
    Button SendEstimateButton;

    [SerializeField]
    GameObject CheckMark;

    List<string> shopsIds = new List<string>();

    string currentShopId = "tempId";

    public bool isDuplicateEstimateToUserEmail = false;

    public void AddOrDeleteShopInList(string shopId)
    {
        Debug.LogFormat("Trying to add or remove shop {0}", shopId);
        if (!shopsIds.Contains(shopId))
        {
            AddShopToList(shopId);
        }
        else
        {
            DeleteShopFromList(shopId);
        }
        if (shopsIds.Count > 0)
		{
            SetProceedButtonActive();
		}
        else
		{
            SetProceedButtonUnactive();
		}
    }

    public void AddShopToList(string shopId)
    {
        Debug.LogFormat("Shop {0} is added to list", shopId);
        shopsIds.Add(shopId);
    }

    public void DeleteShopFromList(string shopId)
    {
        Debug.LogFormat("Shop {0} is removed from list", shopId);
        shopsIds.Remove(shopId);
    }

    public void ClearShopsList()
    {
        Debug.Log("Cleaning selected shops list");
        shopsIds.Clear();
        DeactivateDuplicateEstimateToUserEmail();
        SetProceedButtonUnactive();
    }

    public void ChangeDuplicateEmailCheckboxStatus()
	{
        if (isDuplicateEstimateToUserEmail)
		{
            DeactivateDuplicateEstimateToUserEmail();
		}
        else
		{
            ActivateDuplicateEstimateToUserEmail();
		}
	}

    public void ActivateDuplicateEstimateToUserEmail()
	{
        CheckMark.SetActive(true);
        isDuplicateEstimateToUserEmail = true;
	}

    public void DeactivateDuplicateEstimateToUserEmail()
	{
        CheckMark.SetActive(false);
        isDuplicateEstimateToUserEmail = false;
	}

    public void SetProceedButtonActive()
	{
        proceedToSendToShopPageButton.interactable = true;
        proceedToSendToShopPageButtonImage.sprite = activeButtonSprite;
	}

    public void SetProceedButtonUnactive()
	{
        proceedToSendToShopPageButton.interactable = false;
        proceedToSendToShopPageButtonImage.sprite = unactiveButtonSprite;
    }

    public void OnSendEstimateToShopButtonClick()
    {
        SendEstimateButton.enabled = false;
        bool isDuplicateToUserOneTime = isDuplicateEstimateToUserEmail;
        for (int i = 0; i < shopsIds.Count; i++)
        {
            string body = "";
            body += "{";
            body += "\"name\":\"" + nameInputField.text + "\",";
            if (emailInputField.text.Length > 0) body += "\"email\":\"" + emailInputField.text + "\",";
            if (phoneInputField.text.Length > 0) body += "\"phone\":\"" + phoneInputField.text + "\",";
            if (messageInputField.text.Length > 0) body += "\"message\":\"" + messageInputField.text.Replace("\"", "'") + "\",";

            Room room = FindObjectOfType<Room>();
            body += "\"data\":" + RoomCostCalculator.EstimatePdfJson + ",";
            //body += "\"data\":" + "{}" + ",";
            body += "\"storeId\":\"" + shopsIds[i] + "\",";
            if (isDuplicateToUserOneTime)
			{
                body += "\"withEmail\":" + "true";
            }
            else
			{
                body += "\"withEmail\":" + "false";
            }
            isDuplicateToUserOneTime = false;
            body += "}";
            Debug.Log("Going to send price:\n" + body);
            StartCoroutine(SendPrice(body));
        }
    }

    public IEnumerator SendPrice(string body)
    {
        string link = ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.send_estimate_to_shop;
        yield return PostDataByLink(link, body);
        yield break;
    }

     IEnumerator PostDataByLink(string link, string postData)
    {
        using (var www = UnityWebRequest.Post(link, postData))
        {
            www.SetRequestHeader("Authorization", "Token ezkiz-jwt-secret");
            //www.SetRequestHeader("Content-Type", "application/json");
            byte[] test = System.Text.Encoding.UTF8.GetBytes(postData);
            string testStr = System.BitConverter.ToString(System.Text.Encoding.UTF8.GetBytes(postData));
           // Debug.LogError(testStr);
   //         for (int i = 0; i < test.Length; i++)
			//{
   //             Debug.Log(i + ": " + System.BitConverter.ToChar(test, i));
			//}
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(postData));
            www.SetRequestHeader("Accept", "application/json");
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            while (!www.isDone) yield return null;
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
                Debug.LogError(www.downloadHandler.text);
                OnSendEstimateError();
                yield break;
            }
            string debugSting = "Loading to " + link + " completed.\nData sent:\n" + postData + "\nResponse:\n" + www.responseCode + "\nHeaders:\n";
            Dictionary<string, string> headers = www.GetResponseHeaders();
            foreach (var header in headers) debugSting += header.Key + ": " + header.Value + "\n";
            //MyLogger.Log(debugSting);
            OnSendEstimateSucces();
        }
        yield break;
    }

    public void OnSendEstimateError()
    {
        SendEstimateButton.enabled = true;

        errorWindow.SetActive(true);
        Debug.LogError("Error on send estimate to shop");
    }

    public void OnSendEstimateSucces()
    {
        SendEstimateButton.enabled = true;

        succesWindow.SetActive(true);

        Debug.Log("Estimate has been succesifully sended to shop");
    }
}
