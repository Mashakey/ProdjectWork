using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using static DataTypes;

public class PreviewAndMaterialJsonDownloader : MonoBehaviour
{
	public RoomCreator roomCreator;
	public RoomTemplatesFiller filler;

	int maximumTasksAtTime = ServerDownloaderSettings.GetMaximumWorkingCoroutinesAtTimeCount();
	static int totalAmountOfMaterials = 0;
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
				CreateJsonAndPreviewDownloadingCoroutine();
			}
		}
		//else if (firstNotDownloadedMaterialIndex >= totalAmountOfMaterials)
		//{
		//	if (!isWaitingForEndingLoadingJsonsStarted)
		//	{
		//		isWaitingForEndingLoadingJsonsStarted = true;
		//		StartCoroutine(WaitForEndingDownloadJsons());
		//	}
		//}
	}

	void CreateJsonAndPreviewDownloadingCoroutine()
	{

		//Task.Run(() => GetMaterialJsonFromServerAsync(firstNotDownloadedMaterialIndex));
		StartCoroutine(GetMaterialJsonFromServer(firstNotDownloadedMaterialIndex));
		lock (locker)
		{
			workingTasksCount++;
			firstNotDownloadedMaterialIndex += limitMaterialsDownloadPerCycle;
		}
		//Debug.LogError("Task created " + firstNotDownloadedMaterialIndex);

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

		request.SetRequestHeader("Authorization", "Token ezkiz-jwt-secret");
		request.SendWebRequest();
		while (!request.isDone)
		{
			yield return null;
		}
		if (request.isNetworkError || request.isHttpError)
		{
			Debug.LogError(request.error);
			JsonDownloadCompleted?.Invoke("network error");
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
			yield return (StartCoroutine(CheckMaterialTexturesAccesibilityAsync(material)));
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

	public IEnumerator CheckMaterialTexturesAccesibilityAsync(MaterialJSON materialJson)
	{
		//Debug.Log("Checking material " + materialJson.name);
		downloadedMaterialCount++;
		//Debug.LogWarning(downloadedMaterialCount + " of " + totalAmountOfMaterials);
		MaterialJSON cachedMaterial = findMaterialInCachedMaterialsList(materialJson);
		if (cachedMaterial != null)
		{
			if (IsUpToDate(cachedMaterial.updatedAt, materialJson.updatedAt))
			{
				if (DataCacher.IsTextureExist(materialJson.id, materialJson.preview_icon))
				{
					//Debug.Log(string.Format($"Material {materialJson.name} {materialJson.id} preview and Json found in cache"));
					yield break;
				}
			}
		}
		if (materialJson.type == "paint" || materialJson.type == "ceiling")
		{
			CreateAndCacheColorPreviewAndDiffuse(materialJson);
		}
		else
		{
			var previewRequest = UnityWebRequestTexture.GetTexture(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + materialJson.id + "_" + materialJson.preview_icon);
			var diffuseRequest = UnityWebRequest.Get(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + materialJson.id + "_" + materialJson.tex.tex_diffuse);
			HTTPHeader autorizationHeader = ServerDownloaderSettings.GetAutorizationHeader();
			previewRequest.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
			diffuseRequest.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
			previewRequest.SendWebRequest();
			diffuseRequest.SendWebRequest();
			long previewResponceCode = 0;
			long diffuseResponceCode = 0;
			while (previewResponceCode == 0 || diffuseResponceCode == 0)
			{
				previewResponceCode = previewRequest.responseCode;
				diffuseResponceCode = diffuseRequest.responseCode;
				yield return null;
			}
			if (previewResponceCode == 200 && diffuseResponceCode == 200)
			{
				while (!previewRequest.isDone)
				{
					yield return null;
				}
				Texture2D previewTexture = DownloadHandlerTexture.GetContent(previewRequest);
				previewTexture.name = materialJson.preview_icon;
				DataCacher.CacheTexture(materialJson, previewTexture);
				DataCacher.CacheJSONMaterialData(materialJson);

				//StartCoroutine(DownloadAndCacheMaterialTextures(materialJson));
				yield break;
			}
			else
			{
				if (previewRequest.isHttpError)
					Debug.LogError(diffuseRequest.error + "\nMaterial name = '" + materialJson.name + "'\nId = '" + materialJson.id + "'\nCategory = '" + materialJson.category + "'\nType = " + materialJson.type);
				if (diffuseRequest.isHttpError)
					Debug.LogError(diffuseRequest.error + "\nMaterial name = '" + materialJson.name + "'\nId = '" + materialJson.id + "'\nCategory = '" + materialJson.category + "'\nType = " + materialJson.type);
				yield break;
			}
		}
	}

	IEnumerator DownloadAndCacheMaterialTextures(int cachedMaterialIndex)
	{
		Debug.Log("Start donwload and cache, index = " + cachedMaterialIndex);
		Debug.Log("materials needed for textures count = " + materialsInNeedForTextures.Count);
		MaterialJSON materialJson = materialsInNeedForTextures[cachedMaterialIndex];
		yield return StartCoroutine(DownloadAndCacheOneTexture(materialJson, materialJson.tex.tex_diffuse));
		if (materialJson.tex.tex_normal != "")
		{
			yield return StartCoroutine(DownloadAndCacheOneTexture(materialJson, materialJson.tex.tex_normal));
		}
		if (materialJson.tex.tex_roughness != "")
		{
			yield return StartCoroutine(DownloadAndCacheOneTexture(materialJson, materialJson.tex.tex_roughness));
		}
		TexturesDownloadCompleted?.Invoke(cachedMaterialIndex.ToString());
		yield break;
	}

	IEnumerator DownloadAndCacheOneTexture(MaterialJSON materialJson, string textureId)
	{
		if (DataCacher.GetTextureFromCache(materialJson.id, textureId) != null)
		{
			yield break;
		}
		UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + materialJson.id + "_" + textureId);
		HTTPHeader autorizationHeader = ServerDownloaderSettings.GetAutorizationHeader();
		textureRequest.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
		textureRequest.SendWebRequest();
		while (!textureRequest.isDone)
		{
			yield return (null);
		}
		if (textureRequest.isNetworkError || textureRequest.isHttpError)
		{
			Debug.LogError("Download texture Error on link '" + textureRequest.url + "'\n" + textureRequest.error);
			yield break;
		}
		else
		{
			Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
			texture.name = textureId;
			DataCacher.CacheTexture(materialJson.id, texture);
			yield break;
		}
	}
	//public delegate void WallApplyMaterial(Material material);
	//public static IEnumerator GetMaterialDiffuseRightNow(MaterialJSON materialJson, WallApplyMaterial callback)
	//{
	//	Debug.LogError("Downloading texture right now");

	//	UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + materialJson.id + "_" + materialJson.tex.tex_diffuse);
	//	HTTPHeader autorizationHeader = ServerDownloaderSettings.GetAutorizationHeader();
	//	textureRequest.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
	//	textureRequest.SendWebRequest();
	//	while (!textureRequest.isDone)
	//	{
	//		yield return (null);
	//	}
	//	if (textureRequest.isNetworkError || textureRequest.isHttpError)
	//	{
	//		Debug.LogError("Download texture Error on link '" + textureRequest.url + "'\n" + textureRequest.error);
	//		yield break;
	//	}
	//	else
	//	{
	//		Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
	//		//DataCacher.CacheTexture(materialJson.id, texture);
	//		Debug.LogError("Trying to apply to the wall");
	//		callback(MaterialBuilder.GetMaterialFromTexture(materialJson, texture));
	//		yield break;
	//	}
	//}


	async void StartDownloadJsonAndPreview()
	{
		totalAmountOfMaterials = await GetNumberOfMaterialsOnServerAsync();
		Debug.LogWarning("total amount of materials = " + totalAmountOfMaterials);
		for (int i = 0; i < maximumTasksAtTime && i < limitMaterialsDownloadPerCycle; i++)
		{
			CreateJsonAndPreviewDownloadingCoroutine();
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
			www.SetRequestHeader("Authorization", "Token ezkiz-jwt-secret");
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
		yield break;
	}

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(GetLegacyRoomsData());
		cachedMaterials = DataCacher.GetCachedMaterialsJSONs();
		JsonDownloadCompleted += OnJsonDownloadCompleted;
		StartDownloadJsonAndPreview();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
