using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static itemMaterialForFilt;

public class FiltrationForWallpaper : MonoBehaviour
{
    [SerializeField]
    GameObject contentFieldFilt;
    string idColor;

    public Sprite upArray;
    public Sprite downArray;

    public int indexClick = 0;

    public List<GameObject> ContentForFilt = new List<GameObject>();
    public GameObject ParentForContent;
    GameObject contentPrefab;
    public GameObject arrayGameObject;

    public GameObject scroltForActivate;
    DrumScroll drumScroll;

    private void Awake()
    {
        drumScroll = FindObjectOfType<DrumScroll>();
    }

    public void CreateFiltersColor()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject; 
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            scroltForActivate.SetActive(true);
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();
                for (int i =0; i < WallpaperFilt.color.Count; i++)
                {
                    for (int j = 0; j < GlobalApplicationManager.Colors.Count; j++)
                    {
                        if (WallpaperFilt.color[i] == GlobalApplicationManager.Colors[j].id)
                        {
                            contentFieldFilt.transform.GetComponentInChildren<Text>().text = GlobalApplicationManager.Colors[j].name;
                            idColor = GlobalApplicationManager.Colors[j].id; 
                            
                        }
                    }
                    contentPrefab = Instantiate(contentFieldFilt, ParentForContent.transform);
                    contentPrefab.transform.GetChild(0).name = idColor;
                    contentPrefab.name = "color";
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<Wallpapers>();
                    drumScroll.InstantiatePrefabs.Add(contentPrefab);

                }
                changeVerticleSize();
                ValueScrollBar();
            }
            else
            {
                for (int i = 0; i < ContentForFilt.Count; i++)
                {
                    ContentForFilt[i].SetActive(true);
                }
            }
        }
        else
        {
            arrayGameObject.GetComponent<Image>().sprite = downArray;
            scroltForActivate.SetActive(false);
        }
    }

    public void CreateFiltersManufactureCompany()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            scroltForActivate.SetActive(true);
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {               
                for (int i = 0; i < WallpaperFilt.manufacturer_company.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = WallpaperFilt.manufacturer_company[i];
                    contentPrefab = Instantiate(contentFieldFilt, ParentForContent.transform);
                    contentPrefab.name = "manufacturer_company";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<Wallpapers>();
                    drumScroll.InstantiatePrefabs.Add(contentPrefab);
                }
                changeVerticleSize();
                ValueScrollBar();
            }
            else
            {
                for (int i = 0; i < ContentForFilt.Count; i++)
                {
                    ContentForFilt[i].SetActive(true);
                }
            }
        }
        else
        {
            arrayGameObject.GetComponent<Image>().sprite = downArray;
            scroltForActivate.SetActive(false);
        }

    }

    public void CreateFiltersManufactureCountry()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < WallpaperFilt.manufacturer_country.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = WallpaperFilt.manufacturer_country[i];
                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "manufacturer_country";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<Wallpapers>();
                    drumScroll.InstantiatePrefabs.Add(contentPrefab);
                }
                ValueScrollBar();
            }
            else
            {
                for (int i = 0; i < ContentForFilt.Count; i++)
                {
                    ContentForFilt[i].SetActive(true);
                }
            }
        }
        else
        {
            arrayGameObject.GetComponent<Image>().sprite = downArray;
            for (int i = 0; i < ContentForFilt.Count; i++)
            {
                ContentForFilt[i].SetActive(false);
            }
        }
    }

    public void CreateFiltersCollection()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            scroltForActivate.SetActive(true);
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                for (int i = 0; i < WallpaperFilt.collection.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = WallpaperFilt.collection[i];
                    contentPrefab = Instantiate(contentFieldFilt, ParentForContent.transform);
                    contentPrefab.name = "collection";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<Wallpapers>();
                    drumScroll.InstantiatePrefabs.Add(contentPrefab);
                }
                changeVerticleSize();
                ValueScrollBar();
            }
            else
            {
                for (int i = 0; i < ContentForFilt.Count; i++)
                {
                    ContentForFilt[i].SetActive(true);
                }
            }
        }
        else
        {
            arrayGameObject.GetComponent<Image>().sprite = downArray;
            scroltForActivate.SetActive(false);
        }
    }

    public void CreateFiltersWidth()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < WallpaperFilt.pack_dimensions.x.Count; i++)
                {
                    {
                        contentFieldFilt.transform.GetComponentInChildren<Text>().text = WallpaperFilt.pack_dimensions.x[i].ToString();

                        GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                        contentPrefab.name = "width";
                        contentPrefab.transform.SetSiblingIndex(index + 1);
                        indexClick = 1;
                        ContentForFilt.Add(contentPrefab);
                        contentPrefab.AddComponent<Wallpapers>();
                        drumScroll.InstantiatePrefabs.Add(contentPrefab);

                    }
                }
                ValueScrollBar();
            }
            else
            {
                for (int i = 0; i < ContentForFilt.Count; i++)
                {
                    ContentForFilt[i].SetActive(true);
                }
            }
        }
        else
        {
            arrayGameObject.GetComponent<Image>().sprite = downArray;
            for (int i = 0; i < ContentForFilt.Count; i++)
            {
                ContentForFilt[i].SetActive(false);
            }
        }
    }

    public void CreateFiltersLength()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < WallpaperFilt.pack_dimensions.y.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = WallpaperFilt.pack_dimensions.y[i].ToString();

                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "length";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<Wallpapers>();
                    drumScroll.InstantiatePrefabs.Add(contentPrefab);

                }
                ValueScrollBar();
            }
            else
            {
                for (int i = 0; i < ContentForFilt.Count; i++)
                {
                    ContentForFilt[i].SetActive(true);
                }
            }
        }
        else
        {
            arrayGameObject.GetComponent<Image>().sprite = downArray;
            for (int i = 0; i < ContentForFilt.Count; i++)
            {
                ContentForFilt[i].SetActive(false);
            }
        }

    }

    public void changeVerticleSize()
    {
        var heigthParent = ParentForContent.transform.childCount * contentPrefab.GetComponentInChildren<RectTransform>().sizeDelta.y;
        ParentForContent.GetComponent<RectTransform>().sizeDelta = new Vector2(ParentForContent.GetComponentInChildren<RectTransform>().sizeDelta.x, heigthParent);
    }

    public void ValueScrollBar()
    {
        Scrollbar scrollbar = FindObjectOfType<Scrollbar>();

        scrollbar.value = 0.98f;
    }
}