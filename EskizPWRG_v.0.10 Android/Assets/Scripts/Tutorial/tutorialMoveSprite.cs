using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorialMoveSprite : MonoBehaviour
{
    Color orange;
    bool isActive = false;
    public GameObject circlSwipe;
    public tutorialChangeSprite tutorialChangeSprite;

    private void OnMouseDown()
    {
        MoveObjectToMousePosition moveScript = GetComponentInParent<MoveObjectToMousePosition>();
        if (!isActive)
        {
            ColorUtility.TryParseHtmlString("#FF9900", out orange); //orange
            isActive = true;
            gameObject.GetComponent<Image>().color = orange;        
            moveScript.isActive = true;
            moveScript.enabled = true;
            circlSwipe.SetActive(false);
            tutorialChangeSprite.enabled = true;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
            moveScript.isActive = false;

            moveScript.enabled = false;
            isActive = false;

        }
    }
}
