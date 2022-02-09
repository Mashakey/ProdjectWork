using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class ScrollShops : MonoBehaviour
{

	public GameObject contentFieldPrefab;
	public GameObject contentPage;
	public Text selectCity;
	public List<ShopJson> ShopWhithCity = new List<ShopJson>(GlobalApplicationManager.Shops);
	public void CreateContentFields()
	{
		deleteChildScroll();
		//Debug.LogError("Child count before instantiate = " + contentPage.transform.childCount);
		for (int i = 0; i < GlobalApplicationManager.Shops.Count; i++)
		{
            if (selectCity.text == "Все города")
            {
                GameObject contentField = Instantiate(contentFieldPrefab, contentPage.transform);
				contentField.GetComponent<ValueShop>().FillFieldWithShope(GlobalApplicationManager.Shops[i]);

				//changeVerticleSize();
            }
            else
            {
                if (GlobalApplicationManager.Shops[i].address.locality == selectCity.text)
                {
                    GameObject contentField = Instantiate(contentFieldPrefab, contentPage.transform);
					contentField.name = GlobalApplicationManager.Shops[i].id;
					contentField.GetComponent<ValueShop>().FillFieldWithShope(GlobalApplicationManager.Shops[i]);
                }
            }
        }
		changeVerticleSize();


	}

	public void changeVerticleSize()
    {
		var heigthPage = contentPage.transform.childCount * contentFieldPrefab.GetComponent<RectTransform>().sizeDelta.y;
		//Debug.LogErrorFormat($"Childs count = {contentPage.transform.childCount} sizeDeltaY = {contentFieldPrefab.GetComponent<RectTransform>().sizeDelta.y} height = {heigthPage}");

		contentPage.GetComponent<RectTransform>().sizeDelta = new Vector2(contentPage.GetComponentInChildren<RectTransform>().sizeDelta.x, heigthPage);
	}

	public void deleteChildScroll()
    {
		for (int i = contentPage.transform.childCount - 1; i >= 0; i--)
		{
			DestroyImmediate(contentPage.transform.GetChild(i).gameObject);
			//Debug.LogWarningFormat($"child count = {contentPage.transform.childCount}  i = {i}");
		}
		//foreach (Transform child in contentPage.transform)
  //      {
		//	Destroy(child.gameObject);
  //      }

	}
}
