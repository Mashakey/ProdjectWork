using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFirstWallDefaultValueInParametersHandler : MonoBehaviour
{
	public ParametersChangeHandler parametersChangeHandler;

    public float firstWallDefaultValue;

	private void OnEnable()
	{
		Debug.LogWarning("Setting default first wall length = " + firstWallDefaultValue);
		parametersChangeHandler.SetPreviuosLengthValue(firstWallDefaultValue);
		SetFirstWallAsActive();
	}
	
	public void SetFirstWallAsActive()
	{
		Debug.LogWarning("SettingFirstWallAsActive");
		if (GetComponentInChildren<RoomWallManager>().walls.Length > 0)
		{
			GetComponentInChildren<RoomWallManager>().walls[0].GetComponent<selectWallTouch>().selectForParam();
		}
	}
}
