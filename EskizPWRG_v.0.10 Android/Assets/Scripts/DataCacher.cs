using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json;
using static DataTypes;


public class DataCacher : MonoBehaviour
{
	//static JsonSerializerSettings settings = new JsonSerializerSettings { Error = (se, ev) => { ev.ErrorContext.Handled = true; } };
	private static string _cachePath;

	public static List<MaterialJSON> GetCachedMaterialsJSONs()
	{
		List<MaterialJSON> cachedMaterialsJSONs = new List<MaterialJSON>();
		if (Directory.Exists(Path.Combine(_cachePath, "MaterialDataJSONS")))
		{
			string[] cachedFiles = Directory.GetFiles(Path.Combine(_cachePath, "MaterialDataJSONS"));
			for (int i = 0; i < cachedFiles.Length; i++)
			{
				if (!cachedFiles[i].EndsWith(".meta"))
				{
					cachedMaterialsJSONs.Add(JsonConvert.DeserializeObject<MaterialJSON>(File.ReadAllText(cachedFiles[i])));
				}
			}
		}
		return cachedMaterialsJSONs;
	}

	public static List<LegacyRoomData> GetCachedLegacyRoomDataJSONs()
	{
		List<LegacyRoomData> cachedLegacyRoomDataJSONs = new List<LegacyRoomData>();
		string path = Path.Combine(_cachePath, "LegacyRoomDataJSONS");
		Debug.Log("Getting legacy rooms data in path: " + path);
		if (Directory.Exists(path))
		{
			string[] cachedFiles = Directory.GetFiles(path);
			for (int i = 0; i < cachedFiles.Length; i++)
			{
				if (!cachedFiles[i].EndsWith(".meta") && !cachedFiles[i].EndsWith(".png") && !cachedFiles[i].EndsWith(".jpg"))
				{
					cachedLegacyRoomDataJSONs.Add(JsonConvert.DeserializeObject<LegacyRoomData>(File.ReadAllText(cachedFiles[i])));
				}
			}
		}
		return cachedLegacyRoomDataJSONs;
	}

	public static LegacyRoomData GetCachedLegacyRoomDataJSONs(string roomId)
	{
		List<LegacyRoomData> cachedLegacyRoomDataJSONs = GetCachedLegacyRoomDataJSONs();
		foreach (var room in cachedLegacyRoomDataJSONs)
		{
			if (room.id == roomId)
				return (room);
		}
		return (null);
	}

	public static string[] GetCachedMaterialsFoldersNames()
	{
		string[] _foldersPaths = Directory.GetDirectories(_cachePath);

		for (int i = 0; i < _foldersPaths.Length; i++)
		{
			int lastSlashIndex = _foldersPaths[i].LastIndexOf('/');
			_foldersPaths[i] = _foldersPaths[i].Substring(lastSlashIndex + 1);
		}
		return _foldersPaths;
	}

	public static void CacheFavoritesJsons(Favorites favoritesJsons)
	{
		var dirPath = Path.Combine(_cachePath, "FavoritesJSONS");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		File.WriteAllText(Path.Combine(dirPath, "favoritesJsons"), JsonConvert.SerializeObject(favoritesJsons));
		Debug.Log("Favorites writted");
	}

	public static Favorites GetCachedFavoritesJsons()
	{
		Favorites favoritesJsons = new Favorites();
		var dirPath = Path.Combine(_cachePath, "FavoritesJSONS");

		Debug.Log("Getting cached favorites on path: " + dirPath);
		if (Directory.Exists(dirPath))
		{
			string[] cachedFiles = Directory.GetFiles(dirPath);
			for (int i = 0; i < cachedFiles.Length; i++)
			{
				if (!cachedFiles[i].EndsWith(".meta"))
				{
					favoritesJsons = JsonConvert.DeserializeObject<Favorites>(File.ReadAllText(cachedFiles[i]));
				}
			}
		}
		return (favoritesJsons);
	}

