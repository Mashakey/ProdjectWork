using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    GameObject redactorPage;
    [SerializeField]
    GameObject myRoomsPage;
    [SerializeField]
    GameObject AddUI;
    [SerializeField]
    Button roomDischargeButton;

    public GameObject goToRoom;
    public GameObject loadingWind;

    public Image loadingImage;
    public Text progressText;

    public float timerStart;
    public float progress;

	//private void OnEnable()
	//{
 //       timerStart = 0;
 //       loadingImage.fillAmount = 0f;

 //   }

    public IEnumerator LoadingOnNewRoomCreating(RoomWallManager roomWallManager)
	{
        loadingWind.SetActive(true);
        timerStart = 0;
        loadingImage.fillAmount = 0f;
        bool isRoomCreatingStarted = false;
        while (timerStart < 2)
        {
            timerStart += Time.deltaTime;
            progress = timerStart / 2f;
            loadingImage.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
            if (!isRoomCreatingStarted)
            {
                RoomCreator roomCreator = GameObject.FindObjectOfType<RoomCreator>();
                RoomData roomData = roomWallManager.CreateRoomDataFromConstructor();
                RoomDischarger.CreateEmptyRoom(roomData);
                roomCreator.CreateRoom(roomData);
                isRoomCreatingStarted = true;
            }
        }
        loadingWind.SetActive(false);
        redactorPage.SetActive(true);
        AddUI.SetActive(true);
        roomDischargeButton.GetComponent<RoomDischargeButton>().SetRoomDischargeButtonStatus();

        yield break;
    }

    public IEnumerator LoadingOnLegacyRoomCreating(LegacyRoomData legacyRoomDataJson)
	{
        loadingWind.SetActive(true);
        timerStart = 0;
        loadingImage.fillAmount = 0f;
        bool isRoomCreatingStarted = false;
        while (timerStart < 2)
        {
            timerStart += Time.deltaTime;
            progress = timerStart / 2f;
            loadingImage.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
            if (!isRoomCreatingStarted)
            {
                RoomCreator roomCreator = GameObject.FindObjectOfType<RoomCreator>();
                roomCreator.CreateRoomFromLegacy(legacyRoomDataJson);
                isRoomCreatingStarted = true;
            }
        }
        loadingWind.SetActive(false);
        redactorPage.SetActive(true);
        AddUI.SetActive(true);
        roomDischargeButton.GetComponent<RoomDischargeButton>().SetRoomDischargeButtonStatus();

        yield break;
    }

    public IEnumerator LoadingOnCachedRoomCreating(RoomData roomDataJson)
	{
        loadingWind.SetActive(true);
        timerStart = 0;
        loadingImage.fillAmount = 0f;
        bool isRoomCreatingStarted = false;
        while (timerStart < 2)
        {
            timerStart += Time.deltaTime;
            progress = timerStart / 2f;
            loadingImage.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
            if (!isRoomCreatingStarted)
            {
                RoomCreator roomCreator = GameObject.FindObjectOfType<RoomCreator>();
                roomCreator.CreateRoom(roomDataJson);
                isRoomCreatingStarted = true;
            }
        }
        loadingWind.SetActive(false);
        redactorPage.SetActive(true);
        AddUI.SetActive(true);
        roomDischargeButton.GetComponent<RoomDischargeButton>().SetRoomDischargeButtonStatus();

        yield break;
    }

    public IEnumerator LoadingOnOpeningEstimate(Transform estimatePage)
	{
        loadingWind.SetActive(true);
        timerStart = 0;
        loadingImage.fillAmount = 0f;
        bool isEstimateCreatingStarted = false;
        while (timerStart < 2)
        {
            timerStart += Time.deltaTime;
            progress = timerStart / 2f;
            loadingImage.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
            if (!isEstimateCreatingStarted)
            {
                isEstimateCreatingStarted = true;
                Room room = FindObjectOfType<Room>();
                estimatePage.gameObject.SetActive(true);
                estimatePage.GetComponent<Canvas>().enabled = false;
                estimatePage.GetComponent<CanvasScaler>().enabled = false;
                //EstimateToPDFConverter pdfConverter = FindObjectOfType<EstimateToPDFConverter>();
                //StartCoroutine(pdfConverter.LoadPdfRoutine(RoomCostCalculator.CreateEstimatePdfData(room)));



                estimatePage.GetComponent<EstimatePage>().CreateContentFields(RoomCostCalculator.CalculateEstimate(room));
                estimatePage.GetComponent<EstimatePage>().SetTotalRoomCost(RoomCostCalculator.CalculateRoomCost(room));
            }
        }
        estimatePage.GetComponent<Canvas>().enabled = true;
        estimatePage.GetComponent<CanvasScaler>().enabled = true;

        loadingWind.SetActive(false);
        yield break;
	}

    public IEnumerator LoadingOnHomeButtonClick()
	{
        loadingWind.SetActive(true);
        timerStart = 0;
        loadingImage.fillAmount = 0f;
        bool isHomePageLoadingStarted = false;
        RoomCreator roomCreator = FindObjectOfType<RoomCreator>();

        while (timerStart < 2)
        {
            timerStart += Time.deltaTime;
            progress = timerStart / 2f;
            loadingImage.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
            if (!isHomePageLoadingStarted)
            {
                isHomePageLoadingStarted = true;
                Room currentRoom = FindObjectOfType<Room>();
                RoomCostCalculator.CalculateEstimate(currentRoom);
                RoomCostCalculator.CalculateRoomCost(currentRoom);
                DataCacher.CacheJsonRoomData(roomCreator.ConvertRoomToJSON(currentRoom), currentRoom.Name, ScreenshotMaker.MakeScreenshot());
            }
        }
        loadingWind.SetActive(false);
        redactorPage.gameObject.SetActive(false);
        myRoomsPage.gameObject.SetActive(true);
        roomCreator.DestroyRoom();

        yield break;
    }

    public IEnumerator LoadingOnCreatingBaseboard(MaterialJSON baseboardJson)
	{
        loadingWind.SetActive(true);
        timerStart = 0;
        loadingImage.fillAmount = 0f;
        bool isBaseboardCreatingStarted = false;
        while (timerStart < 2)
        {
            timerStart += Time.deltaTime;
            progress = timerStart / 2f;
            loadingImage.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
            if (!isBaseboardCreatingStarted)
            {
                isBaseboardCreatingStarted = true;
                yield return (null);
                Room room = FindObjectOfType<Room>();
                HistoryAction historyAction = new HistoryAction();
                if (room.baseBoardMaterialId == "")
                {
                    historyAction.CreateDeleteBaseboardHistoryAction();
                }
                else
                {
                    historyAction.CreateChangeBaseboardHistoryAction(room.baseBoardMaterialId);
                }

                HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
                historyChangesStack.AddHistoryAction(historyAction);

                room.CreateBaseboard(baseboardJson);
            }
        }
        loadingWind.SetActive(false);

    }




}
