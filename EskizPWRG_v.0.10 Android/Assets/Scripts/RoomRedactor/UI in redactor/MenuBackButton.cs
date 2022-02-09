using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackButton : MonoBehaviour
{
    public bool IsPreviousPageIsEditor = false;

    public void SetPreviousPageAsEditor()
	{
		IsPreviousPageIsEditor = true;
	}

	public void UnsetPreviousPageAsEditor()
	{
		IsPreviousPageIsEditor = false;
	}

	public void OnBackButtonClick()
	{
		if (IsPreviousPageIsEditor)
		{
			ActiveWindowKeeper.IsRedactorActive = true;
		}
	}
}
