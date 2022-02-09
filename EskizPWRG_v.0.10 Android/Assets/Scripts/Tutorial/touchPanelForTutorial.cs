using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class touchPanelForTutorial : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animButton;
    public string animUp;
    public string animDown;
    public GameObject panel;
    private void Awake()
    {
        animButton = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Touch touch = Input.GetTouch(0);
        
        if (touch.phase == TouchPhase.Moved)
        {
            animButton.Play(animUp);
            Handheld.Vibrate();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Moved)
        {
            animButton.Play(animDown);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            animButton.Play(animDown);
        }
    }
}
