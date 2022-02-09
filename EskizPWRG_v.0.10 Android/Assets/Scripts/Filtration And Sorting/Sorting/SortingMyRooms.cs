using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;
using static DataCacher;
using UnityEngine.UI;

public class SortingMyRooms : MonoBehaviour
{
    MyRoomsFiller myRoomsFiller;

    Color orangeColor;


    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#FF9900", out orangeColor);
        myRoomsFiller = GameObject.FindObjectOfType<MyRoomsFiller>();
    }

    public void SortRoomsByCostAscending(GameObject selectButton)
    {
        List<RoomData> sortedList = new List<RoomData>(GlobalApplicationManager.MyRooms);

        if (selectButton.GetComponent<Image>().color == orangeColor)
        {       
            sortedList.Sort((x, y) => x.Cost.CompareTo(y.Cost));
            //myRoomsFiller.CreateRoomsFields(sortedList);
            StartCoroutine(myRoomsFiller.CreateRoomsFieldsCoroutine(sortedList));
        }
        else
        {
            StartCoroutine(myRoomsFiller.CreateRoomsFieldsCoroutine(sortedList));
            //myRoomsFiller.CreateRoomsFields(sortedList);
        }
    }

    public void SortRoomsByCostDescending(GameObject selectButton)
    {
        List<RoomData> sortedList = new List<RoomData>(GlobalApplicationManager.MyRooms);

        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            sortedList.Sort((x, y) => -x.Cost.CompareTo(y.Cost));
            StartCoroutine(myRoomsFiller.CreateRoomsFieldsCoroutine(sortedList));
            //myRoomsFiller.CreateRoomsFields(sortedList);
        }
        else
        {
            StartCoroutine(myRoomsFiller.CreateRoomsFieldsCoroutine(sortedList));
            //myRoomsFiller.CreateRoomsFields(sortedList);
        }
    }

    public void SortRoomInAscendingALPHABET(GameObject selectButton)
    {
        List<RoomData> sortedList = new List<RoomData>(GlobalApplicationManager.MyRooms);

        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            sortedList.Sort((x, y) => x.Name.CompareTo(y.Name));
            StartCoroutine(myRoomsFiller.CreateRoomsFieldsCoroutine(sortedList));
            //myRoomsFiller.CreateRoomsFields(sortedList);
        }
        else
        {
            StartCoroutine(myRoomsFiller.CreateRoomsFieldsCoroutine(sortedList));
            //myRoomsFiller.CreateRoomsFields(sortedList);
        }
    }

    public void SortRoomInDescendingALPHABET(GameObject selectButton)
    {
        List<RoomData> sortedList = new List<RoomData>(GlobalApplicationManager.MyRooms);

        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            sortedList.Sort((x, y) => -x.Name.CompareTo(y.Name));
            StartCoroutine(myRoomsFiller.CreateRoomsFieldsCoroutine(sortedList));
            //myRoomsFiller.CreateRoomsFields(sortedList);
        }
        else
        {
            StartCoroutine(myRoomsFiller.CreateRoomsFieldsCoroutine(sortedList));
            //myRoomsFiller.CreateRoomsFields(sortedList);
        }
    }
}
