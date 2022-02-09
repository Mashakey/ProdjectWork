using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackPageReseter : MonoBehaviour
{
    [SerializeField]
    InputField nameField;
    [SerializeField]
    InputField emailField;
    [SerializeField]
    InputField phoneField;
    [SerializeField]
    InputField messageField;
    // Start is called before the first frame update
    private void OnEnable()
    {
        nameField.text = "";
        emailField.text = "";
        phoneField.text = "";
        messageField.text = "";
    }
}
