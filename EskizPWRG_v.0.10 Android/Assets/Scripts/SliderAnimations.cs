using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnimations : MonoBehaviour
{
    public Image slider;
    public Image sliderFavorites;
    public Text favotitesText;
    public Text materialText;

    public Font ninitoBold;
    public Font ninitoRegular;
    public string animationSlider;
    Color greyColor;
    Color blueColor;
    public bool favorutList = false;

    private void Awake()
    {
        ColorUtility.TryParseHtmlString("#868686", out greyColor);
        ColorUtility.TryParseHtmlString("#0C68C8", out blueColor);

    }

    public void animationFavoritesForSlider()
    {
        slider.enabled = false;
        sliderFavorites.enabled = true;
        favotitesText.GetComponent<Text>().font = ninitoBold;
        Debug.LogError(favotitesText.GetComponent<Text>().font);
        favotitesText.GetComponent<Text>().color = blueColor;
        materialText.GetComponent<Text>().font = ninitoRegular;
        materialText.GetComponent<Text>().color = greyColor;
        FindObjectOfType<FavoriteStatusSetter>().favorires = true;
    }

    public void animationMaterialsForSlider()
    {
        slider.enabled = true;
        sliderFavorites.enabled = false;
        materialText.GetComponent<Text>().font = ninitoBold;
        materialText.GetComponent<Text>().color = blueColor;
        favotitesText.GetComponent<Text>().font = ninitoRegular;
        favotitesText.GetComponent<Text>().color = greyColor;
        FindObjectOfType<FavoriteStatusSetter>().favorires = false;
    }

    public void favoritesPaint()
    {
        favorutList = true;
    }

    public void regularPaint()
    {
        favorutList = false;
    }
}
