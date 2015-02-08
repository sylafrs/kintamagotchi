using UnityEngine;
using System.Collections;
using System;

public class Wardrobe : cObject
{
	// Use this for initialization
	void Start()
	{
		base.Start();
		_pType = eObjectType._WARDROBE_;
	}
	
	public override void OnTapped()
	{
		if (!hasActivatedObject)
			return;

		if ((DateTime.Now - GameData.Get.Data.LastWardRobeTime).TotalHours >= 1)
		{
			GameData.Get.Data.Moral += 15;
			GameData.Get.Data.LastWardRobeTime = DateTime.Now;
		}

		Monster.instance.SendEvent("OnDance");
		base.OnTapped();
	}
}
