using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons3DFurniture : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
    }

    public void TurnOffRotateButton()
	{
        Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
        GetComponentInChildren<Button3DRotateFurniture>().TurnOff();
    }

    public void TurnOffMoveButton()
	{
        Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
        GetComponentInChildren<Button3DMoveFurniture>().TurnOff();
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
