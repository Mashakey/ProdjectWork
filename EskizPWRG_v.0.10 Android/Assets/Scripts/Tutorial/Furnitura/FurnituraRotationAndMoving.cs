using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnituraRotationAndMoving : MonoBehaviour
{
    public bool moving = false;
    public bool rotation = false;
	public bool mouseUp = true;
	DateTime mouseDownTime;

	Vector3 deltaPosition = Vector3.zero;
	Vector3 currentPosition = Vector3.zero;
	Vector3 lastPosition = Vector3.zero;
	public LayerMask ignoringLayer;

	public GameObject tutorialButtonFurniture;
	public GameObject CirclTapTable;

	public GameObject camera;

	private void OnMouseUp()
    {
		if (mouseUp == true)
		{
			ClickOnTable();
			mouseUp = false;
		}
	}

    private void OnMouseDrag()
	{
		if (rotation)
		{
			if (!CheckClickUI.IsClikedOnUI())
			{
				transform.Rotate(0, -deltaPosition.x * 0.5f, 0, Space.World);
			}
		}
		if (moving)
		{
			MoveObjectToMousePosition();
		}
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
			newPosition.y = -0.7f;
			if (newPosition.x < 0)
			{
				newPosition = new Vector3(0.1f, newPosition.y, newPosition.z);
			}
			else if (newPosition.x >= 1.9f)
			{
				newPosition = new Vector3(1.9f, newPosition.y, newPosition.z);
			}
			transform.position = newPosition;

		}
	}

	private void ClickOnTable()
    {
		tutorialButtonFurniture.SetActive(true);
		CirclTapTable.SetActive(false);

	}

    public void Update()
    {
		//currentPosition = Input.mousePosition;
		//deltaPosition = currentPosition - lastPosition;
		//lastPosition = currentPosition;
		//if (gameObject.transform.position.x < 0)
  //      {
		//	gameObject.transform.position = new Vector3(0.1f, transform.position.y, transform.position.z);
		//}
		//else if (gameObject.transform.position.x >= 1.9f)
  //      {
		//	gameObject.transform.position = new Vector3(1, transform.position.y, transform.position.z);
		//}
	}

	public void CameraPosition()
    {
		camera.transform.rotation = new Quaternion(0.171475396f, -0.683999121f, 0.17041406f, 0.688259006f);

	}
}
