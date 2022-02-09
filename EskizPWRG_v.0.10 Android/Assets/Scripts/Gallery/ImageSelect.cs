using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSelect : MonoBehaviour
{
    [SerializeField]
    GameObject Selection;

    public void selectImageInGallery()
    {
        GameObject selectedImage = GameObject.Find("SelectedImage");
        selectedImage.transform.SetParent(gameObject.transform);
        selectedImage.GetComponent<RectTransform>().localPosition = new Vector3(0,0,0);
        selectedImage.transform.GetComponent<Image>().enabled = true;
    }
}
