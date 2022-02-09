using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using static DataTypes;

public class RoomCostCalculator : MonoBehaviour
{
    public static Dictionary<string, int> materials = new Dictionary<string, int>();
    public static string EstimatePdfJson = "";

    public static float CalculateRoomCost(Room room)
    {
        //Debug.LogWarning("Calculating room cost");
        float totalCost = 0;
        foreach (var material in materials)
        {
            MaterialJSON materialDataJson = GlobalApplicationManager.GetMaterialJsonById(material.Key);
            float cost = materialDataJson.cost * material.Value;
            //Debug.LogError((String.Format("{0} #{1} {2}р.", materialDataJson.name, material.Value, cost)));
            totalCost += cost;
        }
        room.Cost = totalCost;
        EstimatePage estimatePage = FindObjectOfType<EstimatePage>();
        if (estimatePage != null)
        {
            estimatePage.SetTotalRoomCost(totalCost);
        }
        //Debug.LogError("####################Total cost = " + totalCost);
        return (totalCost);
    }

    public static float CalculateRoomCostWithoutSetting(Room room)
	{
        //Debug.LogWarning("Calculating room cost without adding");

        float totalCost = 0;
        if (room != null)
		{
            foreach (var material in materials)
            {
                MaterialJSON materialDataJson = GlobalApplicationManager.GetMaterialJsonById(material.Key);
                float cost = materialDataJson.cost * material.Value;
                //Debug.LogError((String.Format("{0} #{1} {2}р.", materialDataJson.name, material.Value, cost)));
                totalCost += cost;
            }
            //room.Cost = totalCost;
            EstimatePage estimatePage = FindObjectOfType<EstimatePage>();
            if (estimatePage != null)
            {
                estimatePage.SetTotalRoomCost(totalCost);
            }
            //Debug.LogError("####################Total cost = " + totalCost);
        }
        return (totalCost);

    }

    public static Dictionary<string, int> CalculateEstimate(Room room)
    {
        //Debug.LogWarning("Calculating estimate");

        materials.Clear();
        float perimeter = 0;
        room.CalculateArea();
        //EstimatePdfJson = CreateEstimatePdfData(room);
        foreach (Wall wall in room.Walls)
        {
            perimeter += wall.Length;
            //MaterialJSON materialDataJson = DataCacher.GetMaterialById(wall.oldMaterialId);
            if (!wall.materialId.StartsWith("UserPhoto"))
            {
                MaterialJSON materialDataJson = GlobalApplicationManager.GetMaterialJsonById(wall.materialId);
                if (materialDataJson != null && materialDataJson.type != "paint" && wall.materialId != "DefaultWall" && wall.materialId != "Default")
                {
                    float wallpaperWidth = materialDataJson.pack_dimensions.x;
                    float totalNeededWallpaperLength = wall.Length / wallpaperWidth * wall.Height;
                    int totalNeededWallpaperRolls = (int)Math.Ceiling((totalNeededWallpaperLength / materialDataJson.pack_dimensions.y));
                    if (!materials.ContainsKey(materialDataJson.id))
                    {
                        materials.Add(materialDataJson.id, totalNeededWallpaperRolls + 1);
                    }
                    else
                    {
                        materials[materialDataJson.id] += totalNeededWallpaperRolls;
                    }
                }
                else if (materialDataJson != null && wall.materialId != "DefaultWall" && wall.materialId != "Default") //if wall material is paint
                {
                    float wallSquare = wall.Length * wall.Height;
                    if (!materials.ContainsKey(materialDataJson.id))
                    {
                        materials.Add(materialDataJson.id, (int)Math.Round(wallSquare));
                    }
                    else
                    {
                        materials[materialDataJson.id] += (int)Math.Round(wallSquare);
                    }
                }
            }
            foreach (Door door in wall.Doors)
            {
                if (door.MaterialId != "DefaultDoor")
                {
                    //MaterialJSON doorDataJson = DataCacher.GetMaterialById(door.MaterialId);
                    MaterialJSON doorDataJson = GlobalApplicationManager.GetMaterialJsonById(door.MaterialId);
                    if (doorDataJson != null)
                    {
                        if (!materials.ContainsKey(doorDataJson.id))
                        {
                            materials.Add(doorDataJson.id, 1);
                        }
                        else
                        {
                            materials[doorDataJson.id] += 1;
                        }
                    }
                }
            }
        }

        if (room.floorMaterialId != "DefaultMaterial" && room.floorMaterialId != "Default" && room.floorMaterialId != "DefaultFloor") //TODO: Если в админке не добавят цену за упаковку  -- переделать
        {
            //MaterialJSON materialDataJson = DataCacher.GetMaterialById(room.floorMaterialId);
            MaterialJSON materialDataJson = GlobalApplicationManager.GetMaterialJsonById(room.floorMaterialId);
            if (materialDataJson != null)
            {
                if (materialDataJson.type != "paint")
                {
                    int totalNeededFloorMaterial = (int)Math.Ceiling(room.Area / materialDataJson.pack_area);
                    materials.Add(materialDataJson.id, totalNeededFloorMaterial + 1);
                }
                else
                {
                    materials.Add(materialDataJson.id, (int)Math.Round(room.CalculateArea()));
                }
            }

        }
        if (room.ceilingMaterialId != "DefaultMaterial" && room.ceilingMaterialId != "Default" && room.ceilingMaterialId != "DefaultFloor") //TODO: Добавить подсчёт цены потолка и красок
        {
            MaterialJSON materialDataJson = GlobalApplicationManager.GetMaterialJsonById(room.ceilingMaterialId);
            if (materialDataJson.type != "paint")
            {
                int totalNeededFloorMaterial = (int)Math.Ceiling(room.Area / materialDataJson.pack_area);
                materials.Add(materialDataJson.id, totalNeededFloorMaterial + 1);
            }
            else
            {
                materials.Add(materialDataJson.id, (int)Math.Round(room.Area));
            }
        }
        if (room.baseBoardMaterialId != "" && room.baseBoardMaterialId != "Default")
        {
            //MaterialJSON materialDataJson = DataCacher.GetMaterialById(room.baseBoardMaterialId);
            MaterialJSON materialDataJson = GlobalApplicationManager.GetMaterialJsonById(room.baseBoardMaterialId);
            if (materialDataJson != null)
            {
                int totalNeededBaseboardsCount = (int)Math.Ceiling(perimeter / (materialDataJson.custom_properties.length / 100));
                materials.Add(materialDataJson.id, totalNeededBaseboardsCount + 1);
            }
        }
        //foreach (var material in materials)
        //{
        //    Debug.LogWarning("### " + material.Key + " " + material.Value);
        //}
        CreateEstimatePdfData(materials, room);
        return (materials);
    }

