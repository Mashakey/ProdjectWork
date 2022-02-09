using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class FilteringOfMaterials : MonoBehaviour
{
    [SerializeField]
    GameObject ErrorPageHaveNoMaterials;

    public ListOfTyping listOfTyping;

    public List<MaterialJSON> filteredMaterials = new List<MaterialJSON>();

    public void FilterMaterialsWallpapers(List<MaterialJSON> materialJSONs)
    {
        listOfTyping = GameObject.Find("WallpaperFilt").GetComponent<ListOfTyping>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>(materialJSONs);
        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (!listOfTyping.companies.Contains(filteredMaterials[i].custom_properties.manufacturer_company) && listOfTyping.companies.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.countries.Contains(filteredMaterials[i].custom_properties.manufacturer_country) && listOfTyping.countries.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.collection.Contains(filteredMaterials[i].custom_properties.collection) && listOfTyping.collection.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.width.Contains(filteredMaterials[i].pack_dimensions.x.ToString()) && listOfTyping.width.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.length.Contains(filteredMaterials[i].pack_dimensions.y.ToString()) && listOfTyping.length.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (listOfTyping.color.Count > 0 && !IsColorContains(filteredMaterials[i].custom_properties.color))
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
                                       
        }
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();

        drumScroll.filtration = true;
        drumScroll.filtrationList = filteredMaterials;
        drumScroll.SetRectTransormSettings(filteredMaterials);
        NoMaterialErrorPage noMaterialErrorPage = FindObjectOfType<NoMaterialErrorPage>();
        if (filteredMaterials.Count == 0)
        {
            noMaterialErrorPage.EnableNoMaterialErrorPage();
        }
        else
        {
            noMaterialErrorPage.DisableNoMaterialErrorPage();
        }
        //Debug.LogError("my:" + filteredMaterials.Count);
        //Debug.LogError(GlobalApplicationManager.Wallpapers.Count);
    }
    
    bool IsColorContains(string[] materialColors)
    {
        bool isContains = false;
        foreach (string materialColor in materialColors)
        {
            foreach (var color in listOfTyping.color)
            {
                if (color == materialColor)
                {
                    isContains = true;
                    break;
                }
            }
        }
        return isContains;
    }

    public void FilterMaterialsWallpapersOfPainting(List<MaterialJSON> materialJSONs)
    {
        listOfTyping = GameObject.Find("WallpaperOfPaintingFilt").GetComponent<ListOfTyping>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>(materialJSONs);
        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (!listOfTyping.companies.Contains(filteredMaterials[i].custom_properties.manufacturer_company) && listOfTyping.companies.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.countries.Contains(filteredMaterials[i].custom_properties.manufacturer_country) && listOfTyping.countries.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.collection.Contains(filteredMaterials[i].custom_properties.collection) && listOfTyping.collection.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.width.Contains(filteredMaterials[i].pack_dimensions.x.ToString()) && listOfTyping.width.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.length.Contains(filteredMaterials[i].pack_dimensions.y.ToString()) && listOfTyping.length.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (listOfTyping.color.Count > 0 && !IsColorContains(filteredMaterials[i].custom_properties.color))
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
        }
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        drumScroll.SetRectTransormSettings(filteredMaterials);
        drumScroll.filtration = true;
        drumScroll.filtrationList = filteredMaterials;
        NoMaterialErrorPage noMaterialErrorPage = FindObjectOfType<NoMaterialErrorPage>();
        if (filteredMaterials.Count == 0)
        {
            noMaterialErrorPage.EnableNoMaterialErrorPage();
        }
        else
        {
            noMaterialErrorPage.DisableNoMaterialErrorPage();
        }
    }

    public void FilterMaterialsLaminates(List<MaterialJSON> materialJSONs)
    {
        listOfTyping = GameObject.Find("LaminateFilt").GetComponent<ListOfTyping>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>(materialJSONs);
        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (!listOfTyping.companies.Contains(filteredMaterials[i].custom_properties.manufacturer_company) && listOfTyping.companies.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.countries.Contains(filteredMaterials[i].custom_properties.manufacturer_country) && listOfTyping.countries.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.collection.Contains(filteredMaterials[i].custom_properties.collection) && listOfTyping.collection.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.board_thickness.Contains(filteredMaterials[i].custom_properties.board_thickness.ToString()) && listOfTyping.board_thickness.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.chamfer.Contains(filteredMaterials[i].custom_properties.chamfer.ToString()) && listOfTyping.chamfer.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.moisture_resistant.Contains(filteredMaterials[i].custom_properties.moisture_resistant.ToString()) && listOfTyping.moisture_resistant.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (listOfTyping.color.Count > 0 && !IsColorContains(filteredMaterials[i].custom_properties.color))
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
        }
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        drumScroll.SetRectTransormSettings(filteredMaterials);
        drumScroll.filtration = true;
        drumScroll.filtrationList = filteredMaterials;
        NoMaterialErrorPage noMaterialErrorPage = FindObjectOfType<NoMaterialErrorPage>();
        if (filteredMaterials.Count == 0)
        {
            noMaterialErrorPage.EnableNoMaterialErrorPage();
        }
        else
        {
            noMaterialErrorPage.DisableNoMaterialErrorPage();
        }
    }
    public void FilterMaterialsLinoleums(List<MaterialJSON> materialJSONs)
    {
        listOfTyping = GameObject.Find("LinoleumFilt").GetComponent<ListOfTyping>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>(materialJSONs);
        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (!listOfTyping.companies.Contains(filteredMaterials[i].custom_properties.manufacturer_company) && listOfTyping.companies.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.countries.Contains(filteredMaterials[i].custom_properties.manufacturer_country) && listOfTyping.countries.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.collection.Contains(filteredMaterials[i].custom_properties.collection) && listOfTyping.collection.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.total_thickness.Contains(filteredMaterials[i].custom_properties.total_thickness.ToString()) && listOfTyping.total_thickness.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.zc_thickness.Contains(filteredMaterials[i].custom_properties.zs_thickness.ToString()) && listOfTyping.zc_thickness.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.design_type.Contains(filteredMaterials[i].custom_properties.design_type.ToString()) && listOfTyping.design_type.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.use.Contains(filteredMaterials[i].custom_properties.use.ToString()) && listOfTyping.use.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.basis.Contains(filteredMaterials[i].custom_properties.basis.ToString()) && listOfTyping.basis.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (listOfTyping.color.Count > 0 && !IsColorContains(filteredMaterials[i].custom_properties.color))
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
        }
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        drumScroll.SetRectTransormSettings(filteredMaterials);
        drumScroll.filtration = true;
        drumScroll.filtrationList = filteredMaterials;
        NoMaterialErrorPage noMaterialErrorPage = FindObjectOfType<NoMaterialErrorPage>();
        if (filteredMaterials.Count == 0)
        {
            noMaterialErrorPage.EnableNoMaterialErrorPage();
        }
        else
        {
            noMaterialErrorPage.DisableNoMaterialErrorPage();
        }
    }
    public void FilterMaterialsPVC(List<MaterialJSON> materialJSONs)
    {
        listOfTyping = GameObject.Find("PVCsFilt").GetComponent<ListOfTyping>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>(materialJSONs);
        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (!listOfTyping.companies.Contains(filteredMaterials[i].custom_properties.manufacturer_company) && listOfTyping.companies.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.countries.Contains(filteredMaterials[i].custom_properties.manufacturer_country) && listOfTyping.countries.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.collection.Contains(filteredMaterials[i].custom_properties.collection) && listOfTyping.collection.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.chamfer.Contains(filteredMaterials[i].custom_properties.chamfer.ToString()) && listOfTyping.chamfer.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.protective_layer_thickness.Contains(filteredMaterials[i].custom_properties.protective_layer_thickness.ToString()) && listOfTyping.protective_layer_thickness.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.board_thickness.Contains(filteredMaterials[i].custom_properties.board_thickness.ToString()) && listOfTyping.board_thickness.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (listOfTyping.color.Count > 0 && !IsColorContains(filteredMaterials[i].custom_properties.color))
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
        }
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        drumScroll.SetRectTransormSettings(filteredMaterials);
        drumScroll.filtration = true;
        drumScroll.filtrationList = filteredMaterials;
        NoMaterialErrorPage noMaterialErrorPage = FindObjectOfType<NoMaterialErrorPage>();
        if (filteredMaterials.Count == 0)
        {
            noMaterialErrorPage.EnableNoMaterialErrorPage();
        }
        else
        {
            noMaterialErrorPage.DisableNoMaterialErrorPage();
        }
    }

    public void FilterMaterialsBaseboards(List<MaterialJSON> materialJSONs)
    {
        listOfTyping = GameObject.Find("BaseboardFilt").GetComponent<ListOfTyping>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>(materialJSONs);
        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (!listOfTyping.companies.Contains(filteredMaterials[i].custom_properties.manufacturer_company) && listOfTyping.companies.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.countries.Contains(filteredMaterials[i].custom_properties.manufacturer_country) && listOfTyping.countries.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.collection.Contains(filteredMaterials[i].custom_properties.collection) && listOfTyping.collection.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.material.Contains(filteredMaterials[i].custom_properties.material.ToString()) && listOfTyping.material.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.length.Contains(filteredMaterials[i].custom_properties.length.ToString()) && listOfTyping.length.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.height.Contains(filteredMaterials[i].custom_properties.height.ToString()) && listOfTyping.height.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (listOfTyping.color.Count > 0 && !IsColorContains(filteredMaterials[i].custom_properties.color))
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
        }
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        drumScroll.SetRectTransormSettings(filteredMaterials);
        drumScroll.filtration = true;
        drumScroll.filtrationList = filteredMaterials;
        NoMaterialErrorPage noMaterialErrorPage = FindObjectOfType<NoMaterialErrorPage>();
        if (filteredMaterials.Count == 0)
        {
            noMaterialErrorPage.EnableNoMaterialErrorPage();
        }
        else
        {
            noMaterialErrorPage.DisableNoMaterialErrorPage();
        }
    }

    public void FilterMaterialsDoors(List<MaterialJSON> materialJSONs)
    {
        listOfTyping = GameObject.Find("DoorFilt").GetComponent<ListOfTyping>();
        List<MaterialJSON> filteredMaterials = new List<MaterialJSON>(materialJSONs);
        for (int i = filteredMaterials.Count - 1; i >= 0; i--)
        {

            if (!listOfTyping.companies.Contains(filteredMaterials[i].custom_properties.manufacturer_company) && listOfTyping.companies.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.countries.Contains(filteredMaterials[i].custom_properties.manufacturer_country) && listOfTyping.countries.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.collection.Contains(filteredMaterials[i].custom_properties.collection) && listOfTyping.collection.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.coating_type.Contains(filteredMaterials[i].custom_properties.coating_type.ToString()) && listOfTyping.coating_type.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (!listOfTyping.model.Contains(filteredMaterials[i].custom_properties.model.ToString()) && listOfTyping.model.Count != 0)
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
            if (listOfTyping.color.Count > 0 && !IsColorContains(filteredMaterials[i].custom_properties.color))
            {
                filteredMaterials.Remove(filteredMaterials[i]);
                continue;
            }
        }
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        drumScroll.SetRectTransormSettings(filteredMaterials);
        drumScroll.filtration = true;
        drumScroll.filtrationList = filteredMaterials;
        NoMaterialErrorPage noMaterialErrorPage = FindObjectOfType<NoMaterialErrorPage>();
        if (filteredMaterials.Count == 0)
        {
            noMaterialErrorPage.EnableNoMaterialErrorPage();
        }
        else
        {
            noMaterialErrorPage.DisableNoMaterialErrorPage();
        }
    }

    public void nullFiltsResult()
    {
        DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
        if (drumScroll.contentFields.Count == 0)
        {
            drumScroll.popUpNullMaterial.SetActive(true);
        }
        else
            drumScroll.popUpNullMaterial.SetActive(false);
    }
}
