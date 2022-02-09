using System.Collections;
using System.Collections.Generic;
using static DataTypes;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine;

public class SearchingMattPaints : MonoBehaviour
{
    public GameObject buttonCancellation;
    private Animator animationButton;

    List<MaterialJSON> materialSearching = new List<MaterialJSON>();

    [SerializeField]
    MattPaintsPageFiller mattPaintFiller;

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
            Debug.LogError("search message = " + enteredMassage);

            foreach (var paint in GlobalApplicationManager.Paints)
            {
                if (Regex.IsMatch(paint.name, enteredMassage, RegexOptions.IgnoreCase))
                {
                    Debug.LogError("search message = " + enteredMassage);

                    materialSearching.Add(paint);
                }
            }
            mattPaintFiller.CreatePaintFields(materialSearching);

        }
    }

}
