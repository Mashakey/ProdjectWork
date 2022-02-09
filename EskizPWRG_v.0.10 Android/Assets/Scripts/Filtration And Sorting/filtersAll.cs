using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using static DataTypes;

public class filtersAll : MonoBehaviour
{
    public Sprite UpExpand;
    public Sprite DownExpand;
    public int index;

    [SerializeField]
    Text textUnderFilt;



    public void clickBt()
    {
        Sprite thisExpand = gameObject.transform.Find("Expand").gameObject.GetComponent<Image>().sprite;
        if (thisExpand == DownExpand)
        {
            thisExpand = UpExpand;
        }
        else
        {
            thisExpand = DownExpand;
        }    
    }

    public void selectFilt()
    {
        //GameObject checkFilt = gameObject.transform.Find("Check").gameObject;
        //if (checkFilt.active != true)
        //{
        //    checkFilt.SetActive(true);
            
        //}
        //else
        //{
        //    checkFilt.SetActive(false);
            
        //}
    }
    public void inputFilt()
    {

    }
}
