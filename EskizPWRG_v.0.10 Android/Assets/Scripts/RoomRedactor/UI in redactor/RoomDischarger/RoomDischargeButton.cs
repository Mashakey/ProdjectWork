using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomDischargeButton : MonoBehaviour
{
	[SerializeField]
	LoadingScreen loadingScreen;

    public void OnRoomDischargeButtonClick()
	{
		//LoadingScreen loadingScreen = FindObjectOfType<LoadingScreen>();
		loadingScreen.gameObject.SetActive(true);
		loadingScreen.StartCoroutine(loadingScreen.LoadingOnCachedRoomCreating(RoomDischarger.emptyRoom));
	}

	public void SetRoomDischargeButtonStatus()
	{
		GetComponent<Button>().interactable = false;
		Room room = FindObjectOfType<Room>();
		if (room.floorMaterialId != "DefaultFloor")
		{
			Debug.LogWarning("#1");
			GetComponent<Button>().interactable = true;
			return;
		}
		if (room.ceilingMaterialId != "Default")
		{
			Debug.LogWarning("#2");

			GetComponent<Button>().interactable = true;
			return;
		}
		if (room.baseBoardMaterialId != "")
		{
			Debug.LogWarning("#3");

			GetComponent<Button>().interactable = true;
			return;
		}
		foreach (Wall wall in room.Walls)
		{
			if (wall.materialId != "DefaultWall")
			{
				Debug.LogWarning("#4");

				GetComponent<Button>().interactable = true;
				return;
			}
		}

	}
}
