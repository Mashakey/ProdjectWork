using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFueldForTutorial : MonoBehaviour
{
    public GameObject circleThisObject;
    public GameObject circleNextObject;
    public InputField inputFieldNext;
    public GameObject types;

    [SerializeField]
    InputField heightField;

    public void activatedCircls()
    {
        if (gameObject.GetComponent<InputField>().textComponent.GetComponent<Text>().text != "" && gameObject.GetComponent<InputField>().textComponent.GetComponent<Text>().text != " " && gameObject.GetComponent<InputField>().textComponent.GetComponent<Text>().text != "-")
        {
            circleNextObject.SetActive(true);
            gameObject.GetComponent<InputField>().enabled = false;
            if (inputFieldNext.name == "InputFieldLength")
            {
                inputFieldNext.enabled = true;
            }
        }
        else
        {
            gameObject.GetComponent<InputField>().placeholder.GetComponent<Text>().enabled = true;
            circleThisObject.SetActive(true);
        }

    }

    public void activatedCirclsName()
    {
        if (gameObject.GetComponent<InputField>().textComponent.GetComponent<Text>().text != "" && gameObject.GetComponent<InputField>().textComponent.GetComponent<Text>().text != " ")
        {
            circleNextObject.SetActive(true);
            gameObject.GetComponent<InputField>().enabled = false;
            types.GetComponent<Button>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<InputField>().placeholder.GetComponent<Text>().enabled = true;
            circleThisObject.SetActive(true);
        }

    }

    public void OnValueChangeHeight()
    {
        if (int.Parse(heightField.text) > 420)
        {
            heightField.text = "420 cì";
        }
    }

    public void selectWallActive()
    {
        if (gameObject.GetComponent<InputField>().textComponent.GetComponent<Text>().text != "" && gameObject.GetComponent<InputField>().textComponent.GetComponent<Text>().text != " " && gameObject.GetComponent<InputField>().textComponent.GetComponent<Text>().text != "-")
        {
            circleNextObject.SetActive(true);
            gameObject.GetComponent<InputField>().enabled = false;           
        }
        else
        {
            gameObject.GetComponent<InputField>().placeholder.GetComponent<Text>().enabled = true;
            circleThisObject.SetActive(true);
        }
    }
}
