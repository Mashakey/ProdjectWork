using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3DDelete : MonoBehaviour
{
    public void OnMouseDown()
	{
		AddHistoryAction();

		WallGrid wallGrid = GetComponentInParent<WallGrid>();
        Transform parentObject = transform.parent.parent.parent;
        Debug.Log("Parent object of buttons = " + parentObject.name);
        wallGrid.RemoveObjectFromGrid(parentObject);

        Wall wall = parentObject.GetComponentInParent<Wall>();
        if (wall != null)
		{
            Window window = parentObject.GetComponent<Window>();
            if (window != null)
			{
                wall.Windows.Remove(window);
                Debug.Log("Window is removed from list");
            }
            else
			{
                Door door = parentObject.GetComponent<Door>();
                wall.Doors.Remove(door);
                Debug.Log("Door is removed from list");
            }
        }
        else
		{
            Debug.LogError("Wall script is not found");
		}
		//Destroy(parentObject.gameObject);
		Destroy(transform.parent.parent.gameObject);
		parentObject.gameObject.SetActive(false);
		Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
	}

	void AddHistoryAction()
	{
		Transform parentObject = transform.parent.parent.parent;
		WallGridMoverTwoAxis wallGridMoverTwoAxis = parentObject.GetComponent<WallGridMoverTwoAxis>();
		WallGridMoverOneAxis wallGridMoverOneAxis = parentObject.GetComponent<WallGridMoverOneAxis>();
		if (wallGridMoverTwoAxis != null)
		{
			wallGridMoverTwoAxis.AddHistoryActionCreate();
		}
		else if (wallGridMoverOneAxis != null)
		{
			wallGridMoverOneAxis.AddHistoryActionCreate();
		}
		else
		{
			Debug.LogError("Grid movers are not found");
		}
	}
}
