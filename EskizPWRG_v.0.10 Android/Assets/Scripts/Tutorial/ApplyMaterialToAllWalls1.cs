using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using static DataTypes;

public class ApplyMaterialToAllWalls1 : MonoBehaviour
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
        vendorCodeField.text = materialJson.custom_properties.vendor_code;
        priceField.text = materialJson.cost.ToString();
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
        if (DataCacher.GetTextureFromCache(materialJson.id, materialJson.preview_icon) != null)
        {
            //yield break;
        }
        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(ServerDownloaderSettings.UsingURI + ServerDownloaderSettings.get_textures + materialJson.id + "_" + materialJson.preview_icon);
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
            texture.name = materialJson.preview_icon;
            yield return null;
            Sprite preview = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            yield return null;
            previewImageField.GetComponent<UnityEngine.UI.Image>().sprite = preview;
            DataCacher.CacheTexture(materialJson.id, texture);
            yield break;
        }
    }

    public void ApplySelectedMaterialToSurface()
    {
        Canvas canvas = transform.GetComponentInParent<Canvas>();
        transform.GetComponentInParent<CanvasScaler>().enabled = false;
        canvas.enabled = false;

        Room activeRoom = GameObject.FindObjectOfType<Room>();
        foreach (var wall in activeRoom.Walls)
        {
            GlobalApplicationManager.AddSelectedObject(wall.transform);
        }

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
