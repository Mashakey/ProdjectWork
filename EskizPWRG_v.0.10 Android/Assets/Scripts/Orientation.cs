using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientation : MonoBehaviour
{
    private void Start()
    {
        
        Screen.orientation = ScreenOrientation.AutoRotation;
        if (!ActiveWindowKeeper.IsRedactorActive)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    void Update()
    {

        if (!ActiveWindowKeeper.IsRedactorActive)
        {
            Screen.autorotateToPortrait = true;
            Screen.autorotateToLandscapeRight = false;
            Screen.autorotateToLandscapeLeft = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}
