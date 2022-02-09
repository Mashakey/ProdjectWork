using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TutorialAddManagerTouch : MonoBehaviour, IPointerClickHandler/*, IPointerUpHandler, IPointerDownHandler*/
{
    public GameObject panel;
    private Animator animaPanel;
    public GameObject editorRoom;

    private void Awake()
    {
        animaPanel = panel.GetComponent<Animator>();
    }
    private void Start()
    {

    }

    public void onPointerDownButton()
    {
        panel.SetActive(true);
        animaPanel.Play("TutorialPanel");
    }

    public void onPointerUpButton()
    {
        StartCoroutine(WaitForSecond());

        StartCoroutine(unactiveAddPanel());
        //if(GlobalApplicationManager.isInnerRedactorPagesOpen == true)
        //{
        //    Debug.LogError("true");
        //    StartCoroutine(unactiveLongPanelEditor());
        //}
        //else
        //    StartCoroutine(unactivePanelEditor());

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

    public void OnPointerClick(PointerEventData eventData)
    {
        panel.SetActive(true);
        animaPanel.Play("TutorialPanel");

    }
    private IEnumerator unactiveLongPanelEditor()
    {
        yield return new WaitForSeconds(0.43f);
        animaPanel.Play("TutorialPanelDown");
        panel.SetActive(false);
        editorRoom.SetActive(false);
        GlobalApplicationManager.isInnerRedactorPagesOpen = false;
    }

    private IEnumerator unactivePanelEditor()
    {
        yield return new WaitForSeconds(0.02f);
        animaPanel.Play("TutorialPanelDown");
        //panel.SetActive(false);
    }

    IEnumerator unactiveAddPanel()
    {
        yield return new WaitForSeconds(0.4f);
        panel.SetActive(false);
    }
}
