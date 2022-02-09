using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSelectMaterial : MonoBehaviour
{
    public Text descriptionName;
    public Text descriotionArticle;
    public Text descriptionCost;
    public GameObject descriptionImage;
    public Image heart;
    public Sprite UnactiveHeart;
    public Material thisSelectMaterial;

    public Text materialName;
    public Text materialArticle;
    public Text materialCost;
    public GameObject materialImage;

    public void selectMaterial()
    {
        descriptionName.text = materialName.text;
        descriotionArticle.text = materialArticle.text;
        descriptionCost.text = materialCost.text;
        materilImageMassive.materialWalls = thisSelectMaterial;
        descriptionsForMaterial();
    }
    public void descriptionsForMaterial()
    {
        descriptionImage.GetComponent<Image>().sprite = materialImage.GetComponent<Image>().sprite;
        heart.sprite = UnactiveHeart;
    }
}
