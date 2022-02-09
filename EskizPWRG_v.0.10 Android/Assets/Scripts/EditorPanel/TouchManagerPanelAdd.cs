using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchManagerPanelAdd : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Animator animButton;
    public string animUp;
    public string animDown;
    public GameObject panel;

    public GameObject editor;
    public GameObject windUp;
    TouchAddManager touchAddManager;
    public Button Submenu;

    //public GameObject newWin;
    private int id;

    private void Awake()
    {
        animButton = gameObject.GetComponent<Animator>();
        touchAddManager = GameObject.FindObjectOfType<TouchAddManager>();
    }

    private void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                animButton.Play(animUp);
                Submenu.enabled = false;
                Vibration.Init();
                Vibration.VibratePop();
                //Handheld.Vibrate();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("mashaaaaa");

        animButton.Play(animDown);
        touchAddManager.moving = false;
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (gameObject.name != "Notice" && gameObject.name != "Furniture" && gameObject.name != "Decor")
                {
                    GlobalApplicationManager.isInnerRedactorPagesOpen = true;
                }
                if (gameObject.name == "Notice")
                {
                    GameObject editorRoom = GameObject.Find("EditorRoom");
                    Debug.LogWarning("Creating note window");
                    GameObject notePage = Instantiate(windUp, editorRoom.transform);
                    notePage.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                    ActiveWindowKeeper.IsRedactorActive = false;
                }
                else
                {
                    windUp.SetActive(true);
                    ActiveWindowKeeper.IsRedactorActive = false;
                }
            }
        }

    }


}
