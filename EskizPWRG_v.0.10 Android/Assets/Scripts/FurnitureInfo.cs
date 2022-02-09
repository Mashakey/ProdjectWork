using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureInfo : MonoBehaviour
{
    [SerializeField]
    private string _nameFurniture;

    [SerializeField]
    private string _fullNameFurniture;

    [SerializeField]
    private string _colorFurniture;

    [SerializeField]
    private string _sizeFurniture;

    [SerializeField]
    private Sprite _spriteFurniture;

    public GameObject FurnitureObject;

    public SelectInfoFurniture selectInfoFurniture;

    public GameObject FurnitreScroll;
    public GameObject FurniturePage;

    public int indexFurniture;

    public GameObject sofa;
    public GameObject table;
    public GameObject chair;
    public GameObject bed;
    public GameObject wardrobes;

    public void selectFurniture()
    {
        FurnitreScroll.SetActive(false);
        FurniturePage.SetActive(true);
        FurniturePage.GetComponent<FurniturePreviewPage>().SelectedFurnitureObject = FurnitureObject;

        selectInfoFurniture.nameFurniture.text = _nameFurniture;
        selectInfoFurniture.fullName.text = _fullNameFurniture;
        selectInfoFurniture.color.text = _colorFurniture;
        selectInfoFurniture.size.text = _sizeFurniture;
        selectInfoFurniture.imageFurniture.sprite = _spriteFurniture;

    }

    public void inputIndex(int index)
    {
        indexFurniture = index;
    }

    public void backFurniturePage()
    {
        FurnitreScroll.SetActive(true);
        FurniturePage.SetActive(false);

        switch (indexFurniture)
        {
            case 1:
                sofa.SetActive(true);
                break;

            case 2:
                table.SetActive(true);
                break;

            case 3:
                chair.SetActive(true);
                break;

            case 4:
                bed.SetActive(true);
                break;

            case 5:
                wardrobes.SetActive(true);
                break;
        }
    }
}
