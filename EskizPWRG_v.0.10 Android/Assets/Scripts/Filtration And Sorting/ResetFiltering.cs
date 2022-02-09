using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetFiltering : MonoBehaviour
{
    DrumScroll drumScroll;
    GameObject[] filtrationForWallpapers;
    GameObject[] filtrationForWallpaperForPaintings;
    GameObject[] filtrationForLaminates;
    GameObject[] filtrationForLinoleums;
    GameObject[] filtrationForPVCs;
    GameObject[] filtrationForBaseboards;
    GameObject[] filtrationForDoors;
    public List<GameObject> sortingButton = new List<GameObject>();
    Color unActiveColor;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#868686", out unActiveColor);
        drumScroll = FindObjectOfType<DrumScroll>();
    }

    public void materialFilteringReset()
    {
        drumScroll.filtrationList.Clear();

        for (int i = 0; i < drumScroll.InstantiatePrefabs.Count; i++)
        {
            Destroy(drumScroll.InstantiatePrefabs[i]);
        }
        drumScroll.filtration = false;
        drumScroll.selectFilt = 0;
        drumScroll.filters.GetComponent<Image>().color = unActiveColor;
    }

    public void indexForFiltering()
    {
        drumScroll.InstantiatePrefabs.Clear();
        
        if (FindObjectOfType<SortingFforAllMaterials>().activeFilt.name == "WallpaperFilt")
        {
            filtrationForWallpapers = GameObject.FindGameObjectsWithTag("WallpaperFilt");
            for (int j = 0; j < filtrationForWallpapers.Length; j++)
            {
                filtrationForWallpapers[j].GetComponent<FiltrationForWallpaper>().indexClick = 0;
                filtrationForWallpapers[j].GetComponent<FiltrationForWallpaper>().ContentForFilt.Clear();
            }
        }
        else if (FindObjectOfType<SortingFforAllMaterials>().activeFilt.name == "WallpaperForPaintingFilt")
        {
            filtrationForWallpaperForPaintings = GameObject.FindGameObjectsWithTag("WallpaperForPaintingFilt");
            for (int j = 0; j < filtrationForWallpaperForPaintings.Length; j++)
            {
                filtrationForWallpaperForPaintings[j].GetComponent<FiltrationForWallpaperForPainting>().indexClick = 0;
                filtrationForWallpaperForPaintings[j].GetComponent<FiltrationForWallpaperForPainting>().ContentForFilt.Clear();
            }
        }
        else if (FindObjectOfType<SortingFforAllMaterials>().activeFilt.name == "LaminateFilt")
        {
            filtrationForLaminates = GameObject.FindGameObjectsWithTag("LaminateFilt");
            for (int j = 0; j < filtrationForLaminates.Length; j++)
            {
                filtrationForLaminates[j].GetComponent<FiltrationForLaminate>().indexClick = 0;
                filtrationForLaminates[j].GetComponent<FiltrationForLaminate>().ContentForFilt.Clear();
            }
        }
        else if (FindObjectOfType<SortingFforAllMaterials>().activeFilt.name == "LinoleumFilt")
        {
            filtrationForLinoleums = GameObject.FindGameObjectsWithTag("LinoleumFilt");
            for (int j = 0; j < filtrationForLinoleums.Length; j++)
            {
                filtrationForLinoleums[j].GetComponent<FiltrationForLinoleum>().indexClick = 0;
                filtrationForLinoleums[j].GetComponent<FiltrationForLinoleum>().ContentForFilt.Clear();
            }
        }
        else if (FindObjectOfType<SortingFforAllMaterials>().activeFilt.name == "PVCsFilt")
        {
            filtrationForPVCs = GameObject.FindGameObjectsWithTag("PVCsFilt");
            for (int j = 0; j < filtrationForPVCs.Length; j++)
            {
                filtrationForPVCs[j].GetComponent<FiltrationForPVCs>().indexClick = 0;
                filtrationForPVCs[j].GetComponent<FiltrationForPVCs>().ContentForFilt.Clear();
            }
        }
        else if (FindObjectOfType<SortingFforAllMaterials>().activeFilt.name == "BaseboardFilt")
        {
            filtrationForBaseboards = GameObject.FindGameObjectsWithTag("BaseboardFilt");
            for (int j = 0; j < filtrationForBaseboards.Length; j++)
            {
                filtrationForBaseboards[j].GetComponent<FiltrationForBaseboard>().indexClick = 0;
                filtrationForBaseboards[j].GetComponent<FiltrationForBaseboard>().ContentForFilt.Clear();
            }
        }
        else if (FindObjectOfType<SortingFforAllMaterials>().activeFilt.name == "DoorFilt")
        {
            filtrationForDoors = GameObject.FindGameObjectsWithTag("DoorFilt");
            for (int j = 0; j < filtrationForDoors.Length; j++)
            {
                filtrationForDoors[j].GetComponent<FiltrationForDoor>().indexClick = 0;
                filtrationForDoors[j].GetComponent<FiltrationForDoor>().ContentForFilt.Clear();
            }
        }
    }

    public void ResetSortingButton()
    {
        Color otherColor;       
        ColorUtility.TryParseHtmlString("#868686", out otherColor);

        foreach (var button in sortingButton)
        {
            button.GetComponent<Image>().color = otherColor;
        }
    }
}
