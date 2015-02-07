using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TimeEventDepression : TimeEvent
{
	private DateTime lastInteraction;
	private TimeSpan dt;
	
	public override int MustCheck(TimeSpan dt)
	{
		this.dt = dt;
		return 1;
	}

	public override float Chance
	{
		get { return 1; }
	}

	public override void Launch()
	{
		float ratio = (float)dt.TotalHours * 0.020833f; // ( / 48)
		float toLose = ratio * GameData.Get.Data.MoralLastInteraction;
		// Petite depression
		GameData.Get.Data.Moral = 
			Mathf.Min(0, GameData.Get.Data.Moral - toLose);
	}

	public override DateTime GetLastCheck(int i, DateTime lastCheck)
	{
		return DateTime.Now;
	}
}
