using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedRoomIndex : MonoBehaviour
{
    public int RoomIndex = 0;

    public void OnRoomButtonClick()
	{
        MyRoomsFiller roomsFiller = GetComponentInParent<MyRoomsFiller>();
        LoadingScreen[] loadingScreen = Resources.FindObjectsOfTypeAll<LoadingScreen>();
        loadingScreen[0].gameObject.SetActive(true);
        loadingScreen[0].StartCoroutine(loadingScreen[0].LoadingOnCachedRoomCreating(roomsFiller.roomDataJsons[RoomIndex]));
        roomsFiller.DeactivateRoomsPage();
        ActiveWindowKeeper.IsRedactorActive = true;
	}

}
