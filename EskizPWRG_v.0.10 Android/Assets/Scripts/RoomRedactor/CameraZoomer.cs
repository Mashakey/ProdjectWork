using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomer : MonoBehaviour
{
    public bool isActive = true;
    public float pathValue = 1f;

    Camera mainCamera;

    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;
    float zoomModifierSpeed = 0.0005f;

    CinemachineTrackedDolly cinemachineTrackedDolly;

    private void Awake()
    {
        mainCamera = Camera.main;
        cinemachineTrackedDolly = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    private void Update()
    {
        //cinemachineTrackedDolly.m_PathPosition = pathValue;
        if (isActive)
        {
            if (Input.touchCount == 2)
            {
                ZoomCameraTutorial();
                //mainCamera.GetComponent<Camera>().fieldOfView = Mathf.Clamp(mainCamera.GetComponent<Camera>().fieldOfView, 60f, 100f);
            }
        }
    }

    public void ZoomCameraTutorial()
    {
        var touchZero = Input.GetTouch(0);
        var touchOne = Input.GetTouch(1);

        Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

        touchesPrevPosDifference = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
        touchesCurPosDifference = (touchZero.position - touchOne.position).magnitude;

        zoomModifier = (touchZero.deltaPosition - touchOne.deltaPosition).magnitude * zoomModifierSpeed;



        if (touchesPrevPosDifference > touchesCurPosDifference)
        {
            float newZoomValue = cinemachineTrackedDolly.m_PathPosition + zoomModifier;
            if (newZoomValue > 0.9f)
            {
                newZoomValue = 0.9f;
            }
            cinemachineTrackedDolly.m_PathPosition = newZoomValue;

        }
        if (touchesPrevPosDifference < touchesCurPosDifference)
        {
            float newZoomValue = cinemachineTrackedDolly.m_PathPosition - zoomModifier;
            if (newZoomValue < 0.16f)
            {
                newZoomValue = 0.16f;
            }
            cinemachineTrackedDolly.m_PathPosition = newZoomValue;
        }

    }

    public void DeactivateZoom()
	{
        isActive = false;
	}

    public void ActivateZoom()
	{
        isActive = true;
	}
}
