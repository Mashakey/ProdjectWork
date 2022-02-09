using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Note : MonoBehaviour
{


    public GameObject NotePagePrefab;
    public string contentText = "";

    DateTime lastTapTime;
    bool isMoving = false;

	public void Awake()
	{
        lastTapTime = DateTime.Now;
	}

    public void MoveNoteInLookDirection()
	{
        Ray castPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(castPoint, out hit, Mathf.Infinity);
        if (hit.collider != null)
        {
            gameObject.transform.position = hit.point;
        }
    }

    public void Update()
    {
        SetLookOnCamera();
        if (isMoving)
        {
            Camera.main.GetComponent<CameraRotation>().FreezeCamera();

            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            Physics.Raycast(castPoint, out hit, Mathf.Infinity);
            if (hit.collider != null)
            {
                gameObject.transform.position = hit.point;
            }
        }
    }

	public void OnMouseDown()
	{
        Debug.Log("OnMouseDown");
        Debug.Log("time = " + (DateTime.Now - lastTapTime).TotalMilliseconds);
        isMoving = true;
		if ((DateTime.Now - lastTapTime).TotalMilliseconds < 400)
		{
            DoubleTap();
            isMoving = false;
		}
        lastTapTime = DateTime.Now;
	}

    public void DoubleTap()
	{
        Debug.Log("Double tap on note");
        CreateNoteWindow();
	}

    public void CreateNoteWindow()
	{
        NotePage notePage = Instantiate(NotePagePrefab).GetComponentInChildren<NotePage>();
        notePage.ChangeInputFieldText(contentText);
        notePage.activeNote = this;
	}

	public void OnMouseUp()
	{
        Debug.Log("OnMouseUp");
        isMoving = false;
        Debug.LogWarning("trying to unfreeze camera");
        Camera.main.GetComponent<CameraRotation>().UnfreezeCamera();
	}

	public void SetLookOnCamera()
	{
        transform.LookAt(Camera.main.transform);
	}
}
