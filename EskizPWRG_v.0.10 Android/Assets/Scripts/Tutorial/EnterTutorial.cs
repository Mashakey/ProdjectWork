using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTutorial : MonoBehaviour
{

    public void OnEnterTutorialClick()
	{
        GlobalApplicationManager.isStartedMovingJsonsToRam = false;
        SceneManager.LoadScene("myT", LoadSceneMode.Single);
    }
    
}
