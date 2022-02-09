using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGrid : MonoBehaviour
{
	int gridSizeX = 40;
	int gridSizeY = 20;
	public Transform[,] WallGridArray;
	public Transform[,] BoxGridArray;
	GameObject BoxGrid;
	float CellSizeX;
	float CellSizeY;

	public bool IsGridShown;

	public Vector2 GetCellSize()
	{
		return new Vector2(CellSizeX, CellSizeY);
	}

	void CalculateCellSize()
	{
		Wall wall = GetComponent<Wall>();
		CellSizeX = wall.Length / gridSizeX;
		CellSizeY = wall.Height / gridSizeY;
	}

	public void RemoveObjectFromGrid(Transform removingObject)
	{
		for (int i = 0; i < gridSizeX; i++)
		{
			for (int j = 0; j < gridSizeY; j++)
			{
				if (WallGridArray[i, j] == removingObject)
					WallGridArray[i, j] = null;
			}
		}
		RestoreGridColors();
	}

	public void DeleteCollidingObjectAtCreatingNewOne(Transform newObject, Vector2 coordinates, Vector2Int objectSizeInCells)
	{
		Vector2Int downLeftCellIndex = GetObjectDownLeftCellIndex(coordinates, objectSizeInCells);

		for (int i = downLeftCellIndex.x; i < downLeftCellIndex.x + objectSizeInCells.x; i++)
		{
			for (int j = downLeftCellIndex.y; j < downLeftCellIndex.y + objectSizeInCells.y; j++)
			{
				if (i < gridSizeX && j < gridSizeY && i >= 0 && j >= 0)
				{
					if (WallGridArray[i, j] != null && WallGridArray[i, j] != newObject)
					{
						Transform collidingObject = WallGridArray[i, j];
						Wall wall = GetComponent<Wall>();
						if (wall != null)
						{
							Window window = collidingObject.GetComponent<Window>();
							if (window != null)
							{
								wall.Windows.Remove(window);
								Debug.Log("Window is removed from list");
							}
							else
							{
								Door door = collidingObject.GetComponent<Door>();
								wall.Doors.Remove(door);
								Debug.Log("Door is removed from list");
							}
						}
						Debug.LogWarning("Colliding object has been removed");
						RemoveObjectFromGrid(collidingObject);
						Destroy(collidingObject.gameObject);
						return;
					}
				}
			}
		}
	}

	public void MoveObject(Transform movingObject, Vector2 coordinates, Vector2Int objectSizeInCells)
	{
		Window windowScript = movingObject.GetComponent<Window>();
		if (windowScript != null)
		{
			if (windowScript.Type == DataTypes.WindowType.balcony_left_door || windowScript.Type == DataTypes.WindowType.balcony_right_door)
			{
				coordinates = new Vector2(coordinates.x, 1f);
			}
		}
		Debug.LogWarningFormat("{0} coordinates in 'MoveObject' = {1}", movingObject.name, coordinates);
		if (CheckPositionAccesibility(movingObject, coordinates, objectSizeInCells))
		{
			RemoveObjectFromGrid(movingObject);
			Vector2Int downLeftCellIndex = GetObjectDownLeftCellIndex(coordinates, objectSizeInCells);
			AddObjectToGrid(movingObject, coordinates, objectSizeInCells, downLeftCellIndex);
			movingObject.SetParent(BoxGrid.transform);
			//	movingObject.position = new Vector3(coordinates.x, coordinates.y, 0);
			//movingObject.localPosition = new Vector3(movingObject.localPosition.x, movingObject.localPosition.y, 0);
			movingObject.localPosition = new Vector3(coordinates.x, coordinates.y, 0);
			//Debug.LogErrorFormat($"{gameObject.name} local position is {movingObject.localPosition} {gameObject.transform.localPosition}");

			movingObject.SetParent(gameObject.transform);
			//Debug.LogError(string.Format($"move {movingObject.name} position = {movingObject.position}"));
			//Debug.LogError(string.Format($"move {movingObject.name} localPosition = {movingObject.position}"));
		}
		else
		{
			Debug.LogWarning(movingObject.name + " is not fit");
		}
	}

	public bool CheckPositionAccesibility(Transform chekingObject, Vector2 coordinates, Vector2Int objectSizeInCells)
	{
		Debug.LogWarning("Checking accesibility of object" + chekingObject.name);
		Debug.LogFormat($"Object size in cells = {objectSizeInCells}");
		Vector2Int centerBoxIndex = new Vector2Int(Mathf.RoundToInt(coordinates.x / CellSizeX), Mathf.RoundToInt(coordinates.y / CellSizeY));
		Debug.Log("Center box index = " + centerBoxIndex);

		if (centerBoxIndex.x - (objectSizeInCells.x / 2) < 0 || centerBoxIndex.x + (objectSizeInCells.x / 2) > WallGridArray.GetLength(0) - 1
		|| (centerBoxIndex.y + 1) - (objectSizeInCells.y / 2) < 0 || centerBoxIndex.y + (objectSizeInCells.y / 2) > WallGridArray.GetLength(1) - 1)
		{
			if (centerBoxIndex.x - (objectSizeInCells.x / 2) < 0)
			{
				Debug.LogError("Not fit #1.1");

			}
			if (centerBoxIndex.x + (objectSizeInCells.x / 2) >= WallGridArray.GetLength(0))
			{
				Debug.LogError("Not fit #1.2");

			}
			if ((centerBoxIndex.y + 1) - (objectSizeInCells.y / 2) < 0)
			{
				Debug.LogError("Not fit #1.3");

			}
			if (centerBoxIndex.y + (objectSizeInCells.y / 2) >= WallGridArray.GetLength(1))
			{
				Debug.LogErrorFormat("#1 {0}  #2 {1}", centerBoxIndex.y + (objectSizeInCells.y / 2), WallGridArray.GetLength(1));
				Debug.LogError("Not fit #1.4");

			}
			return (false);
		}

		Vector2Int downLeftCellIndex = GetObjectDownLeftCellIndex(coordinates, objectSizeInCells);
		Debug.Log("Down left cell index = " + downLeftCellIndex);
		if (chekingObject.GetComponent<Window>() != null)
		{
			if (chekingObject.GetComponent<Window>().Type == DataTypes.WindowType.double_leaf_window || chekingObject.GetComponent<Window>().Type == DataTypes.WindowType.tricuspid_window)
			{
				if (downLeftCellIndex.y < 2)
				{
					//Debug.LogError("Not fit. Window is too low");
					return (false);
				}
			}
		}
		//for (int i = downLeftCellIndex.x; i < downLeftCellIndex.x + objectSizeInCells.x; i++)
		//{
		//	for (int j = downLeftCellIndex.y; j < downLeftCellIndex.y + objectSizeInCells.y; j++)
		//	{
		//		if (i < gridSizeX && j < gridSizeY)
		//		{
		//			if (WallGridArray[i, j] != null && WallGridArray[i, j] != chekingObject)
		//			{
		//				Debug.LogError("Not fit #2");

		//				return (false);
		//			}
		//		}
		//	}
		//}
		for (int i = downLeftCellIndex.x; i < downLeftCellIndex.x + objectSizeInCells.x; i++)
		{
			for (int j = 0; j < gridSizeY; j++)
			{
				if (i < gridSizeX && j < gridSizeY)
				{
					if (i >= 0 && j >= 0)
					{
						if (WallGridArray[i, j] != null && WallGridArray[i, j] != chekingObject)
						{
							Debug.LogError("Not fit #2");

							return (false);
						}
					}
				}
			}
		}
		return (true);
	}

	public void MoveObjectOneDimention(Transform movingObject, Vector2 coordinates, Vector2Int objectSizeInCells)
	{
		Debug.LogWarningFormat("{0} coordinates in 'MoveObject' = {1}", movingObject.name, coordinates);
		if (CheckPositionAccesibilityOneDimention(movingObject, coordinates, objectSizeInCells))
		{
			RemoveObjectFromGrid(movingObject);
			Vector2Int downLeftCellIndex = GetObjectDownLeftCellIndexOneDimention(coordinates, objectSizeInCells);
			AddObjectToGrid(movingObject, coordinates, objectSizeInCells, downLeftCellIndex);
			movingObject.SetParent(BoxGrid.transform);
			//movingObject.position = new Vector3(coordinates.x, coordinates.y, 0);
			//movingObject.localPosition = new Vector3(coordinates.x, 0, 0);
			movingObject.localPosition = new Vector3(coordinates.x, coordinates.y, 0);
			Debug.LogErrorFormat($"{movingObject.name} local position is {movingObject.localPosition} {gameObject.transform.localPosition}");
			movingObject.SetParent(gameObject.transform);
			movingObject.localPosition = new Vector3(movingObject.localPosition.x, 0f, movingObject.localPosition.z);
			Debug.Log(string.Format($"move {movingObject.name} position = {movingObject.position}"));
		}
		else
		{
			Debug.LogWarning(movingObject.name + " is not fit");
		}
	}

	public bool CheckPositionAccesibilityOneDimention(Transform chekingObject, Vector2 coordinates, Vector2Int objectSizeInCells)
	{
		Debug.LogWarning("Checking accesibility");
		Debug.LogFormat($"Object size in cells = {objectSizeInCells}");
		Vector2Int centerBoxIndex = new Vector2Int(Mathf.RoundToInt(coordinates.x / CellSizeX), 0);
		Debug.Log("Center box index = " + centerBoxIndex);

		if (centerBoxIndex.x - (objectSizeInCells.x / 2) < 0 || centerBoxIndex.x + (objectSizeInCells.x / 2) > WallGridArray.GetLength(0)
		|| centerBoxIndex.y < 0 || centerBoxIndex.y + (objectSizeInCells.y) > WallGridArray.GetLength(1))
		{
			if (centerBoxIndex.x - (objectSizeInCells.x / 2) < 0)
			{
				Debug.LogError("Not fit #1.1");

			}
			if (centerBoxIndex.x + (objectSizeInCells.x / 2) > WallGridArray.GetLength(0))
			{
				Debug.LogError("Not fit #1.2");

			}
			if (centerBoxIndex.y < 0)
			{
				Debug.LogError("Not fit #1.3");

			}
			if (centerBoxIndex.y + (objectSizeInCells.y) > WallGridArray.GetLength(1))
			{
				Debug.LogErrorFormat("#1 {0}  #2 {1}", centerBoxIndex.y + (objectSizeInCells.y / 2), WallGridArray.GetLength(1));
				Debug.LogError("Not fit #1.4");

			}
			return (false);
		}
		Vector2Int downLeftCellIndex = GetObjectDownLeftCellIndexOneDimention(coordinates, objectSizeInCells);
		Debug.Log("Down left cell index = " + downLeftCellIndex);
		for (int i = downLeftCellIndex.x; i < downLeftCellIndex.x + objectSizeInCells.x; i++)
		{
			for (int j = downLeftCellIndex.y; j < downLeftCellIndex.y + objectSizeInCells.y; j++)
			{
				if (i >= 0 && j >= 0)
				{
					if (WallGridArray[i, j] != null && WallGridArray[i, j] != chekingObject)
					{
						Debug.LogError("Not fit #2");

						return (false);
					}
				}
			}
		}
		return (true);
	}

	public Vector2Int GetObjectDownLeftCellIndexOneDimention(Vector2 objectCoords, Vector2Int objectSizeInCells)
	{
		float centreCoordX = (objectCoords.x / CellSizeX);
		Vector2Int centreOfObjectCellIndex = new Vector2Int((int)Mathf.RoundToInt(centreCoordX), 0);
		Vector2Int downLeftCellIndex = new Vector2Int(centreOfObjectCellIndex.x - (objectSizeInCells.x / 2), 0);

		return (downLeftCellIndex);
	}


	private void AddObjectToGrid(Transform movingObject, Vector2 coordinates, Vector2Int objectSizeInCells, Vector2Int downLeftCellIndex)
	{
		//Vector2Int downLeftCellIndex = GetObjectDownLeftCellIndex(coordinates, objectSizeInCells);

		Debug.LogWarningFormat("Add object to grid\n array[{0}|{1}]  maxi = {2}  maxj = {3}", WallGridArray.GetLength(0), WallGridArray.GetLength(1), downLeftCellIndex.x + objectSizeInCells.x, downLeftCellIndex.y + objectSizeInCells.y);
		for (int i = downLeftCellIndex.x; i < downLeftCellIndex.x + objectSizeInCells.x; i++)
		{
			for (int j = downLeftCellIndex.y; j < downLeftCellIndex.y + objectSizeInCells.y; j++)
			{
				if (i < gridSizeX && j < gridSizeY && i >= 0 && j >= 0)
				{
					//Debug.Log("i = " + i + "j = " + j);
					WallGridArray[i, j] = movingObject;
				}
			}
		}
		DrawObjectGrid(downLeftCellIndex, objectSizeInCells);
	}

	public Vector2Int GetObjectDownLeftCellIndex(Vector2 objectCoords, Vector2Int objectSizeInCells)
	{
		float centreCoordX = (objectCoords.x / CellSizeX);
		float centreCoordY = (objectCoords.y / CellSizeY);
		Vector2Int centreOfObjectCellIndex = new Vector2Int((int)Mathf.RoundToInt(centreCoordX), (int)Mathf.RoundToInt(centreCoordY));
		Vector2Int downLeftCellIndex = new Vector2Int(centreOfObjectCellIndex.x - (objectSizeInCells.x / 2), centreOfObjectCellIndex.y - (objectSizeInCells.y / 2));
		return downLeftCellIndex;
	}


	public void DrawObjectGrid(Vector2Int objectDownLeftCellIndex, Vector2Int objectSizeInCells)
	{
		if (IsGridShown)
		{
			RestoreGridColors();
			for (int i = objectDownLeftCellIndex.x; i < objectSizeInCells.x + objectDownLeftCellIndex.x; i++)
			{
				for (int j = objectDownLeftCellIndex.y; j < objectSizeInCells.y + objectDownLeftCellIndex.y; j++)
				{
					if (i < gridSizeX && j < gridSizeY && i >= 0 && j >= 0)
					{
						BoxGridArray[i, j].GetComponent<Renderer>().material.color = Color.red * 0.5f;
					}
				}
			}
		}
	}

	public void RestoreGridColors()
	{
		if (IsGridShown)
		{
			for (int i = 0; i < gridSizeX; i++)
			{
				for (int j = 0; j < gridSizeY; j++)
				{
					if (WallGridArray[i, j] == null)
					{
						if ((i + j) % 2 != 0)
						{
							BoxGridArray[i, j].GetComponent<MeshRenderer>().material.color = Color.green * 0.3f;
						}
						else
						{
							BoxGridArray[i, j].GetComponent<Renderer>().material.color = Color.blue * 0.3f;
						}
					}
				}
			}
		}
	}

	private void CreateBoxGrid()
	{
		ShowBoxGrid();
		HideBoxGrid();
	}

	void ShowBoxGrid()
	{
		float z = 0;
		BoxGrid = new GameObject("Grid");
		BoxGridArray = new Transform[gridSizeX, gridSizeY];
		Wall wall = gameObject.GetComponent<Wall>();
		Vector3 wallDirection = wall.EndCoord - wall.StartCoord;
		float wallRotationAngle = Vector3.SignedAngle(wallDirection, Vector3.up, Vector3.forward) + 90f;
		BoxGrid.transform.SetParent(wall.transform);

		for (int i = 0; i < gridSizeX; i++)
		{
			for (int j = 0; j < gridSizeY; j++)
			{
				Vector3 position = new Vector3(i * CellSizeX, j * CellSizeY, z);

				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				Destroy(cube.GetComponent<BoxCollider>());
				cube.transform.position = position;
				cube.transform.localScale = new Vector3(CellSizeX - 0.01f, CellSizeY - 0.01f, 0.01f);
				cube.transform.SetParent(BoxGrid.transform);
				cube.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
				cube.GetComponent<Renderer>().material.color = Color.blue * 0.3f;
				BoxGridArray[i, j] = cube.transform;
				GameObject textObj = new GameObject("Text");
				textObj.AddComponent(typeof(TextMesh));
				TextMesh text = textObj.GetComponent<TextMesh>();
				text.text = (i.ToString() + "|" + j.ToString());
				text.characterSize = CellSizeX / 3f;
				text.color = Color.black;
				text.anchor = TextAnchor.MiddleCenter;
				textObj.transform.SetParent(cube.transform);
				textObj.transform.localPosition = Vector3.zero;

				if ((i + j) % 2 != 0)
				{
					cube.GetComponent<MeshRenderer>().material.color = Color.green * 0.3f;
				}
			}
		}
		Vector3 rotation = new Vector3(0f, wallRotationAngle, 0f);

		if ((Mathf.Abs(rotation.y) / 90) % 2 == 0)
		{
			if (Mathf.Abs(rotation.y) < 90 || Mathf.Abs(rotation.y) > 180)
				BoxGrid.transform.position = new Vector3(wall.StartCoord.x - (CellSizeX / 2), CellSizeY / 2, wall.StartCoord.y);
			else
				BoxGrid.transform.position = new Vector3(wall.StartCoord.x + (CellSizeX / 2), CellSizeY / 2, wall.StartCoord.y);

		}
		else
		{
			if (Mathf.Abs(rotation.y) < 90 || Mathf.Abs(rotation.y) > 180)
				BoxGrid.transform.position = new Vector3(wall.StartCoord.x, CellSizeY / 2, wall.StartCoord.y - (CellSizeX / 2));
			else
				BoxGrid.transform.position = new Vector3(wall.StartCoord.x, CellSizeY / 2, wall.StartCoord.y + (CellSizeX / 2));

		}
		//Debug.LogError(rotation);
		Quaternion newRotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y + 180f, rotation.z));
		BoxGrid.transform.rotation = newRotation;
	}

	void HideBoxGrid()
	{
		for (int i = 0; i < gridSizeX; i++)
		{
			for (int j = 0; j < gridSizeY; j++)
			{

				Destroy(BoxGridArray[i, j].gameObject);
				BoxGridArray[i, j] = null;
			}
		}

	}

	void Start()
	{
		CalculateCellSize();

		IsGridShown = false;
		previousFrameGridVisibilityStatus = IsGridShown;
		WallGridArray = new Transform[gridSizeX, gridSizeY];
		CreateBoxGrid();
	}

	bool previousFrameGridVisibilityStatus;

	void CheckGridVisibilityStatus()
	{
		if (previousFrameGridVisibilityStatus != IsGridShown)
		{
			if (IsGridShown)
			{
				ShowBoxGrid();
			}
			else
			{
				HideBoxGrid();
			}
		}
		previousFrameGridVisibilityStatus = IsGridShown;
	}

	void DrawOccupedCells()
	{
		if (IsGridShown)
		{
			RestoreGridColors();
			for (int i = 0; i < gridSizeX; i++)
			{
				for (int j = 0; j < gridSizeY; j++)
				{
					if (WallGridArray[i, j] != null)
					{
						BoxGridArray[i, j].GetComponent<MeshRenderer>().material.color = Color.red * 0.3f;
					}
				}
			}
		}
	}

	void Update()
	{
		CheckGridVisibilityStatus();
		DrawOccupedCells();
	}
}
