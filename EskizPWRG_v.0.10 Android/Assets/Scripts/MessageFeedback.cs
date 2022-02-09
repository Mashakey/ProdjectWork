using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class MessageFeedback : MonoBehaviour, IPointerEnterHandler
{
    Color colorOutline;
    Color activeColorOurline;
    public GameObject send;
    public Sprite sendActive;
    public Sprite spriteSend;
    public Outline outlineName;
    public Outline outlineEmail;
    public Outline outlineNamber;
    public Outline outlineMessage;


    private void Awake()
    {
        outlineName = GameObject.Find("NameFeedback").GetComponent<Outline>();
        outlineEmail = GameObject.Find("EmailFeedback").GetComponent<Outline>();
        outlineNamber = GameObject.Find("NamberFeedback").GetComponent<Outline>();
        outlineMessage = GameObject.Find("MessageFeedback").GetComponent<Outline>();
    }

    public void inputText()
    {

        if (gameObject.GetComponent<InputField>().text != "")
        {

            ColorUtility.TryParseHtmlString("#56D253", out colorOutline);
            gameObject.GetComponent<Outline>().effectColor = colorOutline;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#D25353", out colorOutline);
            gameObject.GetComponent<Outline>().effectColor = colorOutline;
        }

        ColorUtility.TryParseHtmlString("#56D253", out activeColorOurline);
        if (outlineName.effectColor == activeColorOurline && outlineEmail.effectColor == activeColorOurline && outlineNamber.effectColor == activeColorOurline && outlineMessage.effectColor == activeColorOurline)
        {
            send.GetComponent<Image>().sprite = sendActive;
        }
        else
            send.GetComponent<Image>().sprite = spriteSend;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ColorUtility.TryParseHtmlString("#5391D2", out colorOutline);
        gameObject.GetComponent<Outline>().effectColor = colorOutline;
    }

    public void ResetOutlineBack()
    {
        ColorUtility.TryParseHtmlString("#868686", out colorOutline);
        outlineName.effectColor = colorOutline;
        outlineEmail.effectColor = colorOutline;
        outlineNamber.effectColor = colorOutline;
        outlineMessage.effectColor = colorOutline;
        send.GetComponent<Image>().sprite = spriteSend;
    }
}
