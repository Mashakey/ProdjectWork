using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectCity : MonoBehaviour
{
    public Image selectImage;
    public Transform parent;
    public FirstStartAndCityScriptableObj firstStartAndCityScriptableObj;
   

    public void OnSelect()
    {
       
        selectImage.transform.SetParent(parent, true);
        selectImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        firstStartAndCityScriptableObj.item.city = gameObject.transform.parent.name;
        firstStartAndCityScriptableObj.SaveField();
    }
   
} 

