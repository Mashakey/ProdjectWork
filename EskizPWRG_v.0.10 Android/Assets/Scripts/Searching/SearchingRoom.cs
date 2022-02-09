using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SearchingRoom : MonoBehaviour
{
    public GameObject buttonCancellation;
    private Animator animationButton;
    public List<GameObject> myRoomsArray = new List<GameObject>();
    public Transform scrollRooms;

    

    private void Awake()
    {
        animationButton = buttonCancellation.GetComponent<Animator>();
    }

    public void OpenKeyboardAndSearch()
    {
        buttonCancellation.SetActive(true);
        animationButton.Play("keyboard");
        myRoomsArray.Clear();
        foreach (Transform child in scrollRooms)
        {
            if (child.name != "AddRoom")
            {
                myRoomsArray.Add(child.gameObject);
            }           
        }
    }

    public void CloseKeyboard()
    {
        animationButton.Play("Cansel");

        if (gameObject.GetComponent<InputField>().text != "" && gameObject.GetComponent<InputField>().text != " ")
        {
            string enteredMassage = gameObject.GetComponent<InputField>().text;

            foreach (var child in myRoomsArray)
            {
                if (!Regex.IsMatch(child.transform.Find("NameRoom").gameObject.GetComponent<Text>().text, enteredMassage, RegexOptions.IgnoreCase))
                {
                    child.SetActive(false);
                }
            }

        }
        else 
        {
            foreach (var child in myRoomsArray)
            {                
                child.SetActive(true);                
            }
        }
    }
}
