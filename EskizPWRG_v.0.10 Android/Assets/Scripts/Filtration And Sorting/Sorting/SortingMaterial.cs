using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DataTypes;

public class SortingMaterial : MonoBehaviour
{
    public void SortByPriceAscending(List<MaterialJSON> materialJsons)
    {
        //List<MaterialJSON> materialJsons = GlobalApplicationManager.allMaterialsJsons;
        //GlobalApplicationManager.allMaterialsJsons
        List<MaterialJSON> sortedList = new List<MaterialJSON>(materialJsons);
        sortedList.Sort((x, y) => x.cost.CompareTo(y.cost));
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        drumScroll.SetRectTransormSettings(sortedList);
    }

    public void SortByPriceDescending(List<MaterialJSON> materialJsons)
    {
        List<MaterialJSON> sortedList = new List<MaterialJSON>(materialJsons);
        sortedList.Sort((x, y) => -x.cost.CompareTo(y.cost));
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        drumScroll.SetRectTransormSettings(sortedList);
    }

    public void SortInAscendingAlphabetOrder(List<MaterialJSON> materialJsons)
    {
        List<MaterialJSON> sortedList = new List<MaterialJSON>(materialJsons);
        sortedList.Sort((x, y) => x.name.CompareTo(y.name));
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        drumScroll.SetRectTransormSettings(sortedList);
    }

    public void SortDescendingAlphabetOrder(List<MaterialJSON> materialJsons)
    {
        List<MaterialJSON> sortedList = new List<MaterialJSON>(materialJsons);
        sortedList.Sort((x, y) => -x.name.CompareTo(y.name));
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        drumScroll.SetRectTransormSettings(sortedList);
    }
    public void clickBut()
    {
        //List<MaterialJSON> materials = DataCacher.GetCachedMaterialsJSONs();
        //SortInAscendingAlphabetOrder(materials);
    }

}
