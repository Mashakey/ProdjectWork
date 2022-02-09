using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class tutorialLikeMaterial : MonoBehaviour
{
    public Sprite likeMat;
    public GameObject materialFavotriteImage;
    public Sprite selectMaterialPreviu;
    public Material selectMaterial;
    public Sprite materilImage;
    public GameObject descriptionImage;

    public Text nameMaterial;
    public Text costMaterial;
    public Text materialArticle;
    public Text descriptionMaterial;
    public Text descriptionName;
    public Text descriotionArticle;
    public Text descriptionCost;

    public Text nameFavorites;
    public Text costFavorites;
    public Text descriptionFavorites;

    public Scrollbar scrollbar;

    public void onClick()
    {
        gameObject.GetComponent<Image>().sprite = likeMat;
        materialFavotriteImage.GetComponent<Image>().sprite = selectMaterialPreviu;
        materilImageMassive.materialImage = materilImage;
        materilImageMassive.materialPreviu = selectMaterialPreviu;
        materilImageMassive.materialWalls = selectMaterial;

        nameFavorites.text = nameMaterial.text;
        costFavorites.text = costMaterial.text;
        descriptionFavorites.text = descriptionMaterial.text;

    }

    public void descriptionsForMaterial()
    {
        descriotionArticle.text = materialArticle.text;
        descriptionName.text = nameMaterial.text;
        descriptionCost.text = costMaterial.text;
        descriptionImage.GetComponent<Image>().sprite = materilImageMassive.materialImage;
        scrollbar.value = 1;
    }
}
