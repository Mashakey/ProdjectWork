using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static DataTypes;
using Newtonsoft.Json;
using System;


public class ClickOnSurfaceHandler : MonoBehaviour
{
	DateTime mouseDownTime;
	Vector3 mouseDownPosition;
	bool isLongClickValid;

	public void OnMouseDown()
	{
		if (FindObjectOfType<Room>().oppened3DButtons == null)
		{
			isLongClickValid = true;
		}
		else
		{
			isLongClickValid = false;
		}
		mouseDownTime = DateTime.Now;
		Debug.Log("Mouse down at" + DateTime.Now);
		mouseDownPosition = Input.mousePosition;


	}

	public void OnMouseDrag()
	{
		var distance = (Input.mousePosition - mouseDownPosition).magnitude;
		if (distance > 100)
		{
			isLongClickValid = false;
		}
		float mouseDownDeltaTime = (float)(DateTime.Now - mouseDownTime).TotalMilliseconds;
		if (mouseDownDeltaTime > 1000 && isLongClickValid)
		{
			if (!CheckClickUI.IsClikedOnUI() && Screen.orientation == ScreenOrientation.Portrait)
			{
				isLongClickValid = false;
				ClickOnSurface();
			}
		}
	}

	public void OnMouseUp()
	{
		var distance = (Input.mousePosition - mouseDownPosition).magnitude;
		Debug.LogError("Distance mouse = " + distance);
		float mouseDownDeltaTime = (float)(DateTime.Now - mouseDownTime).TotalMilliseconds;
		//if (mouseDownDeltaTime > 300 && distance < 100)
		//{

		//	if (!CheckClickUI.IsClikedOnUI() && Screen.orientation == ScreenOrientation.Portrait)
		//	{
		//		ClickOnSurface();
		//	}
		//}
		if (!CheckClickUI.IsClikedOnUI())
		{
			Room room = FindObjectOfType<Room>();
			room.DeleteOppened3DButtons();
			NotePage notePage = FindObjectOfType<NotePage>();
			if (notePage)
			{
				notePage.CloseNoteWindow();
			}
		}
	}

	private bool CheckTachUI()
	{
		foreach (Touch touch in Input.touches)
		{
			int id = touch.fingerId;
			if (EventSystem.current.IsPointerOverGameObject(id))
			{
				// ui touched
				return true;
			}
		}
		return false;
	}

	public void ClickOnSurface()
	{
		Debug.Log("Click on surface '" + name + "'");
		//GlobalApplicationManager.AddSelectedWall(gameObject.transform);
		GlobalApplicationManager.AddSelectedObject(gameObject.transform);
		DrumScroll drumScroll = GameObject.FindObjectOfType<DrumScroll>();
		if (drumScroll != null)
		{
			ActiveWindowKeeper.IsRedactorActive = false;
			Debug.Log("Drum scroll is found");
			if (GetComponent<Wall>() != null)
			{
				OnWallMaterialsHandler onWallMaterialsHandler = FindObjectOfType<OnWallMaterialsHandler>();
				onWallMaterialsHandler.transform.GetComponent<Canvas>().enabled = true;
				onWallMaterialsHandler.transform.GetComponent<CanvasScaler>().enabled = true;
				ScrollbarBackButton scrollbarBackButton = FindObjectOfType<ScrollbarBackButton>();
				scrollbarBackButton.SetPrevousPageAsEditor();
				WallBackButton wallBackButton = FindObjectOfType<WallBackButton>();
				wallBackButton.SetPrevousPageAsEditor();
			}
			else if (GetComponent<Floor>() != null)
			{
				if (name == "ceiling")
				{
					OnCeilingMaterialsHandler onCeilingMaterialsHandler = FindObjectOfType<OnCeilingMaterialsHandler>();
					onCeilingMaterialsHandler.transform.GetComponent<Canvas>().enabled = true;
					onCeilingMaterialsHandler.transform.GetComponent<CanvasScaler>().enabled = true;
					CeilingBackButton ceilingBackButton = FindObjectOfType<CeilingBackButton>();
					ceilingBackButton.SetPrevousPageAsEditor();
				}
				else
				{
					OnFloorMaterialsHandler onFloorMaterialsHandler = FindObjectOfType<OnFloorMaterialsHandler>();
					onFloorMaterialsHandler.transform.GetComponent<Canvas>().enabled = true;
					onFloorMaterialsHandler.transform.GetComponent<CanvasScaler>().enabled = true;
					FloorBackButton floorBackButton = FindObjectOfType<FloorBackButton>();
					floorBackButton.SetPrevousPageAsEditor();
				}
				
			}
		}
		else
		{
			Debug.LogError("Drum scroll is not found");
		}
	}
}
