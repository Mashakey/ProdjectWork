using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputKeyboard : MonoBehaviour, IPointerDownHandler{

    //public TouchScreenKeyboard keyboard;
    //public GameObject activeWall;
    //bool e = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Keyboard opened");
        //keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad, false, false, true);
        
        
        
        
        //TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad, false, false, true);
        
        
        
        
        
        //keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad);
        //keyboard.active = true;
        //keyboard.text = keyboard.text + "см";
        //Debug.Log("aaaa");
        //e = true;
    }

 //   public void Update()
 //   {
 //       if (e == true)
 //       {
 //           Text inputWallText = activeWall.transform.parent.Find("Text").gameObject.GetComponent<Text>();
 //           inputWallText.text = gameObject.GetComponent<InputField>().text;
 //       }
        
 //       if (TouchScreenKeyboard.visible == false && keyboard != null)
 //       {
 //           if (keyboard.done)
 //           {
 //               //Text inputWallText = activeWall.transform.parent.Find("Text").gameObject.GetComponent<Text>();
 //               //inputWallText.text = gameObject.GetComponent<InputField>().text;
 //               keyboard = null;
 //           }
 //       }
 //   }

 //   public void keyboRD()
	//{
 //       keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NumberPad, false, false, true);
 //   }
}