    public static string CreateEstimatePdfData(Dictionary<string, int> materialsDictionary, Room room)
    {
        //Debug.LogWarning("Creating estimate pdf data");

        var materialsList = new List<PdfLine>();
        foreach (var material in materialsDictionary)
        {
            //Debug.LogError("##Material " + material.Key);
            MaterialJSON materialJSON = GlobalApplicationManager.GetMaterialJsonById(material.Key);
            PdfLine pdfLine = new PdfLine();
            string[] materialLine = {materialJSON.name,
            String.Format("{0} {1} {2}<br>{3}", materialJSON.type, materialJSON.custom_properties.manufacturer_company, materialJSON.name, materialJSON.custom_properties.vendor_code),
            material.Value.ToString(),
            materialJSON.cost.ToString(),
            (materialJSON.cost * material.Value).ToString()};
            pdfLine.line = materialLine;
            materialsList.Add(pdfLine);
        }

        var pdfData = new PdfData();
        pdfData.title = "Расчет материалов";
        FirstStartAndCityScriptableObj firstStartAndCityScriptableObj = FindObjectOfType<FirstStartAndCityScriptableObj>();

        pdfData.city = TranslateCity(firstStartAndCityScriptableObj.item.city);
        PdfRoomData pdfRoomData = new PdfRoomData();
        pdfRoomData.title = "Параметры помещения";
        pdfRoomData.heigh = "Высота: " + room.Height.ToString() + " m";
        pdfRoomData.length = "Длина: " + CalculateRoomLength(room).ToString() + " m";
        pdfRoomData.width = "Ширина: " + CalculateRoomWidth(room).ToString() + " m";
        pdfRoomData.doors = "Двери: " + CalculateDoorsCount(room).ToString() + " шт";
        pdfRoomData.windows = "Окна " + CalculateWindowsCount(room).ToString() + " шт";
        pdfData.room = pdfRoomData;

        PdfMaterialsData pdfMaterialsData = new PdfMaterialsData();
        pdfMaterialsData.title = "Необходимое количество материалов";
        pdfMaterialsData.headers = new string[]
                {
                    "Поверхность",
                    "Материал",
                    "Кол-во",
                    "Цена, 1 шт",
                    "Цена",
                };
        pdfMaterialsData.lines = materialsList;
        pdfMaterialsData.total = "Итого: " + CalculateRoomCostWithoutSetting(room) + " \u20BD";

        pdfData.materials = pdfMaterialsData;

        EstimatePdfJson = JsonUtility.ToJson(pdfData, false);
        return (EstimatePdfJson);
    }

