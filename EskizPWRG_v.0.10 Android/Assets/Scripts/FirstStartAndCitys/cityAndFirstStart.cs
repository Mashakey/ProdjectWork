using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cityAndFirstStart : MonoBehaviour
{
    FirstStartAndCityScriptableObj firstStartAndCityScriptableObj;

    public GameObject intro;
    public GameObject myRoom;
    public GameObject cityText;
    public GameObject selShop;
    public GameObject cityChoose;
    public int selShopID;

    private void Awake()
    {
        firstStartAndCityScriptableObj = gameObject.GetComponent<FirstStartAndCityScriptableObj>();
    }
    private void Start()
    {

		if (firstStartAndCityScriptableObj.item.numberRun == 0)
		{
			Debug.LogError("first run!");
			intro.SetActive(true);
			firstStartAndCityScriptableObj.item.numberRun++;
			firstStartAndCityScriptableObj.SaveField();
		}
		else
		{
			firstStartAndCityScriptableObj.item.numberRun++;
			Debug.LogError("Welcome again!");
			myRoom.SetActive(true);
			intro.SetActive(false);
		}

	}

    public void changeCityForShops()
    {
        if(firstStartAndCityScriptableObj.item.numberRun != 0)
        {
            selShop.SetActive(false);
            cityChoose.SetActive(true);
        }
        
    }
   
}
