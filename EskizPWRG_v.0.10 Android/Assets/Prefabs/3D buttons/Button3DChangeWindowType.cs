using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button3DChangeWindowType : MonoBehaviour
{
	public void OnMouseDown()
	{
		Window window = GetComponentInParent<Window>();
		FindObjectOfType<WindowChanger>().OpenChangeWindowPage(window);
		ActiveWindowKeeper.IsRedactorActive = false;
	}
}
