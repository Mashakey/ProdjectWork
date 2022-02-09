using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMovingFurniture : MonoBehaviour
{
    Color orange;
    Color unActive;
    public FurnituraRotationAndMoving furnituraRotationAndMoving;
    public bool SpriteMoving = true;
    public GameObject table;
    Vector3 startPosition;

    public GameObject circleRotation;
    public GameObject circleMoving;
    public GameObject SpriteRotation;
    public GameObject cameraMain;


    private void Start()
    {
        startPosition = table.transform.position;
        cameraMain = Camera.main.gameObject;

    }

    private void OnMouseDown()
    {
        if (SpriteMoving)
        {
            ColorUtility.TryParseHtmlString("#FF9900", out orange);
            gameObject.GetComponent<Image>().color = orange;
            furnituraRotationAndMoving.moving = true;
            cameraMain.GetComponent<TutorialCameraRotation>().enabled = false;

        }
    }

    private void Update()
    {
        if (SpriteMoving)
        {
            if (table.transform.position != startPosition)
            {
                StartCoroutine(startMoving());
            }
        }
    }

    IEnumerator startMoving()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        circleRotation.SetActive(true);
        circleMoving.SetActive(false);
        SpriteRotation.GetComponent<TutorialRotation>().SpriteRotation = true;
        ColorUtility.TryParseHtmlString("#FFFFFF", out unActive);
        gameObject.GetComponent<Image>().color = unActive;
        SpriteMoving = false;
    }
}
