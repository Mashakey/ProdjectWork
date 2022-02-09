using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class RoomDischarger : MonoBehaviour
{
    public static RoomData emptyRoom = null;

    public static void DischargeRoom()
	{
		RoomCreator roomCreator = FindObjectOfType<RoomCreator>();
		roomCreator.CreateRoom(emptyRoom);
	}

	public static void CreateEmptyRoom(RoomData roomData)
	{
		RoomData emptyRoom = new RoomData();
		emptyRoom.Name = roomData.Name;
		emptyRoom.Type = roomData.Type;
		emptyRoom.Interior = roomData.Interior;
		emptyRoom.Height = roomData.Height;
		emptyRoom.Area = roomData.Area;
		emptyRoom.Cost = roomData.Cost;
		emptyRoom.RoomCorners = roomData.RoomCorners;

		for (int i = 0; i < roomData.Walls.Count; i++)
		{
			WallData wallData = new WallData(roomData.Walls[i].startCoord, roomData.Walls[i].endCoord);
			wallData.MaterialId = "DefaultWall";
			wallData.Windows = roomData.Walls[i].Windows;
			foreach(var door in roomData.Walls[i].Doors)
			{
				DoorData doorData = new DoorData();
				doorData.MaterialId = "DefaultDoor";
				doorData.Position = door.Position;
				doorData.Rotation = door.Rotation;
				wallData.Doors.Add(doorData);
			}
			emptyRoom.Walls.Add(wallData);
		}
		emptyRoom.Baseboard = null;
		if (roomData.Floor != null)
		{
			FloorData floorData = new FloorData();
			floorData.MaterialId = "DefaultFloor";
			emptyRoom.Floor = floorData;
		}
		if (roomData.Ceiling != null)
		{
			CeilingData ceilingData = new CeilingData();
			ceilingData.MaterialId = "Default";
			emptyRoom.Ceiling = ceilingData;
		}
		emptyRoom.CameraPosition = roomData.CameraPosition;
		RoomDischarger.emptyRoom = emptyRoom;
	}
}
