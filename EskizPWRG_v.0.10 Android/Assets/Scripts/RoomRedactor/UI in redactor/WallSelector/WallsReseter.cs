using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallsReseter : MonoBehaviour
{
    public List<Transform> Walls;

	private void OnEnable()
	{
		foreach (var wall in Walls)
		{
            Transform activeWall = wall.Find("wallSelActive");
            if (activeWall != null)
			{
                Destroy(activeWall.gameObject);
			}
		}
	}
}
