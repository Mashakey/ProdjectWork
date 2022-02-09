using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;


public class FilteringMaterialRoom : MonoBehaviour
{
    public GameObject check;
    List<MaterialJSON> wallpapers = new List<MaterialJSON>(GlobalApplicationManager.Wallpapers);
    List<MaterialJSON> wallpapersForFilteers = ScriptingLists.WallpapersForFilteers;
    string selectFilt;
    public bool favorites = false;

    public ListOfTyping listOfTyping;

    public FilteringOfMaterials filteringOfMaterials;
    DrumScroll drumScroll;

    Color unActiveColor;
    Color ActiveColor;

    private void Awake()
    {
        drumScroll = FindObjectOfType<DrumScroll>();
        ColorUtility.TryParseHtmlString("#868686", out unActiveColor);
        ColorUtility.TryParseHtmlString("#FF9900", out ActiveColor);
    }

    public void FilteringByTheSelectedView()
    {
        if (check.activeSelf == false)
        {
            drumScroll.selectFilt += 1;
            drumScroll.filters.GetComponent<Image>().color = ActiveColor;
            if (gameObject.GetComponentInParent<Wallpapers>() != null)
            {
                listOfTyping = GameObject.Find("WallpaperFilt").GetComponent<ListOfTyping>();
                check.SetActive(true);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;
                 
                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Add(selectFilt);
                }
                else if (parenSelectFilters == "color")
                {
                    selectFilt = gameObject.transform.parent.name;
                    listOfTyping.color.Add(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Add(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Add(selectFilt);
                }
                else if (parenSelectFilters == "width")
                {
                    listOfTyping.width.Add(selectFilt);
                }
                else if (parenSelectFilters == "length")
                {
                    listOfTyping.length.Add(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsWallpapers(FavoritesStorage.Wallpapers);

                }
                else
                    filteringOfMaterials.FilterMaterialsWallpapers(GlobalApplicationManager.Wallpapers);
            }
            else if (gameObject.GetComponentInParent<WallpapersForPainting>() != null)
            {
                listOfTyping = GameObject.Find("WallpaperForPaintingFilt").GetComponent<ListOfTyping>();
                check.SetActive(true);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Add(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Add(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Add(selectFilt);
                }
                else if (parenSelectFilters == "width")
                {
                    listOfTyping.width.Add(selectFilt);
                }
                else if (parenSelectFilters == "length")
                {
                    listOfTyping.length.Add(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsWallpapersOfPainting(FavoritesStorage.WallpapersForPainting);
                }
                else
                    filteringOfMaterials.FilterMaterialsWallpapersOfPainting(GlobalApplicationManager.WallpapersForPainting);
            }
            else if (gameObject.GetComponentInParent<Laminates>() != null)
            {
                listOfTyping = GameObject.Find("LaminateFilt").GetComponent<ListOfTyping>();
                check.SetActive(true);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Add(selectFilt);
                }
                else if (parenSelectFilters == "color")
                {
                    selectFilt = gameObject.transform.parent.name;
                    listOfTyping.color.Add(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Add(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Add(selectFilt);
                }
                else if (parenSelectFilters == "board_thickness")
                {
                    listOfTyping.board_thickness.Add(selectFilt);
                }
                else if (parenSelectFilters == "chamfer")
                {
                    listOfTyping.chamfer.Add(selectFilt);
                }
                else if (parenSelectFilters == "moisture_resistant")
                {
                    listOfTyping.moisture_resistant.Add(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsLaminates(FavoritesStorage.Laminates);
                }
                else
                    filteringOfMaterials.FilterMaterialsLaminates(GlobalApplicationManager.Laminates);
            }
            else if (gameObject.GetComponentInParent<Linoleums>() != null)
            {
                listOfTyping = GameObject.Find("LinoleumFilt").GetComponent<ListOfTyping>();
                check.SetActive(true);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Add(selectFilt);
                }
                else if (parenSelectFilters == "color")
                {
                    selectFilt = gameObject.transform.parent.name;
                    listOfTyping.color.Add(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Add(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Add(selectFilt);
                }
                else if (parenSelectFilters == "total_thickness")
                {
                    listOfTyping.total_thickness.Add(selectFilt);
                }
                else if (parenSelectFilters == "zc_thickness")
                {
                    listOfTyping.zc_thickness.Add(selectFilt);
                }
                else if (parenSelectFilters == "design_type")
                {
                    listOfTyping.design_type.Add(selectFilt);
                }
                else if (parenSelectFilters == "use")
                {
                    listOfTyping.use.Add(selectFilt);
                }
                else if (parenSelectFilters == "basis")
                {
                    listOfTyping.basis.Add(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsLinoleums(FavoritesStorage.Linoleums);
                }
                else
                    filteringOfMaterials.FilterMaterialsLinoleums(GlobalApplicationManager.Linoleums);
            }
            else if (gameObject.GetComponentInParent<PVCs>() != null)
            {
                listOfTyping = GameObject.Find("PVCsFilt").GetComponent<ListOfTyping>();
                check.SetActive(true);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Add(selectFilt);
                }
                else if (parenSelectFilters == "color")
                {
                    selectFilt = gameObject.transform.parent.name;
                    listOfTyping.color.Add(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Add(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Add(selectFilt);
                }
                else if (parenSelectFilters == "chamfer")
                {
                    listOfTyping.chamfer.Add(selectFilt);
                }
                else if (parenSelectFilters == "protective_layer_thickness")
                {
                    listOfTyping.protective_layer_thickness.Add(selectFilt);
                }
                else if (parenSelectFilters == "board_thickness")
                {
                    listOfTyping.board_thickness.Add(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsPVC(FavoritesStorage.PVCs);
                }
                else
                    filteringOfMaterials.FilterMaterialsPVC(GlobalApplicationManager.PVCs);
            }
            else if (gameObject.GetComponentInParent<Baseboards>() != null)
            {
                listOfTyping = GameObject.Find("BaseboardFilt").GetComponent<ListOfTyping>();
                check.SetActive(true);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Add(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Add(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Add(selectFilt);
                }
                else if (parenSelectFilters == "material")
                {
                    listOfTyping.material.Add(selectFilt);
                }
                else if (parenSelectFilters == "length")
                {
                    listOfTyping.length.Add(selectFilt);
                }
                else if (parenSelectFilters == "height")
                {
                    listOfTyping.height.Add(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsBaseboards(FavoritesStorage.Baseboards);
                }
                else
                    filteringOfMaterials.FilterMaterialsBaseboards(GlobalApplicationManager.Baseboards);
            }
            else if (gameObject.GetComponentInParent<Doors>() != null)
            {
                listOfTyping = GameObject.Find("DoorFilt").GetComponent<ListOfTyping>();
                check.SetActive(true);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Add(selectFilt);
                }
                else if (parenSelectFilters == "color")
                {
                    selectFilt = gameObject.transform.parent.name;
                    listOfTyping.color.Add(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Add(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Add(selectFilt);
                }
                else if (parenSelectFilters == "coating_type")
                {
                    listOfTyping.coating_type.Add(selectFilt);
                }
                else if (parenSelectFilters == "model")
                {
                    listOfTyping.model.Add(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsDoors(FavoritesStorage.Doors);
                }
                else
                    filteringOfMaterials.FilterMaterialsDoors(GlobalApplicationManager.Doors);
            }
        }
        else
        {

            drumScroll.selectFilt -= 1;
            if (drumScroll.selectFilt == 0 && drumScroll.twinSlider.activePrise != true)
            {
                drumScroll.filters.GetComponent<Image>().color = unActiveColor;
            }

            if (gameObject.GetComponentInParent<Wallpapers>() != null)
            {
                listOfTyping = GameObject.Find("WallpaperFilt").GetComponent<ListOfTyping>();
                check.SetActive(false);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Remove(selectFilt);
                }
                else if (parenSelectFilters == "color")
                {
                    selectFilt = gameObject.transform.parent.name;
                    listOfTyping.color.Remove(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Remove(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Remove(selectFilt);
                }
                else if (parenSelectFilters == "width")
                {
                    listOfTyping.width.Remove(selectFilt);
                }
                else if (parenSelectFilters == "length")
                {
                    listOfTyping.length.Remove(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsWallpapers(FavoritesStorage.Wallpapers);
                }
                else
                    filteringOfMaterials.FilterMaterialsWallpapers(GlobalApplicationManager.Wallpapers);
            }
            else if (gameObject.GetComponentInParent<WallpapersForPainting>() != null)
            {
                listOfTyping = GameObject.Find("WallpaperForPaintingFilt").GetComponent<ListOfTyping>();
                check.SetActive(false);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Remove(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Remove(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Remove(selectFilt);
                }
                else if (parenSelectFilters == "width")
                {
                    listOfTyping.width.Remove(selectFilt);
                }
                else if (parenSelectFilters == "length")
                {
                    listOfTyping.length.Remove(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsWallpapersOfPainting(FavoritesStorage.WallpapersForPainting);
                }
                else
                    filteringOfMaterials.FilterMaterialsWallpapersOfPainting(GlobalApplicationManager.WallpapersForPainting);
            }
            else if (gameObject.GetComponentInParent<Laminates>() != null)
            {
                listOfTyping = GameObject.Find("LaminateFilt").GetComponent<ListOfTyping>();
                check.SetActive(false);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Remove(selectFilt);
                }
                else if (parenSelectFilters == "color")
                {
                    selectFilt = gameObject.transform.parent.name;
                    listOfTyping.color.Remove(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Remove(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Remove(selectFilt);
                }
                else if (parenSelectFilters == "board_thickness")
                {
                    listOfTyping.board_thickness.Remove(selectFilt);
                }
                else if (parenSelectFilters == "chamfer")
                {
                    listOfTyping.chamfer.Remove(selectFilt);
                }
                else if (parenSelectFilters == "moisture_resistant")
                {
                    listOfTyping.moisture_resistant.Remove(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsLaminates(FavoritesStorage.Laminates);
                }
                else
                    filteringOfMaterials.FilterMaterialsLaminates(GlobalApplicationManager.Laminates);
            }
            else if (gameObject.GetComponentInParent<Linoleums>() != null)
            {
                listOfTyping = GameObject.Find("LinoleumFilt").GetComponent<ListOfTyping>();
                check.SetActive(false);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Remove(selectFilt);
                }
                else if (parenSelectFilters == "color")
                {
                    selectFilt = gameObject.transform.parent.name;
                    listOfTyping.color.Remove(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Remove(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Remove(selectFilt);
                }
                else if (parenSelectFilters == "total_thickness")
                {
                    listOfTyping.total_thickness.Remove(selectFilt);
                }
                else if (parenSelectFilters == "zc_thickness")
                {
                    listOfTyping.zc_thickness.Remove(selectFilt);
                }
                else if (parenSelectFilters == "design_type")
                {
                    listOfTyping.design_type.Remove(selectFilt);
                }
                else if (parenSelectFilters == "use")
                {
                    listOfTyping.use.Remove(selectFilt);
                }
                else if (parenSelectFilters == "basis")
                {
                    listOfTyping.basis.Remove(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsLinoleums(FavoritesStorage.Linoleums);
                }
                else
                    filteringOfMaterials.FilterMaterialsLinoleums(GlobalApplicationManager.Linoleums);
            }
            else if (gameObject.GetComponentInParent<PVCs>() != null)
            {
                listOfTyping = GameObject.Find("PVCsFilt").GetComponent<ListOfTyping>();
                check.SetActive(false);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Remove(selectFilt);
                }
                else if (parenSelectFilters == "color")
                {
                    selectFilt = gameObject.transform.parent.name;
                    listOfTyping.color.Remove(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Remove(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Remove(selectFilt);
                }
                else if (parenSelectFilters == "chamfer")
                {
                    listOfTyping.chamfer.Remove(selectFilt);
                }
                else if (parenSelectFilters == "protective_layer_thickness")
                {
                    listOfTyping.protective_layer_thickness.Remove(selectFilt);
                }
                else if (parenSelectFilters == "board_thickness")
                {
                    listOfTyping.board_thickness.Remove(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsPVC(FavoritesStorage.PVCs);
                }
                else
                    filteringOfMaterials.FilterMaterialsPVC(GlobalApplicationManager.PVCs);
            }
            else if (gameObject.GetComponentInParent<Baseboards>() != null)
            {
                listOfTyping = GameObject.Find("BaseboardFilt").GetComponent<ListOfTyping>();
                check.SetActive(false);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Remove(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Remove(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Remove(selectFilt);
                }
                else if (parenSelectFilters == "material")
                {
                    listOfTyping.material.Remove(selectFilt);
                }
                else if (parenSelectFilters == "length")
                {
                    listOfTyping.length.Remove(selectFilt);
                }
                else if (parenSelectFilters == "height")
                {
                    listOfTyping.height.Remove(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsBaseboards(FavoritesStorage.Baseboards);
                }
                else
                    filteringOfMaterials.FilterMaterialsBaseboards(GlobalApplicationManager.Baseboards);
            }
            else if (gameObject.GetComponentInParent<Doors>() != null)
            {
                listOfTyping = GameObject.Find("DoorFilt").GetComponent<ListOfTyping>();
                check.SetActive(false);
                selectFilt = gameObject.GetComponentInParent<Text>().text;
                string parenSelectFilters = gameObject.transform.parent.transform.parent.name;

                if (parenSelectFilters == "manufacturer_company")
                {
                    listOfTyping.companies.Remove(selectFilt);
                }
                else if (parenSelectFilters == "color")
                {
                    selectFilt = gameObject.transform.parent.name;
                    listOfTyping.color.Remove(selectFilt);
                }
                else if (parenSelectFilters == "manufacturer_country")
                {
                    listOfTyping.countries.Remove(selectFilt);
                }
                else if (parenSelectFilters == "collection")
                {
                    listOfTyping.collection.Remove(selectFilt);
                }
                else if (parenSelectFilters == "coating_type")
                {
                    listOfTyping.coating_type.Remove(selectFilt);
                }
                else if (parenSelectFilters == "model")
                {
                    listOfTyping.model.Remove(selectFilt);
                }
                if (FindObjectOfType<FavoriteStatusSetter>().favorires == true)
                {
                    filteringOfMaterials.FilterMaterialsDoors(FavoritesStorage.Doors);
                }
                else
                    filteringOfMaterials.FilterMaterialsDoors(GlobalApplicationManager.Doors);
            }
        }
    }
}
