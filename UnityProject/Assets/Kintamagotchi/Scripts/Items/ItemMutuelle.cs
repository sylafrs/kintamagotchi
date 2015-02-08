using UnityEngine;
using System.Collections;

public class ItemMutuelle : Item {

	const int charges = 8;

	public override bool CanUse
	{
		get
		{
			return GameData.Get.Data.SicknessAssurance == 0;
		}
	}

	public override void Use(eObjectType slotUsed, GameObject slot)
	{
		base.Use(slotUsed, slot);
		GameData.Get.Data.SicknessAssurance += charges;

		if(GameData.Get.Data.IsSick)
		{
			GameData.Get.Data.SicknessAssurance--;
			GameData.Get.Data.IsSick = false;
		}
	}
}
