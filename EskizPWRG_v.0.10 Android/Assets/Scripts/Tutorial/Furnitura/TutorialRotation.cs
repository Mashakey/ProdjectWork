using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRotation : MonoBehaviour
{
    Color orange;
    Color unActive;
    public FurnituraRotationAndMoving furnituraRotationAndMoving;
    public bool SpriteRotation = false;
    bool indexRotation = false;
    public GameObject table;
    Quaternion startRotation;

    public GameObject circleRotation;
    public GameObject circleSubMenu;
    public Button ButtonSubMenu;
    public GameObject cameraMain;

    private void Start()
    {
        startRotation = table.transform.rotation;
        cameraMain = Camera.main.gameObject;
    }

    private void OnMouseDown()
    {
        if (SpriteRotation)
        {
            ColorUtility.TryParseHtmlString("#FF9900", out orange);
            gameObject.GetComponent<Image>().color = orange;
            furnituraRotationAndMoving.rotation = true;
            indexRotation = true;
            furnituraRotationAndMoving.moving = false;
            cameraMain.GetComponent<TutorialCameraRotation>().enabled = false;
        }       
    }

    private void Update()
    {
        if (indexRotation)
        {
            if (table.transform.rotation.eulerAngles.y >= 300f)
            {
                StartCoroutine(startRotationCorutina());
                StartCoroutine(cameraRotate());
            }
        }
    }

    IEnumerator startRotationCorutina()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        circleRotation.SetActive(false);
        circleSubMenu.SetActive(true);
        ButtonSubMenu.enabled = true;
        ColorUtility.TryParseHtmlString("#FFFFFF", out unActive);
        gameObject.GetComponent<Image>().color = unActive;
        SpriteRotation = false;
        furnituraRotationAndMoving.tutorialButtonFurniture.SetActive(false);
        furnituraRotationAndMoving.rotation = false;

    }
    IEnumerator cameraRotate()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        cameraMain.GetComponent<TutorialCameraRotation>().enabled = true;

    }
}
