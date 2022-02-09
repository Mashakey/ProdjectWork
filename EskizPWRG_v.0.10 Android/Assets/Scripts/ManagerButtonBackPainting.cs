using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerButtonBackPainting : MonoBehaviour
{
    public GameObject wallPage;
    public GameObject ceilingPage;
    public bool inedexClick = false;

    public void buttonBackPaint()
    {
        if (inedexClick == true)
        {
            wallPage.GetComponent<Canvas>().enabled = true;
            wallPage.GetComponent<CanvasScaler>().enabled = true;
            inedexClick = false;
        }
        else
        {
            ceilingPage.GetComponent<Canvas>().enabled = true;
            ceilingPage.GetComponent<CanvasScaler>().enabled = true;
            ceilingPage.GetComponent<CeilingBackButton>().OnBackButtonClick();
        }

    }

    public void indexTrue()
    {
        inedexClick = true;
    }
}
