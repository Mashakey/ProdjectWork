using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using static DataTypes;

public class MyRoomsFiller : MonoBehaviour
{
    [SerializeField]
    Transform RoomFieldPrefab;

    [SerializeField]
    Transform RoomsPage;

    [SerializeField]
    RectTransform RoomsPageRectTransform;

    [SerializeField]
    Transform RedactorPage;

    [SerializeField]
    Text ZeroRoomsText;

    [SerializeField]
    GameObject CircleLoader;

    public List<RoomData> roomDataJsons;

    List<Transform> roomFields = new List<Transform>();

    string _cachePath;

    public int selectFilt;
    public GameObject filters;
    public TwinSlider twinSlider;

    private void OnEnable()
    {
        Debug.LogWarning("My rooms are enabled");
        roomDataJsons = DataCacher.GetCachedRoomDataJsons();
        GlobalApplicationManager.MyRooms = roomDataJsons;
        //CreateRoomsFields(roomDataJsons);
        StartCoroutine(CreateRoomsFieldsCoroutine(roomDataJsons));

    }

    public IEnumerator CreateRoomsFieldsCoroutine(List<RoomData> roomDataJsons)
    {
        CircleLoader.SetActive(true);

        Resources.UnloadUnusedAssets();
        ResetMyRoomsPage();
        Debug.LogWarning("Trying to create my rooms");
        SetZeroRoomsTextStatus();
        int index = 0;
        foreach (var roomDataJson in roomDataJsons)
        {
            Transform roomField = Instantiate(RoomFieldPrefab, RoomsPageRectTransform);
            roomFields.Add(roomField);
            //Debug.LogWarning("object is " + roomField.name);
            //Texture2D previewTexture = DataCacher.GetRoomPreview(roomDataJson);
            //Sprite previewSprite = Sprite.Create(previewTexture, new Rect(0.0f, 0.0f, previewTexture.width, previewTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
            Transform image = roomField.Find("Image");
            Transform imageRoom = image.Find("ImageRoom");
            //imageRoom.Find("DownRoomImage").GetComponent<Image>().sprite = previewSprite;
            //StartCoroutine(GetAndSetRoomPreviewFromCache(roomDataJson, imageRoom.Find("DownRoomImage").GetComponent<Image>()));
            StartCoroutine(DataCacher.GetRoomPreviewCoroutine(roomDataJson, imageRoom.Find("DownRoomImage").GetComponent<Image>()));



            Text roomNameField = roomField.Find("NameRoom").GetComponent<Text>();
            roomNameField.text = roomDataJson.Name;
            Text roomTypeField = roomField.Find("TypeRoom").GetComponent<Text>();
            roomTypeField.text = TypeTranslator.GetRoomInteriorTypeRussianTranslation(roomDataJson.Interior);
            Transform roomPriceField = roomField.Find("Price");
            Text roomPriceTextField = roomPriceField.Find("PriceText").GetComponent<Text>();
            roomPriceTextField.text = roomDataJson.Cost.ToString() + " \u20BD";
            RoomsPageRectTransform.offsetMin = new Vector2(RoomsPageRectTransform.offsetMin.x, RoomsPageRectTransform.offsetMin.y - 85);
            //roomField.gameObject.AddComponent<CreatedRoomIndex>().RoomIndex = index;
            roomField.GetComponent<CreatedRoomIndex>().RoomIndex = index;
            index++;
            yield return (null);
        }
        CircleLoader.SetActive(false);
    }

    public void CreateRoomsFields(List<RoomData> roomDataJsons)
    {
        Resources.UnloadUnusedAssets();
        ResetMyRoomsPage();
        Debug.LogWarning("Trying to create my rooms");
        SetZeroRoomsTextStatus();
        int index = 0;
        foreach (var roomDataJson in roomDataJsons)
        {
            Transform roomField = Instantiate(RoomFieldPrefab, RoomsPageRectTransform);
            roomFields.Add(roomField);
            //Debug.LogWarning("object is " + roomField.name);
            //Texture2D previewTexture = DataCacher.GetRoomPreview(roomDataJson);
            //Sprite previewSprite = Sprite.Create(previewTexture, new Rect(0.0f, 0.0f, previewTexture.width, previewTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
            Transform image = roomField.Find("Image");
            Transform imageRoom = image.Find("ImageRoom");
            //imageRoom.Find("DownRoomImage").GetComponent<Image>().sprite = previewSprite;
            //StartCoroutine(GetAndSetRoomPreviewFromCache(roomDataJson, imageRoom.Find("DownRoomImage").GetComponent<Image>()));
            StartCoroutine(DataCacher.GetRoomPreviewCoroutine(roomDataJson, imageRoom.Find("DownRoomImage").GetComponent<Image>()));



            Text roomNameField = roomField.Find("NameRoom").GetComponent<Text>();
            roomNameField.text = roomDataJson.Name;
            Text roomTypeField = roomField.Find("TypeRoom").GetComponent<Text>();
            roomTypeField.text = TypeTranslator.GetRoomInteriorTypeRussianTranslation(roomDataJson.Interior);
            Transform roomPriceField = roomField.Find("Price");
            Text roomPriceTextField = roomPriceField.Find("PriceText").GetComponent<Text>();
            roomPriceTextField.text = roomDataJson.Cost.ToString() + " \u20BD";
            RoomsPageRectTransform.offsetMin = new Vector2(RoomsPageRectTransform.offsetMin.x, RoomsPageRectTransform.offsetMin.y - 85);
            //roomField.gameObject.AddComponent<CreatedRoomIndex>().RoomIndex = index;
            roomField.GetComponent<CreatedRoomIndex>().RoomIndex = index;
            index++;
        }
    }

    void ResetMyRoomsPage()
    {
        foreach (var roomField in roomFields)
        {
            Destroy(roomField.gameObject);
        }
        roomFields.Clear();
    }

    void SetZeroRoomsTextStatus()
    {
        if (roomDataJsons.Count > 0)
        {
            ZeroRoomsText.gameObject.SetActive(false);
        }
        else
        {
            ZeroRoomsText.gameObject.SetActive(true);
        }
    }

    public void DeactivateRoomsPage()
    {
        //RedactorPage.gameObject.SetActive(true);
        RoomsPage.gameObject.SetActive(false);

    }

    private void Awake()
    {
#if UNITY_EDITOR

        //_cachePath = Application.dataPath + "/Resources/";
        //_cachePath = "D:/EskizData/Resources/";
        //_cachePath = "D:/EskizDevData/Resources/";
        _cachePath = "D:/EskizDevDataAsync/Resources/";
#else
		_cachePath = Application.persistentDataPath + "/";                       
		//_cachePath = Path.Combine(Application.persistentDataPath, "Resources");                                
#endif
        Debug.LogWarning(_cachePath);
    }
}
