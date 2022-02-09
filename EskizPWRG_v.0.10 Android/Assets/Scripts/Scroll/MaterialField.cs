using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

using UnityEngine.Networking;
using static DataTypes;

public class MaterialField : MonoBehaviour
{
    [SerializeField]
    Image previewImageField;
    [SerializeField]
    Text materialNameField;
    [SerializeField]
    Text vendorCodeField;
    [SerializeField]
    Text priceField;

    public void FillFieldWithMaterialData(MaterialJSON materialJson)
    {
        StartCoroutine(FillFieldPreviewImage(materialJson));
        gameObject.name = materialJson.id;
        materialNameField.text = materialJson.name;
        //vendorCodeField.text = materialJson.custom_properties.vendor_code;
        vendorCodeField.text = materialJson.custom_properties.manufacturer_company;
        priceField.text = materialJson.cost.ToString() + PriceUnitsFormatter.SetUpPriceUnits(materialJson.units);
        SetHeartSpriteStatus(materialJson);
    }

    public void SetHeartSpriteStatus(MaterialJSON materialJson)
    {
        if (FavoritesStorage.IsMaterialInFavorites(materialJson))
        {
            Debug.LogError("Material " + materialJson.name + " is in favorites");
            LikeMaterial likeMaterial = GetComponentInChildren<LikeMaterial>();
            likeMaterial.SetHeartRedWithoutAdding();
        }
        else
        {
            LikeMaterial likeMaterial = GetComponentInChildren<LikeMaterial>();
            likeMaterial.SetHeartWhite();
        }
    }

    //   IEnumerator FillFieldPreviewImage(MaterialJSON materialJson)
    //{
    //       Texture2D texturePreview = DataCacher.GetPreviewFromCache(materialJson);
    //       yield return null;
    //       Sprite preview = Sprite.Create(texturePreview, new Rect(0.0f, 0.0f, texturePreview.width, texturePreview.height), new Vector2(0.5f, 0.5f), 100.0f);
    //       yield return null;
    //       previewImageField.GetComponent<UnityEngine.UI.Image>().sprite = preview;
    //       yield break;
    //   }

    IEnumerator FillFieldPreviewImage(MaterialJSON materialJson)
    {
        //if (DataCacher.GetTextureFromCache(materialJson.id, materialJson.preview_icon) != null)
        //{
        //    //yield break;
        //}
        UnityWebRequest textureRequest;
        if (DataCacher.IsPreviewExist(materialJson))
        {
            UriBuilder uriBuilder = new UriBuilder(Path.Combine(Application.streamingAssetsPath, "Textures", materialJson.id, materialJson.preview_icon));
            string previewPath = uriBuilder.Uri.ToString();
            textureRequest = UnityWebRequestTexture.GetTexture(previewPath);
            Debug.LogWarning("Preview is exist. Downloading from cache");
        }
        else
        {
            textureRequest = UnityWebRequestTexture.GetTexture(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + materialJson.id + "_" + materialJson.preview_icon);
            HTTPHeader autorizationHeader = ServerDownloaderSettings.GetAutorizationHeader();
            textureRequest.SetRequestHeader(autorizationHeader.Key, autorizationHeader.Value);
            Debug.LogWarning("Downloading preview from server");

        }
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
            texture.name = materialJson.preview_icon;
            yield return null;
            Sprite preview = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            yield return null;
            previewImageField.GetComponent<Image>().sprite = preview;
            //DataCacher.CacheTexture(materialJson.id, texture);
            yield break;
        }
    }

    public void CreatePreviewPage()
    {
        PreviewMaterialPageFiller previewMaterialPageFiller = FindObjectOfType<PreviewMaterialPageFiller>();
        if (previewMaterialPageFiller == null)
        {
            Debug.LogError("Preview material page filler is not found");
            return;
        }
        MaterialJSON materialJson = GlobalApplicationManager.GetMaterialJsonById(gameObject.name);
        if (materialJson == null)
        {
            Debug.LogErrorFormat($"Material '{gameObject.name}' json is not found in GlobalApplicationManager");
            return;
        }
        else
        {
            Debug.LogError("Found material is " + materialJson.id);
            Debug.LogError("Founded for " + gameObject.name);
        }
        Canvas canvas = transform.GetComponentInParent<Canvas>();
        //transform.GetComponentInParent<CanvasScaler>().enabled = false;
        //canvas.enabled = false;
        previewMaterialPageFiller.EnableMaterialPreviewPage(materialJson);
    }

    public void ApplySelectedMaterialToSurface()
    {
        Canvas canvas = transform.GetComponentInParent<Canvas>();
        transform.GetComponentInParent<CanvasScaler>().enabled = false;
        canvas.enabled = false;

        MaterialBuilder materialBuilder = Transform.FindObjectOfType<MaterialBuilder>();
        List<Transform> selectedObjects = GlobalApplicationManager.GetAndPopSelectedObjects();
        if (materialBuilder != null)
        {
            Debug.Log("Trying to apply material to surface");
            MaterialJSON materialJson = DataCacher.GetMaterialById(gameObject.name);
            Debug.Log(materialJson.id);
            foreach (var selectedObject in selectedObjects)
            {
                Debug.LogError("We are in material field. Selected object is '" + selectedObject.name + "'");
                StartCoroutine(materialBuilder.UpdateMeshMaterial(materialJson, selectedObject.GetComponent<TextureUpdater>().UpdateTexture));
            }
        }
    }
}
