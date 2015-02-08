using UnityEngine;
using System;

/// <summary>
/// Evenement : maladie.
/// </summary>
public class TimeEventSickness : TimeEvent
{
	public bool DebugMode;

	public override float Chance
	{
		get
		{
			Debug.Log("Try chance");

			if (DebugMode)
				return 0.5f;

			return 0.05f;
		}
	}

	public override void Launch()
	{
		if(!GameData.Get.Data.IsSick)
		{
			Debug.Log("Sick ! </3");

			if (GameData.Get.Data.SicknessAssurance > 0)
			{
				Debug.Log("Sickness prevented");
				GameData.Get.Data.SicknessAssurance--;
			}
			else
			{
				Debug.Log("Sickness NOT prevented :'(");
				GameData.Get.Data.IsSick = true;
			}
		}
		
	}

	public override int MustCheck(TimeSpan dt)
	{
		if (DebugMode)
			return (int)dt.TotalSeconds / 30;
		return ((int)dt.TotalHours) / 14;
	}

	public override DateTime GetLastCheck(int i, DateTime last)
	{
		if (DebugMode)
			return last.AddSeconds(i * 30);

		return last.AddHours(i * 14);
	}
}
