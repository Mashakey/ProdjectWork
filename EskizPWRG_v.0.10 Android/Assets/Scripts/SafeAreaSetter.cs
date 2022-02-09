using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeAreaSetter : MonoBehaviour
{
    public Canvas canvas;
    RectTransform panelSafeArea;

    Rect currrenSafeArea = new Rect();
    ScreenOrientation currentOrientation = ScreenOrientation.AutoRotation;
    void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();

        currentOrientation = Screen.orientation;
        currrenSafeArea = Screen.safeArea;

        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        if (panelSafeArea == null)
            return;

        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;

        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;

        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;

        currentOrientation = Screen.orientation;
        currrenSafeArea = Screen.safeArea;
    }

    void Update()
    {
        if((currentOrientation != Screen.orientation) || (currrenSafeArea != Screen.safeArea))
        {
            ApplySafeArea();
        }
    }
}
