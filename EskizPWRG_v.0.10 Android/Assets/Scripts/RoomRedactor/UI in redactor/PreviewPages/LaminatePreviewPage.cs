using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DataTypes;

public class LaminatePreviewPage : MonoBehaviour
{
    [SerializeField]
    GameObject LoadingPopupCover;

    [SerializeField]
    Image previewImageField;

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
    Text board_length;
    [SerializeField]
    Text board_width;
    [SerializeField]
    Text board_thickness;
    [SerializeField]
    Text count_M2_per_pack;
    [SerializeField]
    Text count_items_per_pack;
    [SerializeField]
    Text lock_type;
    [SerializeField]
    Text chamfer;
    [SerializeField]
    Text class_num;
    [SerializeField]
    Text design_type;
    [SerializeField]
    Text moisture_resistant;

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
        LoadingPopupCover.SetActive(true);
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
        board_length.text = materialJson.custom_properties.board_length.ToString() + " ��";
        board_width.text = materialJson.custom_properties.board_width.ToString() + " ��";
        board_thickness.text = materialJson.custom_properties.board_thickness.ToString() + " ��";
        count_M2_per_pack.text = (materialJson.custom_properties.board_length * materialJson.custom_properties.board_width * materialJson.custom_properties.count_items_per_pack).ToString() + " �2";
        count_items_per_pack.text = materialJson.custom_properties.count_items_per_pack.ToString();
        lock_type.text = materialJson.custom_properties.lock_type;
        chamfer.text = materialJson.custom_properties.chamfer == true ? "��" : "���";
        class_num.text = materialJson.custom_properties.class_num.ToString();
        design_type.text = materialJson.custom_properties.design_type;
        moisture_resistant.text = materialJson.custom_properties.moisture_resistant == true ? "��" : "���";
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
