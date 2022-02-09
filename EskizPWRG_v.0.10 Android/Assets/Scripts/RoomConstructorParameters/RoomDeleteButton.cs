using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class RoomDeleteButton : MonoBehaviour
{
    public void OnRoomDeleteButtonClick()
	{
		CreatedRoomIndex createdRoomIndex = GetComponent<CreatedRoomIndex>();
		MyRoomsFiller myRoomsFiller = GetComponentInParent<MyRoomsFiller>();
		RoomData deletingRoomDataJson = myRoomsFiller.roomDataJsons[createdRoomIndex.RoomIndex];

		DataCacher.DeleteAllRoomData(deletingRoomDataJson);

		myRoomsFiller.roomDataJsons = DataCacher.GetCachedRoomDataJsons();
		GlobalApplicationManager.MyRooms = myRoomsFiller.roomDataJsons;
		myRoomsFiller.CreateRoomsFields(myRoomsFiller.roomDataJsons);
	}
}
