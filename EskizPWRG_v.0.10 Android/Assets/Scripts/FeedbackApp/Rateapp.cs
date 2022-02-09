using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.iOS;


public class Rateapp : MonoBehaviour
{
    public GameObject ratePrefab;
    public GameObject prefabThanks;
    public int indexStar;

    public void AppScore()
    {
        GameObject score = Instantiate(ratePrefab, transform.parent);
    }

    public void DeleteScore()
    {
        Destroy(gameObject);
    }

    public void stringIndexStar(int inputIndex)
    {
        indexStar = inputIndex;
        Debug.LogError(indexStar);
    }

    public void Estimate()
    {
        if (indexStar == 4)
        {
#if UNITY_ANRDOID

            Application.OpenURL("market://details?id=" + Application.identifier);

#elif UNITY_IOS

            UnityEngine.iOS.Device.RequestStoreReview();
#endif

        }
        else
        {
            Debug.LogError("Instantiate Thanks");
            GameObject PanelSafeArea = GameObject.Find("PanelSafeAreaMenu").gameObject;
            Instantiate(prefabThanks, PanelSafeArea.transform);
        }
        Destroy(gameObject);
    }
}
