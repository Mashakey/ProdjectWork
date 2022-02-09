using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FormForShop : MonoBehaviour, IPointerEnterHandler
{
    Color colorOutline;
    public GameObject send;
    public Sprite sendActive;
    public Sprite spriteSend;
    Outline outlineName;
    Outline outlineEmail;
    Outline outlineNamber;
    Outline outlineMessage;


    private void Awake()
    {
        outlineName = GameObject.Find("Name").GetComponent<Outline>();
        outlineEmail = GameObject.Find("Email").GetComponent<Outline>();
        outlineNamber = GameObject.Find("Namber").GetComponent<Outline>();
        outlineMessage = GameObject.Find("Message").GetComponent<Outline>();
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

        ColorUtility.TryParseHtmlString("#56D253", out colorOutline);
        if (outlineName.effectColor == colorOutline && outlineEmail.effectColor == colorOutline && outlineNamber.effectColor == colorOutline)
        {
            send.GetComponent<Image>().sprite = sendActive;
        }
        else
            send.GetComponent<Image>().sprite = spriteSend;
    }

    public void inpMassage()
    {
        if (gameObject.GetComponent<InputField>().text != "")
        {
            ColorUtility.TryParseHtmlString("#56D253", out colorOutline);
            gameObject.GetComponent<Outline>().effectColor = colorOutline;
        }
        else
        {           
            ColorUtility.TryParseHtmlString("#868686", out colorOutline);
            gameObject.GetComponent<Outline>().effectColor = colorOutline;
        }
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
