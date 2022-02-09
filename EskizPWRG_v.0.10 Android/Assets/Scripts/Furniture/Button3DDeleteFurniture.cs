using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3DDeleteFurniture : MonoBehaviour
{
    public void OnDeleteFurnitureButtonClick()
	{
		Room room = FindObjectOfType<Room>();
		room.Furniture.Remove(GetComponentInParent<FurnitureObject>());
		//Destroy(GetComponentInParent<FurnitureObject>().gameObject);
		AddCreateFurnitureHistoryAction(GetComponentInParent<FurnitureObject>().transform);
		Buttons3DFurniture buttons = transform.parent.parent.GetComponent<Buttons3DFurniture>();
		buttons.TurnOffMoveButton();
		buttons.TurnOffRotateButton();
		GetComponentInParent<FurnitureObject>().gameObject.SetActive(false);

		Destroy(buttons.gameObject);
	}

	public void AddCreateFurnitureHistoryAction(Transform furnitureObject)
	{
		HistoryAction historyAction = new HistoryAction();
		historyAction.CreateInstantiateFurnitureObjectHistoryAction(furnitureObject);
		HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
		historyChangesStack.AddHistoryAction(historyAction);
	}
}
