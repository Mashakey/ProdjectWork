using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectToMousePosition : MonoBehaviour
{
	public TutorialCameraRotation tutorialCameraRotation;

	public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
	private void OnMouseDown()
	{
	}

	private void OnMouseUp()
	{
		tutorialCameraRotation.UnfreezeCamera();
	}

	private void OnMouseDrag()
	{
		if (isActive)
		{
			tutorialCameraRotation.FreezeCamera();

			LayerMask layerMask = LayerMask.GetMask("Wall");
			Vector3 mouse = Input.mousePosition;
			Ray castPoint = Camera.main.ScreenPointToRay(mouse);
			RaycastHit hit;
			if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, layerMask))
			{
				gameObject.transform.position = hit.point;
			}
		}
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
