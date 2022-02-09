using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class FilteringCoast : MonoBehaviour
{
    public ListOfTyping listOfTyping;
    public int maxValue;
    public int mivalnValue;
    ValuePrise valuePrise;
    FavoriteStatusSetter favoriteStatusSetter;

    [SerializeField]
    MattPaintsPageFiller mattPaintFiller;

    [SerializeField]
    SliderAnimations sliderAnimations;

    [SerializeField]
    GlossyPaintsPageFiller glossyPaintFiller;
    private void Awake()
    {
       //listOfTyping = GameObject.FindObjectOfType<ListOfTyping>();
        valuePrise = GameObject.FindObjectOfType<ValuePrise>();
        favoriteStatusSetter = FindObjectOfType<FavoriteStatusSetter>();
    }

    public void FilteringMaterialCoastWallpapers()
    {
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>();

        if (favoriteStatusSetter.favorires == true)
        {
            filteredMaterials = new List<MaterialJSON>(FavoritesStorage.Wallpapers);
        }
        else if (drumScroll.filtration == true)
        {
            filteredMaterials = new List<MaterialJSON>(drumScroll.filtrationList);
        }
        else
        {
            filteredMaterials = new List<MaterialJSON>(GlobalApplicationManager.Wallpapers);
        }

        for (int i = filteredMaterials.Count - 1; i>= 0; i--)
        {
            if (filteredMaterials[i].cost < mivalnValue || filteredMaterials[i].cost > maxValue)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }

            
        }
        
        drumScroll.SetRectTransormSettings(filteredMaterials);
    }
    public void FilteringMaterialCoastWallpapersForPainting()
    {

        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>();

        if (favoriteStatusSetter.favorires == true)
        {
            filteredMaterials = new List<MaterialJSON>(FavoritesStorage.WallpapersForPainting);
        }
        else if (drumScroll.filtration == true)
        {
            filteredMaterials = new List<MaterialJSON>(drumScroll.filtrationList);
        }
        else
        {
            filteredMaterials = new List<MaterialJSON>(GlobalApplicationManager.WallpapersForPainting);
        }

        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (filteredMaterials[i].cost < mivalnValue || filteredMaterials[i].cost > maxValue)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }


        }
        drumScroll.SetRectTransormSettings(filteredMaterials);
    }
    public void FilteringMaterialCoastLaminates()
    {
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>();

        if (favoriteStatusSetter.favorires == true)
        {
            filteredMaterials = new List<MaterialJSON>(FavoritesStorage.Laminates);
        }
        else if (drumScroll.filtration == true)
        {
            filteredMaterials = new List<MaterialJSON>(drumScroll.filtrationList);
        }
        else
        {
            filteredMaterials = new List<MaterialJSON>(GlobalApplicationManager.Laminates);
        }

        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (filteredMaterials[i].cost < mivalnValue || filteredMaterials[i].cost > maxValue)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }


        }
        drumScroll.SetRectTransormSettings(filteredMaterials);
    }
    public void FilteringMaterialCoastLinileums()
    {
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>();

        if (favoriteStatusSetter.favorires == true)
        {
            filteredMaterials = new List<MaterialJSON>(FavoritesStorage.Linoleums);
        }
        else if (drumScroll.filtration == true)
        {
            filteredMaterials = new List<MaterialJSON>(drumScroll.filtrationList);
        }
        else
        {
            filteredMaterials = new List<MaterialJSON>(GlobalApplicationManager.Linoleums);
        }

        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (filteredMaterials[i].cost < mivalnValue || filteredMaterials[i].cost > maxValue)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }


        }
        drumScroll.SetRectTransormSettings(filteredMaterials);
    }
    public void FilteringMaterialCoastPVC()
    {
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>();

        if (favoriteStatusSetter.favorires == true)
        {
            filteredMaterials = new List<MaterialJSON>(FavoritesStorage.PVCs);
        }
        else if (drumScroll.filtration == true)
        {
            filteredMaterials = new List<MaterialJSON>(drumScroll.filtrationList);
        }
        else
        {
            filteredMaterials = new List<MaterialJSON>(GlobalApplicationManager.PVCs);
        }

        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (filteredMaterials[i].cost < mivalnValue || filteredMaterials[i].cost > maxValue)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }


        }
        drumScroll.SetRectTransormSettings(filteredMaterials);
    }
    public void FilteringMaterialCoastBaseboard()
    {
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>();

        if (favoriteStatusSetter.favorires == true)
        {
            filteredMaterials = new List<MaterialJSON>(FavoritesStorage.Baseboards);
        }
        else if (drumScroll.filtration == true)
        {
            filteredMaterials = new List<MaterialJSON>(drumScroll.filtrationList);
        }
        else
        {
            filteredMaterials = new List<MaterialJSON>(GlobalApplicationManager.Baseboards);
        }

        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (filteredMaterials[i].cost < mivalnValue || filteredMaterials[i].cost > maxValue)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }


        }
        drumScroll.SetRectTransormSettings(filteredMaterials);
    }
    public void FilteringMaterialCoastDoors()
    {
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>();

        if (favoriteStatusSetter.favorires == true)
        {
            filteredMaterials = new List<MaterialJSON>(FavoritesStorage.Doors);
        }
        else if (drumScroll.filtration == true)
        {
            filteredMaterials = new List<MaterialJSON>(drumScroll.filtrationList);
        }
        else
        {
            filteredMaterials = new List<MaterialJSON>(GlobalApplicationManager.Doors);
        }

        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (filteredMaterials[i].cost < mivalnValue || filteredMaterials[i].cost > maxValue)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }


        }
        drumScroll.SetRectTransormSettings(filteredMaterials);
    }

    public void FilteringMaterialCoastPaintsMatt()
    {
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>();
        if (sliderAnimations.favorutList == true)
        {
            filteredMaterials = new List<MaterialJSON>(FavoritesStorage.Paints);
        }
        else
            filteredMaterials = new List<MaterialJSON>(GlobalApplicationManager.Paints);

        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (filteredMaterials[i].cost < mivalnValue || filteredMaterials[i].cost > maxValue)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
        }
        mattPaintFiller.CreatePaintFields(filteredMaterials);
    }
    public void FilteringMaterialCoastPaintsGlossy()
    {
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>();
        if (sliderAnimations.favorutList == true)
        {
            filteredMaterials = new List<MaterialJSON>(FavoritesStorage.Paints);
        }
        else
            filteredMaterials = new List<MaterialJSON>(GlobalApplicationManager.Paints);

        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (filteredMaterials[i].cost < mivalnValue || filteredMaterials[i].cost > maxValue)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
        }
        glossyPaintFiller.CreatePaintFields(filteredMaterials);
    }

    public void FiltrationCostMyRoom()
    {
        List<RoomData> roomDataJsons = new List<RoomData>(GlobalApplicationManager.MyRooms);
        for (int i = roomDataJsons.Count - 1; i >= 0; i--)
        {
            if (roomDataJsons[i].Cost < mivalnValue || roomDataJsons[i].Cost > maxValue)
            {
                roomDataJsons.Remove(roomDataJsons[i]);
                continue;
            }
        }

        MyRoomsFiller myRoomsFiller = GameObject.FindObjectOfType<MyRoomsFiller>();
        myRoomsFiller.CreateRoomsFields(roomDataJsons);
    }
    }
