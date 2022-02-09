using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class TtypeWallManager : RoomWallManager
{
	[SerializeField]
	Text RoomAreaField;

	[SerializeField]
	ParametersChangeHandler parametersChangeHandler;

	[SerializeField]
	ButtonsColorChanger buttonsColorChanger;

	[SerializeField]
	Transform ConstructorRoomPage;

	//public WallConstructor[] walls;
	public int activeWallIndex;


	private void Awake()
	{
		walls = transform.GetComponentsInChildren<WallConstructor>();
		FloorMaterialId = "DefaultFloor";
		CeilingMaterialId = "DefaultCeiling";
	}

	//private void Start()
	//{
	//	foreach (var wall in walls)
	//	{
	//		wall.buttonsColorChanger = buttonsColorChanger;
	//	}
	//	CalculateAndWriteRoomArea();
	//	RoomHeight = 2.1f;
	//	parametersChangeHandler.SetHeightFieldValue(RoomHeight * 100);
	//	SetActiveWallIndex(0);

	//}

	private void OnEnable()
	{
		foreach (var wall in walls)
		{
			wall.buttonsColorChanger = buttonsColorChanger;
		}
		ResetConstructor();
		CalculateAndWriteRoomArea();
		RoomHeight = 2.1f;
		parametersChangeHandler.SetHeightFieldValue(RoomHeight * 100);
		SetActiveWallIndex(0);
	}

	public override void ResetConstructor()
	{
		RoomHeight = 2.1f;
		foreach (var wall in walls)
		{
			wall.ResetWallConstructor();
			Transform yellowWall = wall.transform.Find("WallCom");
			if (yellowWall != null)
			{
				yellowWall.gameObject.SetActive(false);
			}
		}
		SetActiveWallIndex(0);
		FloorMaterialId = "DefaultFloor";
		CeilingMaterialId = "DefaultCeiling";
	}

	public override void SetActiveWallIndex(int index)
	{
		activeWallIndex = index;
		walls[activeWallIndex].SetDoorAndWindowButtonsStatus();
		parametersChangeHandler.SetLengthFieldValue(walls[activeWallIndex].GetLengthInCm());

		//walls[activeWallIndex].RecalculateObjectPositions();
	}

	public override void DeselectActiveWall()
	{
		activeWallIndex = -1;
	}

	public override void AddDoor(int count)
	{
		walls[activeWallIndex].AddDoor(count);
	}

	public override void AddWindow(int count)
	{
		walls[activeWallIndex].AddWindow(count);
	}

	public override void ChangeWallLength(int length)
	{
		if (activeWallIndex >= 0)
		{
			if (activeWallIndex == 0 || activeWallIndex == 2)
			{
				walls[0].ChangeLength(length);
				walls[2].ChangeLength(length);
			}
			else if (activeWallIndex == 4 || activeWallIndex == 6)
			{
				walls[4].ChangeLength(length);
				walls[6].ChangeLength(length);
			}
			else if (activeWallIndex == 1)
			{
				float proportion3wall = walls[3].GetLengthInCm() / walls[1].GetLengthInCm();
				float proportion5wall = walls[5].GetLengthInCm() / walls[1].GetLengthInCm();
				float proportion7wall = walls[7].GetLengthInCm() / walls[1].GetLengthInCm();
				float newWall3Length = length * proportion3wall;
				float newWall5Length = length * proportion5wall;
				float newWall7Length = length * proportion7wall;
				if (newWall3Length < 100f || newWall5Length < 100f || newWall7Length < 100f)
				{
					parametersChangeHandler.OnRevertLengthChange();
					return;
				}
				//Debug.LogWarningFormat($"wall[3] = {newWall3Length} wall[5] = {newWall5Length} wall[7] = {newWall7Length}");
				parametersChangeHandler.SetLengthFieldValue(length);

				walls[1].ChangeLength(length);
				walls[3].ChangeLength(newWall3Length);
				walls[5].ChangeLength(newWall5Length);
				walls[7].ChangeLength(newWall7Length);
			}
			else if (activeWallIndex == 3)
			{
				if (length > walls[1].GetLengthInCm() - 5)
				{
					float newWall3Length = walls[1].GetLengthInCm() - 5;
					float newWall5Length = (walls[1].GetLengthInCm() - newWall3Length) / 2;
					float newWall7Length = (walls[1].GetLengthInCm() - newWall3Length) / 2;
					//float newWall5Length = (walls[1].GetLengthInCm() - walls[3].GetLengthInCm()) / 2;
					//float newWall7Length = (walls[1].GetLengthInCm() - walls[3].GetLengthInCm()) / 2;
					if (newWall3Length < 100f || newWall5Length < 100f || newWall7Length < 100f)
					{
						parametersChangeHandler.OnRevertLengthChange();
						return;
					}
					parametersChangeHandler.SetLengthFieldValue(newWall3Length);

					walls[3].ChangeLength(newWall3Length);
					walls[5].ChangeLength(newWall5Length);
					walls[7].ChangeLength(newWall7Length);
				}
				else
				{
					float restOfTheWallLength = (walls[1].GetLengthInCm() - walls[3].GetLengthInCm());
					float proportion5wall = walls[5].GetLengthInCm() / restOfTheWallLength;
					float proportion7wall = walls[7].GetLengthInCm() / restOfTheWallLength;

					restOfTheWallLength = walls[1].GetLengthInCm() - length;
					float newWall5Length = restOfTheWallLength * proportion5wall;
					float newWall7Length = restOfTheWallLength * proportion7wall;
					if (newWall5Length < 100f || newWall7Length < 100f)
					{
						parametersChangeHandler.OnRevertLengthChange();
						return;
					}
					parametersChangeHandler.SetLengthFieldValue(length);

					walls[3].ChangeLength(length);
					walls[5].ChangeLength(newWall5Length);
					walls[7].ChangeLength(newWall7Length);

					//walls[3].ChangeLength(length);
					//restOfTheWallLength = (walls[1].GetLengthInCm() - walls[3].GetLengthInCm());
					//walls[5].ChangeLength(restOfTheWallLength * proportion5wall);
					//walls[7].ChangeLength(restOfTheWallLength * proportion7wall);
				}
			}
			else if (activeWallIndex == 5)
			{
				if (length > walls[1].GetLengthInCm() - 10)
				{
					float newWall5Length = walls[1].GetLengthInCm() - 10;
					float newWall3Length = (walls[1].GetLengthInCm() - newWall5Length) / 2;
					float newWall7Length = (walls[1].GetLengthInCm() - newWall5Length) / 2;
					if (newWall3Length < 100f || newWall5Length < 100f || newWall7Length < 100f)
					{
						parametersChangeHandler.OnRevertLengthChange();
						return;
					}
					parametersChangeHandler.SetLengthFieldValue(newWall5Length);

					walls[5].ChangeLength(newWall5Length);
					walls[3].ChangeLength(newWall3Length);
					walls[7].ChangeLength(newWall7Length);
				}
				else
				{
					float restOfTheWallLength = walls[1].GetLengthInCm() - walls[5].GetLengthInCm();
					float proportion3wall = walls[3].GetLengthInCm() / restOfTheWallLength;
					float proportion7wall = walls[7].GetLengthInCm() / restOfTheWallLength;

					restOfTheWallLength = walls[1].GetLengthInCm() - length;
					float newWall3Length = restOfTheWallLength * proportion3wall;
					float newWall7Length = restOfTheWallLength * proportion7wall;
					if (newWall3Length < 100f || newWall7Length < 100f)
					{
						parametersChangeHandler.OnRevertLengthChange();
						return;
					}
					parametersChangeHandler.SetLengthFieldValue(length);

					walls[5].ChangeLength(length);
					walls[3].ChangeLength(newWall3Length);
					walls[7].ChangeLength(newWall7Length);


					//walls[5].ChangeLength(length);
					//restOfTheWallLength = (walls[1].GetLengthInCm() - walls[5].GetLengthInCm());
					//walls[3].ChangeLength(restOfTheWallLength * proportion3wall);
					//walls[7].ChangeLength(restOfTheWallLength * proportion7wall);
				}
			}
			else if (activeWallIndex == 7)
			{
				if (length > walls[1].GetLengthInCm() - 5)
				{
					float newWall7Length = walls[1].GetLengthInCm() - 5;
					float newWall5Length = (walls[1].GetLengthInCm() - newWall7Length) / 2;
					float newWall3Length = (walls[1].GetLengthInCm() - newWall7Length) / 2;
					if (newWall3Length < 100f || newWall5Length < 100f || newWall7Length < 100f)
					{
						parametersChangeHandler.OnRevertLengthChange();
						return;
					}
					parametersChangeHandler.SetLengthFieldValue(newWall7Length);

					walls[7].ChangeLength(newWall7Length);
					walls[5].ChangeLength(newWall5Length);
					walls[3].ChangeLength(newWall3Length);

				}
				else
				{
					float restOfTheWallLength = walls[1].GetLengthInCm() - walls[7].GetLengthInCm();
					float proportion5wall = walls[5].GetLengthInCm() / restOfTheWallLength;
					float proportion3wall = walls[3].GetLengthInCm() / restOfTheWallLength;

					restOfTheWallLength = walls[1].GetLengthInCm() - length;
					float newWall3Length = restOfTheWallLength * proportion3wall;
					float newWall5Length = restOfTheWallLength * proportion5wall;
					if (newWall3Length < 100f || newWall5Length < 100f)
					{
						parametersChangeHandler.OnRevertLengthChange();
						return;
					}
					parametersChangeHandler.SetLengthFieldValue(length);

					walls[7].ChangeLength(length);
					walls[5].ChangeLength(newWall5Length);
					walls[3].ChangeLength(newWall3Length);

					//walls[7].ChangeLength(length);
					//restOfTheWallLength = walls[1].GetLengthInCm() - walls[7].GetLengthInCm();
					//walls[5].ChangeLength(restOfTheWallLength * proportion5wall);
					//walls[3].ChangeLength(restOfTheWallLength * proportion3wall);
				}
			}
		}
		walls[activeWallIndex].SetDoorAndWindowButtonsStatus();

		CalculateAndWriteRoomArea();
	}

	public override void ChangeHeight(float height)
	{
		RoomHeight = height / 100;
	}

	public void CalculateAndWriteRoomArea()
	{
		float areaInM2 = CalculateArea() / 10000;
		RoomAreaField.text = string.Format("S:{0} m2", areaInM2);
	}

	float CalculateArea()
	{
		float area = (walls[0].GetLengthInCm() * walls[1].GetLengthInCm()) + (walls[4].GetLengthInCm() * walls[5].GetLengthInCm());
		return (area);
	}

	public override RoomData CreateRoomDataFromConstructor()
	{
		DisableConstructorRoom();

		RoomData rectangleRoom = new RoomData();

		string tempRoomName = "";
		if (RoomName != "")
		{
			tempRoomName = RoomName;
			//rectangleRoom.Name = RoomName;
		}
		else
		{
			tempRoomName = "Без имени";
			//rectangleRoom.Name = "Без имени";
		}
		string availableRoomName = DataCacher.GetAvailableRoomName(tempRoomName);
		rectangleRoom.Name = availableRoomName;

		FloorData floorData = new FloorData();
		floorData.MaterialId = FloorMaterialId;
		rectangleRoom.Floor = floorData;

		CeilingData ceilingData = new CeilingData();
		ceilingData.MaterialId = CeilingMaterialId;
		rectangleRoom.Ceiling = ceilingData;
		if (BaseboardMaterialId != "")
		{
			BaseBoardData baseBoardData = new BaseBoardData();
			baseBoardData.MaterialId = BaseboardMaterialId;
			rectangleRoom.Baseboard = baseBoardData;
		}
		rectangleRoom.Interior = Interior;
		rectangleRoom.Type = RoomType.T_type;
		rectangleRoom.Height = RoomHeight;
		rectangleRoom.Cost = 0f;

		rectangleRoom.RoomCorners.Add(new Vector2(0, 0));
		rectangleRoom.RoomCorners.Add(new Vector2(0, walls[0].GetLengthInM()));
		rectangleRoom.RoomCorners.Add(new Vector2(walls[1].GetLengthInM(), walls[0].GetLengthInM()));
		rectangleRoom.RoomCorners.Add(new Vector2(walls[1].GetLengthInM(), 0f));
		rectangleRoom.RoomCorners.Add(new Vector2(walls[7].GetLengthInM() + walls[5].GetLengthInM(), 0f));
		rectangleRoom.RoomCorners.Add(new Vector2(walls[7].GetLengthInM() + walls[5].GetLengthInM(), -walls[4].GetLengthInM()));
		rectangleRoom.RoomCorners.Add(new Vector2(walls[7].GetLengthInM(), -walls[4].GetLengthInM()));
		rectangleRoom.RoomCorners.Add(new Vector2(walls[7].GetLengthInM(), 0));



		for (int i = 0; i < rectangleRoom.RoomCorners.Count; i++)
		{
			int firstCornerIndex = i;
			int secondCornerIndex = i + 1;
			if (secondCornerIndex >= rectangleRoom.RoomCorners.Count)
			{
				secondCornerIndex = 0;
			}
			WallData wallData = new WallData(rectangleRoom.RoomCorners[firstCornerIndex], rectangleRoom.RoomCorners[secondCornerIndex]);
			wallData.MaterialId = walls[i].materialId;

			AddWindowsAndDoorsDataToWallData(wallData, walls[i]);
			rectangleRoom.Walls.Add(wallData);
		}
		rectangleRoom.CameraPosition = SetCameraPositionOnMiddle();

		return (rectangleRoom);
	}

	void AddWindowsAndDoorsDataToWallData(WallData wallData, WallConstructor wallConstructor)
	{
		if (wallConstructor.WindowsAndDoors.Count > 0)
		{
			Vector3 middleWallCoord = new Vector3((wallData.startCoord.x + wallData.endCoord.x) / 2, RoomHeight / 2, (wallData.startCoord.y + wallData.endCoord.y) / 2);

			if (wallConstructor.WindowsAndDoors.Count == 1)
			{
				if (wallConstructor.WindowsAndDoors[0].GetComponent<ConstructorWindowObject>() != null)
				{
					CreateWindowData(wallData, middleWallCoord);
				}
				else
				{
					CreateDoorData(wallData, middleWallCoord);
				}
			}
			else
			{
				Vector3 middleOfFirstMiddleCoord = new Vector3((wallData.startCoord.x + middleWallCoord.x) / 2, RoomHeight / 2, (wallData.startCoord.y + middleWallCoord.z) / 2);
				Vector3 middleOfSecondMiddleCoord = new Vector3((middleWallCoord.x + wallData.endCoord.x) / 2, RoomHeight / 2, (middleWallCoord.z + wallData.endCoord.y) / 2);
				Debug.LogErrorFormat($"Middle = {middleWallCoord.x}");
				Debug.LogErrorFormat($"startCoord = {wallData.startCoord} endCoord = {wallData.endCoord} firstMiddle = {middleOfFirstMiddleCoord} secondMidle = {middleOfSecondMiddleCoord}");
				if (wallConstructor.WindowsAndDoors[0].GetComponent<ConstructorWindowObject>() != null)
				{
					CreateWindowData(wallData, middleOfFirstMiddleCoord);
				}
				else
				{
					CreateDoorData(wallData, middleOfFirstMiddleCoord);
				}
				if (wallConstructor.WindowsAndDoors[1].GetComponent<ConstructorWindowObject>() != null)
				{
					CreateWindowData(wallData, middleOfSecondMiddleCoord);
				}
				else
				{
					CreateDoorData(wallData, middleOfSecondMiddleCoord);
				}

			}
		}
	}

	WindowData CreateWindowData(WallData wallData, Vector3 windowPosition)
	{
		WindowData window = new WindowData();
		window.Position = windowPosition;
		Vector3 wallDirection = wallData.endCoord - wallData.startCoord;
		float windowRotationAngle = Vector3.SignedAngle(wallDirection, Vector3.up, Vector3.forward) + 90f;
		window.Rotation = Quaternion.AngleAxis(windowRotationAngle, Vector3.up);
		//window.Type = WindowType.tricuspid_window;
		window.Type = WindowType.double_leaf_window;
		Debug.LogError("Window position = " + window.Position);
		wallData.Windows.Add(window);

		return (window);
	}

	DoorData CreateDoorData(WallData wallData, Vector3 doorPosition)
	{
		DoorData door = new DoorData();
		door.Position = new Vector3(doorPosition.x, 0f, doorPosition.z);
		Vector3 wallDirection = wallData.endCoord - wallData.startCoord;
		float doorRotationAngle = Vector3.SignedAngle(wallDirection, Vector3.up, Vector3.forward) + 90f;
		door.Rotation = Quaternion.AngleAxis(doorRotationAngle, Vector3.up);
		door.MaterialId = "DefaultDoor";
		wallData.Doors.Add(door);

		return (door);
	}

	public override void DisableConstructorRoom()
	{
		ConstructorRoomPage.gameObject.SetActive(false);
	}

	public override Vector3 SetCameraPositionOnMiddle()
	{
		Vector3 middleRoomPosition = new Vector3(walls[1].GetLengthInM() / 2, 1.3f, walls[0].GetLengthInM() / 2);
		//Camera.main.transform.position = middleRoomPosition;
		return (middleRoomPosition);

	}
}
