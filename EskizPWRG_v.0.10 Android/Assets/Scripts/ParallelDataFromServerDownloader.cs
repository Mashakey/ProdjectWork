using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using static DataTypes;

public class ParallelDataFromServerDownloader : MonoBehaviour
{
	[SerializeField]
	int maximumTasksAtTime = 10;


	public static string tempUri = @"https://apidev.ezkiz.ru";
	public static string devUri = @"https://api.ezkiz.ru";
	//public static string devUri = @"https://apidev.ezkiz.ru";
	public static string prodUri = @"https://api.ezkiz.ru";
	public static string UsingURI = prodUri;

	const string get_products = @"/api/products";
	const string get_categories = @"/api/categories";
	const string get_ceilings = @"/api/ceilings";
	const string get_shops = @"/api/stores";
	const string post_orders = @"/api/orders";
	const string api = @"/api/";
	const string get_banner = @"/api/banner";
	const string get_rooms = @"/api/rooms?paginate=false";
	const string get_textures = @"/api/__uploads/";

	const string authorizationHeaderKey = "Authorization";
	const string authorizationHeaderValue = "Token ezkiz-jwt-secret";

	static int totalAmountOfMaterials = 0;
	static int workingTasksCount = 0;
	static object locker = new object();
	static int firstNotDownloadedMaterialIndex = 0;
	static int limitMaterialsDownloadPerCycle = 1;

	static int DownloadedMaterialsCount = 0;

	delegate void TaskCompletedHandler();
	event TaskCompletedHandler TaskCompleted;

	void OnTaskCompleted()
	{
		Debug.LogError("onTaskComplete");
		lock (locker)
		{
			workingTasksCount--;
		}
		Debug.LogError("Working tasks count = " + workingTasksCount);
		Debug.LogError("First not downloaded index = " + firstNotDownloadedMaterialIndex);
		if (firstNotDownloadedMaterialIndex < totalAmountOfMaterials && workingTasksCount < maximumTasksAtTime)
		{
			CreateDownloadingTask();
		}
	}

	void CreateDownloadingTask()
	{

		//Task.Run(() => GetMaterialJsonFromServerAsync(firstNotDownloadedMaterialIndex));
		lock (locker)
		{
			workingTasksCount++;
			firstNotDownloadedMaterialIndex += limitMaterialsDownloadPerCycle;
		}
		GetMaterialJsonFromServerAsync(firstNotDownloadedMaterialIndex);
		Debug.LogError("Task created " + firstNotDownloadedMaterialIndex);
		
	}

	private async Task<int> GetNumberOfMaterialsOnServerAsync()
	{
		var request = UnityWebRequest.Get(UsingURI + get_products);
		request.SetRequestHeader("Authorization", "Token ezkiz-jwt-secret");
		request.SendWebRequest();

		while (!request.isDone)
		{
			await Task.Delay(10);
		}
		if (request.isNetworkError || request.isHttpError)
		{
			Debug.LogError(request.error);
			return 0;
		}
		var text = request.downloadHandler.text;
		DownloadedMaterialsWithHeader downloadedMaterialsWithHeader = JsonConvert.DeserializeObject<DownloadedMaterialsWithHeader>(text);
		limitMaterialsDownloadPerCycle = downloadedMaterialsWithHeader.limit;
		return downloadedMaterialsWithHeader.total;
	}

	//public IEnumerator GetDataByLinkWithSkip<T>(string link, int skip, int ammount)
	//{
	//	string url = link + "?query={\"$skip\":" + skip + ",\"$limit\":" + ammount + "}";
	//	using (var www = UnityWebRequest.Get(url))
	//	{
	//		www.SetRequestHeader("Authorization", "Token ezkiz-jwt-secret");
	//		yield return www.SendWebRequest();

	//		while (!www.isDone) yield return null;
	//		if (www.isNetworkError || www.isHttpError)
	//		{

	//			yield break;
	//		}
	//		var text = www.downloadHandler.text;
	//		if (link == devUri + get_ceilings)
	//			Debug.Log(string.Format("Downloading {0} of {1} ceiling data", skip, totalAmountOfCeilings));
	//		else
	//			Debug.Log(string.Format("Downloading {0} of {1} material data", skip, totalAmountOfMaterials));
	//		try
	//		{
	//			DownloadedMaterialsWithHeader downloadedMaterialsWithHeader = JsonConvert.DeserializeObject<DownloadedMaterialsWithHeader>(text);

	//			if (isCeilingsOn)
	//			{
	//				if (link == devUri + get_ceilings)
	//				{
	//					for (int i = 0; i < downloadedMaterialsWithHeader.data.Count; i++)
	//					{
	//						downloadedMaterialsWithHeader.data[i].type = "ceiling";
	//					}
	//				}
	//			}
	//			//PrintDataToFile(downloadedMaterialsWithHeader, skip);
	//			SaveMaterialsToList(downloadedMaterialsWithHeader);

	//		}
	//		catch (System.Exception e)
	//		{
	//			//PrintDataToFile(String.Format("JSON convertion error on link '{0}'\n{1}\n********\n", url, e.Message));

	//			Debug.LogError(e.Message + "\nJSON converting error on link " + url);
	//		}
	//	}
	//	yield break;
	//}

