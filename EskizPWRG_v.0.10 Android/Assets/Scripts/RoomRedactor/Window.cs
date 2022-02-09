using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static DataTypes;

public class Window : MonoBehaviour
{
    public Vector3 Position;
    public Quaternion Rotation;
	public WindowType Type;
	//public string Type;
	public float Scale = 1f;
	public float Width;
	public float Height;
	DateTime mouseDownTime;


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
						ClickOnWindow();
					}
				}
			}
		}
	}

	void ClickOnWindow()
	{
		Buttons3DWindow buttons3D = transform.GetComponentInChildren<Buttons3DWindow>();
		if (buttons3D != null)
		{
			var mover2D = transform.GetComponentInParent<WallGridMoverTwoAxis>();
			mover2D.Deactivate();
			Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
			DeleteButtons(buttons3D.gameObject);
		}
		else
		{
			CreateButtons();
		}
	}

	void CreateButtons()
	{
		Room room = FindObjectOfType<Room>();
		room.DeleteOppened3DButtons();
		PrefabContainer prefabContainer = GameObject.Find("PrefabContainer").GetComponent<PrefabContainer>();
		GameObject Buttons3D = null;
		if (Type == WindowType.double_leaf_window || Type == WindowType.tricuspid_window)
		{
			Buttons3D = Instantiate(prefabContainer.Buttons3DWindow, transform);
		}
		else
		{
			Buttons3D = Instantiate(prefabContainer.Buttons3DBalconyWindow, transform);
		}
		Buttons3D.transform.Rotate(Vector3.up, 180);
		Buttons3D.transform.localPosition = new Vector3(0f, -0.85f, 0.2f);
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

	public float GetDistanceLeftBorder(float width)
	{
		Wall parentWall = transform.parent.GetComponent<Wall>();
		Vector2 leftBorder = new Vector2(parentWall.StartCoord.x, parentWall.StartCoord.y);
		Vector2 currentWindowPosition = new Vector2(transform.position.x, transform.position.z);
		Debug.LogError("Distance = " + (Vector2.Distance(leftBorder, currentWindowPosition) - ((width / 2) * Scale)));
		return (Vector2.Distance(leftBorder, currentWindowPosition) - ((width / 2) * Scale));
	}

	public float GetDistanceRightBorder(float width)
	{
		Wall parentWall = transform.parent.GetComponent<Wall>();
		Vector2 rightBorder = new Vector2(parentWall.EndCoord.x, parentWall.EndCoord.y);
		Vector2 currentWindowPosition = new Vector2(transform.position.x, transform.position.z);
		Debug.LogError("Distance = " + (Vector2.Distance(rightBorder, currentWindowPosition) - ((width / 2) * Scale)));
		return (Vector2.Distance(rightBorder, currentWindowPosition) - ((width / 2) * Scale));
	}

	Vector2Int CalculateObjectSizeInCells()
	{
		Vector2Int objectSizeInCells = Vector2Int.zero;
		Vector3 objectSizeInUnits = Vector3.zero;

		Collider collider = GetComponent<Collider>();
		Vector3 minPoint = transform.TransformPoint(collider.bounds.min);
		Vector3 maxPoint = transform.TransformPoint(collider.bounds.max);

		objectSizeInUnits = maxPoint - minPoint;
		WallGrid wallGrid = GetComponentInParent<WallGrid>();
		float objectSizeInCellsX = Mathf.Abs(objectSizeInUnits.x / wallGrid.GetCellSize().x);
		float objectSizeInCellsY = Mathf.Abs(objectSizeInUnits.y / wallGrid.GetCellSize().y);
		objectSizeInCells = new Vector2Int((int)Math.Ceiling(objectSizeInCellsX), (int)Math.Ceiling(objectSizeInCellsY));
		if (objectSizeInCells.x % 2 == 0)
			objectSizeInCells.x++;
		if (objectSizeInCells.y % 2 == 0)
			objectSizeInCells.y++;
		return (objectSizeInCells);
		//Debug.LogError(objectSizeInCells);
	}

	Vector2 RoundPositionToCellSize(Vector2 position)
	{
		Vector2 roundedPosition = Vector2.zero;
		WallGrid grid = GetComponentInParent<WallGrid>();

		roundedPosition.x = position.x / grid.GetCellSize().x;
		roundedPosition.x = Mathf.Round(position.x);
		roundedPosition.x = position.x * grid.GetCellSize().x;
		roundedPosition.y = position.y / grid.GetCellSize().y;
		roundedPosition.y = Mathf.Round(position.y);
		roundedPosition.y = position.y * grid.GetCellSize().y;
		Debug.LogError(position);
		return (position);
	}

	void SetObjectToGrid()
	{
		Vector2Int objectSizeInCells = CalculateObjectSizeInCells();
		Vector2 celledCoordinates = RoundPositionToCellSize(transform.localPosition);
		Debug.LogError("local position = " + transform.localPosition);
		WallGrid wallGrid = transform.GetComponentInParent<WallGrid>();
		wallGrid.MoveObject(gameObject.transform, celledCoordinates, objectSizeInCells);
	}

	private void Start()
	{
		//SetObjectToGrid();
		if (GetComponent<WallGridMoverTwoAxis>() == null)
		{
			gameObject.AddComponent<WallGridMoverTwoAxis>();
		}
		gameObject.AddComponent<WindowTouchScaler>();
		GetDistances();

	}

	public Vector4 GetDistances()
	{
		
		Vector4 result = new Vector4();
		if (Type == WindowType.double_leaf_window)
		{
			Width = 1.29f;
			Height = 1.33f;
		}
		else if (Type == WindowType.tricuspid_window)
		{
			Width = 1.96f;
			Height = 1.33f;
		}
		else if (Type == WindowType.balcony_left_door || Type == WindowType.balcony_right_door)
		{
			Width = 2.12f;
			Height = 2.19f;
		}
		else
		{
			Debug.LogError("Unknown window type");
		}
		return result;
	}



	private void Update()
	{
		//GetDistanceLeftBorder();
	}

}
