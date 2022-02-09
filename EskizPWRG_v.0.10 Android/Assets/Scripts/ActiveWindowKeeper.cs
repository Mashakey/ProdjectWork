using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWindowKeeper : MonoBehaviour
{
	public bool isRedactorActive = false;
	public static bool IsRedactorActive = false;

	public void SetRedactorWindowAsActive()
	{
		IsRedactorActive = true;
	}

	public void SetRedactorWindowAsInactive()
	{
		IsRedactorActive = false;
	}

	private void Update()
	{
		isRedactorActive = IsRedactorActive;
	}
}
