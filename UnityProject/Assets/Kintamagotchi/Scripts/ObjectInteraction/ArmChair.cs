using UnityEngine;
using System.Collections;
using System;

public class ArmChair : cObject 
{
	// Use this for initialization
	void Start()
	{
		base.Start();
		_pType = eObjectType._ARMCHAIR_;

	}
	
	public override void OnTapped()
	{
		if (!hasActivatedObject)
			return;

		if ((DateTime.Now - GameData.Get.Data.LastArmChairTime).TotalMinutes >= 15)
		{
			GameData.Get.Data.Moral += 5;
			GameData.Get.Data.LastArmChairTime = DateTime.Now;
		}

		base.OnTapped();
	}
}
