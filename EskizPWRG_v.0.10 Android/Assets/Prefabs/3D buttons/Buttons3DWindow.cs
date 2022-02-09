using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons3DWindow : MonoBehaviour
{
    //void Start()
    //{
    //    GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    //}

    public void TurnOffMoveButton()
	{
        Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
        GetComponentInChildren<Button3DMove>().TurnOff();
	}

    public void TurnOffScaleButton()
	{
        Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
        Button3DScale scaleButton = GetComponentInChildren<Button3DScale>();
        if (scaleButton)
        {
            scaleButton.TurnOff();
        }
	}
}
