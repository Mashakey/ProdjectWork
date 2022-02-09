using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTwinWallChangeLength : MonoBehaviour
{
    [SerializeField]
    Text currentWallTextField;
    [SerializeField]
    Text twinWallLengthField;

    string length = "";

    public void SetWallLength()
	{
        string length = GetComponent<InputField>().text;
        currentWallTextField.text = length;
        twinWallLengthField.text = length;
	}

}