	public async Task<bool> CheckMaterialTexturesAccesibilityAsync(MaterialJSON materialJson)
	{
		//Debug.LogError("Checking material " + materialJson.name);
		var previewRequest = UnityWebRequestTexture.GetTexture(UsingURI + get_textures + materialJson.id + "_" + materialJson.preview_icon);
		var diffuseRequest = UnityWebRequestTexture.GetTexture(UsingURI + get_textures + materialJson.id + "_" + materialJson.tex.tex_diffuse);
		previewRequest.SetRequestHeader(authorizationHeaderKey, authorizationHeaderValue);
		diffuseRequest.SetRequestHeader(authorizationHeaderKey, authorizationHeaderValue);
		previewRequest.SendWebRequest();
		diffuseRequest.SendWebRequest();
		long previewResponceCode = 0;
		long diffuseResponceCode = 0;
		while (previewResponceCode == 0 || diffuseResponceCode == 0)
		{
			previewResponceCode = previewRequest.responseCode;
			diffuseResponceCode = diffuseRequest.responseCode;
			await Task.Delay(10);
		}
		if (previewResponceCode == 200 && diffuseResponceCode == 200)
		{
			while (!previewRequest.isDone || !diffuseRequest.isDone)
			{
				await Task.Delay(10);
			}
			Texture2D previewTexture = DownloadHandlerTexture.GetContent(previewRequest);
			previewTexture.name = materialJson.preview_icon;
			Texture2D diffuseTexture = DownloadHandlerTexture.GetContent(diffuseRequest);
			diffuseTexture.name = materialJson.tex.tex_diffuse;
			DataCacher.CacheJSONMaterialData(materialJson);
			DataCacher.CacheTexture(materialJson, previewTexture);
			DataCacher.CacheTexture(materialJson, diffuseTexture);

			return (true);
		}
		else
		{
			return (false);
		}
	}

	public async Task DownloadAndCacheMaterialTexturesAsync(MaterialJSON material)
	{
		if (await CheckMaterialTexturesAccesibilityAsync(material))
		{
			Debug.Log("Downloading textures for " + material.name);
		}
	}

	public async Task<MaterialJSON> GetMaterialJsonFromServerAsync(int materialIndex)
	{
		//Debug.LogError("index = " + materialIndex);
		string url = UsingURI + get_products + "?query={\"$skip\":" + materialIndex + ",\"$limit\":" + limitMaterialsDownloadPerCycle + "}";
		//Debug.LogError("#0");
		var request = UnityWebRequest.Get(url);
		//Debug.LogError("#01");

		request.SetRequestHeader("Authorization", "Token ezkiz-jwt-secret");
		request.SendWebRequest();
		//Debug.LogError("#1");
		while (!request.isDone)
		{
			//Debug.LogError("WAITING " + materialIndex);
			await Task.Delay(10);
		}
		if (request.isNetworkError || request.isHttpError)
		{
			Debug.LogError(request.error);
			TaskCompleted?.Invoke();
			return (null);
		}
		//Debug.LogError("#2");

		var text = request.downloadHandler.text;
		//Debug.LogWarning(text);
		//Debug.Log(string.Format("Downloading {0} of {1} material data", materialIndex, totalAmountOfMaterials));
		try
		{
			//Debug.LogError("#1");
			DownloadedMaterialsWithHeader downloadedMaterialsWithHeader = JsonConvert.DeserializeObject<DownloadedMaterialsWithHeader>(text);
			//Debug.LogError("#2");
			foreach (var material in downloadedMaterialsWithHeader.data)
			{
				lock (locker)
				{
					DownloadedMaterialsCount++;
				}
				await DownloadAndCacheMaterialTexturesAsync(material);
			}
			TaskCompleted?.Invoke();
			//Debug.LogError("Wtf");
			return null;

		}
		catch (System.Exception e)
		{
			//PrintDataToFile(String.Format("JSON convertion error on link '{0}'\n{1}\n********\n", url, e.Message));

			Debug.LogError(e.Message + "\nJSON converting error on link " + url);
		}
		//Debug.LogError("#3");

		//return;
		TaskCompleted?.Invoke();
		return (null);
	}

	public async void StartDownload()
	{
		totalAmountOfMaterials = await GetNumberOfMaterialsOnServerAsync();
		Debug.LogWarning(string.Format($"totalAmountOfMaterials = {totalAmountOfMaterials}"));
		Debug.LogWarning(string.Format($"Maximum tasks = {maximumTasksAtTime}"));
		Debug.LogWarning(string.Format($"Limit material per cycle = {limitMaterialsDownloadPerCycle}"));
		for (int i = 0; i <= maximumTasksAtTime && i < totalAmountOfMaterials / limitMaterialsDownloadPerCycle; i++)
		{
			
			workingTasksCount++;
			firstNotDownloadedMaterialIndex += limitMaterialsDownloadPerCycle;
			GetMaterialJsonFromServerAsync(i);
		}
		Debug.LogWarning("WORKING TASK = " + workingTasksCount + " NOT DOWNLOADED INDEX = " + firstNotDownloadedMaterialIndex);
	}


	void Start()
	{
		TaskCompleted += OnTaskCompleted;
		StartDownload();
	}

	private void Update()
	{
		//Debug.LogError("Downloaded materials count = " + DownloadedMaterialsCount);

	}
}
