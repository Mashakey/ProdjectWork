using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class socialButton : MonoBehaviour
{
    private void Update()
    {
    }
    public void openURL(string URL)
    {
        Application.OpenURL(URL);
    }
}
