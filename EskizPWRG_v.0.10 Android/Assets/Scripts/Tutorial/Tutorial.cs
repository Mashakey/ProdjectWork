using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject myRoom;
    public GameObject menuWnd;
    public GameObject template;
    public GameObject selectTypeRoom;
    public GameObject circlZoom;
    public GameObject circlWind;
    public GameObject circlMoving;
    public int indexsNext = 0;
    public GameObject swipeCircle;
    public GameObject typeCiecle;
    public GameObject addPanel;
    public GameObject circleEd;
    public GameObject circleEd2;
    public GameObject circleEd3;
    public GameObject notePrefab;
    public GameObject note;
    public GameObject tapWall;
    public GameObject tapNote;
    public GameObject movingNote;
    public GameObject[] wall;
    public GameObject circleAddMateril;
    public GameObject materialForOneWall;
    public GameObject noteAdd;
    public tutorialWindBut tutorialWindBut;
    public GameObject addImage;
    public GameObject UpPanelAdd;
    public MaterialAddTutorial materialAddTutorial;
    public GameObject circleFurniture;
    public GameObject pageFurniture;
    public GameObject circleTapTable;
    public GameObject circleSubMenu;
    public Button subMenu;
    public GameObject buttonTable3D;
    public GameObject table;

    void Update(){}

    public void menu()
    {
        //circle.SetActive(false);
    }

    public void myRoooNext()
    {
        
        if (indexsNext == 0)
        {
            menuWnd.SetActive(true);
            circlWind.SetActive(false);
            indexsNext = 2;
        }
        else if(indexsNext == 1)
        {
            template.SetActive(true);
            circlMoving.SetActive(false);
            indexsNext = 2;
        }
        else if(indexsNext == 2)
        {
            selectTypeRoom.SetActive(true);
            
        }

        myRoom.SetActive(false);
    }

    public void editRoomNext()
    {
        if(indexsNext == 0)
        {
            Destroy(circlMoving);
            circlZoom.SetActive(true);
            indexsNext = 4;
        }
        else if (indexsNext == 1)
        {
            circlWind.SetActive(false);
            swipeCircle.SetActive(false);
            myRoom.SetActive(false);
            menuWnd.SetActive(true);
            indexsNext = 2;
        }
        else if (indexsNext == 2)
        {
            swipeCircle.SetActive(false);
            typeCiecle.SetActive(false);
            tapNote.SetActive(false);
            circleEd.SetActive(false);
            Destroy(circleEd2);
            
            addPanel.SetActive(false);
            addImage.GetComponent<Image>().enabled = true;
            UpPanelAdd.SetActive(false);
            materialAddTutorial.id = true;
            notePrefab.SetActive(true);
            noteAdd.GetComponent<MaterialAddTutorial>().idForNote();
            indexsNext = 3;
            if (circleEd3 != null)
            {
                circleEd3.SetActive(false);
            }
        }
        else if (indexsNext == 3)
        {
            notePrefab.SetActive(false);
            tapNote.SetActive(false);
            movingNote.SetActive(false);
            tapWall.SetActive(true);
            note.SetActive(true);
            Destroy(note.GetComponent<EventTrigger>());
            Destroy(note.GetComponent<NoteMoveAndOpen>());
            for (int i = 0; i < wall.Length; i++)
            {
                wall[i].GetComponent<TutorialWallClick>().click = true;
            }

            indexsNext = 5;
        }
        else if (indexsNext == 4)
        {

            tutorialWindBut.mouseUp = true; 
            circlZoom.SetActive(false);
            circlWind.SetActive(true);
            indexsNext = 1;
        }
        else if (indexsNext == 5)
        {
            
            materialForOneWall.SetActive(true);
            tapWall.SetActive(false);
            for (int i = 0; i < wall.Length; i++)
            {
                wall[i].GetComponent<TutorialWallClick>().click = false;
            }
                
            myRoom.SetActive(false);
            indexsNext = 6;
        }
        else if (indexsNext == 6)
        {
            if (circleAddMateril != null)
                Destroy(circleAddMateril); 
            circleEd.SetActive(false);
            addPanel.SetActive(false);
            myRoom.SetActive(false);
            template.SetActive(true);
            indexsNext = 7;
        }
        else if (indexsNext == 7)
        {
            myRoom.SetActive(false);
            pageFurniture.SetActive(true);
            circleFurniture.SetActive(false);
            indexsNext = 8;
        }
        else if (indexsNext == 8)
        {
            circleTapTable.SetActive(false);
            circleSubMenu.SetActive(true);
            subMenu.enabled = true;
            buttonTable3D.SetActive(false);
            table.GetComponent<FurnituraRotationAndMoving>().mouseUp = false;
            indexsNext = 9;
        }
        else if (indexsNext == 9)
        {
            myRoom.SetActive(false);
            selectTypeRoom.SetActive(true);
            circlWind.SetActive(false);
        }
    }

    public void nextIndexOne()
    {
        indexsNext = 1;
    }

    public void nextIndexTwo()
    {
        indexsNext = 2;
    }
    public void nextIndexThree()
    {
        indexsNext = 3;
    }

    public void nextIndexFoure()
    {
        indexsNext = 4;
    }

    public void nextIndexSix()
    {
        indexsNext = 6;
    }

    public void nextIndexSeven()
    {
        indexsNext = 7;
    }

    public void ActivatedWallTap()
    {
        for (int i = 0; i < wall.Length; i++)
        {
            wall[i].GetComponent<TutorialWallClick>().click = true;
        }

    }

    public void UnactivatedWallTap()
    {
        for (int i = 0; i < wall.Length; i++)
        {
            wall[i].GetComponent<TutorialWallClick>().click = false;
        }

    }
}
