using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using static DataTypes;
using static MaterialBuilder;

public class TextureDownloader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public static IEnumerator DownloadTexture(MaterialJSON materialJson, string textureName, TextureKeeper textureToReturn)
	{
		//Debug.LogWarningFormat($"We are in download texture. '{materialJson.id}'  '{textureName}'");
		//Debug.LogError(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + materialJson.id + "_" + textureName);
		UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + materialJson.id + "_" + textureName);
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
			textureToReturn = null;
			yield break;
		}
		else
		{
			textureToReturn.texture = DownloadHandlerTexture.GetContent(textureRequest);
			textureToReturn.texture.name = textureName;
			DataCacher.CacheTexture(materialJson.id, textureToReturn.texture);
			yield break;
		}
	}

	public IEnumerator DownloadAndCacheAllMaterialTextures(MaterialJSON materialJson, TextureDownloadingStatus downloadingStatus)
	{
		if (materialJson.type == "paint")
		{
			yield break;
		}
		if (materialJson.tex.tex_normal != "")
		{
			yield return (StartCoroutine(DownloadAndCacheTexture(materialJson, materialJson.tex.tex_normal)));
			downloadingStatus.isNormalDownloaded = true;
		}
		if (materialJson.tex.tex_roughness != "")
		{
			yield return (StartCoroutine(DownloadAndCacheTexture(materialJson, materialJson.tex.tex_roughness)));
			downloadingStatus.isRoughnessDownloaded = true;

		}
		yield break;
	}

	public static IEnumerator DownloadAndCacheTexture(MaterialJSON materialJson, string textureName)
	{
		//Debug.LogWarningFormat($"We are in download texture. '{materialJson.id}'  '{textureName}'");
		//Debug.LogError(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + materialJson.id + "_" + textureName);
		UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + materialJson.id + "_" + textureName);
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
			texture.name = textureName;
			DataCacher.CacheTexture(materialJson.id, texture);
			Debug.LogError("DOWNLOAD SINGLE TEXTURE COMPLETED");
			yield break;
		}
	}

	public delegate void WallApplyMaterial(Material material);
	public static IEnumerator GetMaterialDiffuseRightNow(MaterialJSON materialJson, WallApplyMaterial callback)
	{
		UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + materialJson.id + "_" + materialJson.tex.tex_diffuse);
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
			callback(MaterialBuilder.GetMaterialFromTexture(materialJson, texture));
			yield break;
		}
	}
}
