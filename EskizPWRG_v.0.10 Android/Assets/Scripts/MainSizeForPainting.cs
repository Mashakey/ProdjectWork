using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MainSizeForPainting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {          
        float width = gameObject.GetComponent<RectTransform>().rect.width;
        Vector2 newSize = new Vector2((width - 22f) / 3, (width + 22) / 3);
        gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;

    }
}
