using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class OnWallMaterialsHandler : MonoBehaviour
{
	[SerializeField]
	DrumScroll drumScroll;
	[SerializeField]
	ScrollbarMaterialType scrollbarMaterialType;
	[SerializeField]
	GlossyPaintsPageFiller glossyPaintFiller;
	[SerializeField]
	MattPaintsPageFiller mattPaintFiller;

	public void DeactivateWallMaterialsCanvas()
	{
		GetComponent<CanvasScaler>().enabled = false;
		GetComponent<Canvas>().enabled = false;
	}

    public void OpenWallpaperMaterialsScroll()
	{
		DeactivateWallMaterialsCanvas();
		drumScroll.SetRectTransormSettings(GlobalApplicationManager.Wallpapers);
		scrollbarMaterialType.SetMaterialTypeTextFieldValue(MaterialType.Wallpaper);
	}

	public void OpenWallpaperForPaintingMaterialsScroll()
	{
		DeactivateWallMaterialsCanvas();
		drumScroll.SetRectTransormSettings(GlobalApplicationManager.WallpapersForPainting);
		scrollbarMaterialType.SetMaterialTypeTextFieldValue(MaterialType.WallpaperForPainting);
	}

	public void OpenMattPaintMaterialsScroll()
	{
		DeactivateWallMaterialsCanvas();
		mattPaintFiller.CreatePaintFields(GlobalApplicationManager.Paints);
		scrollbarMaterialType.SetMaterialTypeTextFieldValue(MaterialType.Paint);

	}

	public void OpenGlossyPaintMaterialsScroll()
	{
		DeactivateWallMaterialsCanvas();
		glossyPaintFiller.CreatePaintFields(GlobalApplicationManager.Paints);
		scrollbarMaterialType.SetMaterialTypeTextFieldValue(MaterialType.Paint);
	}

	public void OpenPictureFromGallery()
	{
		DeactivateWallMaterialsCanvas();

	}
}
