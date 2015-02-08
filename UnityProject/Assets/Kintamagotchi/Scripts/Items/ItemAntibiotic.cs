using UnityEngine;
using System.Collections;

public class ItemAntibiotic : Item 
{
	public override bool CanUse
	{
		get
		{
			return GameData.Get.Data.IsSick;
		}
	}

	public override void Use(eObjectType slotUsed, GameObject slot)
	{
		base.Use(slotUsed, slot);
		GameData.Get.Data.IsSick = false;
	}
}
