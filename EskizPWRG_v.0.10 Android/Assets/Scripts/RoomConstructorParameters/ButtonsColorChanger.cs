using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsColorChanger : MonoBehaviour
{
    [SerializeField]
    Button DoorZeroButton;
    [SerializeField]
    Button DoorOneButton;
    [SerializeField]
    Button DoorTwoButton;

    [SerializeField]
    Button WindowZeroButton;
    [SerializeField]
    Button WindowOneButton;
    [SerializeField]
    Button WindowTwoButton;

    Color darkGrey;
    Color grey;
    Color orange;
    ColorBlock standartButtonColors;
    ColorBlock pressedButtonColors;

	private void Awake()
	{
        ColorUtility.TryParseHtmlString("#868686", out darkGrey); //darkgrey
        ColorUtility.TryParseHtmlString("#C0C0C0", out grey); //grey
        ColorUtility.TryParseHtmlString("#FF9900", out orange); //orange
        standartButtonColors = DoorZeroButton.colors;
        pressedButtonColors = DoorZeroButton.colors;
        pressedButtonColors.disabledColor = Color.white;
    }

    public void ResetDoorButtonsColor()
	{
        DoorZeroButton.colors = standartButtonColors;
        DoorZeroButton.GetComponent<Image>().color = darkGrey;

        DoorOneButton.colors = standartButtonColors;
        DoorOneButton.GetComponent<Image>().color = darkGrey;

        DoorTwoButton.colors = standartButtonColors;
        DoorTwoButton.GetComponent<Image>().color = darkGrey;
	}

    public void ResetWindowButtonsColor()
	{
        WindowZeroButton.colors = standartButtonColors;
		WindowZeroButton.GetComponent<Image>().color = darkGrey;

        WindowOneButton.colors = standartButtonColors;
        WindowOneButton.GetComponent<Image>().color = darkGrey;

        WindowTwoButton.colors = standartButtonColors;
        WindowTwoButton.GetComponent<Image>().color = darkGrey;
	}

    public void OnDoorZeroButtonClick()
    {
		ResetDoorButtonsColor();
        DoorZeroButton.colors = pressedButtonColors;
		DoorZeroButton.GetComponent<Image>().color = orange;
	}

    public void OnDoorOneButtonClick()
    {

		ResetDoorButtonsColor();
        DoorOneButton.colors = pressedButtonColors;
        DoorOneButton.GetComponent<Image>().color = orange;
	}

    public void OnDoorTwoButtonClick()
    {

		ResetDoorButtonsColor();
        DoorTwoButton.colors = pressedButtonColors;
        DoorTwoButton.GetComponent<Image>().color = orange;
	}

    public void OnWindowZeroButtonClick()
	{
		ResetWindowButtonsColor();
        WindowZeroButton.colors = pressedButtonColors;
		WindowZeroButton.GetComponent<Image>().color = orange;
	}

    public void OnWindowOneButtonClick()
    {
		ResetWindowButtonsColor();
        WindowOneButton.colors = pressedButtonColors;
        WindowOneButton.GetComponent<Image>().color = orange;
	}

    public void OnWindowTwoButtonClick()
    {
		ResetWindowButtonsColor();
        WindowTwoButton.colors = pressedButtonColors;
        WindowTwoButton.GetComponent<Image>().color = orange;
	}
}
