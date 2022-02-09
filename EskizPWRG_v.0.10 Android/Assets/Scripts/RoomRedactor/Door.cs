using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static DataTypes;

public class Door : MonoBehaviour
{
    public Vector3 Position;
    public Quaternion Rotation;
    public string MaterialId;
	DateTime mouseDownTime;

	private void Start()
	{
		gameObject.AddComponent<WallGridMoverOneAxis>();
		gameObject.AddComponent<TextureUpdater>();
	}

	public void OnMouseDown()
	{

		mouseDownTime = DateTime.Now;
		//Debug.LogError("Mouse down at" + DateTime.Now);

	}

	public void OnMouseUp()
	{
		//Debug.LogError("Mouse up at" + DateTime.Now);
		float mouseDownDeltaTime = (float)(DateTime.Now - mouseDownTime).TotalMilliseconds;
		//Debug.LogError("Delta time = " + mouseDownDeltaTime);
		if (mouseDownDeltaTime < 100)
		{
			if (!CheckClickUI.IsClikedOnUI() && Screen.orientation == ScreenOrientation.Portrait)
			{
				Room room = FindObjectOfType<Room>();
				if (room != null)
				{
					if (room.oppened3DButtons == null)
					{
						ClickOnDoor();
					}
				}
			}
		}
	}

	void ClickOnDoor()
	{
		Buttons3DDoor buttons3D = transform.GetComponentInChildren<Buttons3DDoor>();
		if (buttons3D != null)
		{
			var mover1D = transform.GetComponentInParent<WallGridMoverOneAxis>();
			mover1D.Deactivate();
			Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();

			DeleteButtons(buttons3D.gameObject);
		}
		else
		{
			CreateButtons();
		}
		//gameObject.transform.parent.gameObject.AddComponent(typeof(WallGrid));
		//gameObject.AddComponent(typeof(WallGridMoverTwoAxis));
	}

	void CreateButtons()
	{
		Room room = FindObjectOfType<Room>();
		room.DeleteOppened3DButtons();
		PrefabContainer prefabContainer = GameObject.Find("PrefabContainer").GetComponent<PrefabContainer>();
		GameObject Buttons3D = Instantiate(prefabContainer.Buttons3DDoor, transform);
		//Buttons3D.transform.Rotate(Vector3.up, 180);
		Buttons3D.transform.localPosition = new Vector3(-0.6f, 1f, 0.05f);
		room.oppened3DButtons = Buttons3D;
	}

	void DeleteButtons(GameObject buttons3D)
	{
		Destroy(buttons3D);
		Room room = FindObjectOfType<Room>();
		room.oppened3DButtons = null;
	}

	public void UpdatePosition(Vector3 newPosition)
    {
        Position = newPosition;
        gameObject.transform.position = newPosition;
    }

    public void UpdateRotation(Quaternion newRotation)
    {
        gameObject.transform.rotation = newRotation;
    }

	public void UpdateMaterial(Material updateMaterial)
	{
		Debug.Log("We are in door. Trying to update material. Material is '" + updateMaterial.name + "'");
		if (updateMaterial == null)
			return;
		MaterialId = updateMaterial.name;
		Transform doorModel = transform.Find("DoorSurface");
		doorModel.GetComponent<MeshRenderer>().material = updateMaterial;
		doorModel.GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(1f, 1f);
		//UpdateTextureScale();
	}

}
