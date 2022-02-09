using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class typeRoom : MonoBehaviour
{
    public string typeMyRoom;
    private GameObject selType;

    public void clickButt()
    {
        typeMyRoom = EventSystem.FindObjectOfType<ManagerType>().typeMyRoom;
        selType = GameObject.Find("PanelSafeAreaRoomTypes").transform.Find(typeMyRoom).gameObject;
        selType.SetActive(true);    
    }
}

