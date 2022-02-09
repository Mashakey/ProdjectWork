using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class PrefabContainer : MonoBehaviour
{
    public GameObject door;
    public GameObject double_leaf_window;
    public GameObject tricuspid_window;
    public GameObject balcony_left_door;
    public GameObject balcony_right_door;

    public GameObject constructorWindow;
    public GameObject constructorDoor;

    public GameObject Buttons3DWindow;
    public GameObject Buttons3DBalconyWindow;
    public GameObject Buttons3DWindowTutorial;
    public GameObject Buttons3DDoor;

    public GameObject Buttons3dFurniture;

    public GameObject Note;

    public List<GameObject> Furniture = new List<GameObject>();

    public GameObject SendEstimateErrorPage;
    public GameObject SendEstimateSuccesPage;

    public GameObject GetWindow(WindowType type)
	{
        if (type == WindowType.double_leaf_window)
		{
            return double_leaf_window;
		}
        else if (type == WindowType.tricuspid_window)
		{
            return tricuspid_window;
		}
        else if (type == WindowType.balcony_left_door)
		{
            return balcony_left_door;
		}
        else if (type == WindowType.balcony_right_door)
		{
            return balcony_right_door;
		}            
        else
		{
            Debug.LogError("Prefab container does not have that type of window '" + type + "'");
            return (null);
		}
	}

	public GameObject GetFurnitureObjectByName(string name)
	{
		foreach (var furnitureObject in Furniture)
		{
            if (furnitureObject.name == name)
			{
                return (furnitureObject);
			}
		}
        return null;
	}
}
