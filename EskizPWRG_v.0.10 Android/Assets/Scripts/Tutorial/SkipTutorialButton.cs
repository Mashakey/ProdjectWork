using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipTutorialButton : MonoBehaviour
{
    public void SkipTutorial()
	{
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    
}
