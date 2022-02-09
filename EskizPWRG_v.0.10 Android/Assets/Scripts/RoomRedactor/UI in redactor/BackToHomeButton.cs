using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class BackToHomeButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform myRoomsWindow;

    [SerializeField]
    Transform redactorWindow;

    [SerializeField]
    RoomCreator roomCreator;

    [SerializeField]
    LoadingScreen loadingScreen;

    public void OnHomeButtonClick()
	{
        loadingScreen.gameObject.SetActive(true);
        loadingScreen.StartCoroutine(loadingScreen.LoadingOnHomeButtonClick());
        //Room currentRoom = FindObjectOfType<Room>();
        //RoomCostCalculator.CalculateEstimate(currentRoom);
        //RoomCostCalculator.CalculateRoomCost(currentRoom);
        //DataCacher.CacheJsonRoomData(roomCreator.ConvertRoomToJSON(currentRoom), currentRoom.Name, ScreenshotMaker.MakeScreenshot());
        //roomCreator.DestroyRoom();
        //redactorWindow.gameObject.SetActive(false);
        //myRoomsWindow.gameObject.SetActive(true);
	}
}
