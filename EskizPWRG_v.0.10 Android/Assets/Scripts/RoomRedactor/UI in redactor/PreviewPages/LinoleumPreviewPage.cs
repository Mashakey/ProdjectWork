using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using static DataTypes;

public class LinoleumPreviewPage : MonoBehaviour
{
    [SerializeField]
    Image previewImageField;
    [SerializeField]
    GameObject LoadingPopupCover;

    [SerializeField]
    Text headNameTextField;
    [SerializeField]
    Text headVendorCodeTextField;
    [SerializeField]
    Text priceTextField;
    [SerializeField]
    Text manufacturerTextField;
    [SerializeField]
    Text manufacturerCountryTextField;
    [SerializeField]
    Text collectionTextField;
    [SerializeField]
    Text windingTheRoll;
    [SerializeField]
    Text width_list;
    [SerializeField]
    Text total_thickness;
    [SerializeField]
    Text zs_thikness;
    [SerializeField]
    Text weight;
    [SerializeField]
    Text basis;
    [SerializeField]
    Text use;
    [SerializeField]
    Text fireHazardClass;
    [SerializeField]
    Text resident_usage_class;
    [SerializeField]
    Text public_usage_class;
    [SerializeField]
    Text possibilityHeatingSystem;

    public MaterialJSON currentMaterialJson;


    public void ActivatePreviewPage()
    {
        gameObject.SetActive(true);
    }

    public void DeactivatePreviewPage()
    {
        gameObject.SetActive(false);
    }

    public void FillPage(MaterialJSON materialJson)
    {
        currentMaterialJson = materialJson;
        GetComponent<CurrentMaterialJsonKeeper>().SetCurrentMaterialJson(currentMaterialJson);

        ActivatePreviewPage();
        headNameTextField.text = materialJson.name;
		headVendorCodeTextField.text = materialJson.custom_properties.manufacturer_company;

        //headVendorCodeTextField.text = materialJson.custom_properties.vendor_code;
        StartCoroutine(FillFieldPreviewImage(materialJson));

        string priceUnits = PriceUnitsFormatter.SetUpPriceUnits(materialJson.units);
        priceTextField.text = string.Format($"{materialJson.cost} {priceUnits}");
        manufacturerTextField.text = materialJson.custom_properties.manufacturer_company;
        manufacturerCountryTextField.text = materialJson.custom_properties.manufacturer_country;
        collectionTextField.text = materialJson.custom_properties.collection;
        string widths = "";
        foreach (var width in materialJson.width_list)
		{
            widths += width + " ל,";
		}
        widths.TrimEnd(',');
        width_list.text = widths;
        total_thickness.text = materialJson.custom_properties.total_thickness.ToString() + " לל";
        zs_thikness.text = materialJson.custom_properties.zs_thickness.ToString() + " לל";
        weight.text = materialJson.custom_properties.weight.ToString() + " ךד";
        basis.text = materialJson.custom_properties.basis;
        use.text = materialJson.custom_properties.use;
        fireHazardClass.text = "";
        string[] usageClasses = materialJson.custom_properties.usage_class.Split('/');
        if (usageClasses.Length > 0)
		{
            resident_usage_class.text = usageClasses[0];
		}
        else
		{
            resident_usage_class.text = "";
		}
        if (usageClasses.Length > 1)
        {
            public_usage_class.text = usageClasses[1];
        }
        else
		{
            public_usage_class.text = "";
		}
        possibilityHeatingSystem.text = "";
        SetHeartStatusInPreview();

    }

    IEnumerator FillFieldPreviewImage(MaterialJSON materialJson)
    {
        if (DataCacher.IsTextureExist(materialJson.id, materialJson.tex.tex_diffuse))
        {
            Debug.LogWarningFormat($"Texture for {materialJson.name} is exist");
            Texture2D texture = DataCacher.GetTextureFromCache(materialJson.id, materialJson.tex.tex_diffuse);
            texture.name = materialJson.tex.tex_diffuse;
            yield return null;
            Sprite preview = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            yield return null;
            previewImageField.GetComponent<UnityEngine.UI.Image>().sprite = preview;
            LoadingPopupCover.SetActive(false);

            yield break;
        }
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
            TextureDownloadingStatus downloadingStatus = new TextureDownloadingStatus();
            StartCoroutine(FindObjectOfType<TextureDownloader>().DownloadAndCacheAllMaterialTextures(materialJson, downloadingStatus));
            Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
            texture.name = materialJson.tex.tex_diffuse;
            //texture.name = materialJson.preview_icon;
            yield return null;
            Sprite preview = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            yield return null;
            previewImageField.GetComponent<UnityEngine.UI.Image>().sprite = preview;
            DataCacher.CacheTexture(materialJson.id, texture);
            while (downloadingStatus.isNormalDownloaded == false && downloadingStatus.isRoughnessDownloaded == false)
            {
                yield return (null);
            }
            LoadingPopupCover.SetActive(false);
            yield break;
        }
    }

    public void SetHeartStatusInPreview()
    {
        LikeMaterialInPreviewPage likeHandler = GetComponentInChildren<LikeMaterialInPreviewPage>();
        if (FavoritesStorage.IsMaterialInFavorites(currentMaterialJson))
        {
            likeHandler.SetHeartRedWithoutAdding();
        }
        else
        {
            likeHandler.SetEmptyHeart();
        }
    }

    public void ApplySelectedMaterialToSurface()
    {
        MaterialBuilder materialBuilder = Transform.FindObjectOfType<MaterialBuilder>();
        List<Transform> selectedObjects = GlobalApplicationManager.GetAndPopSelectedObjects();
        if (materialBuilder != null)
        {
            Debug.LogFormat($"Trying to apply material '{currentMaterialJson}' to surface");
            Debug.Log(currentMaterialJson);
            foreach (var selectedObject in selectedObjects)
            {
                Debug.LogError("We are in PreviewPage. Selected object is '" + selectedObject.name + "'");
                materialBuilder.StartCoroutine(materialBuilder.UpdateMeshMaterial(currentMaterialJson, selectedObject.GetComponent<TextureUpdater>().UpdateTexture));
            }
        }
        previewImageField.GetComponent<UnityEngine.UI.Image>().sprite = null;

        DeactivatePreviewPage();

    }
}
