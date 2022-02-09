using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using static DataTypes;
using UnityEngine.UI;

public class ShowShopOnMap : MonoBehaviour
{
    const string linkBase = "https://www.google.com/maps/search/?api=1&query=";
    public GameObject linkWesite;
    public Text phone;

    public void OpenMapOnShopPosiiton()
    {
        ShopJson shopJson = GlobalApplicationManager.GetShopJsonById(name);
        if (shopJson == null)
        {
            return;
        }
        string link = linkBase + WWW.EscapeURL(shopJson.address.formatted) + "&language=ru";
        Debug.LogWarning("Trying to open shop on map. Adress is " + shopJson.address.formatted);
#if !UNITY_WEBGL
        Application.OpenURL(link);
#else
            OpenLinkJSPlugin.OpenLink(link);
#endif

        //MyTools.Helpers.CorouWaiter.Start(ServerDataController.SendShopStatistics(m_ShopInfo.id, new[] { "map" }));
    }

    public void OpenLinkTheWebsite()
    {
        if (linkWesite.name != " " && linkWesite.name != "")
        {
            string link = linkWesite.name;
            if (!link.StartsWith("http") || !link.StartsWith("https"))
            {
                link = @"http://" + link;
            }
#if !UNITY_WEBGL
            Application.OpenURL(link);
#else
            OpenLinkJSPlugin.OpenLink(link);
#endif
        }

    }

    public void OpenPhoneShop()
    {
        MakeCall(phone.text);
    }
    void MakeCall(string nNumber)
    {
        string number = FixedPhone(nNumber);
#if UNITY_EDITOR
        Debug.Log("Making a call to:\n" + number);
#else
        Application.OpenURL("tel:" + number);
#endif
    }
    string FixedPhone(string rawPhone)
    {
        Regex pattern = new Regex(@"[(),\-\s]");
        return pattern.Replace(rawPhone, string.Empty);
        // string number = rawPhone;
        // //number = number.Replace("+", "");
        // number = number.Replace("(", "");
        // number = number.Replace(")", "");
        // number = number.Replace(" ", "");
        // number = number.Replace("-", "");
        // number = number.Replace(",", "");
        // return number;
    }
}
