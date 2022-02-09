using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLoadingScreen : MonoBehaviour
{
	[SerializeField]
	Slider LoadingSlider;
	[SerializeField]
	Text percentField;
	[SerializeField]
	GameObject NoConnectionToServerPopup;

    public void OpenErrorPopup()
	{
		NoConnectionToServerPopup.SetActive(true);
	}

	public void SetSliderValue(float value)
	{
		LoadingSlider.value = value;
		percentField.text = Mathf.Round(value * 100) + "%";
	}
}
