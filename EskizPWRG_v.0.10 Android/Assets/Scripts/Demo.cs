using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour {

	public Text slideOneText;
    public GameObject sliderOne;
    

    public void textUpdate()
    {
        
        float value = sliderOne.GetComponent<Slider>().value;
        
        slideOneText.text = Mathf.RoundToInt(value).ToString() + "₽";
    }
}