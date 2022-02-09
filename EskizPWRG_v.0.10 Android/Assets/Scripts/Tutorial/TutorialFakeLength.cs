using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFakeLength : MonoBehaviour
{
    [SerializeField]
    Text lengthField;
    [SerializeField]
    InputField inputField;
    public void OnValueChange() 
    {
        Debug.LogError("Length value = " + lengthField.text);
        Debug.LogError("Length int value = " + int.Parse(inputField.text));

        if (int.Parse(inputField.text) > 500)
        {
            inputField.text = "500";
            lengthField.text = "500 cì";

        }
        else if (int.Parse(inputField.text) < 100)
        {
            inputField.text = "100";
            lengthField.text = "100 cì";
        }
        else
        {
            inputField.text = inputField.text;
            lengthField.text = inputField.text + "ñì";
        }
    }
}
