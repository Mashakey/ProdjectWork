using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialMaterialRoom : MonoBehaviour
{
    public GameObject[] wallsRoom;
    public Material newMaterialRoom;
    public int j;

    public void materialForWall()
    {
        for(int i = 0; i < wallsRoom.Length; i++)
        {
            newMaterialRoom = materilImageMassive.materialWalls;
            wallsRoom[i].GetComponent<MeshRenderer>().material = newMaterialRoom;
        }
    }

    public void materialForTouchWall()
    {
        newMaterialRoom = materilImageMassive.materialWalls;
        wallsRoom[j].GetComponent<MeshRenderer>().material = newMaterialRoom;   
    }
}
