using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortingFforAllMaterials : MonoBehaviour
{
    SortingMaterial sortingMaterial;
    DrumScroll drumScroll;
    FavoriteStatusSetter favoriteStatusSetter;
    public GameObject activeFilt; 
    Color orangeColor;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#FF9900", out orangeColor);
        sortingMaterial = GameObject.FindObjectOfType<SortingMaterial>();
        drumScroll = FindObjectOfType<DrumScroll>();
        favoriteStatusSetter = FindObjectOfType<FavoriteStatusSetter>();
    }

    public void sortByCostAscending(GameObject selectButton)
    {
        sortingMaterial = GameObject.FindObjectOfType<SortingMaterial>();
        favoriteStatusSetter = FindObjectOfType<FavoriteStatusSetter>();

        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            if (activeFilt.name == "WallpaperFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceAscending(FavoritesStorage.Wallpapers);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceAscending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceAscending(GlobalApplicationManager.Wallpapers);
            }
            else if (activeFilt.name == "WallpaperForPaintingFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceAscending(FavoritesStorage.WallpapersForPainting);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceAscending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceAscending(GlobalApplicationManager.WallpapersForPainting);
            }
            else if (activeFilt.name == "LaminateFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceAscending(FavoritesStorage.Laminates);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceAscending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceAscending(GlobalApplicationManager.Laminates);
            }
            else if (activeFilt.name == "LinoleumFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceAscending(FavoritesStorage.Linoleums);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceAscending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceAscending(GlobalApplicationManager.Linoleums);
            }
            else if (activeFilt.name == "PVCsFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceAscending(FavoritesStorage.PVCs);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceAscending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceAscending(GlobalApplicationManager.PVCs);
            }
            else if (activeFilt.name == "BaseboardFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceAscending(FavoritesStorage.Baseboards);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceAscending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceAscending(GlobalApplicationManager.Baseboards);
            }
            else if (activeFilt.name == "DoorFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceAscending(FavoritesStorage.Doors);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceAscending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceAscending(GlobalApplicationManager.Doors);
            }
        }
        else
        {
            if (activeFilt.name == "WallpaperFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Wallpapers);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Wallpapers);
            }
            else if (activeFilt.name == "WallpaperForPaintingFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.WallpapersForPainting);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.WallpapersForPainting);
            }
            else if (activeFilt.name == "LaminateFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Laminates);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Laminates);
            }
            else if (activeFilt.name == "LinoleumFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Linoleums);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Linoleums);
            }
            else if (activeFilt.name == "PVCsFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.PVCs);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.PVCs);
            }
            else if (activeFilt.name == "BaseboardFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Baseboards);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Baseboards);
            }
            else if (activeFilt.name == "DoorFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Doors);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Doors);
            }
        }
        
    }
    public void sortByCostDescending(GameObject selectButton)
    {
        sortingMaterial = GameObject.FindObjectOfType<SortingMaterial>();
        favoriteStatusSetter = FindObjectOfType<FavoriteStatusSetter>();

        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            if (activeFilt.name == "WallpaperFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceDescending(FavoritesStorage.Wallpapers);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceDescending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceDescending(GlobalApplicationManager.Wallpapers);
            }
            else if (activeFilt.name == "WallpaperForPaintingFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceDescending(FavoritesStorage.WallpapersForPainting);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceDescending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceDescending(GlobalApplicationManager.WallpapersForPainting);
            }
            else if (activeFilt.name == "LaminateFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceDescending(FavoritesStorage.Laminates);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceDescending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceDescending(GlobalApplicationManager.Laminates);
            }
            else if (activeFilt.name == "LinoleumFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceDescending(FavoritesStorage.Linoleums);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceDescending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceDescending(GlobalApplicationManager.Linoleums);
            }
            else if (activeFilt.name == "PVCsFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceDescending(FavoritesStorage.PVCs);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceDescending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceDescending(GlobalApplicationManager.PVCs);
            }
            else if (activeFilt.name == "BaseboardFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceDescending(FavoritesStorage.Baseboards);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceDescending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceDescending(GlobalApplicationManager.Baseboards);
            }
            else if (activeFilt.name == "DoorFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortByPriceDescending(FavoritesStorage.Doors);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortByPriceDescending(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortByPriceDescending(GlobalApplicationManager.Doors);
            }
        }
        else
        {
            if (activeFilt.name == "WallpaperFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Wallpapers);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Wallpapers);
            }
            else if (activeFilt.name == "WallpaperForPaintingFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.WallpapersForPainting);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.WallpapersForPainting);
            }
            else if (activeFilt.name == "LaminateFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Laminates);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Laminates);
            }
            else if (activeFilt.name == "LinoleumFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Linoleums);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Linoleums);
            }
            else if (activeFilt.name == "PVCsFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.PVCs);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.PVCs);
            }
            else if (activeFilt.name == "BaseboardFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Baseboards);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Baseboards);
            }
            else if (activeFilt.name == "DoorFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Doors);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Doors);
            }
        }
        
    }

    public void sortInAscendingALPHABET(GameObject selectButton)
    {
        sortingMaterial = GameObject.FindObjectOfType<SortingMaterial>();
        favoriteStatusSetter = FindObjectOfType<FavoriteStatusSetter>();

        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            if (activeFilt.name == "WallpaperFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(FavoritesStorage.Wallpapers);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortInAscendingAlphabetOrder(GlobalApplicationManager.Wallpapers);
            }
            else if (activeFilt.name == "WallpaperForPaintingFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(FavoritesStorage.WallpapersForPainting);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortInAscendingAlphabetOrder(GlobalApplicationManager.WallpapersForPainting);
            }
            else if (activeFilt.name == "LaminateFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(FavoritesStorage.Laminates);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortInAscendingAlphabetOrder(GlobalApplicationManager.Laminates);
            }
            else if (activeFilt.name == "LinoleumFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(FavoritesStorage.Linoleums);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortInAscendingAlphabetOrder(GlobalApplicationManager.Linoleums);
            }
            else if (activeFilt.name == "PVCsFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(FavoritesStorage.PVCs);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortInAscendingAlphabetOrder(GlobalApplicationManager.PVCs);
            }
            else if (activeFilt.name == "BaseboardFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(FavoritesStorage.Baseboards);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortInAscendingAlphabetOrder(GlobalApplicationManager.Baseboards);
            }
            else if (activeFilt.name == "DoorFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(FavoritesStorage.Doors);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortInAscendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortInAscendingAlphabetOrder(GlobalApplicationManager.Doors);
            }
        }
        else
        {
            if (activeFilt.name == "WallpaperFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Wallpapers);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Wallpapers);
            }
            else if (activeFilt.name == "WallpaperForPaintingFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.WallpapersForPainting); ;
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.WallpapersForPainting);
            }
            else if (activeFilt.name == "LaminateFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Laminates);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Laminates);
            }
            else if (activeFilt.name == "LinoleumFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Linoleums);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Linoleums);
            }
            else if (activeFilt.name == "PVCsFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.PVCs);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.PVCs);
            }
            else if (activeFilt.name == "BaseboardFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Baseboards);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Baseboards);
            }
            else if (activeFilt.name == "DoorFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Doors);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Doors);
            }
        }

    }

    public void sortInDescendingALPHABET(GameObject selectButton)
    {
        sortingMaterial = GameObject.FindObjectOfType<SortingMaterial>();
        favoriteStatusSetter = FindObjectOfType<FavoriteStatusSetter>();

        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            if (activeFilt.name == "WallpaperFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(FavoritesStorage.Wallpapers);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortDescendingAlphabetOrder(GlobalApplicationManager.Wallpapers);
            }
            else if (activeFilt.name == "WallpaperForPaintingFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(FavoritesStorage.WallpapersForPainting);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortDescendingAlphabetOrder(GlobalApplicationManager.WallpapersForPainting);
            }
            else if (activeFilt.name == "LaminateFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(FavoritesStorage.Laminates);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortDescendingAlphabetOrder(GlobalApplicationManager.Laminates);
            }
            else if (activeFilt.name == "LinoleumFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(FavoritesStorage.Linoleums);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortDescendingAlphabetOrder(GlobalApplicationManager.Linoleums);
            }
            else if (activeFilt.name == "PVCsFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(FavoritesStorage.PVCs);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortDescendingAlphabetOrder(GlobalApplicationManager.PVCs);
            }
            else if (activeFilt.name == "BaseboardFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(FavoritesStorage.Baseboards);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortDescendingAlphabetOrder(GlobalApplicationManager.Baseboards);
            }
            else if (activeFilt.name == "DoorFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(FavoritesStorage.Doors);
                }
                else if (drumScroll.filtration == true)
                {
                    sortingMaterial.SortDescendingAlphabetOrder(drumScroll.filtrationList);
                }
                else
                    sortingMaterial.SortDescendingAlphabetOrder(GlobalApplicationManager.Doors);
            }
        }
        else
        {
            if (activeFilt.name == "WallpaperFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Wallpapers);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Wallpapers);
            }
            else if (activeFilt.name == "WallpaperForPaintingFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.WallpapersForPainting);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.WallpapersForPainting);
            }
            else if (activeFilt.name == "LaminateFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Laminates);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Laminates);
            }
            else if (activeFilt.name == "LinoleumFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Linoleums);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Linoleums);
            }
            else if (activeFilt.name == "PVCsFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.PVCs);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.PVCs);
            }
            else if (activeFilt.name == "BaseboardFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Baseboards);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Baseboards);
            }
            else if (activeFilt.name == "DoorFilt")
            {
                if (favoriteStatusSetter.favorires == true)
                {
                    drumScroll.SetRectTransormSettings(FavoritesStorage.Doors);
                }
                else if (drumScroll.filtration == true)
                {
                    drumScroll.SetRectTransormSettings(drumScroll.filtrationList);
                }
                else
                    drumScroll.SetRectTransormSettings(GlobalApplicationManager.Doors);
            }
        }

    }
}
