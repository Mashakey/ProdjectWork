using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMaterialErrorPage : MonoBehaviour
{
    [SerializeField]
    GameObject ErrorPage;

    public void EnableNoMaterialErrorPage()
    {
        ErrorPage.SetActive(true);
    }

    public void DisableNoMaterialErrorPage()
    {
        ErrorPage.SetActive(false);
    }
}
