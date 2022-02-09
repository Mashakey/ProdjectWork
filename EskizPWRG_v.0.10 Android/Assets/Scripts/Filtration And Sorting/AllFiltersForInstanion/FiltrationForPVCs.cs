using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static itemMaterialForFilt;

public class FiltrationForPVCs : MonoBehaviour
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
                for (int i = 0; i < PVCsFilt.color.Count; i++)
                {
                    for (int j = 0; j < GlobalApplicationManager.Colors.Count; j++)
                    {
                        if (PVCsFilt.color[i] == GlobalApplicationManager.Colors[j].id)
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
                    contentPrefab.AddComponent<PVCs>();
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

                for (int i = 0; i < PVCsFilt.manufacturer_company.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = PVCsFilt.manufacturer_company[i];
                    contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "manufacturer_company";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<PVCs>();
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

                for (int i = 0; i < PVCsFilt.manufacturer_country.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = PVCsFilt.manufacturer_country[i];
                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "manufacturer_country";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<PVCs>();
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
                for (int i = 0; i < PVCsFilt.collection.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = PVCsFilt.collection[i];
                    contentPrefab = Instantiate(contentFieldFilt, ParentForContent.transform);
                    contentPrefab.name = "collection";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<PVCs>();
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

    public void CreateFiltersChamfer()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < PVCsFilt.chamfer.Count; i++)
                {
                    
                    if (PVCsFilt.chamfer[i].ToString() == "False")
                    {
                        contentFieldFilt.transform.GetComponentInChildren<Text>().text = "Нет";
                    }
                    else
                        contentFieldFilt.transform.GetComponentInChildren<Text>().text = "Да";

                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.name = "chamfer";
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<PVCs>();
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

    public void CreateFiltersProtective_layer_thickness()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < PVCsFilt.protective_layer_thickness.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = PVCsFilt.protective_layer_thickness[i].ToString();

                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "protective_layer_thickness";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<PVCs>();
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
    public void CreateFiltersBoard_thickness()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            if (indexClick == 0)
            {
                int index = transform.GetSiblingIndex();

                for (int i = 0; i < PVCsFilt.board_thickness.Count; i++)
                {
                    contentFieldFilt.transform.GetComponentInChildren<Text>().text = PVCsFilt.board_thickness[i].ToString();

                    GameObject contentPrefab = Instantiate(contentFieldFilt, gameObject.transform.parent);
                    contentPrefab.transform.SetSiblingIndex(index + 1);
                    contentPrefab.name = "board_thickness";
                    indexClick = 1;
                    ContentForFilt.Add(contentPrefab);
                    contentPrefab.AddComponent<PVCs>();
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
