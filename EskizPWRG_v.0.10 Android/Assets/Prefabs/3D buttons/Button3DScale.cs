using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button3DScale : MonoBehaviour
{
    Color orange;
    bool isActive = false;
    public void OnMouseDown()
    {
        Debug.Log("Window scale button click");
        if (!isActive)
        {
            GetComponentInParent<Buttons3DWindow>().TurnOffMoveButton();
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void TurnOn()
	{
        CameraZoomer cameraZoomer = FindObjectOfType<CameraZoomer>();
        cameraZoomer.DeactivateZoom();
        Buttons3DWindow windowButtonsScript = GetComponentInParent<Buttons3DWindow>();

        isActive = true;
        ColorUtility.TryParseHtmlString("#FF9900", out orange); //orange
        var windowTouchScaler = transform.GetComponentInParent<WindowTouchScaler>();
        if (windowTouchScaler != null)
        {
            GetComponent<SpriteRenderer>().color = orange;
            Debug.Log("windowTouchScaler is found");
            windowTouchScaler.TurnOn();
        }
    }

    public void TurnOff()
	{
        CameraZoomer cameraZoomer = FindObjectOfType<CameraZoomer>();
        cameraZoomer.ActivateZoom();
        var windowTouchScaler = transform.GetComponentInParent<WindowTouchScaler>();
        if (windowTouchScaler != null)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Debug.Log("windowTouchScaler is found");
            windowTouchScaler.TurnOff();
        }
        isActive = false;
    }
}
