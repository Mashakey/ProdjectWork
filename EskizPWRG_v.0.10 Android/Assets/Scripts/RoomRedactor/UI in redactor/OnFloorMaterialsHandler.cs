using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class OnFloorMaterialsHandler : MonoBehaviour
{
	[SerializeField]
	DrumScroll drumScroll;
	[SerializeField]
	ScrollbarMaterialType scrollbarMaterialType;


	public void DeactivateFloorMaterialsCanvas()
	{
		GetComponent<Canvas>().enabled = false;
		GetComponent<CanvasScaler>().enabled = false;
	}

	public void OpenLaminateMaterialsScroll()
	{
		DeactivateFloorMaterialsCanvas();
		drumScroll.SetRectTransormSettings(GlobalApplicationManager.Laminates);
		scrollbarMaterialType.SetMaterialTypeTextFieldValue(MaterialType.Laminate);
	}

	public void OpenLinoleumMaterialsScroll()
	{
		DeactivateFloorMaterialsCanvas();
		drumScroll.SetRectTransormSettings(GlobalApplicationManager.Linoleums);
		scrollbarMaterialType.SetMaterialTypeTextFieldValue(MaterialType.Linoleum);
	}

	public void OpenPVCMaterialsScroll()
	{
		DeactivateFloorMaterialsCanvas();
		drumScroll.SetRectTransormSettings(GlobalApplicationManager.PVCs);
		scrollbarMaterialType.SetMaterialTypeTextFieldValue(MaterialType.PVC);
	}

}
