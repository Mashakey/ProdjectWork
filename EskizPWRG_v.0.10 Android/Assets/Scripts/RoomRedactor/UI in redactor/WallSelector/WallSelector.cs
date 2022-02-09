using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSelector : MonoBehaviour
{
    [SerializeField]
    GameObject checkMark;

    List<int> activeWallsIndexes = new List<int>();

    public void ResetWallSelector()
    {
        checkMark.SetActive(false);
        activeWallsIndexes.Clear();

        WallInWallSelector[] wallScripts = Resources.FindObjectsOfTypeAll<WallInWallSelector>();
        foreach (var wall in wallScripts)
        {
            wall.IsActive = false;
            Transform activeWall = wall.transform.Find("wallSelActive");
            if (activeWall != null)
            {
                Destroy(activeWall.gameObject);
            }
            activeWall = wall.transform.Find("orangeSelWall");
            if (activeWall != null)
            {
                Destroy(activeWall.gameObject);
            }
        }
    }

    public void AddActiveWallIndex(int wallIndex)
    {
        if (!activeWallsIndexes.Contains(wallIndex))
        {
            activeWallsIndexes.Add(wallIndex);
        }
    }

    public void DeleteActiveWallIndex(int wallIndex)
    {
        if (activeWallsIndexes.Contains(wallIndex))
        {
            activeWallsIndexes.Remove(wallIndex);
        }
    }

    public void AddSelectedWallsInGlobalManager()
    {
        Transform room = FindObjectOfType<Room>().transform;
        if (checkMark.activeSelf)
        {
            Room roomScript = room.GetComponent<Room>();
            foreach (var wall in roomScript.Walls)
            {
                GlobalApplicationManager.AddSelectedObject(wall.transform);
            }
        }
        else
        {
            foreach (int index in activeWallsIndexes)
            {
                GlobalApplicationManager.AddSelectedObject(room.GetChild(index));
            }
        }
    }
}
