using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;
using UnityEngine.UI;

public class SortingPainting : MonoBehaviour
{

    [SerializeField]
    GlossyPaintsPageFiller glossyPaintsPageFiller;

    [SerializeField]
    MattPaintsPageFiller mattPaintsPageFiller;

    Color orangeColor;

    public List<GameObject> sortingButton = new List<GameObject>();


    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#FF9900", out orangeColor);
    }

    public void MattSortByCostAscending( GameObject selectButton)
    {
        if(selectButton.GetComponent<Image>().color == orangeColor)
        {
            List<MaterialJSON> sortedList = new List<MaterialJSON>(GlobalApplicationManager.Paints);
            sortedList.Sort((x, y) => x.cost.CompareTo(y.cost));
            mattPaintsPageFiller.CreatePaintFields(sortedList);
        }
        else
        {
            mattPaintsPageFiller.CreatePaintFields(GlobalApplicationManager.Paints);
        }
    }
    public void MattSortByCostDescending(GameObject selectButton)
    {
        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            List<MaterialJSON> sortedList = new List<MaterialJSON>(GlobalApplicationManager.Paints);
            sortedList.Sort((x, y) => -x.cost.CompareTo(y.cost));
            mattPaintsPageFiller.CreatePaintFields(sortedList);
        }
        else
        {
            mattPaintsPageFiller.CreatePaintFields(GlobalApplicationManager.Paints);
        }
    }

    public void MattSortInAscendingALPHABET(GameObject selectButton)
    {
        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            List<MaterialJSON> sortedList = new List<MaterialJSON>(GlobalApplicationManager.Paints);
            sortedList.Sort((x, y) => x.name.CompareTo(y.name));
            mattPaintsPageFiller.CreatePaintFields(sortedList);
        }
        else
        {
            mattPaintsPageFiller.CreatePaintFields(GlobalApplicationManager.Paints);
        }
    }

    public void MattSortInDescendingALPHABET(GameObject selectButton)
    {
        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            List<MaterialJSON> sortedList = new List<MaterialJSON>(GlobalApplicationManager.Paints);
            sortedList.Sort((x, y) => -x.name.CompareTo(y.name));
            mattPaintsPageFiller.CreatePaintFields(sortedList);
        }
        else
        {
            mattPaintsPageFiller.CreatePaintFields(GlobalApplicationManager.Paints);
        }
    }


    public void GlossySortByCostAscending(GameObject selectButton)
    {
        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            List<MaterialJSON> sortedList = new List<MaterialJSON>(GlobalApplicationManager.Paints);
            sortedList.Sort((x, y) => x.cost.CompareTo(y.cost));
            mattPaintsPageFiller.CreatePaintFields(sortedList);
        }
        else
        {
            mattPaintsPageFiller.CreatePaintFields(GlobalApplicationManager.Paints);
        }
    }
    public void GlossySortByCostDescending(GameObject selectButton)
    {
        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            List<MaterialJSON> sortedList = new List<MaterialJSON>(GlobalApplicationManager.Paints);
            sortedList.Sort((x, y) => -x.cost.CompareTo(y.cost));
            mattPaintsPageFiller.CreatePaintFields(sortedList);
        }
        else
        {
            mattPaintsPageFiller.CreatePaintFields(GlobalApplicationManager.Paints);
        }
    }

    public void GlossySortInAscendingALPHABET(GameObject selectButton)
    {
        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            List<MaterialJSON> sortedList = new List<MaterialJSON>(GlobalApplicationManager.Paints);
            sortedList.Sort((x, y) => x.name.CompareTo(y.name));
            mattPaintsPageFiller.CreatePaintFields(sortedList);
        }
        else
        {
            mattPaintsPageFiller.CreatePaintFields(GlobalApplicationManager.Paints);
        }
    }

    public void GlossySortInDescendingALPHABET(GameObject selectButton)
    {
        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            List<MaterialJSON> sortedList = new List<MaterialJSON>(GlobalApplicationManager.Paints);
            sortedList.Sort((x, y) => -x.name.CompareTo(y.name));
            mattPaintsPageFiller.CreatePaintFields(sortedList);
        }
        else
        {
            mattPaintsPageFiller.CreatePaintFields(GlobalApplicationManager.Paints);
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
