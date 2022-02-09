using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectWallTouchForMaterial : MonoBehaviour
{
    public GameObject selectWall;
    public GameObject createWall;
    public GameObject check;
    public GameObject[] wallsAll;
    public GameObject typeRoom;
    public string typeMyRoom;
    public selectWallTouch selectWallTouch;

    void Start()
    {
        typeMyRoom = GameObject.Find("EventSystem").GetComponent<ManagerType>().typeMyRoom;
        typeRoom = GameObject.Find("PanelSafeAreaRoomTypes").transform.Find(typeMyRoom).gameObject;
        //typeRoom = GameObject.Find("PanelSafeArea").transform.Find(typeMyRoom).gameObject;

        wallsAll = new GameObject[typeRoom.transform.childCount];
        for (int i = 0; i < typeRoom.transform.childCount; i++)
        {
            wallsAll[i] = typeRoom.transform.GetChild(i).gameObject;
        }
    }

    public void selectWallTouchWind()
    {
        if (gameObject.transform.childCount == 0)
        {
            RectTransform myWall = gameObject.GetComponent<RectTransform>();
           
            GameObject wallSelActive = Instantiate(selectWall, myWall.transform.position, myWall.transform.rotation);
            wallSelActive.name = "orangeSelWall";
            wallSelActive.transform.parent = myWall;
            RectTransform orangeSelWall = wallSelActive.GetComponent<RectTransform>();
            //selectWallTouch.activeCheck();
        }
        else
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
            if (check.activeInHierarchy == true)
            {
                check.SetActive(false);
            }
        }
    }
}
