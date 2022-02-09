using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public abstract class RoomWallManager : MonoBehaviour
{
	public string RoomName;

	public InteriorType Interior;

	public float RoomHeight;

	public WallConstructor[] walls;

	public string FloorMaterialId;

	public string CeilingMaterialId;

	public string BaseboardMaterialId;

	public abstract void ResetConstructor();

	public abstract void SetActiveWallIndex(int index);

	public abstract void DeselectActiveWall();

	public abstract void ChangeWallLength(int length);

	public abstract void ChangeHeight(float height);

	public abstract void AddDoor(int count);

	public abstract void AddWindow(int count);

	public abstract RoomData CreateRoomDataFromConstructor();

	public abstract void DisableConstructorRoom();

	public abstract Vector3 SetCameraPositionOnMiddle();
	//public abstract float CalculateArea();

}
