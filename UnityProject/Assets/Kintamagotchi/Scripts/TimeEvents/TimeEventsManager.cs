using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using DateTime = System.DateTime;
using TimeSpan = System.TimeSpan;

public class TimeEventsManager : MonoBehaviour {

	public TimeEvent[]	Events		{ get; private set; }
	public DateTime		LastCheck	{ get; private set; }
	
	private void Awake()
	{
		Events		= this.GetComponents<TimeEvent>();
		InvokeRepeating("Check", 0, 600);
	}

	public void Check()
	{
		DateTime lastCheck;
		DateTime now = DateTime.Now;
		Dictionary<string, DateTime> checks = GameData.Get.Data.EventChecks;

		foreach(TimeEvent e in Events)
		{			
			lastCheck = now;
			if(checks.ContainsKey(e.Name))		
				lastCheck = checks[e.Name];			
			else			
				checks.Add(e.Name, now);
			
			int count = e.MustCheck(now - lastCheck);
			for (int i = 0; i < count; i++)
			{
				if (e.Chance != 0)
				{
					float rand = Random.Range(0f, 1f);
					if (rand < e.Chance)
					{
						e.Launch();
					}
				}

				checks[e.Name] = e.GetLastCheck(i, lastCheck);
			}
		}
	}	

}
