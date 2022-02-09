using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pointUpPanel : MonoBehaviour, IPointerClickHandler, IPointerUpHandler,IPointerDownHandler
{
    public GameObject editor;
    public GameObject windUp;

    public void OnPointerClick(PointerEventData eventData)
    {
        editor.SetActive(false);
        windUp.SetActive(true);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        editor.SetActive(false);
        windUp.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        editor.SetActive(false);
        windUp.SetActive(true);
    }
    private void Start()
    {
        
    }

    void Update()
    {
        
    }
}
