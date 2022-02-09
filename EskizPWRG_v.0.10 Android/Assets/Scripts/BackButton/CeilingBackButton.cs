using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingBackButton : MonoBehaviour
{
	public GameObject EditorPage;
	public GameObject SurfacesPage;

	public GameObject PrevousPage;

    public void OnBackButtonClick()
	{

		if (PrevousPage == EditorPage)
		{
			BackToEditor();
		}
		else
		{
			BackToSurfaces();
		}
		GlobalApplicationManager.ClearSelectedObjects();
	}

	public void SetPrevousPageAsEditor()
	{
		PrevousPage = EditorPage;
	}

	public void SetPrevousPageASurfaces()
	{
		PrevousPage = SurfacesPage;
	}

	public void BackToEditor()
	{
		ActiveWindowKeeper.IsRedactorActive = true;

		EditorPage.SetActive(true);
	}

	public void BackToSurfaces()
	{
		SurfacesPage.SetActive(true);
	}
}
