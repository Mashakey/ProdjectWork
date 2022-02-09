using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchEdit : MonoBehaviour
{
    public Image activeFrame;
    public GameObject roomParameters;
    public GameObject selectTypeRoom;
    public Sprite activBut;
    public GameObject[] typesPrefab;
    public GameObject selType;

	private void Start()
	{
		
	}

	public void editTypeRoom()
    {
        if (gameObject.GetComponent<Image>().sprite == activBut)
        {
            roomParameters.SetActive(true);
            selectTypeRoom.SetActive(false);
        }
    }
}