    public static string CreateEstimatePdfData(Room room)
    {
       // Debug.LogWarning("Creating estimate pdf data");

        //Room room = FindObjectOfType<Room>();
        Dictionary<string, int> materialsDictionary = CalculateEstimate(room);

        var materialsList = new List<PdfLine>();
        foreach (var material in materialsDictionary)
        {
            MaterialJSON materialJSON = GlobalApplicationManager.GetMaterialJsonById(material.Key);
            PdfLine pdfLine = new PdfLine();
            string[] materialLine = {materialJSON.name,
            String.Format("{0} {1} {2}<br>{3}", materialJSON.type, materialJSON.custom_properties.manufacturer_company, materialJSON.name, materialJSON.custom_properties.vendor_code),
            material.Value.ToString(),
            materialJSON.cost.ToString(),
            (materialJSON.cost * material.Value).ToString()};
            pdfLine.line = materialLine;
            materialsList.Add(pdfLine);
        }

        var pdfData = new PdfData();
        pdfData.title = "Расчет материалов";
        FirstStartAndCityScriptableObj firstStartAndCityScriptableObj = FindObjectOfType<FirstStartAndCityScriptableObj>();

        pdfData.city = TranslateCity(firstStartAndCityScriptableObj.item.city);
        PdfRoomData pdfRoomData = new PdfRoomData();
        pdfRoomData.title = "Параметры помещения";
        pdfRoomData.heigh = "Высота: " + room.Height.ToString() + " m";
        pdfRoomData.length = "Длина: " + CalculateRoomLength(room).ToString() + " m";
        pdfRoomData.width = "Ширина: " + CalculateRoomWidth(room).ToString() + " m";
        pdfRoomData.doors = "Двери: " + CalculateDoorsCount(room).ToString() + " шт";
        pdfRoomData.windows = "Окна " + CalculateWindowsCount(room).ToString() + " шт";
        pdfData.room = pdfRoomData;

        PdfMaterialsData pdfMaterialsData = new PdfMaterialsData();
        pdfMaterialsData.title = "Необходимое количество материалов";
        pdfMaterialsData.headers = new string[]
                {
                    "Поверхность",
                    "Материал",
                    "Кол-во",
                    "Цена, 1 шт",
                    "Цена",
                };
        pdfMaterialsData.lines = materialsList;
        pdfMaterialsData.total = "Итого: " + CalculateRoomCostWithoutSetting(room) + " \u20BD";

        pdfData.materials = pdfMaterialsData;

        EstimatePdfJson = JsonUtility.ToJson(pdfData, false);
        return (EstimatePdfJson);
    }

    public static int CalculateDoorsCount(Room room)
    {
        int doorCount = 0;
        foreach (var wall in room.Walls)
        {
            foreach (var door in wall.Doors)
            {
                doorCount++;
            }
        }
        return (doorCount);
    }

    public static int CalculateWindowsCount(Room room)
    {
        int windowCount = 0;
        foreach (var wall in room.Walls)
        {
            foreach (var window in wall.Windows)
            {
                windowCount++;
            }
        }
        return (windowCount);

    }

    public static float CalculateRoomLength(Room room)
	{
        float roomLength = 0f;
        if (room.Type == RoomType.Rectangle)
		{
            roomLength = room.Walls[1].Length;
		}
        else if (room.Type == RoomType.G_type)
		{
            roomLength = room.Walls[1].Length;
        }
        else if (room.Type == RoomType.Trapezoidal)
		{
            roomLength = room.Walls[4].Length;
        }
        else if (room.Type == RoomType.Z_type)
		{
            roomLength = room.Walls[1].Length + room.Walls[3].Length;
        }
        else if (room.Type == RoomType.T_type)
		{
            roomLength = room.Walls[1].Length;
        }
        return (roomLength);
	}

    public static float CalculateRoomWidth(Room room)
	{
        float roomWidth = 0f;
        if (room.Type == RoomType.Rectangle)
        {
            roomWidth = room.Walls[0].Length;
        }
        else if (room.Type == RoomType.G_type)
        {
            roomWidth = room.Walls[0].Length;
        }
        else if (room.Type == RoomType.Trapezoidal)
        {
            roomWidth = room.Walls[0].Length;
        }
        else if (room.Type == RoomType.Z_type)
        {
            roomWidth = room.Walls[0].Length + room.Walls[2].Length;
        }
        else if (room.Type == RoomType.T_type)
        {
            roomWidth = room.Walls[0].Length + room.Walls[6].Length;
        }
        return (roomWidth);
	}

    public static string TranslateCity(string englishCityName)
    {
        if (englishCityName == "All city")
        {
            return("Все города");
        }
        else if (englishCityName == "Belovo")
        {
            return ("Белово");
        }
        else if (englishCityName == "Berdsk")
        {
            return ("Бердск");
        }
        else if (englishCityName == "Kemerovo")
        {
            return ("Кемерово");
        }
        else if (englishCityName == "Novosibirsk")
        {
            return ("Новосибирск");
        }
        else if (englishCityName == "Tomsk")
        {
            return ("Томск");
        }
        else
		{
            return ("Все города");

        }
    }
}
