using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWallTutorial : MonoBehaviour
{
    public GameObject activeWall;

    public void selectWallType()
    {
        gameObject.transform.Find("WallCom").gameObject.SetActive(true);

        RectTransform myWall = gameObject.GetComponent<RectTransform>();
        RectTransform orangeSelWall = activeWall.GetComponent<RectTransform>();
        orangeSelWall.sizeDelta = new Vector2(myWall.sizeDelta.x + 1, 5);
        activeWall.transform.SetParent(gameObject.transform);
        activeWall.transform.SetSiblingIndex(2);

        orangeSelWall.position = myWall.position;
        orangeSelWall.rotation = myWall.rotation;
    }
}
