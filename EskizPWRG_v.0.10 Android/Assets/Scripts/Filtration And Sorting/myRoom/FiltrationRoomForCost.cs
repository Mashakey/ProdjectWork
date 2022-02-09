using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using static DataCacher;
using static DataTypes;

public class FiltrationRoomForCost : MonoBehaviour
{
    [SerializeField]
    GameObject contentFieldFilt;

    public Sprite upArray;
    public Sprite downArray;
    public GameObject arrayGameObject;

    MyRoomsFiller myRoomsFiller;
    public TwinSlider twinSlider;
    listRoom listRoom;

    private void Awake()
    {
        myRoomsFiller = GameObject.FindObjectOfType<MyRoomsFiller>();

        listRoom = GameObject.FindObjectOfType<listRoom>();
    }

    public void CreateFiltersPrice()
    {
        if (arrayGameObject.GetComponent<Image>().sprite == downArray)
        {
            arrayGameObject.GetComponent<Image>().sprite = upArray;
            contentFieldFilt.SetActive(true);
            FillingPriceList();
        }
        else
        {
            arrayGameObject.GetComponent<Image>().sprite = downArray;
            contentFieldFilt.SetActive(false);
        }
    }

    public void FillingPriceList()
    {
        List<RoomData> roomDataJsons = DataCacher.GetCachedRoomDataJsons();
        //twinSlider = contentFieldFilt.GetComponentInChildren<TwinSlider>();

        for (int i = 0; i < roomDataJsons.Count; i++)
        {

            listRoom.cost.Add((int)(roomDataJsons[i].Cost));

            //twinSlider.Max = listRoom.cost.Max();
            //twinSlider.Min = listRoom.cost.Min();
            Debug.LogError(GetMaxRoomPrice());
            Debug.LogError(GetMinRoomPrice());
            twinSlider.Max = GetMaxRoomPrice();
            twinSlider.Min = GetMinRoomPrice();

        }
        
    }

    public float GetMinRoomPrice()
    {
        float minPrice = 99999999f;
        List<RoomData> roomDataJsons = DataCacher.GetCachedRoomDataJsons();
        foreach(var room in roomDataJsons)
        {
       
            if (room.Cost < minPrice)
            {
                minPrice = room.Cost;
            }
            
        }
        return minPrice;
    }

    public float GetMaxRoomPrice()
    {
        float maxPrice = 0f;
        List<RoomData> roomDataJsons = DataCacher.GetCachedRoomDataJsons();
        foreach (var room in roomDataJsons)
        {
            if (room.Cost > maxPrice)
            {
                maxPrice = room.Cost;
            }
        }
        return maxPrice;
    }
}
