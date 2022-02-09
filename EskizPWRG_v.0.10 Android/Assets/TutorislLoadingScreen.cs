using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorislLoadingScreen : MonoBehaviour
{

    public Image loadingImage;
    public Text progressText;

    public float progress;

    void Start()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");
    }

    IEnumerator AsyncLoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingImage.fillAmount = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }

    }
}
