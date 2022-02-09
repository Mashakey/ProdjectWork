using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialChangeSprite : MonoBehaviour
{
    Color orange;
    bool isActive = false;
    public GameObject circlType;
    public GameObject windowType;
    public GameObject editRoom;
    public GameObject tutorialBurWind;
    public bool mouseUp = false;


    private void Start()
    {
        
    }

    private void OnMouseDown()
    {
        if(mouseUp == true)
        {
            MoveObjectToMousePosition moveScript = GetComponentInParent<MoveObjectToMousePosition>();
            Debug.Log("Window move button click");
            if (!isActive)
            {
                ColorUtility.TryParseHtmlString("#FF9900", out orange); //orange
                isActive = true;
                gameObject.GetComponent<Image>().color = orange;
                circlType.SetActive(false);
                editRoom.SetActive(false);
                windowType.SetActive(true);
                moveScript.isActive = false;
                moveScript.enabled = false;
                tutorialBurWind.SetActive(false);
            }
            else
            {
                gameObject.GetComponent<Image>().color = Color.white;
                isActive = false;

            }
        }
        
    }
}
