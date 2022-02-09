using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackRoomParam : MonoBehaviour
{
    public GameObject roomParameters;
    public GameObject selectTypeRoom;
    public GameObject Proceed;
    public managerMaterilBack managerMaterilBack;
    public GameObject selectedWall;
    public GameObject floor;
    public GameObject selectObject;
    public Sprite unActivBut;


    private void Awake()
    {
        managerMaterilBack = GameObject.FindObjectOfType<managerMaterilBack>();
    }

    public void BackFromRoomParameters()
    {
        int index = managerMaterilBack.indexForBack;

        if (index == 5)
        {
            selectObject.SetActive(true);
            roomParameters.SetActive(false);
        }
        else
        {
            selectTypeRoom.SetActive(true);
            string typeRoom = Proceed.GetComponent<typeRoom>().typeMyRoom;
            GameObject.Find("ActiveFrameType").transform.position = new Vector2(10000f, 1000f);
            if (roomParameters.name == "RoomParameters")
            {
                roomParameters.transform.Find("PanelSafeAreaRoomTypes").transform.Find(typeRoom).gameObject.SetActive(false);

            }
            Proceed.GetComponent<SwitchEdit>().selType = null;
            Proceed.GetComponent<Image>().sprite = unActivBut;
            Proceed.GetComponent<Button>().interactable = false;

            roomParameters.SetActive(false);
        }
    }
    public void BackFromSelectTyperoom()
    {
        selectTypeRoom.SetActive(true);
        string typeRoom = Proceed.GetComponent<typeRoom>().typeMyRoom;
        GameObject.Find("ActiveFrameType").transform.position = new Vector3(10000f, 1000f,0);
        //Debug.LogError(GameObject.Find("ActiveFrame").name);
        if (roomParameters.name == "RoomParameters")
        {
            roomParameters.transform.Find("PanelSafeAreaRoomTypes").transform.Find(typeRoom).gameObject.SetActive(false);

        }
        Proceed.GetComponent<SwitchEdit>().selType = null;
        Proceed.GetComponent<Image>().sprite = unActivBut;
        Proceed.GetComponent<Button>().interactable = false;
        roomParameters.SetActive(false);
    }

    public void BackMaterialsForWall()
    {
        int index = managerMaterilBack.indexForBack;

        if (index == 1)
        {
            selectedWall.GetComponent<Canvas>().enabled = true;
            selectedWall.GetComponent<CanvasScaler>().enabled = true;
        }
        else if(index == 2)
        {
            floor.GetComponent<Canvas>().enabled = true;
            floor.GetComponent<CanvasScaler>().enabled = true;
        }
        else if (index == 3)
        {
            selectObject.SetActive(true);
        }
    }

    public void BackInWall()
    {
        int index = managerMaterilBack.indexForBack;

        if (index == 4 || index == 1)
        {
            roomParameters.SetActive(true);
        }
    }
}
