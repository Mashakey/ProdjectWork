using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotMaker : MonoBehaviour
{
    static Camera mainCamera;

    public static int resWidth = 300;
    public static int resHeight = 300;

    static string filePath = "D:/EskizDevDataAsync/Resources/";

    public static Texture2D MakeScreenshot()
	{

        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        mainCamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        mainCamera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        mainCamera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        return (screenShot);
        //byte[] bytes = screenShot.EncodeToPNG();
        //string filename = filePath + "Screen1";
        //System.IO.File.WriteAllBytes(filename, bytes);
        //Debug.Log(string.Format("Took screenshot to: {0}", filename));
    }

	private void Awake()
	{
        mainCamera = GetComponent<Camera>();
	}

}
