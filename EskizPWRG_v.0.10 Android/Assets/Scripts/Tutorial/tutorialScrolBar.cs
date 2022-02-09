using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialScrolBar : MonoBehaviour
{
    public Scrollbar scrollBar;
    float scrolBarItem;
    public GameObject circleFilt;
    public Button filters;
    public List<GameObject> likesButton = new List<GameObject>();

    private void Awake()
    {
        scrolBarItem = scrollBar.GetComponent<Scrollbar>().value;
    }
    private void Update()
    {
        if(scrollBar.GetComponent<Scrollbar>().value < 0.6f)
        {
            gameObject.SetActive(false);

            circleFilt.SetActive(true);
            //filters.enabled = true;
            activatedLikes();
        }
    }

    public void activatedLikes()
    {
        for (int i = 0; i < likesButton.Count; i++)
        {
            likesButton[i].GetComponent<Button>().enabled = true;
        }
    }
}
