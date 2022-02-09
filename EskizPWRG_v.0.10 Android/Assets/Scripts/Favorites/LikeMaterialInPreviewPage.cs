using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class LikeMaterialInPreviewPage : MonoBehaviour
{
    public Sprite like;
    public Sprite unlike;



    public void LikeUp()
    {
        MaterialJSON materialJson = GetComponentInParent<CurrentMaterialJsonKeeper>().GetCurrentMaterialJson();
        if (gameObject.GetComponent<Image>().sprite == unlike)
        {
            Debug.LogWarning("liking");

            FavoritesStorage.AddMaterialJsonToFavorites(materialJson);
            gameObject.GetComponent<Image>().sprite = like;
        }
        else
        {
            Debug.LogWarning("Unliking");
            FavoritesStorage.DeleteMaterialFromFavorites(materialJson);
            gameObject.GetComponent<Image>().sprite = unlike;
        }
    }

    public void SetHeartRedWithoutAdding()
    {
        gameObject.GetComponent<Image>().sprite = like;
    }

    public void SetEmptyHeart()
	{
        gameObject.GetComponent<Image>().sprite = unlike;
    }
}
