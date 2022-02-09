using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManagerType : MonoBehaviour
{
    public string typeMyRoom;
    public GameObject[] wallsType;
    public GameObject type;
    int i = 0;
    public GameObject selectWall;
    public GameObject goToRoom;
    public GameObject Proceed;
    private void Update()
    {
    }

    public void clickButt()
    {
        //Debug.LogError("#1");
        typeMyRoom = EventSystem.current.currentSelectedGameObject.name;
        //Debug.LogError("#2");

    }
    public void arrayWalls()
    {
        type = GameObject.Find("PanelSafeAreaRoomTypes").transform.Find(typeMyRoom).gameObject.transform.Find(typeMyRoom).gameObject;
        wallsType = new GameObject[type.transform.childCount];

        for(int i = 0; i < type.transform.childCount; i++)
        {
            wallsType[i] = type.transform.GetChild(i).gameObject;
        }
    }


    public void proceedClick()
    {

        if (i >= 0 && i < wallsType.Length)
        {
            if (i == wallsType.Length - 1)
            {
                
                goToRoom.SetActive(true);
            }

            wallsType[0].transform.Find("WallCom").gameObject.SetActive(true);
            RectTransform myWall = wallsType[0].GetComponent<RectTransform>();
            RectTransform orangeSelWall = selectWall.GetComponent<RectTransform>();
            orangeSelWall.sizeDelta = new Vector2(myWall.sizeDelta.x + 1, 5);
            selectWall.transform.SetParent(wallsType[0].transform);
            selectWall.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            orangeSelWall.position = myWall.position;
            orangeSelWall.rotation = myWall.rotation;

            Proceed.SetActive(false);
        }
        i++;
    }

   }

