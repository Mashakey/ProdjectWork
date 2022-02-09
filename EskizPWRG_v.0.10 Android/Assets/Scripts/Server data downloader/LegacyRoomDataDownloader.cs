using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using static DataTypes;

public class LegacyRoomDataDownloader : MonoBehaviour
{
	public StartLoadingScreen startLoadingScreen;

	public IEnumerator GetLegacyRoomsData()
	{
		//string url = devUri + get_rooms;
		//string url = ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_rooms;
		string url = ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_rooms;
		using (var www = UnityWebRequest.Get(url))
		{
			www.SetRequestHeader("Authorization", "Token ezkiz-jwt-secret");
			yield return www.SendWebRequest();

			while (!www.isDone) yield return null;
			if (www.isNetworkError || www.isHttpError)
			{

#if DEBUG_QUERIES
				Debug.LogWarning(www.error);
#endif
				startLoadingScreen.OpenErrorPopup();

				Debug.LogError(www.error);
				Debug.LogError(url);
				yield break;
			}
			var text = www.downloadHandler.text;

#if DEBUG_QUERIES
			MyLogger.Log("Loading from " + link + " completed.\nLoadedRawData:\n" + text);
#endif
			LegacyRoomData[] roomsData = JsonConvert.DeserializeObject<LegacyRoomData[]>(text);
			for (int i = 0; i < roomsData.Length; i++)
			{
				yield return (StartCoroutine(GetLegacyRoomPreview(roomsData[i])));
				//downloadedLegacyRooms.Add(roomsData[i]);
			}
			//roomCreator.CreateRoomFromLegacy(roomsData[0]);
			//try
			//{
			//	LegacyRoomData[] roomsData = JsonConvert.DeserializeObject<LegacyRoomData[]>(text);
			//	for (int i = 0; i < roomsData.Length; i++)
			//	{
			//		//downloadedLegacyRooms.Add(roomsData[i]);
			//	}
			//	RoomCreator.CreateRoomFromLegacy(roomsData[0]);

			//}
			//catch (System.Exception e)
			//{
			//	Debug.LogError(e.Message + "\nJSON converting error on link " + url);
			//}
		}
		//filler.CreateRoomTemplatesList();
		Debug.LogWarning("LEgacy rooms downloaded");
		yield break;
	}

	public IEnumerator GetLegacyRoomPreview(LegacyRoomData roomDataJson)
	{
		//string getRoomPreviewURL = ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + roomDataJson.id + "_" + roomDataJson.preview_icon;
		string getRoomPreviewURL = ServerDownloaderSettings.prodUri + ServerDownloaderSettings.get_textures + roomDataJson.id + "_" + roomDataJson.preview_icon;
		var previewRequest = UnityWebRequestTexture.GetTexture(getRoomPreviewURL);
		HTTPHeader autorizationHeader = ServerDownloaderSettings.GetAutorizationHeader();
		previewRequest.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
		previewRequest.SendWebRequest();
		while (!previewRequest.isDone)
		{
			yield return null;
		}
		if (previewRequest.isNetworkError || previewRequest.isHttpError)
		{
			Debug.LogError(previewRequest.error + "\n" + getRoomPreviewURL);
			//JsonDownloadCompleted?.Invoke("network error");
			yield break;
		}
		else
		{
			Texture2D previewTexture = DownloadHandlerTexture.GetContent(previewRequest);
			previewTexture.name = roomDataJson.id + "_" + roomDataJson.preview_icon;
			DataCacher.CacheLegacyRoomData(roomDataJson, previewTexture);
		}
	}

	private void Start()
	{
		StartCoroutine(GetLegacyRoomsData());
	}
}
