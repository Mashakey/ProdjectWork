using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class HistoryAction
{
    HistoryActionType actionType;

	UpdateMeshMaterialDelegate updateMeshMaterialDelegate;
	MoveObjectOnWallDelegate moveObjectOnWallDelegate;


	Material updateMaterial;

	string oldMaterialId;
	string newMaterialId;

	Vector2 celledCoordinates;
	Vector2Int objectSizeInCells;

	Transform onActionObject;
	Transform CreatedObject;
	Transform DeletedObject;
	GameObject testObject;
	Vector3 objectPosition;
	Quaternion objectRotation;
	float scale;

	public void CreateCreateBaseboardHistoryAction(string materialId)
	{
		actionType = HistoryActionType.CreateBaseboard;
		this.oldMaterialId = materialId;

	}

	public void CreateChangeBaseboardHistoryAction(string materialId)
	{
		actionType = HistoryActionType.ChangeBaseboard;
		this.oldMaterialId = materialId;

	}

	public void CreateDeleteBaseboardHistoryAction()
	{
		actionType = HistoryActionType.DeleteBaseboard;

	}

	public void CreateChangeMaterialHistoryAction(UpdateMeshMaterialDelegate updateMeshMaterialDelegate, string oldMaterialId, string newMaterialId)
	{
		actionType = HistoryActionType.ChangeSurfaceTexture;
		this.oldMaterialId = oldMaterialId;
		this.newMaterialId = newMaterialId;
		this.updateMeshMaterialDelegate = updateMeshMaterialDelegate;
	}

	public void CreateMoveObjectOnWallHistoryAction(MoveObjectOnWallDelegate moveObjectOnWallDelegate, Transform onActionObject, Vector2 celledCoordinates, Vector2Int objectSizeInCells)
	{
		actionType = HistoryActionType.MoveObjectOnWall;
		this.moveObjectOnWallDelegate = moveObjectOnWallDelegate;
		this.onActionObject = onActionObject;
		testObject = onActionObject.gameObject;
		this.celledCoordinates = celledCoordinates;
		this.objectSizeInCells = objectSizeInCells;
	}


	public void CreateAddObjectOnWallHistoryAction(MoveObjectOnWallDelegate moveObjectOnWallDelegate, Transform onActionObject, Vector2 celledCoordinates, Vector2Int objectSizeInCells)
	{
		Debug.LogError("Create object on wall history action created");
		actionType = HistoryActionType.CreateObject;
		this.moveObjectOnWallDelegate = moveObjectOnWallDelegate;
		this.onActionObject = onActionObject;
		this.celledCoordinates = celledCoordinates;
		this.objectSizeInCells = objectSizeInCells;
	}

	public void CreateDeleteObjectOnWallHistoryAction(MoveObjectOnWallDelegate moveObjectOnWallDelegate, Transform onActionObject, Vector2 celledCoordinates, Vector2Int objectSizeInCells)
	{
		actionType = HistoryActionType.DeleteObject;
		this.moveObjectOnWallDelegate = moveObjectOnWallDelegate;
		this.onActionObject = onActionObject;
		this.celledCoordinates = celledCoordinates;
		this.objectSizeInCells = objectSizeInCells;
	}

	public void CreateOnObjectHistoryAction(HistoryActionType actionType, Transform onActionObject, Vector3 position, Quaternion rotation)
	{
		this.actionType = actionType;
		this.onActionObject = onActionObject;
		objectPosition = position;
		objectRotation = rotation;
	}

	public void CreateWindowScaleHistoryAction(Transform onActionObject, float scale)
	{
		this.actionType = HistoryActionType.ScaleWindow;
		this.onActionObject = onActionObject;
		this.scale = scale;
	}

	public void CreateChangeWindowHistoryAction(Transform createdObject, Transform deletedObject)
	{
		this.actionType = HistoryActionType.ChangeWindowType;
		CreatedObject = createdObject;
		DeletedObject = deletedObject;
	}

	public void CreateInstantiateFurnitureObjectHistoryAction(Transform createdObject)
	{
		actionType = HistoryActionType.CreateFurnitureObject;
		onActionObject = createdObject;
	}

	public void CreateDeleteFurnitureObjectHistoryAction(Transform deletingObject)
	{
		actionType = HistoryActionType.DeleteFurnitureObject;
		onActionObject = deletingObject;
	}

	public void CreateMoveFurnitureObjectHistoryAction(Transform movingObject, Vector3 objectPosition)
	{
		actionType = HistoryActionType.MoveFurnitureObject;
		onActionObject = movingObject;
		this.objectPosition = objectPosition;
	}

	public void CreateRotateFurnitureObjectHistoryAction(Transform rotatingObject, Quaternion objectRotation)
	{
		actionType = HistoryActionType.RotateFurnitureObject;
		onActionObject = rotatingObject;
		this.objectRotation = objectRotation;
	}

	public void InvokeHistoryAction()
	{
		if (actionType == HistoryActionType.ChangeSurfaceTexture)
		{
			//CreateAndAddRedoAction();

			UpdateMaterial();
		}
		else if (actionType == HistoryActionType.CreateObject)
		{
			//CreateAndAddRedoAction();

			CreateObject();
		}
		else if (actionType == HistoryActionType.DeleteObject)
		{
			//CreateAndAddRedoAction();

			DeleteObject();
		}
		else if (actionType == HistoryActionType.MoveObjectOnWall)
		{
			//CreateAndAddRedoAction();

			MoveObjectOnWall();
		}
		else if (actionType == HistoryActionType.CreateBaseboard)
		{
			CreateBaseboard();
		}
		else if (actionType == HistoryActionType.ChangeBaseboard)
		{
			ChangeBaseboard();
		}
		else if (actionType == HistoryActionType.DeleteBaseboard)
		{
			DeleteBaseboard();
		}	
		else if (actionType == HistoryActionType.ScaleWindow)
		{
			ScaleWindow();
		}
		else if (actionType == HistoryActionType.ChangeWindowType)
		{
			ChangeWindowType();
		}
		else if (actionType == HistoryActionType.CreateFurnitureObject)
		{
			CreateFurnitureObject();
		}
		else if (actionType == HistoryActionType.DeleteFurnitureObject)
		{
			DeleteFurnitureObject();
		}
		else if (actionType == HistoryActionType.MoveFurnitureObject)
		{
			MoveFurnitureObject();
		}
		else if (actionType == HistoryActionType.RotateFurnitureObject)
		{
			RotateFurnitureObject();
		}
		else
		{
			Debug.LogWarning("Unknown historyAction type");
			return;
		}
	}

	public void CreateAndAddRedoAction()
	{
		if (actionType == HistoryActionType.ChangeSurfaceTexture)
		{
			HistoryAction redoAction = new HistoryAction();
			redoAction.CreateChangeMaterialHistoryAction(updateMeshMaterialDelegate, newMaterialId, oldMaterialId);
			GameObject.FindObjectOfType<HistoryChangesStack>().AddRedoAction(redoAction);
		}
		else if (actionType == HistoryActionType.CreateObject)
		{
			HistoryAction redoAction = new HistoryAction();
			redoAction.CreateDeleteObjectOnWallHistoryAction(moveObjectOnWallDelegate, onActionObject, celledCoordinates, objectSizeInCells);
			GameObject.FindObjectOfType<HistoryChangesStack>().AddRedoAction(redoAction);
		}
		else if (actionType == HistoryActionType.DeleteObject)
		{
			HistoryAction redoAction = new HistoryAction();
			redoAction.CreateAddObjectOnWallHistoryAction(moveObjectOnWallDelegate, onActionObject, celledCoordinates, objectSizeInCells);
			GameObject.FindObjectOfType<HistoryChangesStack>().AddRedoAction(redoAction);
		}
		else if (actionType == HistoryActionType.MoveObjectOnWall)
		{
			HistoryAction redoAction = new HistoryAction();
			Vector2 celledCoordinates = Vector2.zero;
			WallGridMoverOneAxis wallGridMoverOneAxis;
			if ((wallGridMoverOneAxis = onActionObject.GetComponent<WallGridMoverOneAxis>()) != null)
			{
				celledCoordinates = wallGridMoverOneAxis.RoundPositionToCellSize(wallGridMoverOneAxis.GetObjectPositionOnWall());
			}
			WallGridMoverTwoAxis wallGridMoverTwoAxis;
			if ((wallGridMoverTwoAxis = onActionObject.GetComponent<WallGridMoverTwoAxis>()) != null)
			{
				celledCoordinates = wallGridMoverTwoAxis.RoundPositionToCellSize(wallGridMoverTwoAxis.GetWindowPositionOnWall());
			}
			redoAction.CreateMoveObjectOnWallHistoryAction(moveObjectOnWallDelegate, onActionObject, celledCoordinates, objectSizeInCells);
			GameObject.FindObjectOfType<HistoryChangesStack>().AddRedoAction(redoAction);
		}
		else if (actionType == HistoryActionType.CreateBaseboard)
		{
			//CreateBaseboard();
		}
		else if (actionType == HistoryActionType.ChangeBaseboard)
		{
			//ChangeBaseboard();
		}
		else if (actionType == HistoryActionType.DeleteBaseboard)
		{
			//DeleteBaseboard();
		}
		else if (actionType == HistoryActionType.ScaleWindow)
		{
			HistoryAction redoAction = new HistoryAction();
			redoAction.CreateWindowScaleHistoryAction(onActionObject, onActionObject.GetComponent<Window>().Scale);
			GameObject.FindObjectOfType<HistoryChangesStack>().AddRedoAction(redoAction);
			//ScaleWindow();
		}
		else if (actionType == HistoryActionType.ChangeWindowType)
		{
			HistoryAction redoAction = new HistoryAction();
			redoAction.CreateChangeWindowHistoryAction(DeletedObject, CreatedObject);
			GameObject.FindObjectOfType<HistoryChangesStack>().AddRedoAction(redoAction);
		}
		else if (actionType == HistoryActionType.CreateFurnitureObject)
		{
			HistoryAction redoAction = new HistoryAction();
			redoAction.CreateDeleteFurnitureObjectHistoryAction(onActionObject);
			GameObject.FindObjectOfType<HistoryChangesStack>().AddRedoAction(redoAction);
		}
		else if (actionType == HistoryActionType.DeleteFurnitureObject)
		{
			HistoryAction redoAction = new HistoryAction();
			redoAction.CreateInstantiateFurnitureObjectHistoryAction(onActionObject);
			GameObject.FindObjectOfType<HistoryChangesStack>().AddRedoAction(redoAction);
		}
		else if (actionType == HistoryActionType.MoveFurnitureObject)
		{
			HistoryAction redoAction = new HistoryAction();
			redoAction.CreateMoveFurnitureObjectHistoryAction(onActionObject, onActionObject.position);
			GameObject.FindObjectOfType<HistoryChangesStack>().AddRedoAction(redoAction);
		}
		else if (actionType == HistoryActionType.RotateFurnitureObject)
		{
			HistoryAction redoAction = new HistoryAction();
			redoAction.CreateRotateFurnitureObjectHistoryAction(onActionObject, onActionObject.rotation);
			GameObject.FindObjectOfType<HistoryChangesStack>().AddRedoAction(redoAction);

		}
	}

	public void UpdateMaterial()
	{
		Debug.LogWarning("On invoke UpdateMaterial()");

		ActionsInvoker.UpdateMaterial(updateMeshMaterialDelegate, oldMaterialId);
		
	}

	public void CreateObject()
	{
		Debug.LogWarning("On invoke CreateObject()");
		ActionsInvoker.CreateObject(moveObjectOnWallDelegate, onActionObject, celledCoordinates, objectSizeInCells);
	}

	public void DeleteObject()
	{
		Debug.LogWarning("On invoke DeleteObject()");
		ActionsInvoker.DeleteObjectOnWall(onActionObject);
	}

	public void MoveObjectOnWall()
	{
		Debug.LogWarning("On invoke MoveObject()");
		Debug.LogWarning("Object is " + onActionObject.name + " coords = " + celledCoordinates);
		moveObjectOnWallDelegate(onActionObject, celledCoordinates, objectSizeInCells);
	}

	public void CreateBaseboard()
	{
		Debug.LogWarning("On invoke CreateBaseboard()");
		ActionsInvoker.CreateBaseboard(oldMaterialId);
	}

	public void ChangeBaseboard()
	{
		Debug.LogWarning("On invoke ChangeBaseboard()");

		ActionsInvoker.ChangeBaseboard(oldMaterialId);
	}

	public void DeleteBaseboard()
	{
		Debug.LogWarning("On invoke DeleteBaseboard");

		ActionsInvoker.DeleteBaseboard();
	}

	public void ScaleWindow()
	{
		Debug.LogWarning("On invoke ScaleWindow");

		ActionsInvoker.ScaleWindow(onActionObject, scale);
	}

	public void ChangeWindowType()
	{
		Debug.LogWarning("On invoke ChangeWindowType");

		ActionsInvoker.ChangeWindowType(CreatedObject, DeletedObject);
	}

	public void CreateFurnitureObject()
	{
		Debug.LogWarning("On invoke create furniture object");
		ActionsInvoker.CreateFurnitureObject(onActionObject);
	}

	public void DeleteFurnitureObject()
	{
		Debug.LogWarning("On invoke delete furniture object");
		ActionsInvoker.DeleteFurnitureObject(onActionObject);

	}

	public void MoveFurnitureObject()
	{
		Debug.LogWarning("On invoke move furniture object");
		ActionsInvoker.MoveFurnitureObject(onActionObject, objectPosition);

	}

	public void RotateFurnitureObject()
	{
		Debug.LogWarning("On invoke rotate furniture object");
		ActionsInvoker.RotateFurnitureObject(onActionObject, objectRotation);

	}

	public class ActionsInvoker : MonoBehaviour
	{ 
		public static void CreateObject(MoveObjectOnWallDelegate moveObjectOnWallDelegate, Transform onActionObject, Vector2 celledCoordinates, Vector2Int objectSizeInCells)
		{
			Room room = FindObjectOfType<Room>();
			if (room != null)
			{
				room.DeleteOppened3DButtons();
			}
			onActionObject.gameObject.SetActive(true);
			moveObjectOnWallDelegate(onActionObject.transform, celledCoordinates, objectSizeInCells);
			Wall wall = onActionObject.GetComponentInParent<Wall>();
			if (onActionObject.GetComponent<Window>() != null)
			{
				wall.Windows.Add(onActionObject.GetComponent<Window>());
			}
			else if (onActionObject.GetComponent<Door>() != null)
			{
				wall.Doors.Add(onActionObject.GetComponent<Door>());
			}
			else
			{
				Debug.LogError("Can't find Window or Door script");
			}
		}

		public static void DeleteObjectOnWall(Transform onActionObject)
		{
			Room room = FindObjectOfType<Room>();
			if (room != null)
			{
				room.DeleteOppened3DButtons();
			}
			WallGrid wallGrid = onActionObject.GetComponentInParent<WallGrid>();
			Transform parentObject = onActionObject;
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
			//Destroy(onActionObject.transform.parent.parent.gameObject);
			parentObject.gameObject.SetActive(false);
			Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
		}

		public static void UpdateMaterial(UpdateMeshMaterialDelegate updateMaterialDelegate, string materialId)
		{
			MaterialBuilder materialBuilder = Transform.FindObjectOfType<MaterialBuilder>();
			MaterialJSON materialJson = GlobalApplicationManager.GetMaterialJsonById(materialId);
			materialBuilder.StartCoroutine(materialBuilder.UpdateMeshMaterial(materialJson, updateMaterialDelegate));
		}

		public static void CreateBaseboard(string materialId)
		{
			Room room = FindObjectOfType<Room>();
			room.CreateBaseboard(GlobalApplicationManager.GetMaterialJsonById(materialId));
		}

		public static void ChangeBaseboard(string materialId)
		{
			Room room = FindObjectOfType<Room>();
			Debug.LogWarning("MaterialId in history = " + materialId);
			room.CreateBaseboard(GlobalApplicationManager.GetMaterialJsonById(materialId));
		}

		public static void DeleteBaseboard()
		{
			Room room = FindObjectOfType<Room>();
			room.DeleteBaseboard();
		}

		public static void ScaleWindow(Transform onActionObject, float scale)
		{
			Room room = FindObjectOfType<Room>();
			if (room != null)
			{
				room.DeleteOppened3DButtons();
			}
			onActionObject.GetComponent<WindowTouchScaler>().ScaleWindow(scale);
		}

		public static void ChangeWindowType(Transform CreatedObject, Transform DeletedObject)
		{
			Room room = FindObjectOfType<Room>();
			if (room != null)
			{
				room.DeleteOppened3DButtons();
			}
			WindowChanger windowChanger = FindObjectOfType<WindowChanger>();
			windowChanger.DeleteWindow(CreatedObject.GetComponent<Window>());
			DeletedObject.gameObject.SetActive(true);
			DeletedObject.GetComponent<WallGridMoverTwoAxis>().AddObjectToGrid();
			DeletedObject.GetComponentInParent<Wall>().Windows.Add(DeletedObject.GetComponent<Window>());
		}

		public static void CreateFurnitureObject(Transform furnitureObject)
		{
			Room room = FindObjectOfType<Room>();
			if (room != null)
			{
				room.DeleteOppened3DButtons();
			}
			furnitureObject.gameObject.SetActive(true);

			room.Furniture.Add(furnitureObject.GetComponent<FurnitureObject>());
		}

		public static void DeleteFurnitureObject(Transform furnitureObject)
		{
			Room room = FindObjectOfType<Room>();
			if (room != null)
			{
				room.DeleteOppened3DButtons();
			}

			room.Furniture.Remove(furnitureObject.GetComponent<FurnitureObject>());
			furnitureObject.gameObject.SetActive(false);
		}

		public static void MoveFurnitureObject(Transform furnitureObject, Vector3 position)
		{
			Room room = FindObjectOfType<Room>();
			if (room != null)
			{
				room.DeleteOppened3DButtons();
			}
			furnitureObject.position = position;
		}

		public static void RotateFurnitureObject(Transform furnitureObject, Quaternion rotation)
		{
			Room room = FindObjectOfType<Room>();
			if (room != null)
			{
				room.DeleteOppened3DButtons();
			}
			furnitureObject.rotation = rotation;
		}
	}


}
