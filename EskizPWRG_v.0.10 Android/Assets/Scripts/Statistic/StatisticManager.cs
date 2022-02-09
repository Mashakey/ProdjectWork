using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DataTypes;

public class StatisticManager : MonoBehaviour
{
    private readonly string linkAddDevice = "devices/add";
    private readonly string linkAddStats = "device/stats/add";

    private readonly string keyDeviceID = "deviceId";
    private readonly string keyDeviceModel = "deviceModel";
    private readonly string keyDeviceOS = "deviceOS";
        
    private readonly string keyEvent = "event";
    private readonly string keyEventAddition = "addition";

    private string _deviceId;
        
        
    private void Start()
    {
        DontDestroyOnLoad(this);
        _deviceId = SystemInfo.deviceUniqueIdentifier;
        SendNewDeviceInfo();
    }

    private void SendNewDeviceInfo()
    {
        var body = new DeviceObj(_deviceId, SystemInfo.deviceModel, SystemInfo.operatingSystem);
            
        var bodystring = JsonUtility.ToJson(body);
        StartCoroutine(SendStatistic(bodystring, linkAddDevice));
    }

    private void SendStatistic(EventObj eventObj)
    {
        //var body = new EventObj(_deviceId,(int)message.EventID,message.EventAdditional);    
        //var bodyString = JsonUtility.ToJson(body);

        var bodyString = JsonUtility.ToJson(eventObj);
        StartCoroutine(SendStatistic(bodyString, linkAddStats));
    }

    public IEnumerator SendStatistic(string body, string _link)
    {
        string link =ServerDownloaderSettings.UsingURI + @"/api/" + _link;
        yield return StartCoroutine(PostDataByLink(link, body));
        yield break;
    }

    IEnumerator PostDataByLink(string link, string postData)
    {
        using (var www = UnityWebRequest.Post(link, postData))
        {
            www.SetRequestHeader("Authorization", "Token ezkiz-jwt-secret");
            //www.SetRequestHeader("Content-Type", "application/json");
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(postData));
            www.SetRequestHeader("Accept", "application/json");
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();
            while (!www.isDone) yield return null;
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogWarning(www.error);
                Debug.LogWarning(www.downloadHandler.text);
                OnPostDataError();
                yield break;
            }
            string debugSting = "Loading to " + link + " completed.\nData sent:\n" + postData + "\nResponse:\n" + www.responseCode + "\nHeaders:\n";
            Dictionary<string, string> headers = www.GetResponseHeaders();
            foreach (var header in headers) debugSting += header.Key + ": " + header.Value + "\n";
            //MyLogger.Log(debugSting);
            OnPostDataSucces();
        }
        yield break;
    }

    public static void OnPostDataSucces()
	{

	}

    public static void OnPostDataError()
	{

	}
}