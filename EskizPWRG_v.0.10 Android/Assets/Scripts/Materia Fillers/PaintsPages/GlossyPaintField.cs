using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlossyPaintField : MonoBehaviour
{
	GlossyPaintsPageHandler glossyPaintPageHandler;

	public void OnPaintFieldClick()
	{
		glossyPaintPageHandler = GetComponentInParent<GlossyPaintsPageHandler>();
		glossyPaintPageHandler.SetPaintFieldActive(transform);
	}
}
