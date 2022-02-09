using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class GlobalApplicationManager : MonoBehaviour
{
    [SerializeField]
    GameObject startingLoadingScreen;

    static List<Transform> SelectedWalls = new List<Transform>();
    static List<Transform> SelectedObjects = new List<Transform>();

    public static List<LegacyRoomData> RoomTemplates = new List<LegacyRoomData>();
    public static List<RoomData> MyRooms = new List<RoomData>();

    public static List<MaterialJSON> allMaterialsJsons = new List<MaterialJSON>();

    public static List<MaterialJSON> Wallpapers = new List<MaterialJSON>();
    public static List<MaterialJSON> Laminates = new List<MaterialJSON>();
    public static List<MaterialJSON> Linoleums = new List<MaterialJSON>();
    public static List<MaterialJSON> Doors = new List<MaterialJSON>();
    public static List<MaterialJSON> PVCs = new List<MaterialJSON>();
    public static List<MaterialJSON> Baseboards = new List<MaterialJSON>();
    public static List<MaterialJSON> WallpapersForPainting = new List<MaterialJSON>();
    public static List<MaterialJSON> Paints = new List<MaterialJSON>();

    public static List<ColorJson> Colors = new List<ColorJson>();

    public static List<ShopJson> Shops = new List<ShopJson>();

    public static bool isStartedMovingJsonsToRam = false;

    public static bool isInnerRedactorPagesOpen = false;

    private void Awake()
    {
        isStartedMovingJsonsToRam = false;
    }
    public void MoveJsonsToRamAndCreateFilterValues()
    {
        if (!isStartedMovingJsonsToRam)
        {
            startingLoadingScreen.GetComponentInChildren<Text>().text = "Переносим материаы в оперативную память";
            isStartedMovingJsonsToRam = true;

            Colors = DataCacher.GetCachedColorJsons();

            allMaterialsJsons = DataCacher.GetCachedMaterialsJSONs();
            Wallpapers = MaterialFilter.GetOnlyWallpapers(allMaterialsJsons);
            Laminates = MaterialFilter.GetOnlyLaminates(allMaterialsJsons);
            Linoleums = MaterialFilter.GetOnlyLinoleums(allMaterialsJsons);
            Doors = MaterialFilter.GetOnlyDoors(allMaterialsJsons);
            PVCs = MaterialFilter.GetOnlyPVCs(allMaterialsJsons);
            Baseboards = MaterialFilter.GetOnlyBaseboards(allMaterialsJsons);
            WallpapersForPainting = MaterialFilter.GetOnlyWallpapersForPainting(allMaterialsJsons);
            Paints = MaterialFilter.GetOnlyPaints(allMaterialsJsons);

            Debug.LogWarning("All JSONs have been moved to RAM");

            InitializeApplicationValues();
        }
    }

    public void InitializeApplicationValues()
    {
        CreateFiltersValues();
        FavoritesStorage.LoadFavoritesFromCache();
        Debug.LogWarning("Loading is complete");

        StartCoroutine(WaitFor2SecondsAndTurnOffLoadingScreen());
        //startingLoadingScreen.SetActive(false);
    }

    public IEnumerator WaitFor2SecondsAndTurnOffLoadingScreen()
    {
        startingLoadingScreen.GetComponentInChildren<Text>().text = "Завершаем загрузку";
        yield return new WaitForSeconds(2);
        startingLoadingScreen.SetActive(false);
        yield break;
    }

    public void CreateFiltersValues()
    {
        FillingInFilterValues fillingInFilterValues = new FillingInFilterValues();
        fillingInFilterValues.FilterOnlyWallpaper();
        fillingInFilterValues.FilterOnlyWallpaperForPaintings();
        fillingInFilterValues.FilterOnlyLaminate();
        fillingInFilterValues.FilterOnlyLinoleums();
        fillingInFilterValues.FilterOnlyPVCs();
        fillingInFilterValues.FilterOnlyBaseboards();
        fillingInFilterValues.FilterOnlyDoors();
    }

    public static MaterialJSON GetMaterialJsonById(string materialID)
    {
        foreach (var materialJson in allMaterialsJsons)
        {
            if (materialJson.id == materialID)
            {
                return (materialJson);
            }
        }
        if (materialID == "DefaultWall")
        {
            MaterialJSON defaultWallMaterialJson = new MaterialJSON();
            defaultWallMaterialJson.id = "DefaultWall";
            return (defaultWallMaterialJson);
        }
        else if (materialID == "DefaultFloor")
        {
            MaterialJSON defaultFloorMaterialJson = new MaterialJSON();
            defaultFloorMaterialJson.id = "DefaultFloor";
            return (defaultFloorMaterialJson);
        }
        else if (materialID == "DefaultDoor")
        {
            MaterialJSON defaultDoorMaterialJson = new MaterialJSON();
            defaultDoorMaterialJson.id = "DefaultDoor";
            return (defaultDoorMaterialJson);
        }
        else
        {
            MaterialJSON defaultMaterialJson = new MaterialJSON();
            defaultMaterialJson.id = "Default";
            return (defaultMaterialJson);
        }
    }

    public static ShopJson GetShopJsonById(string shopId)
    {

        foreach (var shopJson in Shops)
        {
            if (shopJson.id == shopId)
            {
                return (shopJson);
            }
        }
        return (null);
    }

    public static void AddSelectedWall(Transform wall)
    {
        if (!SelectedWalls.Contains(wall))
            SelectedWalls.Add(wall);
    }

    public static void AddSelectedObject(Transform selectedObject)
    {
        if (!SelectedObjects.Contains(selectedObject))
        {
            SelectedObjects.Add(selectedObject);
        }
    }

    public static List<Transform> GetAndPopSelectedObjects()
    {
        List<Transform> selectedObjects = new List<Transform>();
        foreach (var selectedObject in SelectedObjects)
        {
            selectedObjects.Add(selectedObject);
        }
        SelectedObjects.Clear();
        return (selectedObjects);
    }

    public static void ClearSelectedObjects()
    {
        SelectedObjects.Clear();
    }

    private void Start()
    {
        isStartedMovingJsonsToRam = false;
        if (!isStartedMovingJsonsToRam)
        {
            startingLoadingScreen.SetActive(true);
            //StartCoroutine(CloseStartingLoaderScreen());
        }
        FindObjectOfType<ShopsDownloader>().StartDownload();
        FindObjectOfType<JsonServerDownloader>().StartDownload();

    }

    IEnumerator CloseStartingLoaderScreen()
    {
        yield return new WaitForSeconds(5f);
        //startingLoadingScreen.SetActive(false);
    }

    public static string GetStringRoomTypeFromEnum(RoomType roomType)
    {
        if (roomType == RoomType.Rectangle)
        {
            return ("Rectange");
        }
        else if (roomType == RoomType.G_type)
        {
            return ("G-type");
        }
        else if (roomType == RoomType.Trapezoidal)
        {
            return ("Trapezoidal");
        }
        else if (roomType == RoomType.T_type)
        {
            return ("T-type");
        }
        else if (roomType == RoomType.Z_type)
        {
            return ("Z-type");
        }
        else
        {
            Debug.LogError("Unknown room type. Type switched to rectangular");
            return ("Rectange");
        }
    }

    public static string GetRussianStringRoomTypeFromEnum(RoomType roomType)
    {
        if (roomType == RoomType.Rectangle)
        {
            return ("Прямоугольная");
        }
        else if (roomType == RoomType.G_type)
        {
            return ("Г-образная");
        }
        else if (roomType == RoomType.Trapezoidal)
        {
            return ("Трапецевидная");
        }
        else if (roomType == RoomType.T_type)
        {
            return ("Т-образная");
        }
        else if (roomType == RoomType.Z_type)
        {
            return ("Z-образная");
        }
        else
        {
            Debug.LogError("Unknown room type. Type switched to rectangular");
            return ("Прямоугольная");
        }
    }

    public static string GetRussinStringRoomInteriorTypeFromEnum(InteriorType interiorType)
    {
        if (interiorType == InteriorType.Bedroom)
        {
            return ("Спальня");
        }
        else if (interiorType == InteriorType.Hallway)
        {
            return ("Прихожая");
        }
        else if (interiorType == InteriorType.LivingRoom)
        {
            return ("Гостиная");
        }
        else if (interiorType == InteriorType.Nursery)
        {
            return ("Детская");
        }
        else
        {
            return ("Гостиная");
        }
    }

    private void OnApplicationPause(bool isPause)
    {
        if (isPause)
        {
            Debug.LogWarning("OnApplicationPause");
            Room currentRoom = FindObjectOfType<Room>();
            RoomCreator roomCreator = FindObjectOfType<RoomCreator>();
            if (currentRoom != null && roomCreator != null)
            {
                RoomCostCalculator.CalculateEstimate(currentRoom);
                RoomCostCalculator.CalculateRoomCost(currentRoom);
                DataCacher.CacheJsonRoomData(roomCreator.ConvertRoomToJSON(currentRoom), currentRoom.Name, ScreenshotMaker.MakeScreenshot());
            }
        }
    }

    private void OnApplicationQuit()
    {
        Debug.LogWarning("OnApplicationQuit");
        Room currentRoom = FindObjectOfType<Room>();
        RoomCreator roomCreator = FindObjectOfType<RoomCreator>();
        if (currentRoom != null && roomCreator != null)
        {
            RoomCostCalculator.CalculateEstimate(currentRoom);
            RoomCostCalculator.CalculateRoomCost(currentRoom);
            DataCacher.CacheJsonRoomData(roomCreator.ConvertRoomToJSON(currentRoom), currentRoom.Name, ScreenshotMaker.MakeScreenshot());
        }
    }
}
