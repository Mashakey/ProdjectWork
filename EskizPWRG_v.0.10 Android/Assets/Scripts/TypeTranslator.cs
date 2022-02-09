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
			return ("�������");
		}
		else if (interiorType == InteriorType.Hallway)
		{
			return ("��������");
		}
		else if (interiorType == InteriorType.LivingRoom)
		{
			return ("��������");
		}
		else if (interiorType == InteriorType.Nursery)
		{
			return ("�������");
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
			return ("� - ��������");
		}
		else if (roomType == RoomType.Rectangle)
		{
			return ("�������������");
		}
		else if (roomType == RoomType.Trapezoidal)
		{
			return ("�������������");
		}
		else if (roomType == RoomType.T_type)
		{
			return ("� - ��������");
		}
		else if (roomType == RoomType.Z_type)
		{
			return ("Z - ��������");
		}
		else
		{
			Debug.LogError("Unknown room type");
			return ("");
		}
	}
}
