using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NoteMoveAndOpen : MonoBehaviour
{
    public bool move = true;
    public bool tap = false;
    public GameObject note;
    public GameObject circleTap;

    public void Update()
    {
        SetLookOnCamera();
        if (move == true)
        {
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

    public void TapOnNote()
    {
        move = false;
    }

    public void MouseTapNote()
    {
        if (move == false)
        {
            if (tap == false)
            {
                tap = true;
            }
            else
            {
                note.SetActive(true);
                circleTap.SetActive(false);
                deleteScriptFoRMoveAndTapNote();
            }
        }
    }

    public void deleteScriptFoRMoveAndTapNote()
    {
        Destroy(gameObject.GetComponent<NoteMoveAndOpen>());
        Destroy(gameObject.GetComponent<EventTrigger>());
    }

    public void SetLookOnCamera()
    {
        transform.LookAt(Camera.main.transform);
    }
}
