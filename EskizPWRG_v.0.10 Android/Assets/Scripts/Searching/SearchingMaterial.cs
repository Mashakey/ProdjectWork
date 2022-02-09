using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using static DataTypes;

public class SearchingMaterial : MonoBehaviour
{
    public GameObject buttonCancellation;
    private Animator animationButton;

    public GameObject nameMaterials;

    List<MaterialJSON> materialSearching = new List<MaterialJSON>();

    private void Awake()
    {
        animationButton = buttonCancellation.GetComponent<Animator>();
    }

    public void OpenKeyboardAndSearch()
    {
        buttonCancellation.SetActive(true);
        animationButton.Play("keyboard");
        materialSearching.Clear();
    }

    public void CloseKeyboard()
    {
        animationButton.Play("Cansel");

        if (gameObject.GetComponent<InputField>().text != "" && gameObject.GetComponent<InputField>().text != " ")
        {
            string enteredMassage = gameObject.GetComponent<InputField>().text;

            if (nameMaterials.GetComponent<Text>().text == "Обои")
            {
                foreach (var wallpaper in GlobalApplicationManager.Wallpapers)
                {
                    if (Regex.IsMatch(wallpaper.name, enteredMassage, RegexOptions.IgnoreCase))
                    {
                        materialSearching.Add(wallpaper);
                        
                    }
                }
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(materialSearching);
                drumScroll.nullSearchResult();
            }
            if (nameMaterials.GetComponent<Text>().text == "Обои для покраски")
            {
                foreach (var wallpaper in GlobalApplicationManager.WallpapersForPainting)
                {
                    if (Regex.IsMatch(wallpaper.name, enteredMassage, RegexOptions.IgnoreCase))
                    {
                        materialSearching.Add(wallpaper);
                        
                    }
                }
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(materialSearching);
                drumScroll.nullSearchResult();
            }
            if (nameMaterials.GetComponent<Text>().text == "Ламинат")
            {
                foreach (var laminat in GlobalApplicationManager.Laminates)
                {
                    if (Regex.IsMatch(laminat.name, enteredMassage, RegexOptions.IgnoreCase))
                    {
                        materialSearching.Add(laminat);
                        
                    }
                }
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(materialSearching);
                drumScroll.nullSearchResult();
            }
            if (nameMaterials.GetComponent<Text>().text == "Линолеум")
            {
                foreach (var linoleum in GlobalApplicationManager.Linoleums)
                {
                    if (Regex.IsMatch(linoleum.name, enteredMassage, RegexOptions.IgnoreCase))
                    {
                        materialSearching.Add(linoleum);
                        
                    }
                }
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(materialSearching);
                drumScroll.nullSearchResult();
            }
            if (nameMaterials.GetComponent<Text>().text == "ПВХ")
            {
                foreach (var pvc in GlobalApplicationManager.PVCs)
                {
                    if (Regex.IsMatch(pvc.name, enteredMassage, RegexOptions.IgnoreCase))
                    {
                        materialSearching.Add(pvc);
                        
                    }
                }
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(materialSearching);
                drumScroll.nullSearchResult();
            }
            if (nameMaterials.GetComponent<Text>().text == "Плинтус")
            {
                foreach (var baseboard in GlobalApplicationManager.Baseboards)
                {
                    if (Regex.IsMatch(baseboard.name, enteredMassage, RegexOptions.IgnoreCase))
                    {
                        materialSearching.Add(baseboard);
                        
                    }
                }
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(materialSearching);
                drumScroll.nullSearchResult();
            }
            if (nameMaterials.GetComponent<Text>().text == "Дверь")
            {
                foreach (var door in GlobalApplicationManager.Doors)
                {
                    if (Regex.IsMatch(door.name, enteredMassage, RegexOptions.IgnoreCase))
                    {
                        materialSearching.Add(door);
                        
                    }
                }
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(materialSearching);
                drumScroll.nullSearchResult();
            }
        }
        else 
        {
            if (nameMaterials.GetComponent<Text>().text == "Обои")
            {
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(GlobalApplicationManager.Wallpapers);
            }
            if (nameMaterials.GetComponent<Text>().text == "Обои для покраски")
            {
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(GlobalApplicationManager.WallpapersForPainting);
            }
            if (nameMaterials.GetComponent<Text>().text == "Ламинат")
            {
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(GlobalApplicationManager.Laminates);
            }
            if (nameMaterials.GetComponent<Text>().text == "Линолеум")
            {
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(GlobalApplicationManager.Linoleums);
            }
            if (nameMaterials.GetComponent<Text>().text == "ПВХ")
            {
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(GlobalApplicationManager.PVCs);
            }
            if (nameMaterials.GetComponent<Text>().text == "Плинтус")
            {
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(GlobalApplicationManager.Baseboards);
            }
            if (nameMaterials.GetComponent<Text>().text == "Дверь")
            {
                DrumScroll drumScroll = FindObjectOfType<DrumScroll>();
                drumScroll.SetRectTransormSettings(GlobalApplicationManager.Doors);
            }
        }
    }
}
