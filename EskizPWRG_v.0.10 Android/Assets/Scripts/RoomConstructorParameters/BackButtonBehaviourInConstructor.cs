using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonBehaviourInConstructor : MonoBehaviour
{
	[SerializeField]
	GameObject constructorBackButton;

    public void EnableBackButton()
	{
		constructorBackButton.SetActive(true);
	}

	public void DisableBackButton()
	{
		constructorBackButton.SetActive(false);
	}
}
