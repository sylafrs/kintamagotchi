using UnityEngine;
using System.Collections;

public class ItemAssurance : Item
{

	public override bool CanUse
	{
		get
		{
			return !GameData.Get.Data.MaterialAssurance;
		}
	}

	public override void Use(eObjectType slotUsed, GameObject slot)
	{
		base.Use(slotUsed, slot);
		GameData.Get.Data.MaterialAssurance = true;
		MenuManager.Get.ImgHabitation.SetActive(true);
	}
	
}
