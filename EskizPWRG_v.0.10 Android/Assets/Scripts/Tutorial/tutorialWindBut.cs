using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static DataTypes;


public class tutorialWindBut : MonoBehaviour
{
	public Vector3 Position;
	public Quaternion Rotation;
	public WindowType Type;
	//public string Type;
	public float Scale = 1f;
	public float Width;
	public float Height;
	DateTime mouseDownTime;
	public GameObject tutorialBurWind;
	public GameObject circlType;
	public Vector3 startPosition;
	public bool mouseUp = false;
	public tutorialChangeSprite tutorialChangeSprite;

    private void Start()
    {
		startPosition = transform.localPosition;
    }

    public void OnMouseDown()
	{

		mouseDownTime = DateTime.Now;
		//Debug.LogError("Mouse down at" + DateTime.Now);

	}

	public void OnMouseUp()
	{
        if (mouseUp == true)
        {
			//Debug.LogError("Mouse up at" + DateTime.Now);
			float mouseDownDeltaTime = (float)(DateTime.Now - mouseDownTime).TotalMilliseconds;
			//Debug.LogError("Delta time = " + mouseDownDeltaTime);
			if (mouseDownDeltaTime < 100)
			{
				if (!CheckClickUI.IsClikedOnUI())
				{
					ClickOnWindow();
				}
			}
		}
		
	}

	void ClickOnWindow()
	{
		tutorialBurWind.SetActive(true);
	}

	void circleForType()
    {
		circlType.SetActive(true);
    }

    public void Update()
    {
        if(transform.localPosition != startPosition)
        {
			tutorialChangeSprite.mouseUp = true;
			circleForType();
        }
    }

	public void mouseFalse()
    {
		mouseUp = false;
    }
}
