using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class PaintLikeButtonBehaviour : MonoBehaviour
{
    public Sprite like;
    public Sprite unlike;

    public GameObject likeSprite;
	//public managerMaterial managerMat;

	public void OnLikeButtonClick()
    {
        string materialId = name;
        Debug.LogWarning(materialId);
        MaterialJSON materialJson = GlobalApplicationManager.GetMaterialJsonById(materialId);
        if (likeSprite.GetComponent<Image>().sprite == unlike)
        {
            Debug.LogWarning("liking");

            FavoritesStorage.AddMaterialJsonToFavorites(materialJson);
            likeSprite.GetComponent<Image>().sprite = like;
        }
        else
        {
            Debug.LogWarning("Unliking");
            FavoritesStorage.DeleteMaterialFromFavorites(materialJson);
            likeSprite.GetComponent<Image>().sprite = unlike;
        }
    }

    public void SetHeartRedWithoutAdding()
    {
        likeSprite.GetComponent<Image>().sprite = like;
    }

}
