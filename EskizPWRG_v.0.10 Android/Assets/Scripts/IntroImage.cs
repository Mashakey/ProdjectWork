using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroImage : MonoBehaviour
{
    public Sprite[] spritesImage;
    public Image imageIntro;
    public Image[] spritesDots;
    public string[] textHeading;
    public Text heading;
    public string[] textDescription;
    public Text description;

    public int index = 0;
    public Button nextBt;
    public Color active;
    public GameObject intro;
    public GameObject cityChoose;
    public GameObject backButton;

    private void Start()
    {
        nextBt.onClick.AddListener(introClickButton);
    }

    void Update()
    {

    }

    public void introClickButton()
    {
        if (index == 0)
        {
            backButton.SetActive(true);
            description.GetComponent<AspectRatioFitter>().aspectRatio = 3.6f;
        }
        else
        {
            description.GetComponent<AspectRatioFitter>().aspectRatio = 3.8f;
        }
        index++;

        if (index >= 6)
        {
            
            intro.SetActive(false);
            cityChoose.SetActive(true);
        }
        else
        {
            imageIntro.sprite = spritesImage[index];
            heading.text = textHeading[index];
            description.text = textDescription[index];
            if(index == 3)
            {
                description.GetComponent<RectTransform>().sizeDelta = new Vector2(289f, description.GetComponent<RectTransform>().sizeDelta.y);
            }
            else
                description.GetComponent<RectTransform>().sizeDelta = new Vector2(280f, description.GetComponent<RectTransform>().sizeDelta.y);

            foreach (var x in spritesDots)
            {
                x.color = new Color(0.7686275f, 0.7686275f, 0.7686275f, 1);
            }
            spritesDots[index].color = active;
        }
    }

    public void introBackButton()
    {
        if (index == 2)
        {
            description.GetComponent<AspectRatioFitter>().aspectRatio = 3.6f;
        }
        else if (index == 1)
        {
            backButton.SetActive(false);
            description.GetComponent<AspectRatioFitter>().aspectRatio = 3.2f;
        }
        else 
        {
            description.GetComponent<AspectRatioFitter>().aspectRatio = 3.8f;
        }


        if (index > 0)
        {
            index--;
            imageIntro.sprite = spritesImage[index];
            heading.text = textHeading[index];
            description.text = textDescription[index];

            if (index == 3)
            {
                description.GetComponent<RectTransform>().sizeDelta = new Vector2(289f, description.GetComponent<RectTransform>().sizeDelta.y);
            }
            else
                description.GetComponent<RectTransform>().sizeDelta = new Vector2(280f, description.GetComponent<RectTransform>().sizeDelta.y);

            foreach (var x in spritesDots)
            {
                x.color = new Color(0.7686275f, 0.7686275f, 0.7686275f, 1);
            }
            spritesDots[index].color = active;
        }
       
            
        
    }
}
