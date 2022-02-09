using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectButton : MonoBehaviour
{
    public GameObject thisBut;
    public GameObject[] otherBut;
    Color otherColor;
    Color colorNew;

    public void Update()
    {       
    }
    public void selectButton()
    {
        ColorUtility.TryParseHtmlString("#868686", out otherColor);
        ColorUtility.TryParseHtmlString("#FF9900", out colorNew); 

        if (thisBut.GetComponent<Image>().color == otherColor)
        {
            thisBut.GetComponent<Image>().color = colorNew;
            
            foreach(var other in otherBut)
            {
                other.GetComponent<Image>().color = otherColor;
            }
        }
        else
            thisBut.GetComponent<Image>().color = otherColor;
        
    }
}
