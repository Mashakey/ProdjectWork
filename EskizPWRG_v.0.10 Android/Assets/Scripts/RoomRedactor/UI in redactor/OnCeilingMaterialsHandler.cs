using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class OnCeilingMaterialsHandler : MonoBehaviour
{
	[SerializeField]
	ScrollbarMaterialType scrollbarMaterialType;
	[SerializeField]
	GlossyPaintsPageFiller glossyPaintFiller;
	[SerializeField]
	MattPaintsPageFiller mattPaintFiller;

	public void DeactivateCeilingMaterialsCanvas()
	{
		GetComponent<Canvas>().enabled = false;
		GetComponent<CanvasScaler>().enabled = false;
	}

	public void OpenMattPaintMaterialsScroll()
	{
		DeactivateCeilingMaterialsCanvas();
		mattPaintFiller.CreatePaintFields(GlobalApplicationManager.Paints);
		scrollbarMaterialType.SetMaterialTypeTextFieldValue(MaterialType.Paint);

	}

	public void OpenGlossyPaintMaterialsScroll()
	{
		DeactivateCeilingMaterialsCanvas();
		glossyPaintFiller.CreatePaintFields(GlobalApplicationManager.Paints);
		scrollbarMaterialType.SetMaterialTypeTextFieldValue(MaterialType.Paint);
	}
}
