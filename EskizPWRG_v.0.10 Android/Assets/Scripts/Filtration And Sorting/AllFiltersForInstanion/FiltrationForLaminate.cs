using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static itemMaterialForFilt;

public class FiltrationForLaminate : MonoBehaviour
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
                scroltForActivate.SetActive(true);
                int index = transform.GetSiblingIndex();
                for (int i = 0; i < LaminateFilt.color.Count; i++)
                {
                    for (int j = 0; j < GlobalApplicationManager.Colors.Count; j++)
                    {
                        if (LaminateFilt.color[i] == GlobalApplicationManager.Colors[j].id)
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
                    contentPrefab.AddComponent<Laminates>();
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
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();
                
                for (int i = 0; i < LaminateFilt.manufacturer_company.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = LaminateFilt.manufacturer_company[i];
                    contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "manufacturer_company";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<Laminates>();
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

                for (int i = 0; i < LaminateFilt.manufacturer_country.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = LaminateFilt.manufacturer_country[i];
                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "manufacturer_country";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<Laminates>();
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
                for (int i = 0; i < LaminateFilt.collection.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = LaminateFilt.collection[i];
                    contentPrefab = Instantiate(contentFieldFilt, ParentForContent.transform);
                    contentPrefab.name = "collection";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<Laminates>();
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

    public void CreateFiltersBoard_thicknes()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < LaminateFilt.board_thickness.Count; i++)
                {
                    {
                        contentFieldFilt.transform.GetComponentInChildren<Text>().text = LaminateFilt.board_thickness[i].ToString();

                        GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                        contentPrefab.name = "board_thickness";
                        contentPrefab.transform.SetSiblingIndex(index + 1);
                        indexClick = 1;
                        ContentForFilt.Add(contentPrefab);
                        contentPrefab.AddComponent<Laminates>();
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

    public void CreateFiltersChamfer()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < LaminateFilt.chamfer.Count; i++)
                {
                    if (LaminateFilt.chamfer[i].ToString() == "False")
                    {
                        contentFieldFilt.transform.GetComponentInChildren<Text>().text = "Нет";
                    }
                    else
                        contentFieldFilt.transform.GetComponentInChildren<Text>().text = "Да";

                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "chamfer";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<Laminates>();
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
    public void CreateFiltersMoisture_resistant()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < LaminateFilt.moisture_resistant.Count; i++)
                {
                    if (LaminateFilt.moisture_resistant[i].ToString() == "False")
                    {
                        contentFieldFilt.transform.GetComponentInChildren<Text>().text = "Нет";
                    }
                    else
                        contentFieldFilt.transform.GetComponentInChildren<Text>().text = "Да";

                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "moisture_resistant";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<Laminates>();
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
