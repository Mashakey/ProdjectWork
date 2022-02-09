using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarBackButton : MonoBehaviour
{
	public GameObject EditorPage;
	public GameObject FloorPage;
	public GameObject WallPage;

	public GameObject PrevousPage;

	public void OnBackButtonClick()
	{

		if (PrevousPage == EditorPage)
		{
			BackToEditor();
		}
		else if (PrevousPage == FloorPage)
		{
			BackToFloorPage();
		}
		else
		{
			BackToWallPage();
		}
		GlobalApplicationManager.ClearSelectedObjects();
	}

	public void SetPrevousPageAsEditor()
	{
		PrevousPage = EditorPage;
	}

	public void SetPreviousPageAsFloor()
	{
		PrevousPage = FloorPage;
	}

	public void SetPreviousPageAsWall()
	{
		PrevousPage = WallPage;
	}

	public void BackToEditor()
	{
		EditorPage.SetActive(true);
	}

	public void BackToFloorPage()
	{
		FloorPage.GetComponent<Canvas>().enabled = true;
		FloorPage.GetComponent<CanvasScaler>().enabled = true;
		FloorPage.SetActive(true);
	}

	public void BackToWallPage()
	{
		WallPage.GetComponent<Canvas>().enabled = true;
		WallPage.GetComponent<CanvasScaler>().enabled = true;
		WallPage.SetActive(true);
	}
}
