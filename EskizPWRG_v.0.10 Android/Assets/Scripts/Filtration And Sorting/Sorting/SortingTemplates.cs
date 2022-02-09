using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataCacher;
using static DataTypes;
using UnityEngine.UI;

public class SortingTemplates : MonoBehaviour
{
    //legacyRoomsDataJSONs = DataCacher.GetCachedLegacyRoomDataJSONs();

    Color orangeColor;
    RoomTemplatesFiller roomTemplatesFiller;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#FF9900", out orangeColor);
        roomTemplatesFiller = GameObject.FindObjectOfType<RoomTemplatesFiller>();
    }


    public void SortRoomsByCostAscending(GameObject selectButton)
    {
         List<LegacyRoomData> legacyRoomsDataJSONs = new List<LegacyRoomData>(GlobalApplicationManager.RoomTemplates);

        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            legacyRoomsDataJSONs.Sort((x, y) => x.estimate.CompareTo(y.estimate));
            roomTemplatesFiller.CreateRoomTemplatesList(legacyRoomsDataJSONs);
        }
        else
        {
            roomTemplatesFiller.CreateRoomTemplatesList(legacyRoomsDataJSONs);
        }
    }

    public void SortRoomsByCostDescending(GameObject selectButton)
    {
        List<LegacyRoomData> legacyRoomsDataJSONs = new List<LegacyRoomData>(GlobalApplicationManager.RoomTemplates);

        if (selectButton.GetComponent<Image>().color == orangeColor)
        {
            legacyRoomsDataJSONs.Sort((x, y) => -x.estimate.CompareTo(y.estimate));
            roomTemplatesFiller.CreateRoomTemplatesList(legacyRoomsDataJSONs);
        }
        else
        {
            roomTemplatesFiller.CreateRoomTemplatesList(legacyRoomsDataJSONs);
        }
    }
}
