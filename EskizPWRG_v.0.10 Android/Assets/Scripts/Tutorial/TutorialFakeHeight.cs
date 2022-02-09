using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFakeHeight : MonoBehaviour
{
    [SerializeField]
    InputField inputField;
    public void OnValueChange()
    {
        if (int.Parse(inputField.text) > 500)
        {
            inputField.text = "500";

        }
        else if (int.Parse(inputField.text) < 200)
        {
            inputField.text = "200";

        }
        else
        {
            inputField.text = inputField.text;
        }
    }
}
