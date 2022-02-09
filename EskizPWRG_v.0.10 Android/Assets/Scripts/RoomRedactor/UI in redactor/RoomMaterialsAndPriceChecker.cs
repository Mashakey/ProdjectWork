using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMaterialsAndPriceChecker : MonoBehaviour
{
    [SerializeField]
    Text PercentField;
    [SerializeField]
    Text PercentFieldDescritption;
    [SerializeField]
    Image FillingPercentStroke;
    [SerializeField]
    Text RoomPriceField;

    bool isWallMaterialAdded;
    bool isFloorMaterialAdded;
    bool isCeilingMaterialAdded;
    bool isBaseboardMaterialAdded;
    bool isDoorMaterialAdded;
    bool isWindowAdded;
    bool isFurnitureAdded;

    void ResetFlags()
	{
        isWallMaterialAdded = false;
        isFloorMaterialAdded = false;
        isCeilingMaterialAdded = false;
        isBaseboardMaterialAdded = false;
        isDoorMaterialAdded = false;
        isWindowAdded = false;
        isFurnitureAdded = false;
	}
    // Start is called before the first frame update

    public void CheckMaterials()
	{
        ResetFlags();
        Room room = FindObjectOfType<Room>();
        if (room != null)
        {
            foreach (var wall in room.Walls)
            {
                if (wall.materialId != "DefaultWall")
				{
                    isWallMaterialAdded = true;
				}
                foreach (var door in wall.Doors)
				{
                    if (door.MaterialId != "DefaultDoor")
					{
                        isDoorMaterialAdded = true;
					}
				}
                if (wall.Windows.Count > 0)
				{
                    isWindowAdded = true;
				}
            }
            if (room.floorMaterialId != "DefaultFloor")
			{
                isFloorMaterialAdded = true;
			}
            if (room.ceilingMaterialId != "Default")
			{
                isCeilingMaterialAdded = true;
			}
            if (room.baseBoardMaterialId != "" && room.baseBoardMaterialId != "Default")
			{
                isBaseboardMaterialAdded = true;
			}
            if (room.Furniture.Count > 0)
			{
                isFurnitureAdded = true;
			}
        }
    }

    public void SetPercentValue()
	{
        float roomFillingPercent = 0f;
        if (isFurnitureAdded)
        {
            roomFillingPercent += 0.2f;
        }
        else
        {
            PercentFieldDescritption.text = "Добавь мебель";
        }

        if (isWindowAdded)
        {
            roomFillingPercent += 0.1f;
        }
        else
        {
            PercentFieldDescritption.text = "Добавь окно";
        }

        if (isDoorMaterialAdded)
        {
            roomFillingPercent += 0.1f;
        }
        else
        {
            PercentFieldDescritption.text = "Выбери дверь";
        }

        if (isBaseboardMaterialAdded)
        {
            roomFillingPercent += 0.1f;
        }
        else
        {
            PercentFieldDescritption.text = "Добавь плинтус";
        }

        if (isCeilingMaterialAdded)
        {
            roomFillingPercent += 0.1f;
        }
        else
        {
            PercentFieldDescritption.text = "Выбери материал потолка";
        }

        if (isFloorMaterialAdded)
        {
            roomFillingPercent += 0.2f;
        }
        else
        {
            PercentFieldDescritption.text = "Выбери материал пола";
        }

        if (isWallMaterialAdded)
		{
            roomFillingPercent += 0.2f;
		}
        else
		{
            PercentFieldDescritption.text = "Выбери материалы стен";
		}

        PercentField.text = roomFillingPercent * 100 + "%";
        if (roomFillingPercent >= 1)
		{
            PercentFieldDescritption.text = "Отправь смету в магазин";
        }
        FillingPercentStroke.fillAmount = roomFillingPercent;
        Room room = FindObjectOfType<Room>();
        if (room != null)
        {
            RoomCostCalculator.CalculateEstimate(room);
            RoomPriceField.text = RoomCostCalculator.CalculateRoomCostWithoutSetting(room).ToString() + " \u20BD";
        }
	}


    // Update is called once per frame
    void Update()
    {
        CheckMaterials();
        SetPercentValue();
    }
}
