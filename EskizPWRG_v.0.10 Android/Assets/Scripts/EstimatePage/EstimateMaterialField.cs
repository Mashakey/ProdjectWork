using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstimateMaterialField : MonoBehaviour
{
    public void OnDeleteButtonClick()
	{
		RoomCostCalculator.materials.Remove(name);
		RoomCostCalculator.CreateEstimatePdfData(RoomCostCalculator.materials, FindObjectOfType<Room>());
		EstimatePage estimatePage = GetComponentInParent<EstimatePage>();
		estimatePage.CreateContentFields(RoomCostCalculator.materials);

	}
}
