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

		Item[] items = GameObject.FindObjectsOfType<Item>();
		foreach(Item i in items)
		{
			i.Accident();
		}

		MenuManager.Get.MessageBox.OnValidated += OnPopupValidated;
		if(GameData.Get.Data.MaterialAssurance)
		{
			MenuManager.Get.MessageBox.SetTextAndShow("Tout a brûlé !!! :( Heureusement, vous avez une assurance habitation :)");
		}
		else
		{
			MenuManager.Get.MessageBox.SetTextAndShow("Tout a brûlé !!! :( Avec une assurance habitation, vous auriez pu tout racheter ");
		}

		GameData.Get.Data.MaterialAssurance = false;
	}

	public void OnPopupValidated()
	{
		MenuManager.Get.MessageBox.OnValidated -= OnPopupValidated;
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
