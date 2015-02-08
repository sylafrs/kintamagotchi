using UnityEngine;
using System.Collections;
using System;

public class Speaker : cObject 
{
	// Use this for initialization
	void Start()
	{
		base.Start();
		_pType = eObjectType._SPEAKER_;
	}
	
	public override void OnTapped()
	{
		if (!hasActivatedObject)
			return;

		if ((DateTime.Now - GameData.Get.Data.LastSpeakerTime).TotalHours >= 1)
		{
			GameData.Get.Data.Moral += 10;
			GameData.Get.Data.LastSpeakerTime = DateTime.Now;
		}

		base.OnTapped();
	}
}
