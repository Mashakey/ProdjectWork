using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseboardButton : MonoBehaviour
{
	[SerializeField]
	Transform currentWindow;
	[SerializeField]
	DrumScroll drumScroll;
	[SerializeField]
	ScrollbarMaterialType scrollbarMaterialType;

    public void OnBaseboardButtonClick()
	{
		drumScroll.GetComponent<Canvas>().enabled = true;
		drumScroll.GetComponent<CanvasScaler>().enabled = true;
		drumScroll.SetRectTransormSettings(GlobalApplicationManager.Baseboards);
		scrollbarMaterialType.SetMaterialTypeTextFieldValue(DataTypes.MaterialType.Baseboard);
		currentWindow.gameObject.SetActive(false);
	}
}
