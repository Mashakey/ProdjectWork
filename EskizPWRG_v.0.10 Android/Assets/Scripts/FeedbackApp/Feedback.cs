using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Feedback : MonoBehaviour
{
    public Sprite actStar;
    public Sprite star;
    public Button[] feedbackStars;
    public int indexStar;

    void Start()
    {
        
    }
    void Update()
    {       
    }

    public void feedbackUI()
    {
        for(int i = 0; i < feedbackStars.Length; i++)
        {
            if(feedbackStars[i].GetComponent<Feedback>().indexStar <= gameObject.GetComponent<Feedback>().indexStar)
            {
                feedbackStars[i].image.sprite = actStar;
            }
            else if ((feedbackStars[i].image.sprite == actStar) && (feedbackStars[i].GetComponent<Feedback>().indexStar > gameObject.GetComponent<Feedback>().indexStar) )
            {
                feedbackStars[i].image.sprite = star;
            }
            
        }
     
    }
}
