using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationRoomEditor : MonoBehaviour
{
    public GameObject editorRoom;
    public GameObject panelWall;
    public GameObject panelMaterial;
    public GameObject panelMattPaint;
    public GameObject panelGlossyPaint;
    public GameObject panelFloor;
    public GameObject panelCeiling;
    public GameObject panelWindow;
    public GameObject panelFormShop;


    void Start()
    {
        Screen.autorotateToPortrait = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    void Update()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        if (ActiveWindowKeeper.IsRedactorActive)
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.autorotateToPortraitUpsideDown = true;
            Screen.orientation = ScreenOrientation.AutoRotation;

            if (Screen.orientation == ScreenOrientation.LandscapeLeft || Screen.orientation == ScreenOrientation.LandscapeRight)
            {
                editorRoom.gameObject.SetActive(false);
				panelWall.gameObject.SetActive(false);
				panelCeiling.gameObject.SetActive(false);
				panelFormShop.gameObject.SetActive(false);
				panelGlossyPaint.gameObject.SetActive(false);
				panelMattPaint.gameObject.SetActive(false);
				panelWindow.gameObject.SetActive(false);
				panelFloor.gameObject.SetActive(false);
				panelMaterial.gameObject.SetActive(false);
			}
            else
            {
                editorRoom.gameObject.SetActive(true);
                panelWall.gameObject.SetActive(true);
                panelMattPaint.gameObject.SetActive(true);
                panelGlossyPaint.gameObject.SetActive(true);
                panelFormShop.gameObject.SetActive(true);
                panelFloor.gameObject.SetActive(true);
                panelCeiling.gameObject.SetActive(true);
                panelWindow.gameObject.SetActive(true);
                panelMaterial.gameObject.SetActive(true);
            }

        }
        else
		{
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToPortraitUpsideDown = false;
        }
        if (Screen.orientation != ScreenOrientation.Portrait)
		{
            Room room = FindObjectOfType<Room>();
            if (room)
			{
                room.DeleteOppened3DButtons();
			}
		}

        //if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        //{
        //    editorRoom.gameObject.SetActive(false);
        //}
        //else
        //    editorRoom.gameObject.SetActive(true);

    }
}
