using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class Button3DDoorChange : MonoBehaviour
{

	public void OnMouseDown()
	{
		ClickOnDoorChangeButton();

	}

	public void ClickOnDoorChangeButton()
	{
		FindObjectOfType<ScrollbarBackButton>().SetPrevousPageAsEditor();
		GlobalApplicationManager.AddSelectedObject(transform.parent.parent.parent);
		DrumScroll drumScroll = GameObject.FindObjectOfType<DrumScroll>();
		ScrollbarMaterialType scrollbarMaterialType = FindObjectOfType<ScrollbarMaterialType>();
		scrollbarMaterialType.SetMaterialTypeTextFieldValue(MaterialType.Door);
		if (drumScroll != null)
		{
			ActiveWindowKeeper.IsRedactorActive = false;
			Debug.Log("Drum scroll is found");
			List<MaterialJSON> materials = DataCacher.GetCachedMaterialsJSONs();
			materials = MaterialFilter.GetOnlyDoors(materials);
			drumScroll.SetRectTransormSettings(materials);
		}
		else
		{
			Debug.LogError("Drum scroll is not found");
		}
	}
}
