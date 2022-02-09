using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectShop : MonoBehaviour
{
    public GameObject activeSelect;
    ListOfSelectShop listOfSelectShop;

    private void Awake()
    {
        listOfSelectShop = FindObjectOfType<ListOfSelectShop>();
    }
    public void SelectionOfShop()
    {
        FindObjectOfType<SendEstimateToShop>().AddOrDeleteShopInList(name);
        if (activeSelect.activeSelf != true)
        {
            activeSelect.SetActive(true);
            //listOfSelectShop.SelectionShops.Add(gameObject.name);
        }
        else
        {
            activeSelect.SetActive(false);
            //listOfSelectShop.SelectionShops.Remove(gameObject.name);
        }
    }
}
