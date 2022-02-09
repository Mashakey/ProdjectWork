using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSelecWall : MonoBehaviour
{
    public GameObject activeWall;
    public GameObject wall;
    public GameObject circleWall;
    public GameObject circleWindow;

    public void selectWall()
    {
        activeWall.transform.SetParent(wall.transform);
        RectTransform myWall = wall.GetComponent<RectTransform>();
        RectTransform orangeSelWall = activeWall.GetComponent<RectTransform>();
        orangeSelWall.sizeDelta = new Vector2(myWall.sizeDelta.x + 1, 5);
        activeWall.transform.SetSiblingIndex(2);

        orangeSelWall.position = myWall.position;
        orangeSelWall.rotation = myWall.rotation;

        circleWall.SetActive(false);
        circleWindow.SetActive(true);
    }
}
