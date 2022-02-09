using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerFiltration : MonoBehaviour
{
    public GameObject[] materialFilters;
    public GameObject prefabCost;
    public GameObject costExpand;

    public List<GameObject> materialsChildrens;
    public Sprite downArray;

    public FiltersAnimation filtersAnimation;
    public GameObject[] costPrefabs;
    public GameObject[] costExpands;
    public void OpeningOnlyOneWallpapersFilter()
    {
        for(int i = 0; i < materialFilters.Length; i++)
        {
            if(materialFilters[i].GetComponent<FiltrationForWallpaper>().ContentForFilt.Count != 0)
            {
                if(materialFilters[i].GetComponent<FiltrationForWallpaper>().ContentForFilt.Count >= 7)
                {

                    materialFilters[i].GetComponent<FiltrationForWallpaper>().scroltForActivate.SetActive(false);
                }
                else
                {
                    for(int j = 0; j < materialFilters[i].GetComponent<FiltrationForWallpaper>().ContentForFilt.Count; j++)
                    {
                        materialFilters[i].GetComponent<FiltrationForWallpaper>().ContentForFilt[j].SetActive(false);
                    }
                }
                materialFilters[i].GetComponent<FiltrationForWallpaper>().arrayGameObject.GetComponent<Image>().sprite = materialFilters[i].GetComponent<FiltrationForWallpaper>().downArray;
            }
        }

        if(prefabCost.activeSelf == true && gameObject.name != "cost")
        {
            prefabCost.SetActive(false);
            costExpand.GetComponent<Image>().sprite = downArray;
        }
    }

    public void OpeningOnlyOneWallpapersForPaintingFilter()
    {
        for (int i = 0; i < materialFilters.Length; i++)
        {
            if (materialFilters[i].GetComponent<FiltrationForWallpaperForPainting>().ContentForFilt.Count != 0)
            {
                if (materialFilters[i].GetComponent<FiltrationForWallpaperForPainting>().ContentForFilt.Count >= 7)
                {

                    materialFilters[i].GetComponent<FiltrationForWallpaperForPainting>().scroltForActivate.SetActive(false);
                }
                else
                {
                    for (int j = 0; j < materialFilters[i].GetComponent<FiltrationForWallpaperForPainting>().ContentForFilt.Count; j++)
                    {
                        materialFilters[i].GetComponent<FiltrationForWallpaperForPainting>().ContentForFilt[j].SetActive(false);
                    }
                }
                materialFilters[i].GetComponent<FiltrationForWallpaperForPainting>().arrayGameObject.GetComponent<Image>().sprite = materialFilters[i].GetComponent<FiltrationForWallpaperForPainting>().downArray;
            }
        }

        if (prefabCost.activeSelf == true && gameObject.name != "cost")
        {
            prefabCost.SetActive(false);
            costExpand.GetComponent<Image>().sprite = downArray;
        }
    }

    public void OpeningOnlyOneLaminate()
    {
        for (int i = 0; i < materialFilters.Length; i++)
        {
            if (materialFilters[i].GetComponent<FiltrationForLaminate>().ContentForFilt.Count != 0)
            {
                if (materialFilters[i].GetComponent<FiltrationForLaminate>().ContentForFilt.Count >= 7)
                {

                    materialFilters[i].GetComponent<FiltrationForLaminate>().scroltForActivate.SetActive(false);
                }
                else
                {
                    for (int j = 0; j < materialFilters[i].GetComponent<FiltrationForLaminate>().ContentForFilt.Count; j++)
                    {
                        materialFilters[i].GetComponent<FiltrationForLaminate>().ContentForFilt[j].SetActive(false);
                    }
                }
                materialFilters[i].GetComponent<FiltrationForLaminate>().arrayGameObject.GetComponent<Image>().sprite = materialFilters[i].GetComponent<FiltrationForLaminate>().downArray;
            }
        }

        if (prefabCost.activeSelf == true && gameObject.name != "cost")
        {
            prefabCost.SetActive(false);
            costExpand.GetComponent<Image>().sprite = downArray;
        }
    }

    public void OpeningOnlyOneLinoleum()
    {
        for (int i = 0; i < materialFilters.Length; i++)
        {
            if (materialFilters[i].GetComponent<FiltrationForLinoleum>().ContentForFilt.Count != 0)
            {
                if (materialFilters[i].GetComponent<FiltrationForLinoleum>().ContentForFilt.Count >= 7)
                {

                    materialFilters[i].GetComponent<FiltrationForLinoleum>().scroltForActivate.SetActive(false);
                }
                else
                {
                    for (int j = 0; j < materialFilters[i].GetComponent<FiltrationForLinoleum>().ContentForFilt.Count; j++)
                    {
                        materialFilters[i].GetComponent<FiltrationForLinoleum>().ContentForFilt[j].SetActive(false);
                    }
                }
                materialFilters[i].GetComponent<FiltrationForLinoleum>().arrayGameObject.GetComponent<Image>().sprite = materialFilters[i].GetComponent<FiltrationForLinoleum>().downArray;
            }
        }

        if (prefabCost.activeSelf == true && gameObject.name != "cost")
        {
            prefabCost.SetActive(false);
            costExpand.GetComponent<Image>().sprite = downArray;
        }
    }

    public void OpeningOnlyOnePVCs()
    {
        for (int i = 0; i < materialFilters.Length; i++)
        {
            if (materialFilters[i].GetComponent<FiltrationForPVCs>().ContentForFilt.Count != 0)
            {
                if (materialFilters[i].GetComponent<FiltrationForPVCs>().ContentForFilt.Count >= 7)
                {

                    materialFilters[i].GetComponent<FiltrationForPVCs>().scroltForActivate.SetActive(false);
                }
                else
                {
                    for (int j = 0; j < materialFilters[i].GetComponent<FiltrationForPVCs>().ContentForFilt.Count; j++)
                    {
                        materialFilters[i].GetComponent<FiltrationForPVCs>().ContentForFilt[j].SetActive(false);
                    }
                }
                materialFilters[i].GetComponent<FiltrationForPVCs>().arrayGameObject.GetComponent<Image>().sprite = materialFilters[i].GetComponent<FiltrationForPVCs>().downArray;
            }
        }

        if (prefabCost.activeSelf == true && gameObject.name != "cost")
        {
            prefabCost.SetActive(false);
            costExpand.GetComponent<Image>().sprite = downArray;
        }
    }
    public void OpeningOnlyOneBaseboard()
    {
        for (int i = 0; i < materialFilters.Length; i++)
        {
            if (materialFilters[i].GetComponent<FiltrationForBaseboard>().ContentForFilt.Count != 0)
            {
                if (materialFilters[i].GetComponent<FiltrationForBaseboard>().ContentForFilt.Count >= 7)
                {

                    materialFilters[i].GetComponent<FiltrationForBaseboard>().scroltForActivate.SetActive(false);
                }
                else
                {
                    for (int j = 0; j < materialFilters[i].GetComponent<FiltrationForBaseboard>().ContentForFilt.Count; j++)
                    {
                        materialFilters[i].GetComponent<FiltrationForBaseboard>().ContentForFilt[j].SetActive(false);
                    }
                }
                materialFilters[i].GetComponent<FiltrationForBaseboard>().arrayGameObject.GetComponent<Image>().sprite = materialFilters[i].GetComponent<FiltrationForBaseboard>().downArray;
            }
        }

        if (prefabCost.activeSelf == true && gameObject.name != "cost")
        {
            prefabCost.SetActive(false);
            costExpand.GetComponent<Image>().sprite = downArray;
        }
    }
    public void OpeningOnlyOneDoor()
    {
        for (int i = 0; i < materialFilters.Length; i++)
        {
            if (materialFilters[i].GetComponent<FiltrationForDoor>().ContentForFilt.Count != 0)
            {
                if (materialFilters[i].GetComponent<FiltrationForDoor>().ContentForFilt.Count >= 7)
                {

                    materialFilters[i].GetComponent<FiltrationForDoor>().scroltForActivate.SetActive(false);
                }
                else
                {
                    for (int j = 0; j < materialFilters[i].GetComponent<FiltrationForDoor>().ContentForFilt.Count; j++)
                    {
                        materialFilters[i].GetComponent<FiltrationForDoor>().ContentForFilt[j].SetActive(false);
                    }
                }
                materialFilters[i].GetComponent<FiltrationForDoor>().arrayGameObject.GetComponent<Image>().sprite = materialFilters[i].GetComponent<FiltrationForDoor>().downArray;
            }
        }

        if (prefabCost.activeSelf == true && gameObject.name != "cost")
        {
            prefabCost.SetActive(false);
            costExpand.GetComponent<Image>().sprite = downArray;
        }
    }

    public void CloseAllFilters()
    {
        for(int i =0; i<costPrefabs.Length; i++)
        {
            if(costPrefabs[i].transform.parent.gameObject.activeSelf == true)
            {
                prefabCost = costPrefabs[i];
                costExpands[i].GetComponent<Image>().sprite = downArray;
            }
        }

        for (int i = 0; i < materialsChildrens.Count; i++)
        {
            if (materialsChildrens[i].GetComponent<FiltrationForWallpaper>() != null)
            {
                if (materialsChildrens[i].GetComponent<FiltrationForWallpaper>().ContentForFilt.Count != 0)
                {
                    Debug.LogError(materialsChildrens[0].GetComponent<FiltrationForWallpaper>().ContentForFilt.Count);
                    if (materialsChildrens[i].GetComponent<FiltrationForWallpaper>().ContentForFilt.Count >= 7)
                    {
                       
                        materialsChildrens[i].GetComponent<FiltrationForWallpaper>().scroltForActivate.SetActive(false);
                    }
                    else
                    {
                        for (int j = 0; j < materialsChildrens[i].GetComponent<FiltrationForWallpaper>().ContentForFilt.Count; j++)
                        {
                            materialsChildrens[i].GetComponent<FiltrationForWallpaper>().ContentForFilt[j].SetActive(false);
                        }
                    }
                    materialsChildrens[i].GetComponent<FiltrationForWallpaper>().arrayGameObject.GetComponent<Image>().sprite = materialsChildrens[i].GetComponent<FiltrationForWallpaper>().downArray;
                }
            }
            else if (materialsChildrens[i].GetComponent<FiltrationForWallpaperForPainting>() != null)
            {
                if (materialsChildrens[i].GetComponent<FiltrationForWallpaperForPainting>().ContentForFilt.Count != 0)
                {
                    if (materialsChildrens[i].GetComponent<FiltrationForWallpaperForPainting>().ContentForFilt.Count >= 7)
                    {
                        materialsChildrens[i].GetComponent<FiltrationForWallpaperForPainting>().scroltForActivate.SetActive(false);
                    }
                    else
                    {
                        for (int j = 0; j < materialsChildrens[i].GetComponent<FiltrationForWallpaperForPainting>().ContentForFilt.Count; j++)
                        {
                            materialsChildrens[i].GetComponent<FiltrationForWallpaperForPainting>().ContentForFilt[j].SetActive(false);
                        }
                    }
                    materialsChildrens[i].GetComponent<FiltrationForWallpaperForPainting>().arrayGameObject.GetComponent<Image>().sprite = materialsChildrens[i].GetComponent<FiltrationForWallpaperForPainting>().downArray;
                }
            }
            else if (materialsChildrens[i].GetComponent<FiltrationForLaminate>() != null)
            {
                if (materialsChildrens[i].GetComponent<FiltrationForLaminate>().ContentForFilt.Count != 0)
                {
                    if (materialsChildrens[i].GetComponent<FiltrationForLaminate>().ContentForFilt.Count >= 7)
                    {

                        materialsChildrens[i].GetComponent<FiltrationForLaminate>().scroltForActivate.SetActive(false);
                    }
                    else
                    {
                        for (int j = 0; j < materialsChildrens[i].GetComponent<FiltrationForLaminate>().ContentForFilt.Count; j++)
                        {
                            materialsChildrens[i].GetComponent<FiltrationForLaminate>().ContentForFilt[j].SetActive(false);
                        }
                    }
                    materialsChildrens[i].GetComponent<FiltrationForLaminate>().arrayGameObject.GetComponent<Image>().sprite = materialsChildrens[i].GetComponent<FiltrationForLaminate>().downArray;
                }
            }
            else if (materialsChildrens[i].GetComponent<FiltrationForLinoleum>() != null)
            {
                if (materialsChildrens[i].GetComponent<FiltrationForLinoleum>().ContentForFilt.Count != 0)
                {
                    if (materialsChildrens[i].GetComponent<FiltrationForLinoleum>().ContentForFilt.Count >= 7)
                    {

                        materialsChildrens[i].GetComponent<FiltrationForLinoleum>().scroltForActivate.SetActive(false);
                    }
                    else
                    {
                        for (int j = 0; j < materialsChildrens[i].GetComponent<FiltrationForLinoleum>().ContentForFilt.Count; j++)
                        {
                            materialsChildrens[i].GetComponent<FiltrationForLinoleum>().ContentForFilt[j].SetActive(false);
                        }
                    }
                    materialsChildrens[i].GetComponent<FiltrationForLinoleum>().arrayGameObject.GetComponent<Image>().sprite = materialsChildrens[i].GetComponent<FiltrationForLinoleum>().downArray;
                }
            }
            else if (materialsChildrens[i].GetComponent<FiltrationForPVCs>() != null)
            {
                if (materialsChildrens[i].GetComponent<FiltrationForPVCs>().ContentForFilt.Count != 0)
                {
                    if (materialsChildrens[i].GetComponent<FiltrationForPVCs>().ContentForFilt.Count >= 7)
                    {

                        materialsChildrens[i].GetComponent<FiltrationForPVCs>().scroltForActivate.SetActive(false);
                    }
                    else
                    {
                        for (int j = 0; j < materialsChildrens[i].GetComponent<FiltrationForPVCs>().ContentForFilt.Count; j++)
                        {
                            materialsChildrens[i].GetComponent<FiltrationForPVCs>().ContentForFilt[j].SetActive(false);
                        }
                    }
                    materialsChildrens[i].GetComponent<FiltrationForPVCs>().arrayGameObject.GetComponent<Image>().sprite = materialsChildrens[i].GetComponent<FiltrationForPVCs>().downArray;
                }
            }
            else if (materialsChildrens[i].GetComponent<FiltrationForBaseboard>() != null)
            {
                if (materialsChildrens[i].GetComponent<FiltrationForBaseboard>().ContentForFilt.Count != 0)
                {
                    if (materialsChildrens[i].GetComponent<FiltrationForBaseboard>().ContentForFilt.Count >= 7)
                    {

                        materialsChildrens[i].GetComponent<FiltrationForBaseboard>().scroltForActivate.SetActive(false);
                    }
                    else
                    {
                        for (int j = 0; j < materialsChildrens[i].GetComponent<FiltrationForBaseboard>().ContentForFilt.Count; j++)
                        {
                            materialsChildrens[i].GetComponent<FiltrationForBaseboard>().ContentForFilt[j].SetActive(false);
                        }
                    }
                    materialsChildrens[i].GetComponent<FiltrationForBaseboard>().arrayGameObject.GetComponent<Image>().sprite = materialsChildrens[i].GetComponent<FiltrationForBaseboard>().downArray;
                }
            }
            else if (materialsChildrens[i].GetComponent<FiltrationForDoor>() != null)
            {
                if (materialsChildrens[i].GetComponent<FiltrationForDoor>().ContentForFilt.Count != 0)
                {
                    if (materialsChildrens[i].GetComponent<FiltrationForDoor>().ContentForFilt.Count >= 7)
                    {

                        materialsChildrens[i].GetComponent<FiltrationForDoor>().scroltForActivate.SetActive(false);
                    }
                    else
                    {
                        for (int j = 0; j < materialsChildrens[i].GetComponent<FiltrationForDoor>().ContentForFilt.Count; j++)
                        {
                            materialsChildrens[i].GetComponent<FiltrationForDoor>().ContentForFilt[j].SetActive(false);
                        }
                    }
                    materialsChildrens[i].GetComponent<FiltrationForDoor>().arrayGameObject.GetComponent<Image>().sprite = materialsChildrens[i].GetComponent<FiltrationForDoor>().downArray;
                }
            }
        }
        if (prefabCost.activeSelf == true)
        {
            prefabCost.SetActive(false);
        }
    }

    public void resetFiltersForBack()
    {
        materialsChildrens.Clear();
    }
}
