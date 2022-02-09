using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTypeRoom : MonoBehaviour
{
    [SerializeField]
    ParametersChangeHandler parametersChangeHandler;
    public GameObject nameWindow;
    public Text typeRoom;
    Color selectColor;
    public GameObject arrow;
    public Sprite ArrowDown;
    public string colorText;

    public Sprite activeGoToRoom;
    public Button goToRoom;
    public InputField nameRoom;
    public GameObject iconPencil;
    public Sprite unactiveGoToRoom;

    private void Update()
    {
    }
    public void selectType()
    {
        typeRoom.text = gameObject.GetComponent<Text>().text;
        ColorUtility.TryParseHtmlString(colorText, out selectColor);
        typeRoom.color = selectColor;
        typeRoom.fontStyle = FontStyle.Bold;

        gameObject.GetComponentInParent<Animator>().Play("typeInParamUp");
        arrow.GetComponent<Image>().sprite = ArrowDown;
        typesRoomParameters typesRoomParameters = gameObject.transform.parent.GetComponent<typesRoomParameters>();
        //Debug.LogError("Select type room type = '" + typeRoom.text + "'");
        parametersChangeHandler.UpdateInteriorType(typeRoom.text);
        if (typesRoomParameters != null)
        {
            Debug.LogError("Select type room type = '" + typeRoom.text + "'");
            parametersChangeHandler.UpdateInteriorType(typeRoom.text);
        }

        typeRoom.transform.parent.GetComponent<FiltersAnimation>().click = true;

        if (goToRoom.GetComponent<Image>().sprite != activeGoToRoom && nameWindow.name == "RoomParameters")
        {
            goToRoom.GetComponent<Image>().sprite = activeGoToRoom;
        }
    }

    //public void checkButSprite()
    //{

    //    if (goToRoom.GetComponent<Image>().sprite != activeGoToRoom && nameWindow.name == "RoomParameters")
    //    {
    //        goToRoom.GetComponent<Image>().sprite = activeGoToRoom;
    //    }
    //}

    public void ResetParametersRoom()
    {
        ColorUtility.TryParseHtmlString("#868686", out selectColor);
        typeRoom.color = selectColor;
        typeRoom.text = "Тип комнаты";
        typeRoom.fontStyle = FontStyle.Normal;
        goToRoom.GetComponent<Image>().sprite = unactiveGoToRoom;

        nameRoom.GetComponent<InputField>().text = "";
        nameRoom.GetComponent<InputField>().placeholder.GetComponent<Text>().enabled = true;
        iconPencil.SetActive(true);
    }
}
