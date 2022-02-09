using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pageChanger : MonoBehaviour
{
    public GameObject newPage;
    public GameObject currentPage;

    public Sprite unactiveGoToRoom;
    public Sprite activeGoToRoom;
    public GameObject opsTypeRoom;

    public saveWindHamburger saveWindHamburger;
    public SelectTypeRoom selectTypeRoom;

    private void Update()
    {
    }

    public void clickBut()
    {
         currentPage.SetActive(false);
         newPage.SetActive(true);        
    }

    public void clickWarning()
    {
        currentPage.SetActive(true);
    }
    public void closeWarning()
    {
        currentPage.SetActive(false);
    }

    public void goToEditor()
    {
        if (gameObject.GetComponent<Image>().sprite == activeGoToRoom)
        {
            ParametersChangeHandler parametersChangeHandler = GameObject.FindObjectOfType<ParametersChangeHandler>();
            newPage.SetActive(true);
            parametersChangeHandler.OnEnterRoomButtonClick();
            currentPage.SetActive(false);
            selectTypeRoom.ResetParametersRoom();
            opsTypeRoom.SetActive(false);

        }
        else
            opsTypeRoom.SetActive(true);
    }

    public void goToEditorTutorial()
    {
        if (gameObject.GetComponent<Image>().sprite == activeGoToRoom)
        {
            newPage.SetActive(true);
            currentPage.SetActive(false);

        }
        else
            opsTypeRoom.SetActive(true);
    }

    public void buttonHumburger()
    {
        saveWindHamburger.windForBack = currentPage;
        clickBut();
    }

    public void buttonBackForHumb()
    {
        newPage = saveWindHamburger.windForBack;
        clickBut();
    }

    public void proceedShop()
    {
        currentPage.SetActive(false);
        newPage.SetActive(true);
        newPage.GetComponent<ScrollShops>().CreateContentFields();
    }

}
