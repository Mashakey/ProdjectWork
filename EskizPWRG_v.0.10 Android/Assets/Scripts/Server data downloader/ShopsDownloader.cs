using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using static DataTypes;

public class ShopsDownloader : MonoBehaviour
{
	public int totalShopsAmount = 0;
	public int limitShopssDownloadPerCycle = 1;

	public void StartDownload()
	{
		StartCoroutine(GetShopsFromServer());
	}

    IEnumerator GetTotalShopsCountAndLimitPerLoad()
	{
		var request = UnityWebRequest.Get(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_shops);
		HTTPHeader autorizationHeader = ServerDownloaderSettings.GetAutorizationHeader();
		request.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
		request.SendWebRequest();

		while (!request.isDone)
		{
			yield return null;
		}
		if (request.isNetworkError || request.isHttpError)
		{
			Debug.LogError(request.error);
			yield break;
		}
		var text = request.downloadHandler.text;
		ShopsWithHeader shopsWithHeader = JsonConvert.DeserializeObject<ShopsWithHeader>(text);
		totalShopsAmount = shopsWithHeader.total;
		limitShopssDownloadPerCycle = shopsWithHeader.limit;
		yield break;
	}

	IEnumerator GetShopsFromServer()
	{
		yield return (StartCoroutine(GetTotalShopsCountAndLimitPerLoad()));

		for (int i = 0; i < totalShopsAmount; i += limitShopssDownloadPerCycle)
		{
			string url = ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_shops + "?query={\"$skip\":" + i + ",\"$limit\":" + limitShopssDownloadPerCycle + "}";
			var request = UnityWebRequest.Get(url);
			HTTPHeader autorizationHeader = ServerDownloaderSettings.GetAutorizationHeader();
			request.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
			request.SendWebRequest();
			while (!request.isDone)
			{
				yield return null;
			}
			if (request.isNetworkError || request.isHttpError)
			{
				Debug.LogError(request.error);
				yield break;
			}

			var text = request.downloadHandler.text;
			ShopsWithHeader shopsWithHeader = JsonConvert.DeserializeObject<ShopsWithHeader>(text);
			foreach (var data in shopsWithHeader.data)
			{
				GlobalApplicationManager.Shops.Add(data);
			}
		}

	}
}
