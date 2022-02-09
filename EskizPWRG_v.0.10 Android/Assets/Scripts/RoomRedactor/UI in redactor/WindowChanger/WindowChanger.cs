using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class WindowChanger : MonoBehaviour
{
	[SerializeField]
	Button doubleLeafButton;
	[SerializeField]
	Button tricuspidWindowButton;
	[SerializeField]
	Button balconyRigthButton;
	[SerializeField]
	Button balconyLeftButton;
	[SerializeField]
	Button proceedButton;
	[SerializeField]
	Sprite activeButton;
	[SerializeField]
	Sprite inactiveButton;

	[SerializeField]
	GameObject ActiveFrame;

	WindowType activeWindowType;
	Vector3 changingWindowPosition;

	Window changingWindowScript = null;
	
	public void OpenChangeWindowPage(Window changingWindow)
	{
		GetComponent<Canvas>().enabled = true;
		GetComponent<CanvasScaler>().enabled = true;
		changingWindowScript = changingWindow;
		//Vector3 windowPositionOnWall = changingWindow.GetComponent<WallGridMoverTwoAxis>().GetWindowPositionOnWall();
		//float wallLength = changingWindow.GetComponentInParent<Wall>().Length;
		//Debug.LogErrorFormat($"Window local pos = {windowPositionOnWall}");
		Room room = FindObjectOfType<Room>();
		SetWindowsButtonsInteractibleStatus();
		DisableProceedButton();
		ActiveFrame.transform.position = new Vector3(-2000, -2000, 0);

	}

	public void SetWindowsButtonsInteractibleStatus()
	{
		SetAllButtonsInteractible();
		Vector3 windowPositionOnWall = changingWindowScript.GetComponent<WallGridMoverTwoAxis>().GetWindowPositionOnWall();
		float wallLength = changingWindowScript.GetComponentInParent<Wall>().Length;
		if (windowPositionOnWall.x < 0.7f || wallLength - windowPositionOnWall.x < 0.7f)
		{
			Debug.LogWarning("Not enough space for doubleLeaf window");
			doubleLeafButton.interactable = false;
		}
		if (windowPositionOnWall.x < 1.1f || wallLength - windowPositionOnWall.x < 1.1f)
		{
			Debug.LogWarning("Not enough space for tricuspid window");

			tricuspidWindowButton.interactable = false;
		}
		if (windowPositionOnWall.x < 1.2f || wallLength - windowPositionOnWall.x < 1.2f)
		{
			Debug.LogWarning("Not enough space for balcony door window");

			balconyLeftButton.interactable = false;
			balconyRigthButton.interactable = false;
		}
		if (FindObjectOfType<Room>().Height < 2.4f)
		{
			Debug.LogWarning("Not enough height for balcony door window");

			balconyLeftButton.interactable = false;
			balconyRigthButton.interactable = false;
		}
	}

	public void SetAllButtonsInteractible()
	{
		doubleLeafButton.interactable = true;
		tricuspidWindowButton.interactable = true;
		balconyLeftButton.interactable = true;
		balconyRigthButton.interactable = true;
	}

    public void OnBalconyLeftDoorClick()
	{
		EnableProceedButton();
		Vector3 position = changingWindowScript.transform.position;
		position.y = 1.1f;
		changingWindowPosition = position;
		activeWindowType = WindowType.balcony_left_door;

		//ChangeWindow(WindowType.balcony_left_door, position);

		//Quaternion rotation = changingWindowScript.transform.rotation;
		//DeleteWindow();
		//changingWindowScript.GetComponentInParent<Wall>().CreateWindow(DataTypes.WindowType.balcony_left_door, position, rotation);
		//GetComponent<Canvas>().enabled = false;
		//GetComponent<CanvasScaler>().enabled = false;
	}

	public void OnBalconyRightDoorClick()
	{
		EnableProceedButton();

		Vector3 position = changingWindowScript.transform.position;
		position.y = 1.1f;
		changingWindowPosition = position;
		activeWindowType = WindowType.balcony_right_door;

		//ChangeWindow(WindowType.balcony_right_door, position);

		//Quaternion rotation = changingWindowScript.transform.rotation;
		//DeleteWindow();
		//changingWindowScript.GetComponentInParent<Wall>().CreateWindow(DataTypes.WindowType.balcony_right_door, position, rotation);
		//GetComponent<Canvas>().enabled = false;
		//GetComponent<CanvasScaler>().enabled = false;
	}

	public void OnDoubleLeafWindowClick()
	{
		EnableProceedButton();

		Vector3 position = changingWindowScript.transform.position;
		changingWindowPosition = position;
		activeWindowType = WindowType.double_leaf_window;

		//ChangeWindow(WindowType.double_leaf_window, position);


		//Quaternion rotation = changingWindowScript.transform.rotation;
		//DeleteWindow();
		//changingWindowScript.GetComponentInParent<Wall>().CreateWindow(DataTypes.WindowType.double_leaf_window, position, rotation);
		//GetComponent<Canvas>().enabled = false;
		//GetComponent<CanvasScaler>().enabled = false;

	}

	public void OnTricuspidWindowClick()
	{
		EnableProceedButton();

		Vector3 position = changingWindowScript.transform.position;
		changingWindowPosition = position;
		activeWindowType = WindowType.tricuspid_window;

		//ChangeWindow(WindowType.tricuspid_window, position);

		//Quaternion rotation = changingWindowScript.transform.rotation;
		//DeleteWindow();
		//changingWindowScript.GetComponentInParent<Wall>().CreateWindow(DataTypes.WindowType.tricuspid_window, position, rotation);
		//GetComponent<Canvas>().enabled = false;
		//GetComponent<CanvasScaler>().enabled = false;
	}

	public void OnProceedButtonClick()
	{
		ChangeWindow(activeWindowType, changingWindowPosition);
	}

	public void ChangeWindow(WindowType windowType, Vector3 position)
	{
		Quaternion rotation = changingWindowScript.transform.rotation;
		DeleteWindow();
		Transform newWindow = changingWindowScript.GetComponentInParent<Wall>().CreateWindow(windowType, position, rotation);
		GetComponent<Canvas>().enabled = false;
		GetComponent<CanvasScaler>().enabled = false;
		CreateChangeWindowHistoryAction(newWindow, changingWindowScript.transform);
	}

	public void EnableWindow(Window enablingWindow)
	{
		enablingWindow.gameObject.SetActive(true);
		enablingWindow.GetComponent<WallGrid>();
	}

	public void CreateChangeWindowHistoryAction(Transform newWindow, Transform oldWindow)
	{
		HistoryAction historyAction = new HistoryAction();
		historyAction.CreateChangeWindowHistoryAction(newWindow, oldWindow);
		FindObjectOfType<HistoryChangesStack>().AddHistoryAction(historyAction);

	}

	public void DeleteWindow()
	{
		WallGrid wallGrid = changingWindowScript.GetComponentInParent<WallGrid>();
		Transform parentObject = changingWindowScript.transform;
		Debug.LogError("Parent object of buttons = " + parentObject.name);
		wallGrid.RemoveObjectFromGrid(parentObject);
		Wall wall = changingWindowScript.GetComponentInParent<Wall>();
		wall.Windows.Remove(changingWindowScript);
		changingWindowScript.gameObject.SetActive(false);
		Debug.LogError("Window is removed from list");
	}

	public void DeleteWindow(Window deletingWindowScript)
	{
		WallGrid wallGrid = deletingWindowScript.GetComponentInParent<WallGrid>();
		Transform parentObject = deletingWindowScript.transform;
		Debug.LogError("Parent object of buttons = " + parentObject.name);
		wallGrid.RemoveObjectFromGrid(parentObject);
		Wall wall = deletingWindowScript.GetComponentInParent<Wall>();
		wall.Windows.Remove(deletingWindowScript);
		deletingWindowScript.gameObject.SetActive(false);
		Debug.LogError("Window is removed from list");
	}

	public void EnableProceedButton()
	{
		proceedButton.GetComponent<Image>().sprite = activeButton;
		proceedButton.interactable = true;
	}

	public void DisableProceedButton()
	{
		proceedButton.GetComponent<Image>().sprite = inactiveButton;
		proceedButton.interactable = false;
	}



}
