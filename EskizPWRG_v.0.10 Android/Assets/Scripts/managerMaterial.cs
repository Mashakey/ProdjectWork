using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class managerMaterial : MonoBehaviour
{
	public List<GameObject> arMaterial = new List<GameObject>();
	[SerializeField]
	GameObject materialPage;
	[SerializeField]
	GameObject materialPrefab;

	Wall parentWall;

	public void SetCanvasActive(bool condition)
	{
		if (condition)
		{
			gameObject.GetComponent<Canvas>().enabled = true;
			gameObject.GetComponent<CanvasScaler>().enabled = true;
		}
		else
		{
			gameObject.GetComponent<Canvas>().enabled = false;
			gameObject.GetComponent<CanvasScaler>().enabled = false;
		}
	}

	public void SetParentWall(Wall parentWall)
	{
		this.parentWall = parentWall;
	}

	public Wall GetParentWall()
	{
		return (parentWall);
	}

	public IEnumerator CreateMaterialList()
	{
		List<MaterialJSON> materials = DataCacher.GetCachedMaterialsJSONs();
		int count = 9;
		for (int i = 0; i < count; i++)
		{
			GameObject materialField = Instantiate(materialPrefab, materialPage.transform);
			materialField.name = materials[i].id;
			Transform imMat = materialField.transform.GetChild(0);
			Transform previewField = imMat.GetChild(0);
			Texture2D texturePreview = DataCacher.GetPreviewFromCache(materials[i]);
			Sprite preview = Sprite.Create(texturePreview, new Rect(0.0f, 0.0f, texturePreview.width, texturePreview.height), new Vector2(0.5f, 0.5f), 100.0f);
			previewField.GetComponent<UnityEngine.UI.Image>().sprite = preview;
			Transform text = materialField.transform.GetChild(1);
			text.GetComponent<UnityEngine.UI.Text>().text = materials[i].name;
			Transform vendorCode = materialField.transform.GetChild(2);
			vendorCode.GetComponent<UnityEngine.UI.Text>().text = materials[i].custom_properties.vendor_code;
			Transform price = materialField.transform.GetChild(3);
			price.GetComponent<UnityEngine.UI.Text>().text = materials[i].cost.ToString();
			Resources.UnloadUnusedAssets();
			yield return null;
		}
	}

	//public void CreateMaterialList()
	//{
	//	List<MaterialJSON> materials = DataCacher.GetCachedMaterialsJSONs();
	//	int count = 10;
	//	for (int i = 0; i < count; i++)
	//	{
	//		GameObject materialField = Instantiate(materialPrefab, materialPage.transform);
	//		materialField.name = materials[i].id;
	//		Transform imMat = materialField.transform.GetChild(0);
	//		Transform previewField = imMat.GetChild(0);
	//		Texture2D texturePreview = DataCacher.GetPreviewFromCache(materials[i]);
	//		Sprite preview = Sprite.Create(texturePreview, new Rect(0.0f, 0.0f, texturePreview.width, texturePreview.height), new Vector2(0.5f, 0.5f), 100.0f);
	//		previewField.GetComponent<UnityEngine.UI.Image>().sprite = preview;
	//		Transform text = materialField.transform.GetChild(1);
	//		text.GetComponent<UnityEngine.UI.Text>().text = materials[i].name;
	//		Transform vendorCode = materialField.transform.GetChild(2);
	//		vendorCode.GetComponent<UnityEngine.UI.Text>().text = materials[i].custom_properties.vendor_code;
	//		Transform price = materialField.transform.GetChild(3);
	//		price.GetComponent<UnityEngine.UI.Text>().text = materials[i].cost.ToString();
	//	}
	//}

	private void Start()
	{

	}

}
