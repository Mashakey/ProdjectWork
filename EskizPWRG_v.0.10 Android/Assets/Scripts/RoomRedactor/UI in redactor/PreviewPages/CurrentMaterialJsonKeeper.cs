using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;


public class CurrentMaterialJsonKeeper : MonoBehaviour
{
	public MaterialJSON currentMaterialJson;

	public void SetCurrentMaterialJson(MaterialJSON materialJson)
	{
		currentMaterialJson = materialJson;
	}

	public MaterialJSON GetCurrentMaterialJson()
	{
		return (currentMaterialJson);
	}
}
