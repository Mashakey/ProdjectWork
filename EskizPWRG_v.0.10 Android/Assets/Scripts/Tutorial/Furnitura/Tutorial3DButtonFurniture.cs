using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial3DButtonFurniture : MonoBehaviour
{
    void Start()
    {
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }

    public void TurnOffRotateButton()
    {
        Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
    }

    public void TurnOffMoveButton()
    {
        Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
    }

    public void Update()
    {
        SetLookOnCamera();
    }

    public void SetLookOnCamera()
    {
        transform.LookAt(Camera.main.transform);
    }
}
