using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowTouchScaler : MonoBehaviour
{
    public float EditorScaleFactor = 1;

    bool isEnabled = false;

    Vector2 firstTouchStartPosition = Vector2.zero;
    Vector2 secondTouchStartPosition = Vector2.zero;
    float startTouchesDistance = 0f;
    Vector3 initialObjectScale;

    float initialFingersDistance;
    Vector3 initialScale;

    public void TurnOn()
	{
        isEnabled = true;
	}

    public void TurnOff()
	{
        isEnabled = false;
	}

    public void GetTouchDistanceAndScaleObject()
	{
        if (Input.touchCount == 2)
        {
            if (Input.touches[1].phase == TouchPhase.Began)
            {
                startTouchesDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                initialObjectScale = transform.localScale;
                HistoryAction windowScaleHistoryAction = new HistoryAction();
                windowScaleHistoryAction.CreateWindowScaleHistoryAction(transform, GetComponent<Window>().Scale);
                HistoryChangesStack historyChangesStack = FindObjectOfType<HistoryChangesStack>();
                historyChangesStack.AddHistoryAction(windowScaleHistoryAction);

            }
            else
            {
                float currentTouchDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
                float scaleFactor = currentTouchDistance / startTouchesDistance;

                ScaleWindow(scaleFactor);
                //transform.localScale = initialObjectScale * scaleFactor;
            }
        }
    }

    public void ScaleWindow(float scaleFactor)
	{
        Vector3 newScale = Vector3.one * scaleFactor;
        GetComponent<Window>().Scale = scaleFactor;

        if (newScale != transform.localScale)
        {
            Vector3 prevousObjectScale = transform.localScale;
            var gridMover = GetComponent<WallGridMoverTwoAxis>();
            if (newScale.x >= 0.6f && newScale.x <= 1.4f)
            {
                Debug.LogWarningFormat("In borders");
                transform.localScale = newScale;
                if (gridMover.IsWindowFit())
                {
                    Debug.LogWarning("Window do fit after scaling. Adding to grid");
                    gridMover.AddObjectToGrid();
                    GetComponent<Window>().Scale = scaleFactor;

                }
                else
                {
                    GetComponent<Window>().Scale = prevousObjectScale.x;

                    transform.localScale = prevousObjectScale;
                    Debug.LogWarning("Window does not fit after scaling");
                }
            }
            else
            {
                GetComponent<Window>().Scale = prevousObjectScale.x;

                Debug.LogWarning("Not in borders");
            }
            var buttons3D = GetComponentInChildren<Buttons3DWindow>();
            if (buttons3D != null)
            {
                buttons3D.transform.localScale = new Vector3(0.17f, 0.17f, 1f);
            }
        }
    }

    public void ScaleInEditor()
	{
        Vector3 newScale = Vector3.one * EditorScaleFactor;
        newScale.z = 1;
        GetComponent<Window>().Scale = EditorScaleFactor;

        if (newScale != transform.localScale)
        {
            Vector3 prevousObjectScale = transform.localScale;
            var gridMover = GetComponent<WallGridMoverTwoAxis>();
            if (newScale.x >= 0.6f && newScale.x <= 1.4f) 
            {
                Debug.LogWarningFormat("In borders");
                transform.localScale = newScale;

                if (gridMover.IsWindowFit())
                {
                    Debug.LogWarning("Window do fit after scaling. Adding to grid");
                    gridMover.AddObjectToGrid();
                    GetComponent<Window>().Scale = EditorScaleFactor;
                }
                else
                {
                    GetComponent<Window>().Scale = prevousObjectScale.x;

                    transform.localScale = prevousObjectScale;
                    Debug.LogWarning("Window does not fit after scaling");
                }
            }
            else
			{
                GetComponent<Window>().Scale = prevousObjectScale.x;

                Debug.LogWarning("Not in borders");
			}
            var buttons3D = GetComponentInChildren<Buttons3DWindow>();
            if (buttons3D != null)
            {
                buttons3D.transform.localScale = new Vector3(0.17f, 0.17f, 1f);
            }
            //gridMover.CalculateObjectSizeInCells();

        }

    }

    private void Start()
	{
		ScaleWindow(GetComponent<Window>().Scale);
	}

	void Update()
    {
        if (GetComponent<Window>().Type != DataTypes.WindowType.balcony_left_door && GetComponent<Window>().Type != DataTypes.WindowType.balcony_right_door)
        {
            //ScaleInEditor();
            if (isEnabled)
            {

                GetTouchDistanceAndScaleObject();

            }
        }
	}
}
