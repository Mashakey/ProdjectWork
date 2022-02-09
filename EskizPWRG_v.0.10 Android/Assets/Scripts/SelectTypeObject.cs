using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTypeObject : MonoBehaviour
{
    public Transform parent;
    public Image newFrame;

    public Button proceed;
    public Sprite newProceed;
    public Sprite inactiveProceed;
    Vector2 vector = new Vector2(0.5f, 0.5f);


    private void Update()
    {
    }

    public void selectObj()
    {
        newFrame.transform.SetParent(parent, true);
		newFrame.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        newFrame.GetComponent<RectTransform>().anchorMin = vector;
        newFrame.GetComponent<RectTransform>().anchorMax = vector;
        newFrame.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.transform.Find("Image").GetComponent<RectTransform>().rect.width, gameObject.transform.Find("Image").GetComponent<RectTransform>().rect.height);
        Image imageBt = proceed.GetComponent<Image>();
		imageBt.sprite = newProceed;
        proceed.GetComponent<Button>().interactable = true;
    }
}
