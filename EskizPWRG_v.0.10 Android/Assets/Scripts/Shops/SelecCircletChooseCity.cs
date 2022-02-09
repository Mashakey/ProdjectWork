using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecCircletChooseCity : MonoBehaviour
{
    public Text textCity;
    public GameObject selectCircle;
    public List<GameObject> city = new List<GameObject>();
    public string buttonName;

    public void selectMyCity()
    {
        translateCityForButton();

        for (int i = 0; i < city.Count; i++)
        {
            if(city[i].name == buttonName)
            {
                selectCircle.transform.parent = city[i].transform;
                selectCircle.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

                Debug.LogError(selectCircle.GetComponent<RectTransform>().localPosition );
            }
        }
    }

    public void translateCityForButton()
    {
        if (textCity.text == "Все города")
        {
            buttonName = "AllUI";
        }
        else if (textCity.text == "Белово")
        {
            buttonName = "BelovoUI";
        }
        else if (textCity.text == "Бердск")
        {
            buttonName = "BerdskUI";
        }
        else if (textCity.text == "Кемерово")
        {
            buttonName = "KemerovoUI";
        }
        else if (textCity.text == "Новосибирск")
        {
            buttonName = "NovosibirskUI";
        }
        else if (textCity.text == "Томск")
        {
            buttonName = "TomskUI";
        }
    }
}
