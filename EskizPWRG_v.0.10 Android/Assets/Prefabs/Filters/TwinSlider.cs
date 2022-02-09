using System;
using UnityEngine;
using UnityEngine.UI;


public class TwinSlider : MonoBehaviour {

	public Action<float> OnSliderChange;

	[SerializeField]
	private Slider SliderOne;

	[SerializeField]
	private Slider SliderTwo;

	[SerializeField]
	private Image Background;

	[SerializeField]
	private Image Filler;

	[SerializeField]
	private Color Color;

	public float Min;

	public float Max;

	public float Border;

	private RectTransform _fillerRect;

	private float _width;

	public FilteringCoast filteringCoast;

	float value;

	public bool activePrise;

	public GameObject filters;
	Color unActiveColor;
	Color ActiveColor;

    private void Awake()
    {
		ColorUtility.TryParseHtmlString("#868686", out unActiveColor);
		ColorUtility.TryParseHtmlString("#FF9900", out ActiveColor);
	}

    public void StartPrice() {
		_fillerRect = Filler.GetComponent<RectTransform>();
		_width = GetComponent<RectTransform>().sizeDelta.x / 2f;
		SliderOne.minValue = Min;
		SliderOne.maxValue = Max;
		SliderTwo.minValue = Min;
		SliderTwo.maxValue = Max;
		SliderOne.value = SliderOne.minValue;
		SliderTwo.value = SliderTwo.maxValue;
		Filler.color = Color;
		Border = Max * 0.1f;
		DrawFiller(SliderOne.handleRect.localPosition, SliderTwo.handleRect.localPosition);
	}


	public void OnCorrectSliderOne (/*float value*/) 
	{
		value = SliderOne.GetComponent<Slider>().value;
		if (value > SliderTwo.value - Border) 
		{
			SliderOne.value = SliderTwo.value - Border;
		} 
		else 
		{
			filteringCoast.mivalnValue = (int)SliderOne.value;
		}
		DrawFiller(SliderOne.handleRect.localPosition, SliderTwo.handleRect.localPosition);

		if (SliderOne.value != Min || SliderTwo.value != Max)
		{
			activePrise = true;
			filters.GetComponent<Image>().color = ActiveColor;
		}
        else
        {
			activePrise = false;
			filters.GetComponent<Image>().color = unActiveColor;
		}
	}


	public void OnCorrectSliderTwo (/*float value*/) 
	{
		value = SliderTwo.GetComponent<Slider>().value;
		if (value < SliderOne.value + Border) {
			SliderTwo.value = SliderOne.value + Border;

		}
		else 
		{
			filteringCoast.maxValue = (int)SliderTwo.value;
		}
		DrawFiller(SliderOne.handleRect.localPosition, SliderTwo.handleRect.localPosition);

		if (SliderOne.value != Min || SliderTwo.value != Max)
		{
			activePrise = true;
			filters.GetComponent<Image>().color = ActiveColor;
		}
		else
		{
			activePrise = false;
			filters.GetComponent<Image>().color = unActiveColor;
		}
	}

	void DrawFiller (Vector3 one, Vector3 two) {
		float left = Mathf.Abs(_width + one.x);
		float right = Mathf.Abs(_width - two.x);
		_fillerRect.offsetMax = new Vector2(-right, 0f);
		_fillerRect.offsetMin = new Vector2(left, 0f);
		Debug.LogError(SliderOne.handleRect.localPosition +" " + SliderTwo.handleRect.localPosition);
	}

}

