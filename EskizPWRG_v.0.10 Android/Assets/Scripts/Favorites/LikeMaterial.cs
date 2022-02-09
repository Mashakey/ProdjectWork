using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataTypes;

public class LikeMaterial : MonoBehaviour
{
    public Sprite like;
    public Sprite unlike;
    public GameObject obj;
    //public managerMaterial managerMat;


    public void likedUp()
    {
        string materialId = transform.GetComponentInParent<MaterialField>().transform.name;
        Debug.LogWarning(materialId);
        MaterialJSON materialJson = GlobalApplicationManager.GetMaterialJsonById(materialId);
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

    public void SetHeartWhite()
    {
        gameObject.GetComponent<Image>().sprite = unlike;
    }


}
