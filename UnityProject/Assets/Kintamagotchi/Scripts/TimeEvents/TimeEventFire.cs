using UnityEngine;
using System;

/// <summary>
/// Evenement : incendie.
/// </summary>
public class TimeEventFire : TimeEvent 
{
	public override float Chance
	{
		get { return 0.05f; }
	}

	public override void Launch()
	{
		Debug.Log("FIRE !");
	}

	public override int MustCheck(TimeSpan dt)
	{
		return ((int)dt.TotalHours) / 24;
	}

	public override DateTime GetLastCheck(int i, DateTime last)
	{
		return last.AddHours(i * 24);
	}
}
