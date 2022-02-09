using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureField : MonoBehaviour
{
    public GameObject FurnitureObject;

    public void OnFurnitureFieldClick()
	{
		Room room = FindObjectOfType<Room>();
		GameObject furnitureObject = Instantiate(FurnitureObject, room.transform);
		furnitureObject.name = FurnitureObject.name;
		room.Furniture.Add(furnitureObject.GetComponent<FurnitureObject>());

		Vector3 position = GameObject.Find("FurnitureSpawnPoint").transform.position;

		//Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
		//RaycastHit hit;
		//Physics.Raycast(castPoint, out hit, Mathf.Infinity);
		//if (hit.collider != null)
		//{
		//	position = hit.point;
		//}
		position.y =0f;
		furnitureObject.transform.position = position;
		CloseFurniturePage();
	}

	public void CloseFurniturePage()
	{
		ActiveWindowKeeper.IsRedactorActive = true;
		transform.parent.parent.parent.parent.gameObject.SetActive(false);
	}
}
