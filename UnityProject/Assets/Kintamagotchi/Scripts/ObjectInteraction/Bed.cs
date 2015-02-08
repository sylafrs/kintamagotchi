using UnityEngine;
using System.Collections;
using System;

public class Bed : cObject 
{
    // Use this for initialization
    void Start()
    {
        base.Start();
        _pType = eObjectType._BED_;
    }

    public override void OnTapped()
    {
		if (!hasActivatedObject)
			return;

		if ((DateTime.Now - GameData.Get.Data.LastBedTime).TotalHours >= 3)
		{
			GameData.Get.Data.Moral += 30;
			GameData.Get.Data.LastBedTime = DateTime.Now;
		}

        base.OnTapped();
    }
}
