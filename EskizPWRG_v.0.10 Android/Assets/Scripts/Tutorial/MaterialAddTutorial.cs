using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MaterialAddTutorial : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animButton;
    public string animUp;
    public string animDown;
    public GameObject panel;

    public GameObject editor;
    public GameObject windUp;
    public GameObject notesPref;
    public bool id = false;
    public bool idFurniture = false;
    bool idNote = false;

    public GameObject circleEd3;
    public GameObject AddUI;
    public GameObject addImage;
    public GameObject ADDPanel;
    public GameObject circleEd4;

    public GameObject circleFurniture;
    public GameObject Furniture;

    private void Awake()
    {
        animButton = gameObject.GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved )
        {
            animButton.Play(animUp);
            //Handheld.Vibrate();
            Vibration.Init();
            Vibration.VibratePop();
            if (id == false && circleEd3 != null && idFurniture == false)
            {
                circleEd3.SetActive(true);
            }
            else if(circleEd4 != null)
                circleEd4.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        animButton.Play(animDown);
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Ended && id == true)
        {
            windUp.SetActive(true);
            Destroy(circleEd4);
            id = false;
        }
        else if (touch.phase == TouchPhase.Ended && idNote == true)
        {
            notesPref.SetActive(true);
            Destroy(circleEd3);
            addImage.GetComponent<Image>().enabled = true;
            StartCoroutine(secondForAddUI());
            idNote = false;
        }
        else if (touch.phase == TouchPhase.Ended && idFurniture == true)
        {
            Furniture.SetActive(true);
            circleFurniture.SetActive(false);
            idFurniture = false;
        }

    }

    IEnumerator secondForAddUI()
    {
        yield return new WaitForSeconds(0.39f);
        AddUI.SetActive(false);
        ADDPanel.SetActive(false);
    }

    public void idFoeMaterial()
    {
        id = true;
    }

    public void idForNote()
    {
        idNote = true;
    }

    public void idForFurniture()
    {
        idFurniture = true;
    }
}
