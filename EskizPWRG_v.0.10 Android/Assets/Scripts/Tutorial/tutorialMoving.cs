using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialMoving : MonoBehaviour
{
    public GameObject circleMoving;
    public GameObject circleNext;
    public Tutorial tutorial;
    float cameraRotation;

    bool isMoved = false;

    private void Start()
    {
        cameraRotation = Camera.main.transform.rotation.y;
    }

    private void Update()
    {
        if (!isMoved)
        {
            if (Camera.main.transform.rotation.y >= cameraRotation + 0.5f || Camera.main.transform.rotation.y <= cameraRotation - 0.3f)
            {
                isMoved = true;
                circleNext.SetActive(true);
                tutorial.indexsNext = 4;
                Destroy(circleMoving);
            }
        }

        
    }
}
