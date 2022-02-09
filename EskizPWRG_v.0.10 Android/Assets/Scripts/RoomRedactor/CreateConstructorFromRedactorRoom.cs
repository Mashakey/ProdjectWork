using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static DataTypes;

public class CreateConstructorFromRedactorRoom : MonoBehaviour
{
    [SerializeField]
    ParametersChangeHandler parametersChangeHandler;

    public void CreateConstructorFromRoom()
	{
        Room currentRoom = GameObject.FindObjectOfType<Room>();
        if (currentRoom != null)
        {
            Debug.LogWarning("ConstructorFromRedactor\nRoom is found");
        }
        else
		{
            Debug.LogError("ConstructorFromRedactor\nRoom is not found");
        }

        if (currentRoom == null)
		{
            return;
		}

        RoomWallManager roomWallManager = null;
        if (currentRoom.Type == RoomType.Rectangle)
		{
            roomWallManager = FindObjectOfType<RectangularWallManager>(true);
		}
        else if (currentRoom.Type == RoomType.G_type)
		{
            roomWallManager = FindObjectOfType<GtypeWallManager>(true);

        }
        else if (currentRoom.Type == RoomType.T_type)
        {
            roomWallManager = FindObjectOfType<TtypeWallManager>(true);

        }
        else if (currentRoom.Type == RoomType.Trapezoidal)
        {
            roomWallManager = FindObjectOfType<TrapezoidalWallManager>(true);

        }
        else if (currentRoom.Type == RoomType.Z_type)
        {
            roomWallManager = FindObjectOfType<ZtypeWallManager>(true);

        }
        if (roomWallManager == null)
		{
            Debug.LogError("Room wall manager is not found");
            return;
		}

        Debug.LogError("ConstructorFromRedactor\nWallManager parent is " + roomWallManager.transform.parent.name);
        roomWallManager.transform.parent.gameObject.SetActive(true);
        Debug.LogError("ConstructorFromRedactor\nWallManager parent status is " + roomWallManager.transform.parent.gameObject.activeInHierarchy);


        roomWallManager.ResetConstructor();
        parametersChangeHandler.SetRoomNameFieldValue(currentRoom.Name);
        parametersChangeHandler.SetHeightFieldValue(currentRoom.Height * 100);
        roomWallManager.RoomHeight = currentRoom.Height;
        roomWallManager.FloorMaterialId = currentRoom.floorMaterialId;
        roomWallManager.CeilingMaterialId = currentRoom.ceilingMaterialId;
        roomWallManager.BaseboardMaterialId = currentRoom.baseBoardMaterialId;
        for (int i = 0; i < currentRoom.Walls.Count; i++)
		{
            Debug.LogError("Constructor walls count = " + roomWallManager.walls.Length);
            Debug.LogError("REdactor walls count = " + currentRoom.Walls.Count);
            roomWallManager.walls[i].materialId = currentRoom.Walls[i].materialId;


            Debug.LogError("ConstructorFromRedactor\nWallManager parent status is " + roomWallManager.transform.parent.gameObject.activeSelf);

            roomWallManager.SetActiveWallIndex(i);
            float wallLength = (float)System.Math.Round(currentRoom.Walls[i].Length * 100);
            roomWallManager.ChangeWallLength((int)wallLength);
            //roomWallManager.ChangeWallLength((int)(currentRoom.Walls[i].Length * 100));

            int doorCount = 0;
            int windowCount = 0;
            List<Transform> windowsAndDoors = new List<Transform>();
            foreach (var door in currentRoom.Walls[i].Doors)
			{
                windowsAndDoors.Add(door.transform);
                doorCount++;
			}
            foreach (var window in currentRoom.Walls[i].Windows)
            {
                windowsAndDoors.Add(window.transform);
                windowCount++;
            }
            Debug.LogErrorFormat($"Constructor from redactor\ndoors count = {doorCount}  windows count = {windowCount}");
            if (doorCount == 2)
			{
                roomWallManager.AddDoor(2);
			}
            else if (windowCount == 2)
			{
                roomWallManager.AddWindow(2);
			}
            else
			{
                windowsAndDoors.Sort((x, y) => x.transform.localPosition.x.CompareTo(y.transform.localPosition.x));
                foreach (var windowOrDoor in windowsAndDoors)
                {
                    if (windowOrDoor.GetComponent<Window>() != null)
                    {

                        roomWallManager.AddWindow(1);
                    }
                    else
                    {
                        roomWallManager.AddDoor(1);
                    }
                }
            }
		}
        roomWallManager.SetActiveWallIndex(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
