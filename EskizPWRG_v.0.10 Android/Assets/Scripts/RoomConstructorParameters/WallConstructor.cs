using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallConstructor : MonoBehaviour
{
	[SerializeField]
	ButtonLogic buttonLogic;

	public ButtonsColorChanger buttonsColorChanger;

	public int WallIndex;
	public Text LengthText;
	public Text VisibleLengthText;
	public bool isActive = false;

	public string materialId = "DefaultWall";

	public List<Transform> WindowsAndDoors = new List<Transform>();
	int objectsOnWallCount = 0;
	// Start is called before the first frame update

	private void Awake()
	{
		LengthText = transform.GetComponentInChildren<Text>();
	}

	private void OnEnable()
	{
		LengthText = transform.GetComponentInChildren<Text>();

	}

	public void ResetWallConstructor()
	{
		materialId = "DefaultWall";
		DestroyDoors();
		DestroyWindows();
		SetDoorAndWindowButtonsStatus(); //#####################################################################################
	}

	public void ChangeLength(float length)
	{
		//Debug.LogErrorFormat($"Rounding {length} to whole = {Math.Round(length)}");
		VisibleLengthText.text = (Math.Round(length)).ToString() + " см";
		//LengthText.text = (Math.Round(length)).ToString() + " cm";
		LengthText.text = length.ToString() + " см";
		//Debug.LogWarningFormat($"Visible length = {VisibleLengthText.text} actual length = {LengthText.text}");
		DeleteObjectsIfNotEnoughSpace();
	}

	public float GetLengthInCm()
	{

		return (float.Parse(LengthText.text.Split(' ')[0]));

	}

	public float GetLengthInM()
	{
		return (float.Parse(LengthText.text.Split(' ')[0]) / 100);
	}

	public void AddDoor(int count)
	{

		DestroyDoors();
		RecalculateObjectPositions();
		for (int i = 0; i < count; i++)
		{
			PrefabContainer prefabContainer = GameObject.FindObjectOfType<PrefabContainer>();
			GameObject door = Instantiate(prefabContainer.constructorDoor, transform);
			WindowsAndDoors.Add(door.transform);
			door.transform.localPosition = Vector3.zero;
			door.transform.rotation = transform.rotation;
			//Debug.LogError("Door created");
			RecalculateObjectPositions();
		}
		SetDoorAndWindowButtonsStatus(); //#####################################################################################
	}

	void DestroyDoors()
	{
		for (int i = WindowsAndDoors.Count - 1; i >= 0; i--)
		{
			if (WindowsAndDoors[i].GetComponent<ConstructorDoorObject>() != null)
			{
				Destroy(WindowsAndDoors[i].gameObject);
				WindowsAndDoors.RemoveAt(i);
			}
		}
	}

	public void AddWindow(int count)
	{
		Debug.LogWarning("Adding windows");
		//Debug.LogErrorFormat($"In wallConstructor. activewall = {WallIndex}  windowsCount = {count}");
		DestroyWindows();
		RecalculateObjectPositions();

		for (int i = 0; i < count; i++)
		{
			PrefabContainer prefabContainer = GameObject.FindObjectOfType<PrefabContainer>();
			GameObject window = Instantiate(prefabContainer.constructorWindow, transform);
			WindowsAndDoors.Add(window.transform);
			//window.transform.SetParent(transform);
			window.transform.localPosition = Vector3.zero;
			window.transform.rotation = transform.rotation;
			RecalculateObjectPositions();
		}
		SetDoorAndWindowButtonsStatus(); //#####################################################################################
	}

	void DestroyWindows()
	{
		for (int i = WindowsAndDoors.Count - 1; i >= 0; i--)
		{
			if (WindowsAndDoors[i].GetComponent<ConstructorWindowObject>() != null)
			{
				Destroy(WindowsAndDoors[i].gameObject);
				WindowsAndDoors.RemoveAt(i);
			}
		}
	}

	public void RecalculateObjectPositions()
	{
		RectTransform wallRectTransform = GetComponent<RectTransform>();
		float wallConstructorLength = wallRectTransform.sizeDelta.x;
		int objectsCount = WindowsAndDoors.Count;
		float positionStepValue = wallConstructorLength / (objectsCount * 2);
		for (int i = 0; i < objectsCount; i++)
		{
			Vector3 objectPositonOnWall = new Vector3(positionStepValue * ((i * 2) + 1), 0f, 0f);
			WindowsAndDoors[i].GetComponent<RectTransform>().anchoredPosition = objectPositonOnWall;
		}
	}


	float windowHeight = 133f;
	float windowWidth = 129f;
	float windowAdditionalReservedPlace = 30f;

	float doorHeight = 210f;
	float doorWidth = 80f;
	float doorAdditionalReservedPlace = 25f;

	public void SetDoorAndWindowButtonsStatus()
	{
		buttonsColorChanger.OnDoorZeroButtonClick();
		buttonsColorChanger.OnWindowZeroButtonClick();
		//Debug.LogWarning("Setting status on wall[" + WallIndex + "]");
		if (WindowsAndDoors.Count == 2)
		{
			if (GetWindowsAttachedToWallCount() == 2)
			{
				//Debug.LogWarning("#1");
				buttonLogic.SetWindowsButtonZeroAndOne();
				buttonsColorChanger.OnWindowTwoButtonClick();
				//buttonLogic.SetWindowsButtonZero();
			}
			else if (GetDoorsAttachedToWallCount() == 2)
			{
				//Debug.LogWarning("#2");
				buttonLogic.SetDoorsButtonZeroAndOne();
				buttonsColorChanger.OnDoorTwoButtonClick();
				//buttonLogic.SetDoorsButtonZero();
			}
			else
			{
				//Debug.LogWarning("#3");
				buttonsColorChanger.OnDoorOneButtonClick();
				buttonsColorChanger.OnWindowOneButtonClick();
				buttonLogic.SetButtonsZero();
			}
		}
		else if (WindowsAndDoors.Count == 1)
		{
			if (GetWindowsAttachedToWallCount() == 1)
			{
				//Debug.LogWarning("#4");

				buttonLogic.SetWindowsButtonOne();
				buttonsColorChanger.OnWindowOneButtonClick();
			}
			else
			{
				//Debug.LogWarning("#5");

				buttonLogic.SetDoorsButtonOne();
				buttonsColorChanger.OnDoorOneButtonClick();

			}
		}
		else
		{
			//Debug.LogWarning("#6");

			buttonLogic.SetAllButtonsActive();
			buttonsColorChanger.OnDoorZeroButtonClick();
			buttonsColorChanger.OnWindowZeroButtonClick();
		}
		CheckObjectsSizeAvailability();
	}

	//public void CheckObjectsSizeAvailability()
	//{
	//	float emptySpace = 0;
	//	if (WindowsAndDoors.Count > 0)
	//	{
	//		float occupedSpace = 0;
	//		foreach (var windowOrDoor in WindowsAndDoors)
	//		{
	//			if (windowOrDoor.GetComponent<ConstructorWindowObject>() != null)
	//			{
	//				occupedSpace += windowWidth + 50f;
	//			}
	//			else
	//			{
	//				occupedSpace += doorWidth + 50f;
	//			}
	//		}
	//		emptySpace = GetLengthInCm() - occupedSpace;
	//	}
	//	else
	//	{
	//		emptySpace = GetLengthInCm();
	//	}
	//	//emptySpace = GetLengthInCm();


	//	if (emptySpace < (windowWidth + 50f) * 2)
	//	{
	//		//Debug.LogWarning("#7");
	//		if (GetWindowsAttachedToWallCount() <= 0)
	//		{
	//			buttonLogic.SetTwoWindowButtonInactive();
	//		}
	//		else if (emptySpace < windowWidth + 50f)
	//		{
	//			buttonLogic.SetTwoWindowButtonInactive();
	//		}
	//	}
	//	if (emptySpace < windowWidth + 50f)
	//	{
	//		//Debug.LogWarning("#8");
	//		if (GetWindowsAttachedToWallCount() < 2)
	//		{
	//			buttonLogic.SetOneWindowButtonInactive();
	//		}
	//		buttonLogic.SetTwoWindowButtonInactive();

	//	}
	//	if (emptySpace < (doorWidth + 50f) * 2)
	//	{
	//		//Debug.LogWarning("#9");
	//		if (GetDoorsAttachedToWallCount() <= 0)
	//		{
	//			buttonLogic.SetTwoDoorButtonInactive();
	//		}
	//		else if (emptySpace < doorWidth + 50f)
	//		{
	//			buttonLogic.SetTwoDoorButtonInactive();

	//		}
	//	}
	//	if (emptySpace < doorWidth + 50f)
	//	{
	//		//Debug.LogWarning("#10");
	//		if (GetDoorsAttachedToWallCount() < 2)
	//		{
	//			buttonLogic.SetOneDoorButtonInactive();
	//		}
	//		buttonLogic.SetTwoDoorButtonInactive();

	//	}
	//}
	
	public void CheckObjectsSizeAvailability()
	{
		float emptySpace = 0;
		if (WindowsAndDoors.Count > 0)
		{
			float occupedSpace = 0;
			foreach (var windowOrDoor in WindowsAndDoors)
			{
				if (windowOrDoor.GetComponent<ConstructorWindowObject>() != null)
				{
					occupedSpace += windowWidth + windowAdditionalReservedPlace;
				}
				else
				{
					occupedSpace += doorWidth + doorAdditionalReservedPlace;
				}
			}
			emptySpace = GetLengthInCm() - occupedSpace;
		}
		else
		{
			emptySpace = GetLengthInCm();
		}
		//emptySpace = GetLengthInCm();


		if (emptySpace < (windowWidth + windowAdditionalReservedPlace) * 2)
		{
			//Debug.LogWarning("#7");
			if (GetWindowsAttachedToWallCount() <= 0)
			{
				buttonLogic.SetTwoWindowButtonInactive();
			}
			else if (emptySpace < windowWidth + windowAdditionalReservedPlace)
			{
				buttonLogic.SetTwoWindowButtonInactive();
			}
		}
		if (emptySpace < windowWidth + windowAdditionalReservedPlace)
		{
			//Debug.LogWarning("#8");
			if (GetWindowsAttachedToWallCount() < 2)
			{
				buttonLogic.SetOneWindowButtonInactive();
			}
			buttonLogic.SetTwoWindowButtonInactive();

		}
		if (emptySpace < (doorWidth + doorAdditionalReservedPlace) * 2)
		{
			//Debug.LogWarning("#9");
			if (GetDoorsAttachedToWallCount() <= 0)
			{
				buttonLogic.SetTwoDoorButtonInactive();
			}
			else if (emptySpace < doorWidth + doorAdditionalReservedPlace)
			{
				buttonLogic.SetTwoDoorButtonInactive();

			}
		}
		if (emptySpace < doorWidth + doorAdditionalReservedPlace)
		{
			//Debug.LogWarning("#10");
			if (GetDoorsAttachedToWallCount() < 2)
			{
				buttonLogic.SetOneDoorButtonInactive();
			}
			buttonLogic.SetTwoDoorButtonInactive();

		}
	}

	public void DeleteObjectsIfNotEnoughSpace()
	{
		for (int i = WindowsAndDoors.Count - 1; i >= 0; i--)
		{
			if (!IsSpaceForObjectsEnough())
			{
				Destroy(WindowsAndDoors[i].gameObject);
				WindowsAndDoors.RemoveAt(i);
			}
		}
		SetDoorAndWindowButtonsStatus();
	}

	//bool IsSpaceForObjectsEnough()
	//{
	//	float wallLength = GetLengthInCm();
	//	float occupedSpace = 0f;
	//	foreach (var windowOrDoor in WindowsAndDoors)
	//	{
	//		if (windowOrDoor.GetComponent<ConstructorWindowObject>() != null)
	//		{
	//			occupedSpace += windowWidth + 50f;
	//		}
	//		else
	//		{
	//			occupedSpace += doorWidth + 50f;
	//		}
	//	}
	//	return (occupedSpace <= wallLength);
	//}
	
	bool IsSpaceForObjectsEnough()
	{
		float wallLength = GetLengthInCm();
		float occupedSpace = 0f;
		foreach (var windowOrDoor in WindowsAndDoors)
		{
			if (windowOrDoor.GetComponent<ConstructorWindowObject>() != null)
			{
				occupedSpace += windowWidth + 50f;
			}
			else
			{
				occupedSpace += doorWidth + 50f;
			}
		}
		return (occupedSpace <= wallLength);
	}

	int GetWindowsAttachedToWallCount()
	{
		int windowsCount = 0;
		foreach (var windowOrDoor in WindowsAndDoors)
		{
			if (windowOrDoor.GetComponent<ConstructorWindowObject>() != null)
			{
				windowsCount++;
			}
		}
		return (windowsCount);
	}

	int GetDoorsAttachedToWallCount()
	{
		int doorsCount = 0;
		foreach (var windowOrDoor in WindowsAndDoors)
		{
			if (windowOrDoor.GetComponent<ConstructorDoorObject>() != null)
			{
				doorsCount++;
			}
		}
		return (doorsCount);
	}
}