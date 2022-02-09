using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static itemMaterialForFilt;
using System.Linq;
using static DataTypes;

public class ValuePrise : MonoBehaviour
{
    [SerializeField]
    GameObject contentFieldFilt;

    public Sprite upArray;
    public Sprite downArray;
    public GameObject arrayGameObject;

    public TwinSlider twinSlider; 
    public Text oneSliderText;
    public Text twoSliderText;

    public ListOfTyping listOfTyping;
    public FilteringCoast filteringCoast;

    private void Awake()
    {
        //listOfTyping = GameObject.FindObjectOfType<ListOfTyping>();
    }

    public void CreateFiltersPrice()
    {
        arrayGameObject = transform.Find(gameObject.name).transform.Find("Expand").gameObject;
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            contentFieldFilt.SetActive(true);
            FillingPriceList();
            ValueScrollBar();
        }
        else 
        {
            arrayGameObject.GetComponent<Image>().sprite = downArray;
            contentFieldFilt.SetActive(false);
        }
    }
    public void ValueScrollBar()
    {
        Scrollbar scrollbar = FindObjectOfType<Scrollbar>();

        scrollbar.value = 1;
    }

    public void FillingPriceList()
    {
        twinSlider = contentFieldFilt.GetComponentInChildren<TwinSlider>();

        if (contentFieldFilt.GetComponent<Wallpapers>() != null) 
        {
            for (int i = 0; i<GlobalApplicationManager.Wallpapers.Count; i++) 
            {
                if (!listOfTyping.cost.Contains((int)GlobalApplicationManager.Wallpapers[i].cost))
                {
                    listOfTyping.cost.Add((int)(GlobalApplicationManager.Wallpapers[i].cost));

                }

                twinSlider.Max = listOfTyping.cost.Max();
                twinSlider.Min = listOfTyping.cost.Min();

                oneSliderText.text = twinSlider.Min.ToString();
                twoSliderText.text = twinSlider.Max.ToString();

                filteringCoast.mivalnValue = listOfTyping.cost.Min();
                filteringCoast.maxValue = listOfTyping.cost.Max();

            }
        }
        else if (contentFieldFilt.GetComponent<WallpapersForPainting>() != null)
        {
            for (int i = 0; i < GlobalApplicationManager.WallpapersForPainting.Count; i++)
            {
                if (!listOfTyping.cost.Contains((int)GlobalApplicationManager.WallpapersForPainting[i].cost))
                {
                    listOfTyping.cost.Add((int)GlobalApplicationManager.WallpapersForPainting[i].cost);

                }

                twinSlider.Max = listOfTyping.cost.Max();
                twinSlider.Min = listOfTyping.cost.Min();

                oneSliderText.text = twinSlider.Min.ToString();
                twoSliderText.text = twinSlider.Max.ToString();
            }
        }
        else if (contentFieldFilt.GetComponent<Laminates>() != null)
        {
            for (int i = 0; i < GlobalApplicationManager.Laminates.Count; i++)
            {
                if (!listOfTyping.cost.Contains((int)GlobalApplicationManager.Laminates[i].cost))
                {
                    listOfTyping.cost.Add((int)GlobalApplicationManager.Laminates[i].cost);

                }


                twinSlider.Max = listOfTyping.cost.Max();
                twinSlider.Min = listOfTyping.cost.Min();

                oneSliderText.text = twinSlider.Min.ToString();
                twoSliderText.text = twinSlider.Max.ToString();

                filteringCoast.mivalnValue = listOfTyping.cost.Min();
                filteringCoast.maxValue = listOfTyping.cost.Max();
            }
        }
        else if (contentFieldFilt.GetComponent<Linoleums>() != null)
        {
            for (int i = 0; i < GlobalApplicationManager.Linoleums.Count; i++)
            {
                if (!listOfTyping.cost.Contains((int)GlobalApplicationManager.Linoleums[i].cost))
                {
                    listOfTyping.cost.Add((int)GlobalApplicationManager.Linoleums[i].cost);

                }

                twinSlider.Max = listOfTyping.cost.Max();
                twinSlider.Min = listOfTyping.cost.Min();

                oneSliderText.text = twinSlider.Min.ToString();
                twoSliderText.text = twinSlider.Max.ToString();

                filteringCoast.mivalnValue = listOfTyping.cost.Min();
                filteringCoast.maxValue = listOfTyping.cost.Max();
            }
        }
        else if (contentFieldFilt.GetComponent<PVCs>() != null)
        {
            for (int i = 0; i < GlobalApplicationManager.PVCs.Count; i++)
            {
                if (!listOfTyping.cost.Contains((int)GlobalApplicationManager.PVCs[i].cost))
                {
                    listOfTyping.cost.Add((int)GlobalApplicationManager.PVCs[i].cost);

                }

                twinSlider.Max = listOfTyping.cost.Max();
                twinSlider.Min = listOfTyping.cost.Min();

                oneSliderText.text = twinSlider.Min.ToString();
                twoSliderText.text = twinSlider.Max.ToString();

                filteringCoast.mivalnValue = listOfTyping.cost.Min();
                filteringCoast.maxValue = listOfTyping.cost.Max();
            }
        }
        else if (contentFieldFilt.GetComponent<Baseboards>() != null)
        {
            for (int i = 0; i < GlobalApplicationManager.Baseboards.Count; i++)
            {
                if (!listOfTyping.cost.Contains((int)GlobalApplicationManager.Baseboards[i].cost))
                {
                    listOfTyping.cost.Add((int)GlobalApplicationManager.Baseboards[i].cost);

                }

                twinSlider.Max = listOfTyping.cost.Max();
                twinSlider.Min = listOfTyping.cost.Min();

                oneSliderText.text = twinSlider.Min.ToString();
                twoSliderText.text = twinSlider.Max.ToString();

                filteringCoast.mivalnValue = listOfTyping.cost.Min();
                filteringCoast.maxValue = listOfTyping.cost.Max();
            }
        }
        else if (contentFieldFilt.GetComponent<Doors>() != null)
        {
            for (int i = 0; i < GlobalApplicationManager.Doors.Count; i++)
            {
                if (!listOfTyping.cost.Contains((int)GlobalApplicationManager.Doors[i].cost))
                {
                    listOfTyping.cost.Add((int)GlobalApplicationManager.Doors[i].cost);

                }

                twinSlider.Max = listOfTyping.cost.Max();
                twinSlider.Min = listOfTyping.cost.Min();

                oneSliderText.text = twinSlider.Min.ToString();
                twoSliderText.text = twinSlider.Max.ToString();

                filteringCoast.mivalnValue = listOfTyping.cost.Min();
                filteringCoast.maxValue = listOfTyping.cost.Max();
            }
        }
        else if(contentFieldFilt.GetComponent<Painting>() != null)
        {
            
            for (int i = 0; i < GlobalApplicationManager.Paints.Count; i++)
            {
                if (!listOfTyping.cost.Contains((int)GlobalApplicationManager.Paints[i].cost))
                {
                    listOfTyping.cost.Add((int)GlobalApplicationManager.Paints[i].cost);

                }

                twinSlider.Max = listOfTyping.cost.Max();
                twinSlider.Min = listOfTyping.cost.Min();

                oneSliderText.text = twinSlider.Min.ToString();
                twoSliderText.text = twinSlider.Max.ToString();

                filteringCoast.mivalnValue = listOfTyping.cost.Min();
                filteringCoast.maxValue = listOfTyping.cost.Max();
            }
        }
    }

    public void ValueCoastRoom()
    {
        List<RoomData> roomDataJsons = new List<RoomData>(GlobalApplicationManager.MyRooms);
        for (int i = 0; i < roomDataJsons.Count; i++)
        {
            if (!listOfTyping.cost.Contains((int)roomDataJsons[i].Cost))
            {
                listOfTyping.cost.Add((int)roomDataJsons[i].Cost);
            }
            twinSlider.Max = listOfTyping.cost.Max();
            twinSlider.Min = listOfTyping.cost.Min();

            oneSliderText.text = twinSlider.Min.ToString();
            twoSliderText.text = twinSlider.Max.ToString();
            
            filteringCoast.mivalnValue = listOfTyping.cost.Min();
            filteringCoast.maxValue = listOfTyping.cost.Max();
        }
    }
}
