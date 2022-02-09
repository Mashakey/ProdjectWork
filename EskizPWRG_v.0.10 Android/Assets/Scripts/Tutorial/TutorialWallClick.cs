using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static DataTypes;
using Newtonsoft.Json;
using System;

public class TutorialWallClick : MonoBehaviour
{
	DateTime mouseDownTime;
	Vector3 mouseDownPosition;
	public GameObject circleTap;
	public GameObject Materials;
	public bool click = false;
	public tutorialMaterialRoom tutorialMaterialRoom;
	public int indexWall;

	bool isLongClickValid = false;

    private void Start()
    {    
    }
    public void OnMouseDown()
	{
		isLongClickValid = true;

		mouseDownTime = DateTime.Now;
		mouseDownPosition = Input.mousePosition;
	}

	private void OnMouseDrag()
	{
		if (click == true)
		{
			var distance = (Input.mousePosition - mouseDownPosition).magnitude;
			if (distance > 100)
			{
				isLongClickValid = false;
			}
			float mouseDownDeltaTime = (float)(DateTime.Now - mouseDownTime).TotalMilliseconds;
			if (mouseDownDeltaTime > 1000 && isLongClickValid)
			{
				if (!CheckClickUI.IsClikedOnUI())
				{
					isLongClickValid = false;
					ClickOnWall();
					click = false;

				}
			}
		}
	}

	//public void OnMouseUp()
	//{
	//	if(click == true)
 //       {
	//		var distance = (Input.mousePosition - mouseDownPosition).magnitude;
	//		float mouseDownDeltaTime = (float)(DateTime.Now - mouseDownTime).TotalMilliseconds;
	//		if (mouseDownDeltaTime > 300 && distance < 100)
	//		{

	//			if (!CheckClickUI.IsClikedOnUI())
	//			{
	//				ClickOnWall();
	//				click = false;
	//			}

	//		}
			
	//	}
		
	//}

	public void ClickOnWall()
	{
		circleTap.SetActive(false);
		Materials.SetActive(true);
		tutorialMaterialRoom.j = indexWall;
	}
}
