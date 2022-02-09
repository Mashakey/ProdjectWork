using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class citysForShop : MonoBehaviour
{
    public GameObject textCity;
    public FirstStartAndCityScriptableObj firstStartAndCityScriptableObj;
    public cityAndFirstStart cityAndFirstStart;
    public int shopId = 1;

    public GameObject proceed;
    public GameObject proeedShop;

    void Start()
    {       
        cityAndFirstStart.selShopID = shopId;
    }

    public void cityName()
    {
        textCity.GetComponent<Text>().text = firstStartAndCityScriptableObj.item.city;
        translateCity();
    }

    public void choseShop()
    {
        proceed.SetActive(false); 
        proeedShop.SetActive(true);
    }

    public void translateCity()
    {
        if(textCity.GetComponent<Text>().text == "All city")
        {
            textCity.GetComponent<Text>().text = "Все города";
        }
        else if(textCity.GetComponent<Text>().text == "Belovo")
        {
            textCity.GetComponent<Text>().text = "Белово";
        }
        else if (textCity.GetComponent<Text>().text == "Berdsk")
        {
            textCity.GetComponent<Text>().text = "Бердск";
        }
        else if (textCity.GetComponent<Text>().text == "Kemerovo")
        {
            textCity.GetComponent<Text>().text = "Кемерово";
        }
        else if (textCity.GetComponent<Text>().text == "Novosibirsk")
        {
            textCity.GetComponent<Text>().text = "Новосибирск";
        }
        else if (textCity.GetComponent<Text>().text == "Tomsk")
        {
            textCity.GetComponent<Text>().text = "Томск";
        }
    }
}