	public static void CacheJSONMaterialData(MaterialJSON material)
	{
		var dirPath = Path.Combine(_cachePath, "MaterialDataJSONS");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		File.WriteAllText(Path.Combine(dirPath, material.id), JsonConvert.SerializeObject(material));
		Debug.Log(material.id + " Json writted");
		if (material.type == "paint")
		{
			Texture2D previewTexture = MaterialBuilder.MakePreviewForPaint(material);
			CacheTexture(material, previewTexture);
			Debug.Log("Paint texture is cached " + material.id);
		}
	}

	public static void CacheLegacyJsonRoomData(LegacyRoomData roomData)
	{
		var dirPath = Path.Combine(_cachePath, "LegacyRoomDataJSONS");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		File.WriteAllText(Path.Combine(dirPath, roomData.id), JsonConvert.SerializeObject(roomData));
	}

	public static void CacheJsonRoomData(string roomData, string roomName)
	{
		var dirPath = Path.Combine(_cachePath, "RoomDataJSONS");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		string[] filesWithSameName = Directory.GetFiles(dirPath, roomName + "*");
		foreach (var room in filesWithSameName)
		{
			Debug.LogError("#" + room);
		}
		for (int i = 1; i < 100; i++)
		{
			if (Array.IndexOf(filesWithSameName, string.Format($"{dirPath}{roomName}({i.ToString()})")) < 0)
			{
				Debug.LogError("##" + string.Format($"{dirPath}{roomName}({i.ToString()})"));
				File.WriteAllText(Path.Combine(dirPath, roomName + i.ToString()), roomData);
				Debug.Log("Room '" + roomName + "' Json writted");
				return;
			}
		}
		Debug.LogError("Room " + roomName + " cannot be writed due to limit files with same name");
	}

	public static void CacheJsonRoomData(string roomData, string roomName, Texture2D previewTexture)
	{
		var dirPath = Path.Combine(_cachePath, "RoomDataJSONS");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		//string availableRoomName = GetAvailableRoomName(roomName);
		File.WriteAllText(Path.Combine(dirPath, roomName), roomData);
		CacheRoomDataPreview(roomName, previewTexture);

	}

