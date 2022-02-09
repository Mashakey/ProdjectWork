using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialSelectTypeWindow : MonoBehaviour
{
    public GameObject[] typeWind;
    public GameObject thisTypeWind;

    public void selectType()
    {
        for (int i = 0; i < typeWind.Length; i++)
        {
            if(typeWind[i].name == thisTypeWind.name)
            {
                thisTypeWind.SetActive(true);
            }
            else
            {
                typeWind[i].SetActive(false);
            }
        }
    }

    public void backTypeRoom()
    {
        for (int i = 0; i <= typeWind.Length; i++)
        {
            if (typeWind[i] == typeWind[3])
            {
                if (thisTypeWind.activeSelf!= true)
                {
                    thisTypeWind.SetActive(true);
                }
                
            }
            else
            {
                typeWind[i].SetActive(false);
            }
        }
    }
}
