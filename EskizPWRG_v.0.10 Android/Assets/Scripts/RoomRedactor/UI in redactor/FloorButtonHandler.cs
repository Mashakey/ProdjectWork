using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButtonHandler : MonoBehaviour
{
    public void OnFloorButtonClick()
	{
		Room room = FindObjectOfType<Room>();
		GlobalApplicationManager.AddSelectedObject(room.transform.Find("floor"));
	}
}
