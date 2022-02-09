using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;
using UnityEngine.UI;

public class ValueShop : MonoBehaviour
{
    [SerializeField]
    Text nameShop;

    [SerializeField]
    Text addressShop;

    [SerializeField]
    Text availabilityShop;

    [SerializeField]
    Text phoneShop;

    [SerializeField]
    GameObject LinkTheWebsite;

    public void FillFieldWithShope(ShopJson shopJson)
    {
        gameObject.name = shopJson.id;
        nameShop.text = shopJson.name;
        addressShop.text = shopJson.address.streetAddress;
        //availabilityShop.text = shopJson.description;
        availabilityShop.text = GetAvailableMaterialsFormattedLine(shopJson);
        LinkTheWebsite.name = shopJson.website;
        phoneShop.text = shopJson.phone;
    }

    public string GetAvailableMaterialsFormattedLine(ShopJson shopJson)
    {
        string formattedString = "";
        List<string> availableMaterialTypes = GetAvailableMaterialTypes(shopJson);
        if (availableMaterialTypes.Count > 0)
        {
            formattedString = "Доступно в магазине: ";
            foreach (var materialType in availableMaterialTypes)
            {
                formattedString += materialType + ", ";
            }
            formattedString = formattedString.TrimEnd(',', ' ');
        }
        return (formattedString);
    }

    public List<string> GetAvailableMaterialTypes(ShopJson shopJson)
    {
        List<string> availableMaterialTypes = new List<string>();
        foreach (var material in RoomCostCalculator.materials)
        {
            if (shopJson.goods.Contains(material.Key))
            {
                string availableMaterialType = GetMaterialTypeRussianTranslation(GlobalApplicationManager.GetMaterialJsonById(material.Key).type);
                if (!availableMaterialTypes.Contains(availableMaterialType))
                {
                    availableMaterialTypes.Add(availableMaterialType);
                }
            }
        }
        return (availableMaterialTypes);
    }

    public string GetMaterialTypeRussianTranslation(string materialType)
    {
        if (materialType == "wallpaper")
        {
            return ("обои");
        }
        else if (materialType == "wallpaper_for_painting")
        {
            return ("обои для покраски");
        }
        else if (materialType == "baseboard")
        {
            return ("плинтус");
        }
        else if (materialType == "door")
        {
            return ("дверь");
        }
        else if (materialType == "laminate")
        {
            return ("ламинат");
        }
        else if (materialType == "linoleum")
        {
            return ("линолеум");
        }
        else if (materialType == "paint")
        {
            return ("краска");
        }
        else if (materialType == "pvc")
        {
            return ("ПВХ");
        }
        else
        {
            Debug.LogErrorFormat("Unknown material type: '{1}'", materialType);
            return ("");
        }
    }

}
