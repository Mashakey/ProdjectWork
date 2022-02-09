using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class OpenImageGallery : MonoBehaviour
{
    public GameObject ImageGalleryPrefab;
    public GameObject GridLayout;

    void Start()
    {

    }

    public void PickImageFromGallery(int maxSize = 1024)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.LogError("Image path: " + path);
            if (path != null)
            {
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);

                if (texture == null)
                {
                    Debug.LogError("Couldn't load texture from " + path);
                    return;
                }
                DataCacher.CacheUserPhotoTexture(createReadableTexture2D(texture));
                Resources.UnloadUnusedAssets();
                UserPhotosPageFiller userPhotosPageFiller = GetComponentInParent<UserPhotosPageFiller>();
                userPhotosPageFiller.FillUserPhotosPage();
                //GameObject image = Instantiate(ImageGalleryPrefab, GridLayout.transform.parent);
                //image.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0,0, texture.width, texture.height), Vector2.zero);
            }
        });
    }


    Texture2D createReadableTexture2D(Texture2D texture2d)
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(
                    texture2d.width,
                    texture2d.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(texture2d, renderTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;
        Texture2D readableTextur2D = new Texture2D(texture2d.width, texture2d.height);
        readableTextur2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        readableTextur2D.Apply();

        //Material tempMaterialToRescaleTexture = new Material(Shader.Find("Pavel/CustomPBS"));
        //tempMaterialToRescaleTexture.mainTexture = readableTextur2D;
        //Sprite tempSprite = ()
        //tempMaterialToRescaleTexture.mainTextureScale = new Vector2(readableTextur2D.width / 5, readableTextur2D.height / 5);
        //readableTextur2D = (Texture2D)tempMaterialToRescaleTexture.mainTexture;
        //Debug.LogWarning("SCALE ### = " + readableTextur2D.width + " " + readableTextur2D.height);
        //readableTextur2D.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTexture);
        return Resize(readableTextur2D);
    }

    Texture2D Resize(Texture2D texture2D)
    {
        float resize—oefficient = 500f / texture2D.width;
        int targetX = (int)(resize—oefficient * texture2D.width);
        int targetY = (int)(resize—oefficient * texture2D.height);
        Debug.LogWarningFormat($"coef = {resize—oefficient}  targetX = {targetX}  targetY = {targetY}");
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }
}
