using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using static DataTypes;

public class EstimatePage : MonoBehaviour
{

	[SerializeField]
	Transform contentFieldsPage;

	[SerializeField]
	Transform contentFieldPrefab;

	[SerializeField]
	Text RoomTotalCostField;

	[SerializeField]
	Button GoToShopsButton;
	[SerializeField]
	Sprite ActiveGoToShopsButtonSprite;
	[SerializeField]
	Sprite InactiveGoToShopsButtonSprite;

	List<Transform> contentFields = new List<Transform>();

	public void CreateContentFields(Dictionary<string, int> materialsWithCost)
	{
		ClearContentPage();
		foreach (var material in materialsWithCost)
		{
			Transform contentField = Instantiate(contentFieldPrefab, contentFieldsPage);
			contentField.name = material.Key;
			//MaterialJSON materialDataJson = DataCacher.GetMaterialById(material.Key);
			MaterialJSON materialDataJson = GlobalApplicationManager.GetMaterialJsonById(material.Key);
			if (materialDataJson != null)
			{
				Transform ImMat = contentField.transform.Find("ImMat");

				Transform previewImageField = ImMat.Find("DownImageMaterial");
				StartCoroutine(FillFieldPreviewImage(materialDataJson, previewImageField));
				Text materialName = contentField.transform.Find("MaterialName").GetComponent<Text>();
				materialName.text = materialDataJson.name;
				Text materialOnePiecePrice = contentField.transform.Find("Price").GetComponent<Text>();
				materialOnePiecePrice.text = materialDataJson.cost.ToString() + PriceUnitsFormatter.SetUpPriceUnits(materialDataJson.units);
				Text materialCount = contentField.transform.Find("Count").GetComponent<Text>();
				materialCount.text = material.Value.ToString() + PriceUnitsFormatter.SetUpOneUnit(materialDataJson.units);
				float totalCost = materialDataJson.cost * material.Value;
				Text totalPrice = contentField.transform.Find("TotalPrice").GetComponent<Text>();
				totalPrice.text = totalCost.ToString() + " \u20BD";
			}
			contentFields.Add(contentField);
		}
	}

	public void SetTotalRoomCost(float cost)
	{
		if (cost <= 0)
		{
			GoToShopsButton.interactable = false;
			GoToShopsButton.image.sprite = InactiveGoToShopsButtonSprite;
		}
		else
		{
			GoToShopsButton.interactable = true;
			GoToShopsButton.image.sprite = ActiveGoToShopsButtonSprite;
		}
		RoomTotalCostField.text = cost.ToString() + " \u20BD";
	}

	public void ClearContentPage()
	{
		foreach (var contentField in contentFields)
		{
			Destroy(contentField.gameObject);
		}
		contentFields.Clear();
	}

	IEnumerator FillFieldPreviewImage(MaterialJSON materialJson, Transform previewImageField)
	{
		if (DataCacher.GetTextureFromCache(materialJson.id, materialJson.preview_icon) != null)
		{
			//yield break;
		}
		if (materialJson.type == "paint")
		{
			Texture2D texturePreview = DataCacher.GetPaintPreviewFromCache(materialJson);
			Sprite preview = Sprite.Create(texturePreview, new Rect(0.0f, 0.0f, texturePreview.width, texturePreview.height), new Vector2(0.5f, 0.5f), 100.0f);
			previewImageField.GetComponent<Image>().sprite = preview;
			yield break;
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
	// Start is called before the first frame update
	void Start()
	{
		
	}
}
