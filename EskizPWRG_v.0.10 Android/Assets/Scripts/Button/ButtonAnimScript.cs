using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimScript : MonoBehaviour, IPointerDownHandler
{
    private Animator animator ;
    public GameObject temp;
    public GameObject myObject;
    public GameObject otherObject;
    public string animation;

    private void Update()
    {
    }

    private void Awake()
    {
        animator = temp.GetComponent<Animator>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
  
        Debug.Log("aaaa");
        animator.Play(animation);
        myObject.SetActive(true);
        otherObject.SetActive(false);
    }
}
