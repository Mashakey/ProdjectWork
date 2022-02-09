using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button3DMove : MonoBehaviour
{
    Color orange;
    bool isActive = false;
    public void OnMouseDown()
    {
        Debug.Log("Window move button click");
        if (!isActive)
        {
            Buttons3DWindow buttons3dWindow = GetComponentInParent<Buttons3DWindow>();
            if (buttons3dWindow != null)
            {
                buttons3dWindow.TurnOffScaleButton();
            }
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }


    public void TurnOn()
    {
        isActive = true;
        ColorUtility.TryParseHtmlString("#FF9900", out orange); //orange
        var mover2D = transform.GetComponentInParent<WallGridMoverTwoAxis>();
        if (mover2D != null)
        {
            Debug.Log("GridMoverTwoAxis is found");
            GetComponent<SpriteRenderer>().color = orange;
            mover2D.Activate();
        }
        var mover1D = transform.GetComponentInParent<WallGridMoverOneAxis>();
        if (mover1D != null)
        {
            GetComponent<SpriteRenderer>().color = orange;

            Debug.Log("GridMoverTwoAxis is found");
            mover1D.Activate();
        }
    }

    public void TurnOff()
    {
        var mover2D = transform.GetComponentInParent<WallGridMoverTwoAxis>();
        if (mover2D != null)
        {
            Debug.Log("GridMoverTwoAxis is found");
            GetComponent<SpriteRenderer>().color = Color.white;

            mover2D.Deactivate();
        }
        var mover1D = transform.GetComponentInParent<WallGridMoverOneAxis>();
        if (mover1D != null)
        {
            GetComponent<SpriteRenderer>().color = Color.white;

            Debug.Log("GridMoverTwoAxis is found");
            mover1D.Deactivate();
        }
        isActive = false;
    }
}
