using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public static class MaterialFilter
{
	public static List<MaterialJSON> GetOnlyWallpapers(List<MaterialJSON> allMaterialJsons)
	{
		List<MaterialJSON> filtredList = new List<MaterialJSON>();
		foreach(var materialJson in allMaterialJsons)
		{
			if (materialJson.type == "wallpaper")
			{
				filtredList.Add(materialJson);
			}
		}
		return (filtredList);
	}

	public static List<MaterialJSON> GetOnlyLaminates(List<MaterialJSON> allMaterialJsons)
	{
		List<MaterialJSON> filtredList = new List<MaterialJSON>();
		foreach (var materialJson in allMaterialJsons)
		{
			if (materialJson.type == "laminate")
			{
				filtredList.Add(materialJson);
			}
		}
		return (filtredList);
	}

	public static List<MaterialJSON> GetOnlyLinoleums(List<MaterialJSON> allMaterialJsons)
	{
		List<MaterialJSON> filtredList = new List<MaterialJSON>();
		foreach (var materialJson in allMaterialJsons)
		{
			if (materialJson.type == "linoleum")
			{
				filtredList.Add(materialJson);
			}
		}
		return (filtredList);
	}

	public static List<MaterialJSON> GetOnlyDoors(List<MaterialJSON> allMaterialJsons)
	{
		List<MaterialJSON> filtredList = new List<MaterialJSON>();
		foreach (var materialJson in allMaterialJsons)
		{
			if (materialJson.type == "door")
			{
				filtredList.Add(materialJson);
			}
		}
		return (filtredList);
	}

	public static List<MaterialJSON> GetOnlyPVCs(List<MaterialJSON> allMaterialJsons)
	{
		List<MaterialJSON> filtredList = new List<MaterialJSON>();
		foreach (var materialJson in allMaterialJsons)
		{
			if (materialJson.type == "pvc")
			{
				filtredList.Add(materialJson);
			}
		}
		return (filtredList);
	}

	public static List<MaterialJSON> GetOnlyBaseboards(List<MaterialJSON> allMaterialJsons)
	{
		List<MaterialJSON> filtredList = new List<MaterialJSON>();
		foreach (var materialJson in allMaterialJsons)
		{
			if (materialJson.type == "baseboard")
			{
				filtredList.Add(materialJson);
			}
		}
		return (filtredList);
	}

	public static List<MaterialJSON> GetOnlyWallpapersForPainting(List<MaterialJSON> allMaterialJsons)
	{
		List<MaterialJSON> filtredList = new List<MaterialJSON>();
		foreach (var materialJson in allMaterialJsons)
		{
			if (materialJson.type == "wallpaper_for_painting")
			{
				filtredList.Add(materialJson);
			}
		}
		return (filtredList);
	}

	public static List<MaterialJSON> GetOnlyPaints(List<MaterialJSON> allMaterialJsons)
	{
		List<MaterialJSON> filtredList = new List<MaterialJSON>();
		foreach (var materialJson in allMaterialJsons)
		{
			if (materialJson.type == "paint")
			{
				filtredList.Add(materialJson);
			}
		}
		return (filtredList);
	}
}
