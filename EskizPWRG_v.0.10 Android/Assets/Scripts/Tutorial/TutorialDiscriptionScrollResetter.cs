using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDiscriptionScrollResetter : MonoBehaviour
{
    [SerializeField]
    Scrollbar wallpaperDescriptionScrollbar;

	private void OnEnable()
	{
		wallpaperDescriptionScrollbar.value = 1;
	}

}
