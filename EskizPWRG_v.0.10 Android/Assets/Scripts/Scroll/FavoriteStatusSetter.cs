using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataTypes;

public class FavoriteStatusSetter : MonoBehaviour
{
	public bool favorires = false;

    public void SetFavoritesStatus()
	{
		DrumScroll drumScroll = GetComponent<DrumScroll>();
		foreach (var contentField in drumScroll.contentFields)
		{
			contentField.SetHeartSpriteStatus(GlobalApplicationManager.GetMaterialJsonById(contentField.name));
		}
	}
}
