using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRoomArea : MonoBehaviour
{
    [SerializeField]
    Text AreaField;
    [SerializeField]
    InputField WallLengthInputField;

    public void CalculateArea()
    {
        AreaField.text = "S: " + ((int.Parse(WallLengthInputField.text) / 100) * 5.2f).ToString() + "ì2";
    }
}
