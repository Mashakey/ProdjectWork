using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSelectTypeRoom : MonoBehaviour
{
    public GameObject nameWindow;
    public Text typeRoom;
    Color selectColor;
    public GameObject arrow;
    public Sprite ArrowDown;
    public string colorText;

    public Sprite activeGoToRoom;
    public Button goToRoom;

    public InputField heigth;
    public GameObject circlHeigth;

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
        
        typeRoom.transform.parent.GetComponent<FiltersAnimation>().click = true;

        if(heigth.GetComponent<InputField>().text == "")
        {
            heigth.GetComponent<InputField>().enabled = true;
            circlHeigth.SetActive(true);
        }

        if (goToRoom.GetComponent<Image>().sprite != activeGoToRoom && nameWindow.name == "RoomParameters")
        {
            goToRoom.GetComponent<Image>().sprite = activeGoToRoom;
        }
    }

}
