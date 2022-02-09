using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using static DataTypes;

public class JsonServerDownloader : MonoBehaviour
{
	public StartLoadingScreen startLoadingScreen;
	public RoomCreator roomCreator;
	public RoomTemplatesFiller filler;

	int maximumTasksAtTime = ServerDownloaderSettings.GetMaximumWorkingCoroutinesAtTimeCount();
	static int totalAmountOfMaterials = 0;
	static int downloadedMaterialJsonsCount = 0;
	static int workingTasksCount = 0;
	static object locker = new object();
	static int firstNotDownloadedMaterialIndex = 0;

	string ip = "power-games.me:3000";
	static int limitMaterialsDownloadPerCycle = 1;

	static int downloadedMaterialCount = 0;

	static List<MaterialJSON> materialsInNeedForTextures;
	static List<MaterialJSON> cachedMaterials;

	static bool isWaitingForEndingLoadingJsonsStarted = false;

	delegate void JsonDownloadCompletedHandler(string msg);
	delegate void TexturesDownloadCompletedHandler(string msg);
	event JsonDownloadCompletedHandler JsonDownloadCompleted;
	event TexturesDownloadCompletedHandler TexturesDownloadCompleted;

	void OnJsonDownloadCompleted(string msg)
	{
		//Debug.Log("onTaskComplete '" + msg + "'");
		lock (locker)
		{
			workingTasksCount--;
		}
		//Debug.Log("Working tasks count = " + workingTasksCount);
		//Debug.LogWarning("First not downloaded index = " + firstNotDownloadedMaterialIndex);
		if (firstNotDownloadedMaterialIndex < totalAmountOfMaterials - 1 && workingTasksCount < maximumTasksAtTime)
		{
			while (workingTasksCount < maximumTasksAtTime)
			{
				//CreateJsonAndPreviewDownloadingCoroutine();
				CreateJsonDownloadingCoroutine();
			}
		}
	}

	void CreateJsonDownloadingCoroutine()
	{
		StartCoroutine(GetMaterialJsonFromServer(firstNotDownloadedMaterialIndex));
		lock (locker)
		{
			workingTasksCount++;
			firstNotDownloadedMaterialIndex += limitMaterialsDownloadPerCycle;
		}
	}

	private async Task<int> GetNumberOfMaterialsOnServerAsync()
	{

		var request = UnityWebRequest.Get(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_products);
		HTTPHeader autorizationHeader = ServerDownloaderSettings.GetAutorizationHeader();
		request.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
		request.SendWebRequest();

		while (!request.isDone)
		{
			await Task.Delay(1);
		}
		if (request.isNetworkError || request.isHttpError)
		{
			startLoadingScreen.OpenErrorPopup();
			Debug.LogError(request.error);
			return 0;
		}
		var text = request.downloadHandler.text;
		DownloadedMaterialsWithHeader downloadedMaterialsWithHeader = JsonConvert.DeserializeObject<DownloadedMaterialsWithHeader>(text);
		limitMaterialsDownloadPerCycle = downloadedMaterialsWithHeader.limit;
		return downloadedMaterialsWithHeader.total;
	}

	public IEnumerator GetMaterialJsonFromServer(int materialIndex)
	{
		//Debug.LogError("Index = " + materialIndex);
		string url = ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_products + "?query={\"$skip\":" + materialIndex + ",\"$limit\":" + limitMaterialsDownloadPerCycle + "}";
		var request = UnityWebRequest.Get(url);
		HTTPHeader autorizationHeader = ServerDownloaderSettings.GetAutorizationHeader();
		request.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
		//request.SetRequestHeader("Authorization", "Token ezkiz-jwt-secret");
		request.SendWebRequest();
		while (!request.isDone)
		{
			yield return null;
		}
		if (request.isNetworkError || request.isHttpError)
		{
			Debug.LogError(request.error);
			startLoadingScreen.OpenErrorPopup();

			JsonDownloadCompleted?.Invoke("Network error");
			yield break;
		}

		var text = request.downloadHandler.text;
		//Debug.Log(string.Format("Downloading {0} of {1} material data", materialIndex, totalAmountOfMaterials));
		DownloadedMaterialsWithHeader downloadedMaterialsWithHeader;
		try
		{
			downloadedMaterialsWithHeader = JsonConvert.DeserializeObject<DownloadedMaterialsWithHeader>(text);

		}
		catch (System.Exception e)
		{

			Debug.LogError(e.Message + "\nJSON converting error on link " + url);
			JsonDownloadCompleted?.Invoke("json convert error");
			yield break;
		}
		foreach (var material in downloadedMaterialsWithHeader.data)
		{
			yield return (StartCoroutine(CacheMaterialIfNotUpToDate(material)));
		}
		JsonDownloadCompleted?.Invoke("normal end on " + materialIndex.ToString());
		yield break;
	}

	public bool IsUpToDate(string oldObjectUpdateTime, string newObjectUpdateTime)
	{
		DateTime newMaterialUpdateTime;
		DateTime cachedMaterialUpdateTime;
		if (DateTime.TryParse(newObjectUpdateTime, out newMaterialUpdateTime)
		&& DateTime.TryParse(oldObjectUpdateTime, out cachedMaterialUpdateTime))
		{
			if (newMaterialUpdateTime > cachedMaterialUpdateTime)
			{
				return (false);
			}
			else
			{
				return (true);
			}
		}
		return (false);
	}

	public MaterialJSON findMaterialInCachedMaterialsList(MaterialJSON materialToFind)
	{
		foreach (var cachedMaterial in cachedMaterials)
		{
			if (materialToFind.id == cachedMaterial.id)
				return (cachedMaterial);
		}
		return (null);
	}