	public static void CacheRoomDataPreview(string roomName, Texture2D previewTexture)
	{
		var dirPath = Path.Combine(_cachePath, "RoomPreviews");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");

		}
		byte[] bytes;
		try
		{
			bytes = previewTexture.EncodeToPNG();
			File.WriteAllBytes(Path.Combine(dirPath, roomName + ".png"), bytes);
		}
		catch (System.Exception e)
		{
			Debug.LogError(e.Message);
		}
	}

	//public string GetAvailableRoomName(string roomName)
	//{
	//	var dirPath = Path.Combine(_cachePath, "RoomDataJSONS");
	//	if (!Directory.Exists(dirPath))
	//	{
	//		Directory.CreateDirectory(dirPath);
	//		Debug.Log("Directory created '" + dirPath + "'");
	//	}
	//	string[] filesWithSameName = Directory.GetFiles(dirPath, roomName + "*");
	//	if (filesWithSameName.Length == 0)
	//	{
	//		return (roomName);
	//	}
	//	foreach (var room in filesWithSameName)
	//	{
	//		Debug.LogError("#" + room);
	//	}
	//	for (int i = 1; i < 100; i++)
	//	{
	//		if (Array.IndexOf(filesWithSameName, string.Format($"{dirPath}{roomName}({i.ToString()})")) < 0)
	//		{
	//			Debug.LogError("##" + string.Format($"{dirPath}{roomName}({i.ToString()})"));
	//			return (roomName + i.ToString());
	//		}
	//	}
	//	return (roomName);
	//}

	public static Texture2D GetRoomPreview(RoomData roomDataJson)
	{
		var dirPath = Path.Combine(_cachePath, "RoomPreviews");
		Texture2D cachedTexture = new Texture2D(2, 2);
		if (File.Exists(Path.Combine(dirPath, roomDataJson.Name + ".png")))
		{
			cachedTexture.LoadImage(File.ReadAllBytes(Path.Combine(dirPath, roomDataJson.Name + ".png")));
		}
		return (cachedTexture);
	}

	public static IEnumerator GetRoomPreviewCoroutine(RoomData roomDataJson, Image roomPreviewImage)
	{
		UriBuilder uriBuilder = new UriBuilder(Path.Combine(_cachePath, "RoomPreviews", roomDataJson.Name + ".png"));
		var dirPath = uriBuilder.Uri.ToString();
		UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(dirPath);
		textureRequest.SendWebRequest();
		while (!textureRequest.isDone)
		{
			yield return null;
		}

		if (textureRequest.isNetworkError || textureRequest.isHttpError)
		{
			Debug.LogError(textureRequest.error + "\n" + dirPath);
			yield break;
		}
		else
		{
			Debug.Log("Download succes");
			Texture2D previewTexture = DownloadHandlerTexture.GetContent(textureRequest);
			yield return (null);
			Sprite previewSprite = Sprite.Create(previewTexture, new Rect(0.0f, 0.0f, previewTexture.width, previewTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
			yield return (null);
			if (roomPreviewImage != null)
			{
				roomPreviewImage.sprite = previewSprite;
			}
		}
	}

	public static string GetAvailableRoomName(string roomName)
	{
		var dirPath = Path.Combine(_cachePath, "RoomDataJSONS");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		string[] filesWithSameName = Directory.GetFiles(dirPath, roomName + "*");
		if (filesWithSameName.Length == 0)
		{
			return (roomName);
		}
		else
		{
			for (int i = 1; i < 100; i++)
			{
				string formattedRoomNamePath = Path.Combine(dirPath, string.Format($"{roomName}({i.ToString()})"));
				if (Array.IndexOf(filesWithSameName, formattedRoomNamePath) < 0)
				{
					roomName = string.Format($"{roomName}({i.ToString()})");
					break;
				}
			}
		}
		return (roomName);
	}

	public static void DeleteAllRoomData(RoomData roomDataJson)
	{
		var dirPath = Path.Combine(_cachePath, "RoomPreviews");
		Debug.LogErrorFormat($"Deleting preview {Path.Combine(dirPath, roomDataJson.Name + ".png")}");
		if (File.Exists(Path.Combine(dirPath, roomDataJson.Name + ".png")))
		{
			Debug.LogErrorFormat("Preview deleted");
			File.Delete(Path.Combine(dirPath, roomDataJson.Name + ".png"));
		}
		dirPath = Path.Combine(_cachePath, "RoomDataJSONS");
		Debug.LogErrorFormat($"Deleting room {Path.Combine(dirPath, roomDataJson.Name)}");

		if (File.Exists(Path.Combine(dirPath, roomDataJson.Name)))
		{
			Debug.LogErrorFormat("Room deleted");

			File.Delete(Path.Combine(dirPath, roomDataJson.Name));

		}
	}

	public static List<RoomData> GetCachedRoomDataJsons()
	{
		List<RoomData> roomsDataJsons = new List<RoomData>();
		var dirPath = Path.Combine(_cachePath, "RoomDataJSONS");
		if (Directory.Exists(dirPath))
		{
			string[] cachedFiles = Directory.GetFiles(Path.Combine(_cachePath, "RoomDataJSONS"));
			for (int i = 0; i < cachedFiles.Length; i++)
			{
				if (!cachedFiles[i].EndsWith(".meta") && !cachedFiles[i].EndsWith(".png") && !cachedFiles[i].EndsWith(".jpg"))
				{
					RoomData roomData = JsonConvert.DeserializeObject<RoomData>(File.ReadAllText(cachedFiles[i]));
					roomData.Name = Path.GetFileName(cachedFiles[i]);
					roomsDataJsons.Add(roomData);
				}
			}
		}
		return (roomsDataJsons);
	}

	public static void CacheTexture(MaterialJSON material, Texture2D texture)
	{
		var dirPath = Path.Combine(_cachePath, "Textures", material.id);
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");

		}
		/*if (material.type == "paint" || material.type == "ceiling")
			material.MakeMaterialPreview();*/
		byte[] bytes;
		try
		{
			bytes = texture.EncodeToPNG();
			//Debug.LogError("Texture name = " + texture.name);
			File.WriteAllBytes(Path.Combine(dirPath, texture.name), bytes);
		}
		catch (System.Exception e)
		{
			//material.tex.tex_diffuse = "";
			material.isValid = false;
			Debug.LogError(e.Message);
		}
	}

	public static void CacheTexture(string materialId, Texture2D texture)
	{
		var dirPath = Path.Combine(_cachePath, "Textures", materialId);
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");

		}
		/*if (material.type == "paint" || material.type == "ceiling")
			material.MakeMaterialPreview();*/
		byte[] bytes;
		try
		{
			bytes = texture.EncodeToPNG();
			File.WriteAllBytes(Path.Combine(dirPath, texture.name), bytes);
			Debug.Log("Texture " + texture.name + " is cached");
		}
		catch (System.Exception e)
		{
			Debug.LogErrorFormat($"Texture name = '{texture.name}");
			Debug.LogError(e.Message);
		}
	}

	public static void CacheUserPhotoTexture(Texture2D texture)
	{
		var dirPath = Path.Combine(_cachePath, "UserPhotos");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		byte[] bytes;
		try
		{
			bytes = texture.EncodeToPNG();
			string userPhotoName = GetAvailableUserPhotoName();
			File.WriteAllBytes(Path.Combine(dirPath, userPhotoName), bytes);
			Debug.Log("Texture " + userPhotoName + " is cached");
			Debug.Log("user photo path: " + Path.Combine(dirPath, userPhotoName));
		}
		catch (System.Exception e)
		{
			Debug.LogError(e.Message);
		}
	}

	public static string GetAvailableUserPhotoName()
	{
		const string UserPhotoNameTemplate = "UserPhoto";
		string availablePhotoName = UserPhotoNameTemplate;
		string dirPath = Path.Combine(_cachePath, "UserPhotos");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		string[] filesWithSameName = Directory.GetFiles(dirPath, UserPhotoNameTemplate + "*");
		if (filesWithSameName.Length == 0)
		{
			return (UserPhotoNameTemplate);
		}
		else
		{
			for (int i = 1; i < 100; i++)
			{
				if (Array.IndexOf(filesWithSameName, Path.Combine(dirPath, UserPhotoNameTemplate + "(" + i.ToString() + ")")) < 0)
				{
					availablePhotoName = string.Format($"{UserPhotoNameTemplate}({i.ToString()})");
					break;
				}
			}
		}
		return (availablePhotoName);
	}

	public static List<string> GetCachedUserPhotosNames()
	{
		List<string> userPhotosNames;
		var dirPath = Path.Combine(_cachePath, "UserPhotos");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		userPhotosNames = new List<string>(Directory.GetFiles(dirPath));
		for (int i = 0; i < userPhotosNames.Count; i++)
		{
			userPhotosNames[i] = Path.GetFileName(userPhotosNames[i]);
		}
		return (userPhotosNames);
	}

	public static IEnumerator GetUserPhotoTextureCoroutine(string photoName, TextureKeeper imageToFill)
	{
		UriBuilder uriBuilder = new UriBuilder(Path.Combine(_cachePath, "UserPhotos", photoName));
		UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(uriBuilder.Uri.ToString());
		yield return textureRequest.SendWebRequest();
		while (!textureRequest.isDone)
		{
			yield return null;
		}

		if (textureRequest.isNetworkError || textureRequest.isHttpError)
		{
			Debug.LogError("###\n" + textureRequest.uri);
			Debug.LogError(textureRequest.url);
			Debug.LogError(textureRequest.error + "\n" + Path.Combine(_cachePath, "UserPhotos", photoName));
			yield break;
		}
		else
		{
			Debug.Log("Download succes");
			Texture2D previewTexture = DownloadHandlerTexture.GetContent(textureRequest);
			imageToFill.texture = previewTexture;
			//Sprite previewSprite = Sprite.Create(previewTexture, new Rect(0.0f, 0.0f, previewTexture.width, previewTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
			//imageToFill.sprite = previewSprite;
		}
	}

	public static void CacheColorJsons(List<ColorJson> colorJsons)
	{
		var dirPath = Path.Combine(_cachePath, "ColorJsons");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		File.WriteAllText(Path.Combine(dirPath, "colorJsons"), JsonConvert.SerializeObject(colorJsons));
	}

	public static List<ColorJson> GetCachedColorJsons()
	{
		List<ColorJson> colorJsons = new List<ColorJson>();
		var dirPath = Path.Combine(_cachePath, "ColorJsons");
		//Debug.LogError("### path in colors = " + dirPath);
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		if (File.Exists(Path.Combine(dirPath, "colorJsons")))
		{
			colorJsons = (JsonConvert.DeserializeObject<ColorJson[]>(File.ReadAllText(Path.Combine(dirPath, "colorJsons")))).ToList();
		}
		foreach (var color in colorJsons)
		{
			Debug.Log(color.id + " " + color.name);
		}
		return (colorJsons);
	}

	public static void CacheLegacyRoomData(LegacyRoomData roomDataJson, Texture2D previewTexture)
	{
		var dirPath = Path.Combine(_cachePath, "LegacyRoomDataJSONS");
		if (!Directory.Exists(dirPath))
		{
			Directory.CreateDirectory(dirPath);
			Debug.Log("Directory created '" + dirPath + "'");
		}
		File.WriteAllText(Path.Combine(dirPath, roomDataJson.id), JsonConvert.SerializeObject(roomDataJson));
		byte[] bytes;
		try
		{
			bytes = previewTexture.EncodeToPNG();
			File.WriteAllBytes(Path.Combine(dirPath, previewTexture.name), bytes);
			Debug.Log("Texture " + previewTexture.name + " is cached");
		}
		catch (System.Exception e)
		{
			//material.tex.tex_diffuse = "";
			//material.isValid = false;
			Debug.LogError(e.Message);

		}
	}

	public static Texture2D GetCachedLegacyRoomDataPreview(LegacyRoomData roomDataJson)
	{
		var dirPath = Path.Combine(_cachePath, "LegacyRoomDataJSONS");
		Texture2D cachedTexture = new Texture2D(2, 2);
		if (File.Exists(Path.Combine(dirPath, roomDataJson.id + "_" + roomDataJson.preview_icon)))
		{
			cachedTexture.LoadImage(File.ReadAllBytes(Path.Combine(dirPath, roomDataJson.id + "_" + roomDataJson.preview_icon)));
		}
		return (cachedTexture);
	}

	public static void GetTexturesFromCache(MaterialJSON material)
	{
		var dirPath = Path.Combine(_cachePath, "Textures", material.id);
		if (material.tex.tex_diffuse != "")
		{
			Texture2D cachedTexture = new Texture2D(2, 2);
			if (File.Exists(Path.Combine(dirPath, material.tex.tex_diffuse)))
			{
				cachedTexture.LoadImage(File.ReadAllBytes(Path.Combine(dirPath, material.tex.tex_diffuse)));
			}
			material.applyedTextures.tex_diffuse = cachedTexture;
		}
		if (material.tex.tex_normal != "")
		{
			Texture2D cachedTexture = new Texture2D(2, 2);
			if (File.Exists(Path.Combine(dirPath, material.tex.tex_normal)))
			{
				cachedTexture.LoadImage(File.ReadAllBytes(Path.Combine(dirPath, material.tex.tex_normal)));
			}
			material.applyedTextures.tex_normal = cachedTexture;
		}
		if (material.tex.tex_roughness != "")
		{
			Texture2D cachedTexture = new Texture2D(2, 2);
			if (File.Exists(Path.Combine(dirPath, material.tex.tex_roughness)))
			{
				cachedTexture.LoadImage(File.ReadAllBytes(Path.Combine(dirPath, material.tex.tex_roughness)));
			}
			material.applyedTextures.tex_roughness = cachedTexture;
		}
	}

	public static MaterialJSON GetMaterialById(string materialToFindId)
	{
		List<MaterialJSON> materials = GetCachedMaterialsJSONs();
		foreach (var material in materials)
		{
			if (material.id == materialToFindId)
				return (material);
		}
		return (null);
	}

	public static bool IsPreviewExist(MaterialJSON material)
	{
		string dirPath = Path.Combine(Application.streamingAssetsPath, "Textures", material.id);
		if (Directory.Exists(dirPath))
		{
			if (File.Exists(Path.Combine(dirPath, material.preview_icon)))
			{
				Debug.LogWarningFormat($"Preview for material {material.id} is exist in cache");
				return (true);
			}
		}
		Debug.LogWarningFormat($"Preview for material {material.id} is not exist in cache");
		return (false);
	}

	public static Texture2D GetPreviewFromCache(MaterialJSON material)
	{
		var dirPath = Path.Combine(_cachePath, "Textures", material.id);
		if (material.preview_icon != "")
		{
			Texture2D cachedTexture = new Texture2D(2, 2);
			if (File.Exists(Path.Combine(dirPath, material.preview_icon)))
			{
				cachedTexture.LoadImage(File.ReadAllBytes(Path.Combine(dirPath, material.preview_icon)));
				return (cachedTexture);
			}
		}
		return (null);
	}

	public static Texture2D GetPaintPreviewFromCache(MaterialJSON material)
	{
		var dirPath = Path.Combine(_cachePath, "Textures", material.id);
		Texture2D cachedTexture = new Texture2D(2, 2);
		if (File.Exists(Path.Combine(dirPath, "preview_icon.png")))
		{
			cachedTexture.LoadImage(File.ReadAllBytes(Path.Combine(dirPath, "preview_icon.png")));
			return (cachedTexture);
		}
		return (null);
	}

	public static Texture2D GetTextureFromCache(string materialId, string textureId)
	{
		var dirPath = Path.Combine(_cachePath, "Textures", materialId);
		if (File.Exists(Path.Combine(dirPath, textureId)))
		{
			Texture2D cachedTexture = new Texture2D(2, 2);
			cachedTexture.LoadImage(File.ReadAllBytes(Path.Combine(dirPath, textureId)));
			return (cachedTexture);
		}
		return (null);
	}

	public static bool IsTextureExist(string materialId, string textureId)
	{
		var dirPath = Path.Combine(_cachePath, "Textures", materialId);
		if (File.Exists(Path.Combine(dirPath, textureId)))
		{
			//Debug.Log("Texture is exist");
			return (true);
		}
		else
		{
			//Debug.Log("Texture is not exist");
			return (false);
		}
	}


	public static void DeleteAllMaterialDataFromCache(MaterialJSON material)
	{
		var texturesPath = Path.Combine(_cachePath, "Textures", material.id);
		var jsonPath = Path.Combine(_cachePath, "MaterialDataJSONS");
		if (Directory.Exists(texturesPath))
		{
			Debug.Log(texturesPath + " Directiry deleted");
			Directory.Delete(texturesPath, true);
		}
		if (Directory.Exists(jsonPath))
		{
			string[] files = Directory.GetFiles(jsonPath, material.id + "*");
			foreach (string file in files)
			{
				Debug.Log(file + " Deleted");
				File.Delete(file);
			}
		}

	}


	private void Awake()
	{
#if UNITY_EDITOR

		//_cachePath = Application.dataPath + "/Resources/";
		//_cachePath = "D:/EskizData/Resources/";
		//_cachePath = "D:/EskizDevData/Resources/";
		_cachePath = "D:/EskizDevDataAsync/Resources/";
#else
		//_cachePath = Application.persistentDataPath;                       
		//_cachePath = Application.persistentDataPath + "/";                       
		//_cachePath = Path.Combine(Application.persistentDataPath, "Resources");                                
		_cachePath = Path.Combine("file:///", Application.persistentDataPath);                                
#endif
		Debug.LogWarning(_cachePath);
	}
}
