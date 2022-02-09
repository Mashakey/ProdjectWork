using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using static DataTypes;

public class ServerDataDownloaderCoroutine : MonoBehaviour
{
	[SerializeField]
	int maximumTasksAtTime = 3;


	public static string tempUri = @"https://apidev.ezkiz.ru";
	public static string devUri = @"https://apidev.ezkiz.ru";
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
	static int firstNotDownloadedTexturesIndex = 0;
	static int limitMaterialsDownloadPerCycle = 1;

	static List<MaterialJSON> materialsInNeedForTextures;
	static List<MaterialJSON> cachedMaterials;

	static bool isWaitingForEndingLoadingJsonsStarted = false;

	delegate void JsonDownloadCompletedHandler(string msg);
	delegate void TexturesDownloadCompletedHandler(string msg);
	event JsonDownloadCompletedHandler JsonDownloadCompleted;
	event TexturesDownloadCompletedHandler TexturesDownloadCompleted;

	void OnTexturesDownloadCompleted(string msg)
	{
		Debug.Log("onTexturesDownloadComplete '" + msg + "'");
		Resources.UnloadUnusedAssets();
		lock (locker)
		{
			workingTasksCount--;
		}
		if (firstNotDownloadedTexturesIndex < totalAmountOfMaterials - 1 && workingTasksCount < maximumTasksAtTime)
		{
			while (workingTasksCount < maximumTasksAtTime && firstNotDownloadedMaterialIndex < totalAmountOfMaterials - 1)
			{
				CreateTextureDownloadingCoroutine();
			}
		}
		if (firstNotDownloadedMaterialIndex >= totalAmountOfMaterials)
		{
			Debug.LogError("Download has ended");
		}
	}

	void OnJsonDownloadCompleted(string msg)
	{
		Debug.Log("onTaskComplete '" + msg +"'");
		lock (locker)
		{
			workingTasksCount--;
		}
		//Debug.Log("Working tasks count = " + workingTasksCount);
		//Debug.LogWarning("First not downloaded index = " + firstNotDownloadedMaterialIndex);
		if (firstNotDownloadedMaterialIndex < totalAmountOfMaterials - 1 && workingTasksCount < maximumTasksAtTime)
		{
			while (workingTasksCount < maximumTasksAtTime && firstNotDownloadedMaterialIndex < totalAmountOfMaterials - 1)
			{
				CreateJsonAndPreviewDownloadingCoroutine();
			}
		}
		else if (firstNotDownloadedMaterialIndex >= totalAmountOfMaterials)
		{
			if (!isWaitingForEndingLoadingJsonsStarted)
			{
				isWaitingForEndingLoadingJsonsStarted = true;
				StartCoroutine(WaitForEndingDownloadJsons());
			}
		}
	}

	void CreateJsonAndPreviewDownloadingCoroutine()
	{

		//Task.Run(() => GetMaterialJsonFromServerAsync(firstNotDownloadedMaterialIndex));
		lock (locker)
		{
			workingTasksCount++;
			firstNotDownloadedMaterialIndex += limitMaterialsDownloadPerCycle;
		}
		StartCoroutine(GetMaterialJsonFromServer(firstNotDownloadedMaterialIndex));
		//Debug.LogError("Task created " + firstNotDownloadedMaterialIndex);

	}

	void CreateTextureDownloadingCoroutine()
	{
		lock (locker)
		{
			workingTasksCount++;
			firstNotDownloadedTexturesIndex++;
		}
		StartCoroutine(DownloadAndCacheMaterialTextures(firstNotDownloadedTexturesIndex));
	}