	void CreateAndCacheColorPreviewAndDiffuse(MaterialJSON materialJson)
	{
		Texture2D previewTexture = MaterialBuilder.MakePreviewForPaint(materialJson);
		DataCacher.CacheTexture(materialJson, previewTexture);
		DataCacher.CacheJSONMaterialData(materialJson);

	}

	public IEnumerator CacheMaterialIfNotUpToDate(MaterialJSON materialJson)
	{
		downloadedMaterialCount++;
		//Debug.Log(downloadedMaterialCount + " of " + totalAmountOfMaterials + " material JSONs save been loaded");
		float percent = totalAmountOfMaterials * 0.01f;
		//startLoadingScreen.transform.Find("LoaderText").GetComponent<Text>().text = string.Format($"Загружено {downloadedMaterialCount} из {totalAmountOfMaterials} материалов");
		startLoadingScreen.GetComponent<StartLoadingScreen>().SetSliderValue((downloadedMaterialCount / percent) / 100);
		//startLoadingScreen.transform.Find("LoaderText").GetComponent<Text>().text = string.Format($"Загрузка {(int)(downloadedMaterialCount / percent)} %");
		//startLoadingScreen.GetComponentInChildren<Text>().text = string.Format($"Загружено {downloadedMaterialCount} из {totalAmountOfMaterials} материалов");
		MaterialJSON cachedMaterial = findMaterialInCachedMaterialsList(materialJson);
		if (cachedMaterial != null)
		{
			if (!IsUpToDate(cachedMaterial.updatedAt, materialJson.updatedAt))
			{
				DataCacher.CacheJSONMaterialData(materialJson);
				yield return null;
			}
		}
		else
		{
			DataCacher.CacheJSONMaterialData(materialJson);
			yield return null;
		}
		if (downloadedMaterialCount == totalAmountOfMaterials)
		{
			MoveJsonsToRAM();
		}
		yield break;
	}

	void MoveJsonsToRAM()
	{
		GlobalApplicationManager globalManager = GameObject.FindObjectOfType<GlobalApplicationManager>();
		Debug.LogWarning("Start moving jsons to RAM");
		globalManager.MoveJsonsToRamAndCreateFilterValues();
	}

	async void StartDownloadingJson()
	{
		totalAmountOfMaterials = await GetNumberOfMaterialsOnServerAsync();
		Debug.LogWarning("total amount of materials = " + totalAmountOfMaterials);
		for (int i = 0; i < maximumTasksAtTime && i < limitMaterialsDownloadPerCycle; i++)
		{
			CreateJsonDownloadingCoroutine();
		}
	}

	public IEnumerator GetLegacyRoomPreview(LegacyRoomData roomDataJson)
	{
		string getRoomPreviewURL = ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + roomDataJson.id + "_" + roomDataJson.preview_icon;
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
			Debug.LogError("LEGACY ROOM NAME = " + roomDataJson.name);
			previewTexture.name = roomDataJson.id + "_" + roomDataJson.preview_icon;
			DataCacher.CacheLegacyRoomData(roomDataJson, previewTexture);
		}
	}

	public IEnumerator GetLegacyRoomsData()
	{
		//string url = devUri + get_rooms;
		string url = ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_rooms;
		using (var www = UnityWebRequest.Get(url))
		{
			HTTPHeader autorizationHeader = ServerDownloaderSettings.GetAutorizationHeader();
			www.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
			yield return www.SendWebRequest();

			while (!www.isDone) yield return null;
			if (www.isNetworkError || www.isHttpError)
			{

#if DEBUG_QUERIES
				Debug.LogWarning(www.error);
#endif
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
		}
		List<LegacyRoomData> legacyRoomsDataJSONs = DataCacher.GetCachedLegacyRoomDataJSONs();
		filler.CreateRoomTemplatesList(legacyRoomsDataJSONs);
		yield break;
	}

	public IEnumerator GetColorsData()
	{
		string url = ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_colors;
		using (var webRequest = UnityWebRequest.Get(url))
		{
			HTTPHeader autorizationHeader = ServerDownloaderSettings.GetAutorizationHeader();
			webRequest.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
			yield return webRequest.SendWebRequest();

			while (!webRequest.isDone) yield return null;
			if (webRequest.isNetworkError || webRequest.isHttpError)
			{

#if DEBUG_QUERIES
				Debug.LogWarning(www.error);
#endif
				Debug.LogError(webRequest.error);
				Debug.LogError(url);
				yield break;
			}
			var text = webRequest.downloadHandler.text;

#if DEBUG_QUERIES
			MyLogger.Log("Loading from " + link + " completed.\nLoadedRawData:\n" + text);
#endif
			ColorJson[] colorJsons = JsonConvert.DeserializeObject<ColorJson[]>(text);
			DataCacher.CacheColorJsons(colorJsons.ToList());

		}
		yield break;
	}

	public void StartDownload()
	{
		StartCoroutine(GetColorsData());
		//StartCoroutine(GetLegacyRoomsData());
		cachedMaterials = DataCacher.GetCachedMaterialsJSONs();
		JsonDownloadCompleted += OnJsonDownloadCompleted;
		//StartDownloadJsonAndPreview();
		StartDownloadingJson();
	}

	// Start is called before the first frame update
	void Start()
	{
		maximumTasksAtTime = ServerDownloaderSettings.GetMaximumWorkingCoroutinesAtTimeCount();
		totalAmountOfMaterials = 0;
		downloadedMaterialJsonsCount = 0;
		workingTasksCount = 0;
		locker = new object();
		firstNotDownloadedMaterialIndex = 0;

		downloadedMaterialCount = 0;
	}
}
