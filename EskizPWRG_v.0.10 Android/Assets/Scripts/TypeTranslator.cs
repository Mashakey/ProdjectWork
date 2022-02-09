using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;


public class TypeTranslator : MonoBehaviour
{
	public static string GetRoomInteriorTypeRussianTranslation(InteriorType interiorType)
	{
		if (interiorType == InteriorType.Bedroom)
		{
			return ("Спальня");
		}
		else if (interiorType == InteriorType.Hallway)
		{
			return ("Прихожая");
		}
		else if (interiorType == InteriorType.LivingRoom)
		{
			return ("Гостиная");
		}
		else if (interiorType == InteriorType.Nursery)
		{
			return ("Детская");
		}
		else
		{
			Debug.LogError("Unknown interior type");
			return ("");
		}
	}

    public static string GetRoomTypeRussianTranslation(RoomType roomType)
	{
		if (roomType == RoomType.G_type)
		{
			return ("Г - образная");
		}
		else if (roomType == RoomType.Rectangle)
		{
			return ("Прямоугольная");
		}
		else if (roomType == RoomType.Trapezoidal)
		{
			return ("Трапецевидная");
		}
		else if (roomType == RoomType.T_type)
		{
			return ("Т - образная");
		}
		else if (roomType == RoomType.Z_type)
		{
			return ("Z - образная");
		}
		else
		{
			Debug.LogError("Unknown room type");
			return ("");
		}
	}
}