	private async Task<int> GetNumberOfMaterialsOnServerAsync()
	{
		var request = UnityWebRequest.Get(UsingURI + get_products);
		request.SetRequestHeader("Authorization", "Token ezkiz-jwt-secret");
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
		string url = UsingURI + get_products + "?query={\"$skip\":" + materialIndex + ",\"$limit\":" + limitMaterialsDownloadPerCycle + "}";
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

	public IEnumerator CheckMaterialTexturesAccesibilityAsync(MaterialJSON materialJson)
	{
		//Debug.Log("Checking material " + materialJson.name);
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
		var previewRequest = UnityWebRequestTexture.GetTexture(UsingURI + get_textures + materialJson.id + "_" + materialJson.preview_icon);
		var diffuseRequest = UnityWebRequest.Get(UsingURI + get_textures + materialJson.id + "_" + materialJson.tex.tex_diffuse);
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
				Debug.LogError(previewRequest.error + "\nMaterial name = '" + materialJson.name + "'\nId = '" + materialJson.id + "'\nCategory = '" + materialJson.category + "'");
			if (diffuseRequest.isHttpError)
				Debug.LogError(diffuseRequest.error + "\nMaterial name = '" + materialJson.name +"'\nId = '" + materialJson.id +"'\nCategory = '" + materialJson.category +"'");
			yield break;
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

	IEnumerator DownloadAndCacheOneTexture (MaterialJSON materialJson, string textureId)
	{
		if (DataCacher.GetTextureFromCache(materialJson.id, textureId) != null)
		{
			yield break;
		}
		UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(UsingURI + get_textures + materialJson.id + "_" + textureId);
		textureRequest.SetRequestHeader(authorizationHeaderKey, authorizationHeaderValue);
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
	public delegate void WallApplyMaterial(Material material);
	public static IEnumerator GetMaterialDiffuseRightNow(MaterialJSON materialJson, WallApplyMaterial callback)
	{
		Debug.LogError("Downloading texture right now");

		UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(UsingURI + get_textures + materialJson.id + "_" + materialJson.tex.tex_diffuse);
		textureRequest.SetRequestHeader(authorizationHeaderKey, authorizationHeaderValue);
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
			//DataCacher.CacheTexture(materialJson.id, texture);
			Debug.LogError("Trying to apply to the wall");
			callback(MaterialBuilder.GetMaterialFromTexture(materialJson, texture));
			yield break;
		}
	}

	IEnumerator WaitForEndingDownloadJsons()
	{
		while (workingTasksCount > 0)
		{
			//Debug.LogWarning("WaitongForEndOfDownload");
			yield return null;
		}
		StartDownloadTextures();
		yield break;
	}

	async void StartDownloadJsonAndPreview()
	{
		totalAmountOfMaterials = await GetNumberOfMaterialsOnServerAsync();
		Debug.LogWarning("total amount of materials = " + totalAmountOfMaterials);
		for (int i = 0; i <= maximumTasksAtTime && i < totalAmountOfMaterials / limitMaterialsDownloadPerCycle; i++)
		{
			workingTasksCount++;
			firstNotDownloadedMaterialIndex += limitMaterialsDownloadPerCycle;
			StartCoroutine(GetMaterialJsonFromServer(i * limitMaterialsDownloadPerCycle));
		}
	}

	void StartDownloadTextures()
	{
		materialsInNeedForTextures = DataCacher.GetCachedMaterialsJSONs();
		totalAmountOfMaterials = materialsInNeedForTextures.Count;
		firstNotDownloadedMaterialIndex = 0;
		Debug.LogError("Start textures");
		Debug.LogWarning("needed for textures count = " + totalAmountOfMaterials);
		for (int i = 0; i <= maximumTasksAtTime && i < totalAmountOfMaterials; i++)
		{
			CreateTextureDownloadingCoroutine();
		}
		//TexturesDownloadCompleted?.Invoke("Start Downloading Textures");
	}

	public IEnumerator GetLegacyRoomsData()
	{
		//string url = devUri + get_rooms;
		string url = tempUri + get_rooms;
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
				//downloadedLegacyRooms.Add(roomsData[i]);
			}
			//RoomCreator.CreateRoomFromLegacy(roomsData[0]);
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
		yield break;
	}

	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine(GetLegacyRoomsData());
		//cachedMaterials = DataCacher.GetCachedMaterialsJSONs();
		//JsonDownloadCompleted += OnJsonDownloadCompleted;
		//TexturesDownloadCompleted += OnTexturesDownloadCompleted;
		//StartDownloadJsonAndPreview();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
