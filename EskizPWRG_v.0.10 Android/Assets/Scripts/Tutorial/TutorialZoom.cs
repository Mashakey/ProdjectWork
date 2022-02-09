using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialZoom : MonoBehaviour
{
    Camera mainCamera;

    float touchesPrevPosDifference, touchesCurPosDifference, zoomModifier;

    float zoomModifierSpeed = 0.1f;


    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount == 2)
        {
            ZoomCameraTutorial();
            mainCamera.GetComponent<Camera>().fieldOfView = Mathf.Clamp(mainCamera.GetComponent<Camera>().fieldOfView, 60f, 100f);
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
            mainCamera.GetComponent<Camera>().fieldOfView += zoomModifier;

        }
        if (touchesPrevPosDifference < touchesCurPosDifference)
        {
            mainCamera.GetComponent<Camera>().fieldOfView -= zoomModifier;
        }

    }
}
