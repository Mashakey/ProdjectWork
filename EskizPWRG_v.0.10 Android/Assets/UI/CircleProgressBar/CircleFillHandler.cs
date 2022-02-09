using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleFillHandler : MonoBehaviour
{
    public Text textProcents;
    public bool chooseDoor;
    public bool chooseWindow;
    public bool addBaseboard;
    public bool addFurnityre;
    public bool addDecor;
    public bool chooseCeilingMaterial;
    public bool chooseWallMaterial;
    public bool chooseFloorMaterial;

    public void fillingOfTheRoomPercentage()
    {
        if (chooseDoor == true)
        {
            gameObject.GetComponent<Image>().fillAmount += 0.1f;
            chooseDoor = false;
        }
        else if (chooseWindow == true)
        {
            gameObject.GetComponent<Image>().fillAmount += 0.1f;
            chooseWindow = false;
        }
        else if (addBaseboard == true)
        {
            gameObject.GetComponent<Image>().fillAmount += 0.1f;
            addBaseboard = false;
        }
        else if (addFurnityre == true)
        {
            gameObject.GetComponent<Image>().fillAmount += 0.2f;
            addFurnityre = false;
        }
        else if (addDecor == true)
        {
            gameObject.GetComponent<Image>().fillAmount += 0.1f;
            addDecor = false;
        }
        else if (chooseCeilingMaterial == true)
        {
            gameObject.GetComponent<Image>().fillAmount += 0.1f;
            chooseCeilingMaterial = false;
        }
        else if (chooseWallMaterial == true)
        {
            gameObject.GetComponent<Image>().fillAmount += 0.2f;
            chooseWallMaterial = false;
        }
        else if (chooseFloorMaterial == true)
        {
            gameObject.GetComponent<Image>().fillAmount += 0.2f;
            chooseFloorMaterial = false;
        }

        textProcents.text = gameObject.GetComponent<Image>().fillAmount * 100 + "%";

    }
}
