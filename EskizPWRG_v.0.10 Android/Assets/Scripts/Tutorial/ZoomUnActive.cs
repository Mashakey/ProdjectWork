using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomUnActive : MonoBehaviour
{
    Camera mainCamera;
    public tutorialWindBut tutorialWindBut;
    public GameObject nextCircle;
    public Tutorial tutorial;
    float fistOrthographicSize;

    private void Awake()
    {
        mainCamera = Camera.main;
        fistOrthographicSize = mainCamera.GetComponent<Camera>().fieldOfView;
    }

    void Update()
    {
        if(Input.touchCount == 2)
        {
            if (mainCamera.GetComponent<Camera>().fieldOfView > fistOrthographicSize + 10f || mainCamera.GetComponent<Camera>().fieldOfView < fistOrthographicSize + 10f)
            {
                tutorialWindBut.mouseUp = true;
                tutorial.indexsNext = 1;
                gameObject.SetActive(false);
                nextCircle.SetActive(true);
            }
        }       
    }
}
