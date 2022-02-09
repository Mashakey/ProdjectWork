using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class CheckClickUI
{

    public static bool IsClikedOnUI()
	{
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id))
                {
                    return true;
                }
            }
        }
        if (EventSystem.current.IsPointerOverGameObject())
		{
            return true;
		}
        return false;
    }
}
