using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class RoomTemplateButton : MonoBehaviour
{

    public void CreateLegacyRoomAndGoToRedactor()
	{

        gameObject.GetComponentInParent<RoomTemplatesFiller>().EnterEditorPage();
        Debug.LogWarning("Trying to createLegacyRoom");
        //RoomCreator roomCreator = GameObject.FindObjectOfType<RoomCreator>();
        //roomCreator.CreateRoomFromLegacy(DataCacher.GetCachedLegacyRoomDataJSONs(gameObject.name));

        LoadingScreen[] loadingScreen = Resources.FindObjectsOfTypeAll<LoadingScreen>();
        loadingScreen[0].gameObject.SetActive(true);
        loadingScreen[0].StartCoroutine(loadingScreen[0].LoadingOnLegacyRoomCreating(DataCacher.GetCachedLegacyRoomDataJSONs(gameObject.name)));
        ActiveWindowKeeper.IsRedactorActive = true;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
