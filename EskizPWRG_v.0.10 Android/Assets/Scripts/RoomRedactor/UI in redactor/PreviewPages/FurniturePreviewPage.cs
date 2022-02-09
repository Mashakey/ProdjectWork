using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurniturePreviewPage : MonoBehaviour
{
    public GameObject SelectedFurnitureObject;

    public void CreateFurniture()
	{
		Room room = FindObjectOfType<Room>();
		GameObject furnitureObject = Instantiate(SelectedFurnitureObject, room.transform);
		furnitureObject.name = SelectedFurnitureObject.name;
		room.Furniture.Add(furnitureObject.GetComponent<FurnitureObject>());

		Vector3 position = GameObject.Find("FurnitureSpawnPoint").transform.position;

		//Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
		//RaycastHit hit;
		//Physics.Raycast(castPoint, out hit, Mathf.Infinity);
		//if (hit.collider != null)
		//{
		//	position = hit.point;
		//}
		position.y = 0f;
		furnitureObject.transform.position = position;
		CreateDeleteFurnitureHistoryAction(furnitureObject.transform);
		CloseFurniturePage();
	}

	public void CreateDeleteFurnitureHistoryAction(Transform deletingObject)
	{
		HistoryAction historyAction = new HistoryAction();
		historyAction.CreateDeleteFurnitureObjectHistoryAction(deletingObject);
		FindObjectOfType<HistoryChangesStack>().AddHistoryAction(historyAction);
	}

	public void CloseFurniturePage()
	{
		ActiveWindowKeeper.IsRedactorActive = true;
		gameObject.SetActive(false);
	}
}
