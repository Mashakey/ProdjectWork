using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class TrapezoidalWallManager : RoomWallManager
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
			if (activeWallIndex == 0)
			{
				float proportion3wall = walls[3].GetLengthInCm() / walls[0].GetLengthInCm();

				float newWall3Length = length * proportion3wall;
				float newWall2Length = calculateHypotenuse(length, walls[1].GetLengthInCm(), newWall3Length, walls[4].GetLengthInCm());
				if (newWall3Length < 100f || newWall2Length < 100f)
				{
					parametersChangeHandler.SetPreviuosLengthValueInInputField();
					return;
				}
				parametersChangeHandler.SetLengthFieldValue(length);

				walls[0].ChangeLength(length);
				walls[3].ChangeLength(newWall3Length);
				walls[2].ChangeLength(newWall2Length);

			}
			else if (activeWallIndex == 1)
			{
				float newWall1Length;
				if (length > walls[4].GetLengthInCm() - 5)
				{
					newWall1Length = walls[4].GetLengthInCm() - 5;
					//walls[1].ChangeLength(walls[4].GetLengthInCm() - 5);
				}
				else
				{
					newWall1Length = length;
					//walls[1].ChangeLength(length);
				}
				float newWall2Length = calculateHypotenuse(walls[0].GetLengthInCm(), newWall1Length, walls[3].GetLengthInCm(), walls[4].GetLengthInCm());
				if (newWall1Length < 100f || newWall2Length < 100f)
				{
					parametersChangeHandler.SetPreviuosLengthValueInInputField();
					return;
				}
				parametersChangeHandler.SetLengthFieldValue(newWall1Length);

				walls[1].ChangeLength(newWall1Length);
				walls[2].ChangeLength(newWall2Length);

			}
			else if (activeWallIndex == 2)
			{
				if (Mathf.Pow(length, 2) < (Mathf.Pow(walls[0].GetLengthInCm(), 2) + Mathf.Pow(walls[4].GetLengthInCm(), 2)))
				{
					//Debug.LogError("LENGTH = " + length);

					float lengthA = walls[0].GetLengthInCm() - walls[3].GetLengthInCm();
					float lengthB = walls[4].GetLengthInCm() - walls[1].GetLengthInCm();
					float lengthC = walls[2].GetLengthInCm();
					//Debug.LogError(string.Format($"length1_A = {walls[1].GetLengthInCm()}  length_B = {walls[3].GetLengthInCm()}"));
					//Debug.LogError(string.Format($"lengthA = {lengthA}  lengthB = {lengthB}  LengthC = {lengthC}"));
					float sinAC = lengthB / lengthC;
					float cosAC = lengthA / lengthC;
					//Debug.LogError(string.Format($"sinAC = {sinAC}  cosAC = {cosAC}"));

					float deltaLength = (length - lengthC) / 2;
					float deltaA = deltaLength * cosAC * 2;
					float deltaB = deltaLength * sinAC * 2;
					//Debug.LogError(string.Format($"deltaLength = {deltaLength}  deltaA = {deltaA}   deltaB = {deltaB}"));

					float newWall1Length = walls[1].GetLengthInCm() - deltaB;
					float newWall3Length = walls[3].GetLengthInCm() - deltaA;
					if (newWall1Length < 100f || newWall3Length < 100f)
					{
						parametersChangeHandler.SetPreviuosLengthValueInInputField();
						return;
					}
					parametersChangeHandler.SetLengthFieldValue(length);

					walls[2].ChangeLength(length);
					walls[1].ChangeLength(newWall1Length);
					walls[3].ChangeLength(newWall3Length);
					//Debug.LogError(string.Format($"wall[2] = {walls[2].GetLengthInCm()}  walls[1] = {walls[1].GetLengthInCm()}   walls[3] = {walls[3].GetLengthInCm()}"));

				}
			}
			else if (activeWallIndex == 3)
			{
				float newWall3Length;
				if (length > walls[0].GetLengthInCm() - 5)
				{
					newWall3Length = walls[0].GetLengthInCm() - 5;
					//walls[3].ChangeLength(walls[0].GetLengthInCm() - 5);
				}
				else
				{
					newWall3Length = length;
					//walls[3].ChangeLength(length);
				}
				float newWall2Length = calculateHypotenuse(walls[0].GetLengthInCm(), walls[1].GetLengthInCm(), newWall3Length, walls[4].GetLengthInCm());
				if (newWall2Length < 100f || newWall3Length < 100f)
				{
					parametersChangeHandler.SetPreviuosLengthValueInInputField();
					return;
				}
				parametersChangeHandler.SetLengthFieldValue(newWall3Length);

				walls[3].ChangeLength(newWall3Length);
				walls[2].ChangeLength(newWall2Length);

			}
			else if (activeWallIndex == 4)
			{
				float proportion1wall = walls[1].GetLengthInCm() / walls[4].GetLengthInCm();

				float newWall1Length = length * proportion1wall;
				float newWall2Length = calculateHypotenuse(walls[0].GetLengthInCm(), newWall1Length, walls[3].GetLengthInCm(), length);
				if (newWall1Length < 100f || newWall2Length < 100f)
				{
					parametersChangeHandler.SetPreviuosLengthValueInInputField();
					return;
				}
				parametersChangeHandler.SetLengthFieldValue(length);

				walls[4].ChangeLength(length);
				walls[1].ChangeLength(newWall1Length);
				walls[2].ChangeLength(newWall2Length);

			}

		}
		walls[activeWallIndex].SetDoorAndWindowButtonsStatus();

		CalculateAndWriteRoomArea();
	}

	public override void ChangeHeight(float height)
	{
		RoomHeight = height / 100;
	}

	float calculateHypotenuse(float wall0Length, float wall1Length, float wall3Length, float wall4Length)
	{
		float cathetusA = Mathf.Pow(wall0Length - wall3Length, 2);
		float cathetusB = Mathf.Pow(wall4Length - wall1Length, 2);
		float hypotenuseLength = Mathf.Sqrt(cathetusA + cathetusB);
		return (hypotenuseLength);
	}

	public void CalculateAndWriteRoomArea()
	{
		float areaInM2 = CalculateArea() / 10000;
		RoomAreaField.text = string.Format("S:{0} m2", areaInM2);
	}

	float CalculateArea()
	{
		float area = walls[0].GetLengthInCm() * walls[4].GetLengthInCm() - ((walls[0].GetLengthInCm() - walls[3].GetLengthInCm())
			* (walls[4].GetLengthInCm() - walls[1].GetLengthInCm()) * 0.5f);
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
		rectangleRoom.Type = RoomType.Trapezoidal;
		rectangleRoom.Height = RoomHeight;
		rectangleRoom.Cost = 0f;

		rectangleRoom.RoomCorners.Add(new Vector2(0, 0));
		rectangleRoom.RoomCorners.Add(new Vector2(0, walls[0].GetLengthInM()));
		rectangleRoom.RoomCorners.Add(new Vector2(walls[1].GetLengthInM(), walls[0].GetLengthInM()));
		rectangleRoom.RoomCorners.Add(new Vector2(walls[4].GetLengthInM(), walls[3].GetLengthInM()));
		rectangleRoom.RoomCorners.Add(new Vector2(walls[4].GetLengthInM(), 0));



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
		Vector3 middleRoomPosition = new Vector3(walls[4].GetLengthInM() / 2, 1.3f, walls[0].GetLengthInM() / 2);
		//Camera.main.transform.position = middleRoomPosition;
		return (middleRoomPosition);

	}
}
