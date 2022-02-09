using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class selectWallTouch : MonoBehaviour
{
    public GameObject selectWall;
    public GameObject createWall;
    public GameObject check;
    public GameObject[] wallsAll;
    public GameObject typeRoom;
    public string typeMyRoom;
    bool selWall = true;

    [SerializeField]
    ParametersChangeHandler parametersChangeHandler;

    [SerializeField]
    RoomWallManager roomWallManager;

	private void Awake()
	{
        roomWallManager = GetComponentInParent<RoomWallManager>();
	}

	private void Update()
    {
    }

    private void Start()
    {
        typeMyRoom = GameObject.Find("EventSystem").GetComponent<ManagerType>().typeMyRoom;
		typeRoom = GameObject.Find("PanelSafeAreaRoomTypes").transform.Find(typeMyRoom).gameObject;
		//typeRoom = GameObject.Find("PanelSafeArea").transform.Find(typeMyRoom).gameObject;

		wallsAll = new GameObject[typeRoom.transform.Find(typeRoom.name).transform.childCount];
        for (int i = 0; i < typeRoom.transform.Find(typeRoom.name).transform.childCount; i++)
        {
            wallsAll[i] = typeRoom.transform.Find(typeRoom.name).transform.GetChild(i).gameObject;
        }
    }
    public void selectForParam()
    {      
            gameObject.transform.Find("WallCom").gameObject.SetActive(true);
            GetComponent<WallConstructor>().isActive = true;
            //Debug.LogError(GetComponent<WallConstructor>().WallIndex + " is active");
            //Debug.LogError("index = " + GetComponent<WallConstructor>().WallIndex);
            roomWallManager.SetActiveWallIndex(GetComponent<WallConstructor>().WallIndex);

            RectTransform myWall = gameObject.GetComponent<RectTransform>();
            RectTransform orangeSelWall = selectWall.GetComponent<RectTransform>();
            orangeSelWall.sizeDelta = new Vector2(myWall.sizeDelta.x + 1, 5);
            selectWall.transform.SetParent(gameObject.transform);
            selectWall.transform.SetSiblingIndex(2);

            orangeSelWall.position = myWall.position;
            orangeSelWall.rotation = myWall.rotation;
               
    }

    public void selectAllWall()
    {
        int type = wallsAll.Length;
        if (check.activeSelf != true)
        {
            check.SetActive(true);

            for (int i = 0; i < type; i++)
            {
                GameObject orangeSelWall = Instantiate(selectWall, wallsAll[i].transform.position, wallsAll[i].transform.rotation);
                orangeSelWall.name = "orangeSelWall";
                orangeSelWall.transform.SetParent(wallsAll[i].transform);
                RectTransform actWall = wallsAll[i].GetComponent<RectTransform>();
                RectTransform instWall = orangeSelWall.GetComponent<RectTransform>();
                instWall.sizeDelta = new Vector2(actWall.sizeDelta.x + 1, 5);
                orangeSelWall.transform.localScale = wallsAll[i].transform.GetComponent<RectTransform>().localScale;
            }            
        }
        else
        {
            check.SetActive(false);
            for (int i = 0; i < type; i++)
            {
                Destroy(wallsAll[i].transform.Find("orangeSelWall").gameObject);
            }            
        }       
    }
    
    public void activeCheck()
    {
        int a = 0;
        int type = EventSystem.FindObjectOfType<ManagerType>().wallsType.Length;

        for (int i = 0; i < type; i++)
        {
            GameObject wallsType = EventSystem.FindObjectOfType<ManagerType>().wallsType[i];
            if (wallsType.transform.childCount != 0)
            {
                a++;
                if (a == type)
                {
                    check.SetActive(true);
                }                   
            } 
        }

    }


    public void selectWallForMaterial()
    {
        if (gameObject.transform.childCount == 0)
        {
            RectTransform myWall = gameObject.GetComponent<RectTransform>();

            GameObject wallSelActive = Instantiate(selectWall, myWall.transform.position, myWall.transform.rotation);
            wallSelActive.name = "wallSelActive";
            wallSelActive.transform.parent = myWall;
            RectTransform orangeSelWall = wallSelActive.GetComponent<RectTransform>();

            orangeSelWall.sizeDelta = new Vector2(myWall.sizeDelta.x + 1, 5);
            orangeSelWall.localScale = new Vector3(1, 1, 1);
            //selectWall.transform.SetParent(gameObject.transform);
        }
        else
        {
            Destroy(gameObject.transform.GetChild(0).gameObject);
            //if (check.activeInHierarchy == true)
            //{
            //    check.SetActive(false);
            //}
        }
    }
}
