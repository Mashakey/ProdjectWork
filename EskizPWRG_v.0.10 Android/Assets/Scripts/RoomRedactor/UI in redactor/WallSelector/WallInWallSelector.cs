using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInWallSelector : MonoBehaviour
{
	public int wallIndex;

	WallSelector wallSelector;
	public bool IsActive = false;

	private void Awake()
	{
		wallSelector = transform.GetComponentInParent<WallSelector>();
	}

	public void SetWallActivityStatus()
	{
		if (!IsActive)
		{
			IsActive = true;
			wallSelector.AddActiveWallIndex(wallIndex);
		}
		else
		{
			IsActive = false;
			wallSelector.DeleteActiveWallIndex(wallIndex);
		}
	}
}
