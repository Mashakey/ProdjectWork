using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estimate : MonoBehaviour
{
    [SerializeField]
    Transform estimatePage;
    [SerializeField]
    LoadingScreen loadingScreen;

    public void OnEstimateButtonClick()
	{
        loadingScreen.gameObject.SetActive(true);
        Room currentRoom = GameObject.FindObjectOfType<Room>();
        RoomCreator roomCreator = FindObjectOfType<RoomCreator>();
        DataCacher.CacheJsonRoomData(roomCreator.ConvertRoomToJSON(currentRoom), currentRoom.Name, ScreenshotMaker.MakeScreenshot());
        loadingScreen.StartCoroutine(loadingScreen.LoadingOnOpeningEstimate(estimatePage));
	}

}
