using UnityEngine;

public class Item : MonoBehaviour 
{


	[HideInInspector]
	public ItemDesc		ItemDesc;

	public virtual bool CanUse { get { return true; } }

	public virtual void Use(eObjectType slotUsed)
	{
		GameData.Get.Data.Exp	+= ItemDesc.XP;
		GameData.Get.Data.Moral += 0.01f * ItemDesc.Morale;

		GameData.Get.CheckXP();
		GameData.Get.CheckMorale();
	}
}
