using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeGridLayoutImageGallery : MonoBehaviour
{

    float index = 0;
    void Update()
    {
        float width = gameObject.GetComponent<RectTransform>().rect.width;
        if (gameObject.transform.childCount % 2 == 0)
        {
            index = gameObject.transform.childCount / 4;
        }
        else
            index = gameObject.transform.childCount / 4 + 1;

        Vector2 newSize = new Vector2((width - 1.35f) / 4, (width + 1.35f) / 4);
        gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
        float height = gameObject.GetComponent<GridLayoutGroup>().cellSize.y + gameObject.GetComponent<GridLayoutGroup>().spacing.y;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, height * index - 22);

    }
}
