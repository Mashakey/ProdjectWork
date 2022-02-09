using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class FavoritesButtonHandler : MonoBehaviour
{
	[SerializeField]
	DrumScroll drumScroll;
	[SerializeField]
	ScrollbarMaterialType scrollbarMaterialType;

    public void OnFavoritesButtonClick()
	{

		MaterialType currentMaterialType = scrollbarMaterialType.GetCurrentUsingMaterialType();

		switch (currentMaterialType)
		{
			case MaterialType.Baseboard:
				drumScroll.SetRectTransormSettings(FavoritesStorage.Baseboards);
				break;
			case MaterialType.Door:
				drumScroll.SetRectTransormSettings(FavoritesStorage.Doors);
				break;
			case MaterialType.Laminate:
				drumScroll.SetRectTransormSettings(FavoritesStorage.Laminates);
				break;
			case MaterialType.Linoleum:
				drumScroll.SetRectTransormSettings(FavoritesStorage.Linoleums);
				break;
			case MaterialType.Paint:
				drumScroll.SetRectTransormSettings(FavoritesStorage.Paints);
				break;
			case MaterialType.PVC:
				drumScroll.SetRectTransormSettings(FavoritesStorage.PVCs);
				break;
			case MaterialType.Wallpaper:
				drumScroll.SetRectTransormSettings(FavoritesStorage.Wallpapers);
				break;
			case MaterialType.WallpaperForPainting:
				drumScroll.SetRectTransormSettings(FavoritesStorage.WallpapersForPainting);
				break;
			default:
				Debug.LogError("Can't create scroll content. Unknown material type " + currentMaterialType);
				break;
		}
	}        
}
