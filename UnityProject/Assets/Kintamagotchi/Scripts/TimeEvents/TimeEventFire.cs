using UnityEngine;
using System;

/// <summary>
/// Evenement : incendie.
/// </summary>
public class TimeEventFire : TimeEvent 
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
		Debug.Log("FIRE !");
	}

	public override int MustCheck(TimeSpan dt)
	{
		if(DebugMode)
			return (int)dt.TotalSeconds / 30;
		return ((int)dt.TotalHours) / 24;
	}

	public override DateTime GetLastCheck(int i, DateTime last)
	{
		if (DebugMode)
			return last.AddSeconds(i * 30);

		return last.AddHours(i * 24);
	}
}
