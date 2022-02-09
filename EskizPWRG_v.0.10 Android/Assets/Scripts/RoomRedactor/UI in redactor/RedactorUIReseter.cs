using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedactorUIReseter : MonoBehaviour
{
	[SerializeField]
	Transform OpenedSubmenu;
	[SerializeField]
	Transform Submenu;
	[SerializeField]
	Transform EstimateButton;
	[SerializeField]
	Transform AddingMenu;


	private void OnEnable()
	{
		ResetRedactorUI();
	}


	public void ResetRedactorUI()
	{
		OpenedSubmenu.gameObject.SetActive(false);
		Submenu.gameObject.SetActive(true);
		EstimateButton.gameObject.SetActive(true);
		AddingMenu.gameObject.SetActive(true);
	}
}
