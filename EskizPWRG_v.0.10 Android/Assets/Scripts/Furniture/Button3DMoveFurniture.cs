using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3DMoveFurniture : MonoBehaviour
{
    Color orange;
    bool isActive = false;
    public void OnMoveButtonClick()
    {
        Debug.LogError("Furniture move button click");
        if (!isActive)
        {
            Buttons3DFurniture buttons3DFurniture = GetComponentInParent<Buttons3DFurniture>();
            if (buttons3DFurniture != null)
            {
                buttons3DFurniture.TurnOffRotateButton();
            }
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }


    public void TurnOn()
    {
        isActive = true;
        ColorUtility.TryParseHtmlString("#FF9900", out orange); //orange
        GetComponent<SpriteRenderer>().color = orange;
        GetComponentInParent<FurnitureObject>().isMoving = true;
        Camera.main.GetComponent<CameraRotation>().FreezeCamera();
    }

    public void TurnOff()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponentInParent<FurnitureObject>().isMoving = false;
        Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
        isActive = false;
    }
}
