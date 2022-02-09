using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class CreateRoomFromTemplate : MonoBehaviour
{
    void OnRoomTemplateButtonClick()
	{
        LegacyRoomData roomData = DataCacher.GetCachedLegacyRoomDataJSONs(gameObject.name);
        if (roomData != null)
		{
            Debug.LogWarning("Trying to create room from legacy");
            //RoomCreator roomCreator = game
		}
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
