using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIKeyboardSearch : MonoBehaviour, IPointerDownHandler
{
    public TouchScreenKeyboard keyboard;
    public GameObject button;
    private Animator butt;

    private void Update()
    {
    }
    private void Awake()
    {
        butt = button.GetComponent<Animator>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        button.SetActive(true);
        Debug.Log("aaaa");
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        keyboard.active = true;
        butt.Play("keyboard");
    }

    public void CloseKeyboard()
    {
        butt.Play("Cansel");
        Debug.Log("close");
        keyboard.active = false;
        //button.SetActive(false);
    }
}

