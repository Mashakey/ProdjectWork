using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialForWindow : MonoBehaviour
{
    public GameObject tutorial3DButton;
    //public Tutorial tutorial;

    private void Update()
    {
        if(tutorial3DButton.activeSelf == true)
        {
            //tutorial.indexsNext = 2;
            gameObject.SetActive(false);
            
        }
    }
}
