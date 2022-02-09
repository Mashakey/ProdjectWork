using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainSizeForMaterial : MonoBehaviour
{
    private void Awake()
    {
    }

    void Update()
    {
        float width = gameObject.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2((width - 22f), (width + 22));
        gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }
}
