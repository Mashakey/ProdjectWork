using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPathCreator : MonoBehaviour
{
	[SerializeField]
	LayerMask wallLLayerMask;
	[SerializeField]
	CinemachinePath cinemachinePath;


	void Update()
	{
		Room room = FindObjectOfType<Room>();
		if (room != null)
		{
			RaycastHit hit;
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			forward.y = 0f;
			if (Physics.Raycast(transform.position, forward, out hit, Mathf.Infinity))
			{
				Debug.DrawLine(transform.position, hit.point, Color.red);
				hit.point = new Vector3(hit.point.x, 0.2f, hit.point.z);
				cinemachinePath.m_Waypoints[0].position = hit.point;
			}

			Vector3 backward = -transform.TransformDirection(Vector3.forward);
			backward.y = 0f;
			if (Physics.Raycast(transform.position, backward, out hit, Mathf.Infinity))
			{
				Debug.DrawLine(transform.position, hit.point, Color.green);
				hit.point = new Vector3(hit.point.x, room.Height * 0.9f, hit.point.z);

				cinemachinePath.m_Waypoints[1].position = hit.point;

			}
		}
	}
}
