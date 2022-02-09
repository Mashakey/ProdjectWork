using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class RoomTemplatesFiller : MonoBehaviour
{
    [SerializeField]
    GameObject roomTemplatePrefab;
    [SerializeField]
    GameObject roomTemplatesPage;
    [SerializeField]
    GameObject EditorRoomPage;

    List<LegacyRoomData> legacyRoomsDataJSONs;
    List<GameObject> templateFields = new List<GameObject>();

    public void CreateFieldsWithOutSort()
    {
        legacyRoomsDataJSONs = DataCacher.GetCachedLegacyRoomDataJSONs();
        GlobalApplicationManager.RoomTemplates = legacyRoomsDataJSONs;
        CreateRoomTemplatesList(legacyRoomsDataJSONs);
    }

    // Start is called before the first frame update
    public void CreateRoomTemplatesList(List<LegacyRoomData> legacyRoomsDataJSONs)
    {
        ResetTemplatesPage();
        Debug.LogWarning("LEGACY COUNT = " + legacyRoomsDataJSONs.Count);
        for (int i = 0; i < legacyRoomsDataJSONs.Count; i++)
        {
            GameObject roomTemplate = Instantiate(roomTemplatePrefab, roomTemplatesPage.transform);
            templateFields.Add(roomTemplate);
            roomTemplate.name = legacyRoomsDataJSONs[i].id;
            Transform roomName = roomTemplate.transform.GetChild(1);
            roomName.GetComponent<UnityEngine.UI.Text>().text = legacyRoomsDataJSONs[i].name;
            Transform roomType = roomTemplate.transform.GetChild(2);
            roomType.GetComponent<UnityEngine.UI.Text>().text = "Прямоугольная";
            Transform priceParent = roomTemplate.transform.GetChild(3);
            Transform price = priceParent.GetChild(0);
            price.GetComponent<UnityEngine.UI.Text>().text = legacyRoomsDataJSONs[i].estimate.ToString() + " \u20BD";
            Texture2D previewTexture = DataCacher.GetCachedLegacyRoomDataPreview(legacyRoomsDataJSONs[i]);
            Transform image = roomTemplate.transform.GetChild(0);
            Transform imageRoom = image.transform.GetChild(0);
            Transform downRoomImage = imageRoom.transform.GetChild(0);

            Sprite preview = Sprite.Create(previewTexture, new Rect(0.0f, 0.0f, previewTexture.width, previewTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
            downRoomImage.GetComponent<UnityEngine.UI.Image>().sprite = preview;
        }
    }

    void ResetTemplatesPage()
    {
        foreach (var templateField in templateFields)
        {
            Destroy(templateField.gameObject);
        }
        templateFields.Clear();
    }

    public void EnterEditorPage()
    {
        EditorRoomPage.SetActive(true);
        gameObject.SetActive(false);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
