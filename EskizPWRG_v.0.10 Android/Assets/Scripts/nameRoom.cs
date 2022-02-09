using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nameRoom : MonoBehaviour
{
    public GameObject pencilIcon;
    Vector3 startPositionText;

    private void Start()
    {
        startPositionText = gameObject.transform.parent.transform.localPosition;
    }

    public void inputNameRoom()
    {
        if(gameObject.GetComponent<InputField>().textComponent.GetComponent<Text>().text != "" && gameObject.GetComponent<InputField>().textComponent.GetComponent<Text>().text != " ")
        {
            pencilIcon.SetActive(false);
            //gameObject.transform.parent.transform.localPosition = new Vector3(0, gameObject.transform.parent.transform.localPosition.y, gameObject.transform.parent.transform.localPosition.z);
            gameObject.GetComponent<InputField>().textComponent.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        }
        else
        {
            gameObject.GetComponent<InputField>().placeholder.GetComponent<Text>().enabled = true;
            pencilIcon.SetActive(true);
        }
    }
}
