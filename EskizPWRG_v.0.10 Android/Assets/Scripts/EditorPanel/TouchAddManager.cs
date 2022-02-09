using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class TouchAddManager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public GameObject panel;
    private Animator animaPanel;
    private Animator ClickAnimaPanel;
    public GameObject editorRoom;
    public GameObject ClickButton;
    public GameObject AddClick;
    public bool moving = false;
    bool isPressed = false;
    DateTime pointerDownTime;
    public Button Submenu;

    private void Awake()
    {
        animaPanel = panel.GetComponent<Animator>();
        ClickAnimaPanel = AddClick.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                moving = true;
            }
        }
        if (isPressed)
        {
            if ((DateTime.Now - pointerDownTime).TotalMilliseconds >= 300)
            {
                isPressed = false;
                //onPointerUpButton();
                moving = true;
                panel.SetActive(true);
                animaPanel = panel.GetComponent<Animator>();
                animaPanel.Play("UpPanel");
            }
        }
    }

    public void onPointerUpButton()
    {
        StartCoroutine(WaitForSecond());
        StartCoroutine(unactivePanelEditor());
    }

    IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(0.1f);
        if (GlobalApplicationManager.isInnerRedactorPagesOpen == true)
        {
            Debug.LogError("true");
            StartCoroutine(unactiveLongPanelEditor());
        }
        else
            StartCoroutine(unactivePanelEditor());
        yield break;
    }

    public void OnPointerClickButton()
    {
        ClickAnimaPanel.Play("ClickDownPanel");
        AddClick.SetActive(false);
        ClickButton.SetActive(false);
    }
    private IEnumerator unactiveLongPanelEditor()
    {
        yield return new WaitForSeconds(0.4f);
        animaPanel.Play("DownPanel");
        panel.SetActive(false);
        editorRoom.SetActive(false);
        GlobalApplicationManager.isInnerRedactorPagesOpen = false;
    }

    private IEnumerator unactivePanelEditor()
    {
        yield return new WaitForSeconds(0.4f);
        animaPanel.Play("DownPanel");
        panel.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Submenu.enabled = true;
        isPressed = false;
        if ((DateTime.Now - pointerDownTime).TotalMilliseconds < 300)
        {
            ClickButton.SetActive(true); 
            AddClick.SetActive(true);

            ClickAnimaPanel.Play("ClickUpPanel");
        }
        if (moving == true)
        {
            onPointerUpButton();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        pointerDownTime = DateTime.Now;
        
    }
}
