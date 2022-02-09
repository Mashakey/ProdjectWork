using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSurfaceCeiling : MonoBehaviour
{
    public void OnSelectCeilingButtonClick()
	{
		Room room = FindObjectOfType<Room>();
		Transform ceilinSurface = room.transform.Find("ceiling");
		GlobalApplicationManager.AddSelectedObject(ceilinSurface);
	}
}
