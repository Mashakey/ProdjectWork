using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraRotation : MonoBehaviour
{
    public GameObject VirtualCamera;
    public GameObject MiddleRoomPoint;

    Vector3 FirstPoint;
    Vector3 SecondPoint;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;
    bool isCameraFreezed = false;
    Quaternion oldCameraRotation;
    Quaternion newCameraRotation;

    void Start()
    {
        xAngle = 0;
        yAngle = 0;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
        oldCameraRotation = transform.rotation;
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
        if (Input.touchCount == 1)
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
                    //xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    xAngle = xAngleTemp - (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
                    yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
                    if (yAngle < -25f)
					{
                        yAngle = -25f;
					}
                    else if (yAngle > 25f)
					{
                        yAngle = 25f;
					}
                    newCameraRotation = Quaternion.Euler(yAngle, xAngle, 0.0f);

                    //this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
                }
            }
        }
    }

    public void RotateCameraRight()
    {
        xAngle += 0.5f;
        newCameraRotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    }

    public void RotateCameraLeft()
    {
        xAngle -= 0.5f;
        newCameraRotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    }

    void Update()
    {
        if (!isCameraFreezed)
		{
            RotateCameraByTouch();
            if (oldCameraRotation != transform.rotation)
			{
                Room room = FindObjectOfType<Room>();
                if (room != null)
                {
                    room.DeleteOppened3DButtons();
                }
			}
            oldCameraRotation = transform.rotation;
		}
        VirtualCamera.transform.rotation = Quaternion.Lerp(transform.rotation, newCameraRotation, 0.1f);
        MiddleRoomPoint.transform.rotation = Quaternion.Lerp(transform.rotation, newCameraRotation, 0.1f);
        //transform.rotation = Quaternion.Lerp(transform.rotation, newCameraRotation, 0.1f);

    }
}
