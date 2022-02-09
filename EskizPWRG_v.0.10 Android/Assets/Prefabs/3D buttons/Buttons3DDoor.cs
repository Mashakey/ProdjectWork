using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons3DDoor : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }

    public void TurnOffMoveButton()
    {
        Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
        GetComponentInChildren<Button3DMove>().TurnOff();
    }

}
