using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCameraRotation : MonoBehaviour
{
    Vector3 FirstPoint;
    Vector3 SecondPoint;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;
    bool isCameraFreezed = false;

    void Start()
    {
        xAngle = -60;
        yAngle = 12;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    }

    public void FreezeCamera()
    {
        isCameraFreezed = true;
    }

    public void UnfreezeCamera()
    {
        isCameraFreezed = false;
    }

    void RotateCameraByTouch()
    {
        if (Input.touchCount > 0)
        {
            if (!CheckClickUI.IsClikedOnUI())
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    FirstPoint = Input.GetTouch(0).position;
                    xAngleTemp = xAngle;
                    yAngleTemp = yAngle;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    SecondPoint = Input.GetTouch(0).position;
                    xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;

                    if (yAngle < -55f)
                    {
                        yAngle = -55f;
                    }
                    else if (yAngle > 55f)
                    {
                        yAngle = 55f;
                    }
                    this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
                }
            }
        }
    }

    void Update()
    {
        if (!isCameraFreezed)
        {
            RotateCameraByTouch();
        }

    }
}
