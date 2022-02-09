using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPaintsObject : MonoBehaviour
{
    public Transform parent;
    public GameObject newFrame;

    public GameObject proceed;
    public Sprite newProceed;
    public Sprite inactiveProceed;
    Vector2 vector = new Vector2(0.5f, 0.5f);


    private void Update()
    {
    }

    public void selectPaint()
    {
        newFrame = GameObject.Find("FrameMattPaint").gameObject;
        proceed = GameObject.Find("ApplyButtonMatt").gameObject;
        newFrame.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        newFrame.GetComponent<RectTransform>().anchorMin = vector;
        newFrame.GetComponent<RectTransform>().anchorMax = vector;
        newFrame.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
        Debug.LogError(gameObject.GetComponent<RectTransform>().sizeDelta);
        Debug.LogError(newFrame.GetComponent<RectTransform>().sizeDelta);
        Image imageBt = proceed.GetComponent<Image>();
        imageBt.sprite = newProceed;
    }
}
