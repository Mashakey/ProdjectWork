using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MiddleRoomPoint : MonoBehaviour
{
	[SerializeField]
	CinemachinePath cinemachinePath;
	[SerializeField]
	CinemachineVirtualCamera virtualCamera;

    public void SetMiddlePointAndDollyTrackPosition(Vector3 position)
	{
		transform.position = position;
		virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 0.5f;
		cinemachinePath.m_Waypoints[0].position = position;
		cinemachinePath.m_Waypoints[1].position = position;
	}
}
