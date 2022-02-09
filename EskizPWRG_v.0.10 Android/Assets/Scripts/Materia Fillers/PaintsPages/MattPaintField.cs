using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MattPaintField : MonoBehaviour
{
    MattPaintPageHandler mattPaintPageHandler;

    public void OnPaintFieldClick()
	{
		mattPaintPageHandler = GetComponentInParent<MattPaintPageHandler>();
		mattPaintPageHandler.SetPaintFieldActive(transform);
	}
}
