using UnityEngine;
using System.Collections;
using System;

public class ItemFood : Item
{

	public override bool CanUse
	{
		get
		{
			return (DateTime.Now - GameData.Get.Data.LastFoodTime).TotalHours > 24;
		}
	}

	public override void Use(eObjectType slotUsed, GameObject slot)
	{
		base.Use(slotUsed, slot);
		GameData.Get.Data.LastFoodTime = DateTime.Now;
	}

}
