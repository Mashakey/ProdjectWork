using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using static DataTypes;

public class FavoritesStorage : MonoBehaviour
{
	public static List<MaterialJSON> Wallpapers = new List<MaterialJSON>();
	public static List<MaterialJSON> Laminates = new List<MaterialJSON>();
	public static List<MaterialJSON> Linoleums = new List<MaterialJSON>();
	public static List<MaterialJSON> Doors = new List<MaterialJSON>();
	public static List<MaterialJSON> PVCs = new List<MaterialJSON>();
	public static List<MaterialJSON> Baseboards = new List<MaterialJSON>();
	public static List<MaterialJSON> WallpapersForPainting = new List<MaterialJSON>();
	public static List<MaterialJSON> Paints = new List<MaterialJSON>();

	public static void LoadFavoritesFromCache()
	{
		Favorites favoritesJsons = DataCacher.GetCachedFavoritesJsons();
		Wallpapers = favoritesJsons.Wallpapers;
		Laminates = favoritesJsons.Laminates;
		Linoleums = favoritesJsons.Linoleums;
		Doors = favoritesJsons.Doors;
		PVCs = favoritesJsons.PVCs;
		Baseboards = favoritesJsons.Baseboards;
		WallpapersForPainting = favoritesJsons.WallpapersForPainting;
		Paints = favoritesJsons.Paints;
	}

	public static void CacheFavorites()
	{
		Favorites favoritesJsons = new Favorites();
		favoritesJsons.Wallpapers = Wallpapers;
		favoritesJsons.Laminates = Laminates;
		favoritesJsons.Linoleums = Linoleums;
		favoritesJsons.Doors = Doors;
		favoritesJsons.PVCs = PVCs;
		favoritesJsons.Baseboards = Baseboards;
		favoritesJsons.WallpapersForPainting = WallpapersForPainting;
		favoritesJsons.Paints = Paints;
		DataCacher.CacheFavoritesJsons(favoritesJsons);
	}

	public static void AddMaterialJsonToFavorites(MaterialJSON materialJson)
	{
		List<MaterialJSON> favoritesList;
		switch (materialJson.type)
		{
			case "wallpaper":
				favoritesList = Wallpapers;
				break;

			case "laminate":
				favoritesList = Laminates;
				break;

			case "linoleum":
				favoritesList = Linoleums;
				break;

			case "door":
				favoritesList = Doors;
				break;

			case "pvc":
				favoritesList = PVCs;
				break;

			case "baseboard":
				favoritesList = Baseboards;
				break;

			case "wallpaper_for_painting":
				favoritesList = WallpapersForPainting;
				break;

			case "paint":
				favoritesList = Paints;
				break;

			default:
				Debug.LogError(string.Format($"Can't add material {materialJson.id} {materialJson.name} to favorites. Unknown type '{materialJson.type}'."));
				return;
		}
		bool isMaterialInFavorites = false;
		foreach (var favoriteJson in favoritesList)
		{
			if (favoriteJson.id == materialJson.id)
			{
				isMaterialInFavorites = true;
				break;
			}
		}
		if (!isMaterialInFavorites)
		{
			favoritesList.Add(materialJson);
			Debug.LogWarning(string.Format($"Material {materialJson.id} {materialJson.name} is added to favorites"));
			CacheFavorites();
			LoadFavoritesFromCache();
		}
	}

	public static void DeleteMaterialFromFavorites(MaterialJSON materialJson)
	{
		List<MaterialJSON> favoritesList;
		switch (materialJson.type)
		{
			case "wallpaper":
				favoritesList = Wallpapers;
				break;

			case "laminate":
				favoritesList = Laminates;
				break;

			case "linoleum":
				favoritesList = Linoleums;
				break;

			case "door":
				favoritesList = Doors;
				break;

			case "pvc":
				favoritesList = PVCs;
				break;

			case "baseboard":
				favoritesList = Baseboards;
				break;

			case "wallpaper_for_painting":
				favoritesList = WallpapersForPainting;
				break;

			case "paint":
				favoritesList = Paints;
				break;

			default:
				Debug.LogError(string.Format($"Can't delete material {materialJson.id} {materialJson.name} from favorites. Unknown type '{materialJson.type}'."));
				return;
		}
		foreach (var favoriteJson in favoritesList)
		{
			if (favoriteJson.id == materialJson.id)
			{
				favoritesList.Remove(favoriteJson);
				break;
			}
		}
		Debug.LogWarning(string.Format($"Material {materialJson.id} {materialJson.name} is deleted from favorites"));

		CacheFavorites();
		LoadFavoritesFromCache();
	}

	public static bool IsMaterialInFavorites(MaterialJSON materialJson)
	{
		List<MaterialJSON> favoritesList;
		switch (materialJson.type)
		{
			case "wallpaper":
				favoritesList = Wallpapers;
				break;

			case "laminate":
				favoritesList = Laminates;
				break;

			case "linoleum":
				favoritesList = Linoleums;
				break;

			case "door":
				favoritesList = Doors;
				break;

			case "pvc":
				favoritesList = PVCs;
				break;

			case "baseboard":
				favoritesList = Baseboards;
				break;

			case "wallpaper_for_painting":
				favoritesList = WallpapersForPainting;
				break;

			case "paint":
				favoritesList = Paints;
				break;

			default:
				Debug.LogError(string.Format($"Can't delete material {materialJson.id} {materialJson.name} from favorites. Unknown type '{materialJson.type}'."));
				return (false);
		}
		foreach (var favoriteJson in favoritesList)
		{
			if (materialJson.id == favoriteJson.id)
			{
				return (true);
			}
		}
		return (false);
	}
}
