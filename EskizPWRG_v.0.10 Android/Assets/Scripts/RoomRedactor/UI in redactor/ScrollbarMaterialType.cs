using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class ScrollbarMaterialType : MonoBehaviour
{
	[SerializeField]
	DrumScroll drumScroll;
	[SerializeField]
	Text materialTypeTextField;

	[SerializeField]
	SortingFforAllMaterials sortingFforAllMaterials;

	MaterialType currentUsingMaterialType = MaterialType.Wallpaper;

	public FiltersAnimation filtersAnimation;
	public GameObject wallpapers;
	public GameObject wallpapersForPainting;
	public GameObject laminates;
	public GameObject linoleums;
	public GameObject PVC;
	public GameObject baseboards;
	public GameObject doors;

	public TwinSlider wallpapersTwinSlider;
	public TwinSlider wallpapersForPaintingTwinSlider;
	public TwinSlider laminatesTwinSlider;
	public TwinSlider linoleumsTwinSlider;
	public TwinSlider PVCTwinSlider;
	public TwinSlider baseboardsTwinSlider;
	public TwinSlider doorsTwinSlider;


	public void SetMaterialTypeTextFieldValue(MaterialType materialType)
	{
		currentUsingMaterialType = materialType;
		switch (materialType)
		{
			case MaterialType.Baseboard:
				materialTypeTextField.text = "Плинтус";
				DisableAllFilters();
				baseboards.SetActive(true);
				filtersAnimation.objectAnimator = baseboards;
				sortingFforAllMaterials.activeFilt = baseboards;
				drumScroll.twinSlider = baseboardsTwinSlider;
				break;

			case MaterialType.Door:
				materialTypeTextField.text = "Дверь";
				DisableAllFilters();

				doors.SetActive(true);
				filtersAnimation.objectAnimator = doors;
				sortingFforAllMaterials.activeFilt = doors;
				drumScroll.twinSlider = doorsTwinSlider;

				break;

			case MaterialType.Laminate:
				materialTypeTextField.text = "Ламинат";
				DisableAllFilters();

				laminates.SetActive(true);
				sortingFforAllMaterials.activeFilt = laminates;
				filtersAnimation.objectAnimator = laminates;
				drumScroll.twinSlider = laminatesTwinSlider;

				break;

			case MaterialType.Linoleum:
				materialTypeTextField.text = "Линолеум";
				DisableAllFilters();

				linoleums.SetActive(true);
				filtersAnimation.objectAnimator = linoleums;
				drumScroll.twinSlider = linoleumsTwinSlider;
				sortingFforAllMaterials.activeFilt = linoleums;
				break;

			case MaterialType.Paint:
				materialTypeTextField.text = "Краски";
				DisableAllFilters();

				break;

			case MaterialType.PVC:
				materialTypeTextField.text = "ПВХ";
				DisableAllFilters();

				PVC.SetActive(true);
				sortingFforAllMaterials.activeFilt = PVC;
				filtersAnimation.objectAnimator = PVC;
				drumScroll.twinSlider = PVCTwinSlider;

				break;

			case MaterialType.Wallpaper:
				materialTypeTextField.text = "Обои";
				DisableAllFilters();

				wallpapers.SetActive(true);
				sortingFforAllMaterials.activeFilt = wallpapers;
				filtersAnimation.objectAnimator = wallpapers;
				drumScroll.twinSlider = wallpapersTwinSlider;

				break;

			case MaterialType.WallpaperForPainting:
				materialTypeTextField.text = "Обои";
				DisableAllFilters();

				wallpapersForPainting.SetActive(true);
				sortingFforAllMaterials.activeFilt = wallpapersForPainting;
				filtersAnimation.objectAnimator = wallpapersForPainting;
				drumScroll.twinSlider = wallpapersForPaintingTwinSlider;

				break;

			default:
				materialTypeTextField.text = "Материалы";
				break;
		}
	}

	public MaterialType GetCurrentUsingMaterialType()
	{
		return (currentUsingMaterialType);
	}

	public void OnMaterialTypeButtonClick()
	{
		drumScroll.ClearContentFields();
		switch (currentUsingMaterialType)
		{
			case MaterialType.Baseboard:
				drumScroll.SetRectTransormSettings(GlobalApplicationManager.Baseboards);
				break;

			case MaterialType.Door:
				drumScroll.SetRectTransormSettings(GlobalApplicationManager.Doors);
				break;

			case MaterialType.Laminate:
				drumScroll.SetRectTransormSettings(GlobalApplicationManager.Laminates);
				break;

			case MaterialType.Linoleum:
				drumScroll.SetRectTransormSettings(GlobalApplicationManager.Linoleums);
				break;

			case MaterialType.Paint:
				break;

			case MaterialType.PVC:
				drumScroll.SetRectTransormSettings(GlobalApplicationManager.PVCs);
				break;

			case MaterialType.Wallpaper:
				drumScroll.SetRectTransormSettings(GlobalApplicationManager.Wallpapers);
				break;

			case MaterialType.WallpaperForPainting:
				drumScroll.SetRectTransormSettings(GlobalApplicationManager.WallpapersForPainting);
				break;

			default:
				drumScroll.SetRectTransormSettings(GlobalApplicationManager.Wallpapers);
				break;
		}

	}

	public void DisableAllFilters()
    {
		wallpapers.SetActive(false);
		wallpapersForPainting.SetActive(false);
		laminates.SetActive(false);
		linoleums.SetActive(false);
		PVC.SetActive(false);
		baseboards.SetActive(false);
		doors.SetActive(false);
	}
}
