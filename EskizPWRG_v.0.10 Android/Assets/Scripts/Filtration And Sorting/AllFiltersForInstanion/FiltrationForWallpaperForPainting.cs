using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static itemMaterialForFilt;

public class FiltrationForWallpaperForPainting : MonoBehaviour
{
    [SerializeField]
    GameObject contentFieldFilt;

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

    public void CreateFiltersManufactureCompany()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < WallpaperForPaintingFilt.manufacturer_company.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = WallpaperForPaintingFilt.manufacturer_company[i];
                    contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "manufacturer_company";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<WallpapersForPainting>();
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

    public void CreateFiltersManufactureCountry()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < WallpaperForPaintingFilt.manufacturer_country.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = WallpaperForPaintingFilt.manufacturer_country[i];
                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "manufacturer_country";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<WallpapersForPainting>();
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
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < WallpaperForPaintingFilt.collection.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = WallpaperForPaintingFilt.collection[i];
                    contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "collection";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<WallpapersForPainting>();
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

    public void CreateFiltersWidth()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < WallpaperForPaintingFilt.pack_dimensions.x.Count; i++)
                {
                    
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = WallpaperForPaintingFilt.pack_dimensions.x[i].ToString();

                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.name = "width";
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<WallpapersForPainting>();
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

    public void CreateFiltersLength()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < WallpaperForPaintingFilt.pack_dimensions.y.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = WallpaperForPaintingFilt.pack_dimensions.y[i].ToString();

                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "length";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<WallpapersForPainting>();
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

        scrollbar.value = 1;
    }
}
