using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintsHeadButtonsHandler : MonoBehaviour
{
	[SerializeField]
	MattPaintsPageFiller mattPaintsPageFiller;
	[SerializeField]
	GlossyPaintsPageFiller glossyPaintsPageFiller;

    public void OnMattPaintsButtonClick()
	{
		Debug.LogError("Creating matt paints");
		mattPaintsPageFiller.CreatePaintFields(GlobalApplicationManager.Paints);
	}

	public void OnMattPaintFavoritesClick()
	{
		Debug.LogError("Creating matt paints favorites");

		mattPaintsPageFiller.CreatePaintFields(FavoritesStorage.Paints);
	}

	public void OnGlossyPaintButtonClick()
	{
		glossyPaintsPageFiller.CreatePaintFields(GlobalApplicationManager.Paints);
	}

	public void OnGlossyPaintFavoritesButtonClick()
	{
		glossyPaintsPageFiller.CreatePaintFields(FavoritesStorage.Paints);
	}
}
