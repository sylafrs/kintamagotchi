using UnityEngine;
using System.Collections;

public class ItemBall : Item {

	public override void Use(eObjectType slotUsed, GameObject slot)
	{
		base.Use(slotUsed, slot);
		Monster.instance.SendEvent("OnPlayBall");
	}

}
