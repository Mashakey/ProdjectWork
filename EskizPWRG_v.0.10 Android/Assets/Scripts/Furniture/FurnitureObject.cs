using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class FurnitureObject : MonoBehaviour
{
    public string Name;
    public FurnitureType Type;

	public LayerMask ignoringLayer;

	public GameObject furnitureButtons;

	public bool isMoving = false;
	public bool isRotating = false;

	Vector3 currentPosition = Vector3.zero;
	Vector3 deltaPosition = Vector3.zero;
	Vector3 lastPosition = Vector3.zero;

	public bool IsColliding = false;
	public int CollisionCount = 0;

	public bool isFirstFrame;

	private void Start()
	{
		isFirstFrame = true;
	}

	public void OnMouseDown()
	{
		Debug.LogError("Click on furniture");
		if (isMoving)
		{
			AddHistoryActionFurnitureMove();
		}
		else if (isRotating)
		{
			AddHistoryActionFurnitureRotate();
		}
		if (!CheckClickUI.IsClikedOnUI() && Screen.orientation == ScreenOrientation.Portrait)
		{
			Room room = FindObjectOfType<Room>();
			if (room != null)
			{
				if (room.oppened3DButtons == null)
				{
					OnFurnitureClick();

				}
			}

		}
	}

	public void OnFurnitureClick()
	{
		Debug.LogWarning("OnFurnitureClick");
		if (!isMoving && !isRotating)
		{
			CreateOrDestroyFurnitureMenu();
		}
	}

	public void CreateOrDestroyFurnitureMenu()
	{
		if (furnitureButtons == null)
		{
			CreateFurnitureMenu();
		}
		else
		{
			DestroyFurnitureMenu();
		}
	}

    public void CreateFurnitureMenu()
	{
		PrefabContainer prefabContainer = FindObjectOfType<PrefabContainer>();
		Vector3 buttonsPosition = transform.position;
		buttonsPosition.y += 1f;
		furnitureButtons = Instantiate(prefabContainer.Buttons3dFurniture, buttonsPosition, Quaternion.identity, transform);
		Room room = FindObjectOfType<Room>();
		room.DeleteOppened3DButtons();
		room.oppened3DButtons = furnitureButtons;
		//furnitureButtons.transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, 1f);
	}

	public void DestroyFurnitureMenu()
	{
		GetComponentInChildren<Buttons3DFurniture>().TurnOffMoveButton();
		GetComponentInChildren<Buttons3DFurniture>().TurnOffRotateButton();
		Destroy(furnitureButtons);
		furnitureButtons = null;
	}

	public void Update()
	{
		isFirstFrame = false;

		if (isMoving)
		{
			//if (!CheckClickUI.IsClikedOnUI())
			//{
			//	Debug.LogWarning("Moving");
			//	MoveObjectToMousePosition();
			//}
		}
		if (isRotating)
		{
			//if (!CheckClickUI.IsClikedOnUI())
			{
				//RotateObjectWithMouse();
			}
		}
		currentPosition = Input.mousePosition;
		deltaPosition = currentPosition - lastPosition;
		lastPosition = currentPosition;
	}

	public void MoveObjectToMousePosition()
	{
		Vector3 mouse = Input.mousePosition;
		Ray castPoint = Camera.main.ScreenPointToRay(mouse);
		RaycastHit hit;
		Physics.Raycast(castPoint, out hit, Mathf.Infinity, ~ignoringLayer);
		if (hit.collider != null)
		{
			Vector3 newPosition = hit.point;
			newPosition.y = 0f;
			transform.position = newPosition;
		}
	}

	public void RotateObjectWithMouse()
	{
		float rotationSpeed = 4f;
		//Get mouse position
		Vector3 mousePos = Input.mousePosition;

		//Adjust mouse z position
		mousePos.z = Camera.main.transform.position.y - transform.position.y;

		//Get a world position for the mouse
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

		Vector3 lastPosition = Vector3.zero;
		Vector3 currentPosition = Input.mousePosition;
		Vector3 deltaPosition = currentPosition - lastPosition;
		lastPosition = currentPosition;

		//Get the angle to rotate and rotate
		Debug.LogError("NAME = " + transform.name);
		float angle = -Mathf.Atan2(transform.position.z - mouseWorldPos.z, transform.position.x - mouseWorldPos.x) * Mathf.Rad2Deg;
		//transform.Rotate(Vector3.up, angle);
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.deltaTime);
		//transform.Rotate(0, deltaPosition.y * 0.5, 0, Space.World);
		//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, angle, 0), rotationSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (isMoving || isRotating)
		{
			CollisionCount++;
			ChangeFurnitureColor(Color.red);
			IsColliding = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (isMoving || isRotating)
		{
			CollisionCount--;
			if (CollisionCount == 0)
			{
				ChangeFurnitureColor(Color.white);
				IsColliding = false;
			}

		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (isFirstFrame)
		{
			FindObjectOfType<HistoryChangesStack>().PopAndInvokeHistoryAction();
			FindObjectOfType<PagesContainer>().OpenNotEnoughPlaceForFurnitureWindow();
		}
		isFirstFrame = false;
	}

	//private void OnTriggerStay(Collider other)
	//{
	//	Debug.LogError("COLLISION");

	//}
	//void OnCollisionStay(Collision collisionInfo)
	//{
	//	Debug.LogError("COLLISION");
	//	// Debug-draw all contact points and normals
	//	foreach (ContactPoint contact in collisionInfo.contacts)
	//	{
	//		Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
	//	}
	//}

	private void OnMouseDrag()
	{

		if (isRotating)
		{
			FindObjectOfType<CameraZoomer>().isActive = false;

			if (!CheckClickUI.IsClikedOnUI())
			{
				transform.Rotate(0, -deltaPosition.x * 0.5f, 0, Space.World);

			}
		}
		if (isMoving)
		{
			FindObjectOfType<CameraZoomer>().isActive = false;
			if (!CheckClickUI.IsClikedOnUI())
			{
				MoveObjectToMousePosition();
				CheckCursorPositionAndRotateCamera();

			}
		}
	}

	public void CheckCursorPositionAndRotateCamera()
	{
		Vector3 mousePositionOnScreen = Input.mousePosition;
		if (mousePositionOnScreen.x < Screen.width * 0.1f)
		{
			Camera.main.GetComponent<CameraRotation>().RotateCameraLeft();
		}
		else if (mousePositionOnScreen.x > Screen.width * 0.9f)
		{
			Camera.main.GetComponent<CameraRotation>().RotateCameraRight();
		}
	}

	private void OnMouseUp()
	{
		FindObjectOfType<CameraZoomer>().isActive = true;
		if (IsColliding)
		{
			HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
			historyChangesStack.PopAndInvokeHistoryAction();
			IsColliding = false;
			ChangeFurnitureColor(Color.white);
		}
		CollisionCount = 0;
	}

	public void ChangeFurnitureColor(Color color)
	{
		foreach (var renderer in GetComponentsInChildren<MeshRenderer>())
		{
			foreach (var material in renderer.materials)
			{
				material.color = color;
			}
		}
	}

	public void AddHistoryActionFurnitureMove()
	{
		HistoryAction historyAction = new HistoryAction();
		historyAction.CreateMoveFurnitureObjectHistoryAction(transform, transform.position);
		HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
		historyChangesStack.AddHistoryAction(historyAction);
	}

	public void AddHistoryActionFurnitureRotate()
	{
		HistoryAction historyAction = new HistoryAction();
		historyAction.CreateRotateFurnitureObjectHistoryAction(transform, transform.rotation);
		HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
		historyChangesStack.AddHistoryAction(historyAction);
	}
}
