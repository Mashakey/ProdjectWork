using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataCacher;
using static DataTypes;

public class FilteringTypeRoom : MonoBehaviour
{
    public string selectTypeRoom;
    MyRoomsFiller myRoomsFiller;
    listRoom listRoom;

    public GameObject check;

    Color unActiveColor;
    Color ActiveColor;

    private void Awake()
    {
        myRoomsFiller = GameObject.FindObjectOfType<MyRoomsFiller>();
        listRoom = GameObject.FindObjectOfType<listRoom>();
        ColorUtility.TryParseHtmlString("#868686", out unActiveColor);
        ColorUtility.TryParseHtmlString("#FF9900", out ActiveColor);
    }

    public void FilterTypeRoom()
    {
        List<RoomData> roomDataJsons = new List<RoomData>(GlobalApplicationManager.MyRooms);
        
        for (int i = roomDataJsons.Count - 1; i >= 0 ; i--)
        {
            if (!listRoom.selectType.Contains(roomDataJsons[i].Interior.ToString()) && listRoom.selectType.Count != 0)
            {

                roomDataJsons.Remove(roomDataJsons[i]);
                continue;
            }
        }

        myRoomsFiller.CreateRoomsFields(roomDataJsons);

    }

    public void ButtonSelectType()
    {
        selectTypeRoom = gameObject.transform.parent.name.ToString();

        if (check.activeSelf == false)
        {
            check.SetActive(true);
            listRoom.selectType.Add(selectTypeRoom);
            FilterTypeRoom();
            myRoomsFiller.selectFilt += 1;
            myRoomsFiller.filters.GetComponent<Image>().color = ActiveColor;
        }
        else
        {
            check.SetActive(false);
            listRoom.selectType.Remove(selectTypeRoom);
            FilterTypeRoom();
            //myRoomsFiller.CreateRoomsFields(roomDataJsons);
            myRoomsFiller.selectFilt -= 1;

            if (myRoomsFiller.selectFilt == 0 && myRoomsFiller.twinSlider.activePrise == false)
            {
                myRoomsFiller.filters.GetComponent<Image>().color = unActiveColor;
            }
        }

    }
}
