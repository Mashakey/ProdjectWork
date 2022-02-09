using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField]
    InputField heightInputField;
    [SerializeField]
    Button heightPlusButton;
    [SerializeField]
    Button heightMinusButton;

    [SerializeField]
    InputField lengthInputField;
    [SerializeField]
    Button lengthPlusButton;
    [SerializeField]
    Button lengthMinusButton;

    [SerializeField]
    Button zeroDoorButton;
    [SerializeField]
    Button oneDoorButton;
    [SerializeField]
    Button twoDoorButton;

    [SerializeField]
    Button zeroWindowButton;
    [SerializeField]
    Button oneWindowButton;
    [SerializeField]
    Button twoWindowButton;

    [SerializeField]
    ParametersChangeHandler parametersChangeHandler;

    int doorsOnWallCount = 0;
    int windowsOnDoorCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        //heightMinusButton.interactable = false;
        //lengthMinusButton.interactable = false;
        //zeroDoorButton.interactable = false;
        //zeroWindowButton.interactable = false;
    }

    public void OnPlusHeightPress()
    {
        heightMinusButton.interactable = true;

        if (heightInputField.text == "")
        {
            heightInputField.text = "1";
        }
        else
        {
            int heightValue = int.Parse(heightInputField.text);
            heightValue++;
            heightInputField.text = heightValue.ToString();
        }
        parametersChangeHandler.OnHeightChanged();
    }

    public void OnMinusHeightPress()
    {
        int heightValue = int.Parse(heightInputField.text);
        heightValue--;
        heightInputField.text = heightValue.ToString();
        if (heightValue == 0)
        {
            heightMinusButton.interactable = false;
        }
        parametersChangeHandler.OnHeightChanged();

    }

    public void OnPlusLengthPress()
    {
        lengthMinusButton.interactable = true;

        if (lengthInputField.text == "")
        {
            lengthInputField.text = "1";
        }
        else
        {
            int lengthValue = int.Parse(lengthInputField.text);
            lengthValue++;
            lengthInputField.text = lengthValue.ToString();
        }
        parametersChangeHandler.OnLengthChanged();

    }

    public void OnMinusLengthPress()
    {
        int lengthValue = int.Parse(lengthInputField.text);
        lengthValue--;
        lengthInputField.text = lengthValue.ToString();
        if (lengthValue == 0)
        {
            lengthMinusButton.interactable = false;
        }
        parametersChangeHandler.OnLengthChanged();

    }

    public void SetButtonsZero()
    {
		zeroDoorButton.interactable = true;
		oneDoorButton.interactable = false;
		twoDoorButton.interactable = false;
		zeroWindowButton.interactable = true;
		oneWindowButton.interactable = false;
		twoWindowButton.interactable = false;
	}

    public void SetWindowsButtonZero()
    {
		zeroDoorButton.interactable = true;
		oneDoorButton.interactable = false;
		twoDoorButton.interactable = false;
		zeroWindowButton.interactable = true;
		oneWindowButton.interactable = false;
		twoWindowButton.interactable = false;
	}

    public void SetDoorsButtonZero()
    {
		zeroDoorButton.interactable = true;
		oneDoorButton.interactable = false;
		twoDoorButton.interactable = false;
		zeroWindowButton.interactable = true;
		oneWindowButton.interactable = false;
		twoWindowButton.interactable = false;
	}

    public void SetWindowsButtonOne()
    {
		zeroDoorButton.interactable = true;
		oneDoorButton.interactable = true;
		twoDoorButton.interactable = false;
		zeroWindowButton.interactable = true;
		oneWindowButton.interactable = false;
		twoWindowButton.interactable = true;
	}

    public void SetWindowsButtonZeroAndOne()
    {
		zeroDoorButton.interactable = true;
		oneDoorButton.interactable = false;
		twoDoorButton.interactable = false;
		zeroWindowButton.interactable = true;
		oneWindowButton.interactable = true;
		twoWindowButton.interactable = false;
	}

    public void SetDoorsButtonOne()
    {
		zeroDoorButton.interactable = true;
		oneDoorButton.interactable = false;
		twoDoorButton.interactable = true;
		zeroWindowButton.interactable = true;
		oneWindowButton.interactable = true;
		twoWindowButton.interactable = false;
	}

    public void SetDoorsButtonZeroAndOne()
    {
		zeroDoorButton.interactable = true;
		oneDoorButton.interactable = true;
		twoDoorButton.interactable = false;
		zeroWindowButton.interactable = true;
		oneWindowButton.interactable = false;
		twoWindowButton.interactable = false;
	}

    public void SetAllButtonsActive()
    {
		zeroDoorButton.interactable = true;
		oneDoorButton.interactable = true;
		twoDoorButton.interactable = true;
		zeroWindowButton.interactable = true;
		oneWindowButton.interactable = true;
		twoWindowButton.interactable = true;
	}

    public void SetTwoWindowButtonInactive()
    {
		twoWindowButton.interactable = false;
	}

    public void SetOneWindowButtonInactive()
    {
		zeroWindowButton.interactable = true;
		oneWindowButton.interactable = false;
	}

    public void SetTwoDoorButtonInactive()
    {
		twoDoorButton.interactable = false;
	}

    public void SetOneDoorButtonInactive()
    {
		zeroDoorButton.interactable = true;
		oneDoorButton.interactable = false;
	}


}
