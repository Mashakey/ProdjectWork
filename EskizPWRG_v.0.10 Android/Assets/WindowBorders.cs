using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBorders : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x < -0.22f)
            transform.localPosition = new Vector3(-0.22f, transform.localPosition.y, transform.localPosition.z);
        if (transform.localPosition.x > 0.23f)
            transform.localPosition = new Vector3(0.23f, transform.localPosition.y, transform.localPosition.z);
        if (transform.localPosition.y < -0.06f)
            transform.localPosition = new Vector3(transform.localPosition.x, -0.06f, transform.localPosition.z);
        if (transform.localPosition.y > 0.23f)
            transform.localPosition = new Vector3(transform.localPosition.x, 0.23f, transform.localPosition.z);


    }
}
