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

	public EventCheck HasEvent(List<EventCheck> events, string name)
	{
		foreach (EventCheck e in events)
		{
			if (e.EventChecked == name)
				return e;
		}

		return null;
	}

	public void Check()
	{
		DateTime lastCheck;
		DateTime now = DateTime.Now;
		List<EventCheck> checks = GameData.Get.Data.EventChecks;
		EventCheck eCheck;

		foreach(TimeEvent e in Events)
		{			
			lastCheck = now;
			
			eCheck = HasEvent(checks, e.Name);
			if (eCheck == null)
			{
				eCheck = new EventCheck();
				eCheck.EventChecked = e.Name;
				eCheck.LastCheckTime = now;
				checks.Add(eCheck);
			}
			else
			{
				lastCheck = eCheck.LastCheckTime;
			}

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

				eCheck.LastCheckTime = e.GetLastCheck(i, lastCheck);
			}
		}
	}	

}
