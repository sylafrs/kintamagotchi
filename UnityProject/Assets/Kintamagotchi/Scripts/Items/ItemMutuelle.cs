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

		MenuManager.Get.ImgMaladie.SetActive(true);

		if(GameData.Get.Data.IsSick)
		{
			GameData.Get.Data.SicknessAssurance--;
			GameData.Get.Data.IsSick = false;

			if(GameData.Get.Data.SicknessAssurance <= 0)
			{
				GameData.Get.Data.SicknessAssurance = 0;
				MenuManager.Get.ImgMaladie.SetActive(false);
			}
		}
	}
}
