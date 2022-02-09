using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PagesContainer : MonoBehaviour
{
    [SerializeField]
    GameObject NotEnoughPlaceForFurniture;
    
    public void OpenNotEnoughPlaceForFurnitureWindow()
	{
        NotEnoughPlaceForFurniture.SetActive(true);
	}
}
