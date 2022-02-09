using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddWinDoor : MonoBehaviour {
    public Button otherOne;
    public Button otherTwo;
    Color firstColor;
    Color otherColor;
    Color colorNew;
    public GameObject window;
    public GameObject door;
    ManagerType manager;

    public void selectButton()
    {
        
        ColorUtility.TryParseHtmlString("#868686", out firstColor); //darkgrey
        ColorUtility.TryParseHtmlString("#C0C0C0", out otherColor); //grey
        ColorUtility.TryParseHtmlString("#FF9900", out colorNew); //orange

        //if (gameObject.GetComponent<Image>().color == otherColor || gameObject.GetComponent<Image>().color == firstColor)
        //{
        //    gameObject.GetComponent<Image>().color = colorNew;

        //    otherOne.GetComponent<Image>().color = otherColor;
        //    otherTwo.GetComponent<Image>().color = otherColor;
        //}
        //else
        //    gameObject.GetComponent<Image>().color = otherColor;

    }

    public AddWinDoor()
    {
        //if(gameObject.name == "Null")
        //{
        //    selectButton();
        //}
        //else if(gameObject.name == "Two")
        //{

        //    //manager.wallsType[i].
        //    //Instantiate(door, );
        //}
    }
}
