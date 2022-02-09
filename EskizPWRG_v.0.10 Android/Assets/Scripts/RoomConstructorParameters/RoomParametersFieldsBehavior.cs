using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomParametersFieldsBehavior : MonoBehaviour
{
    [SerializeField]
    Sprite ActiveGoToRoomButtonSprite;
    [SerializeField]
    Button GoToRoomButton;
    [SerializeField]
    GameObject InteractibleRoomTypeField;
    [SerializeField]
    GameObject NotInteractibleRoomTypeField;
    [SerializeField]
    Text NotInteractibleRoomTypeText;

    public void OnEnterConstructorFromRedactor()
    {
        Room room = FindObjectOfType<Room>();
        InteractibleRoomTypeField.SetActive(false);
        NotInteractibleRoomTypeField.SetActive(true);
        NotInteractibleRoomTypeText.text = GlobalApplicationManager.GetRussinStringRoomInteriorTypeFromEnum(room.Interior);
        GoToRoomButton.image.sprite = ActiveGoToRoomButtonSprite;
    }

    public void OnEnterConstructorFromMyRooms()
	{
        InteractibleRoomTypeField.SetActive(true);
        NotInteractibleRoomTypeField.SetActive(false);
	}

}
