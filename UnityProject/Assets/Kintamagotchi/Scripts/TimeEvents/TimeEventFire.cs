using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Evenement : incendie.
/// </summary>
public class TimeEventFire : TimeEvent 
{
	public bool DebugMode;
	private bool OnFire;
	
	public override float Chance
	{
		get 
		{
			Debug.Log("Try chance");

			if (DebugMode)
				return 0.80f;

			return 0.05f; 
		}
	}

	public override void Launch()
	{
		if (OnFire)
			return;

		Debug.Log("FIRE !");

		OnFire = true;
		Item[] items = GameObject.FindObjectsOfType<Item>();
		foreach (Item i in items)
		{
			i.Fire();
		}

		this.toListen.Push(clipList[0]);
		this.toListen.Push(clipList[1]);

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
		Item[] items = GameObject.FindObjectsOfType<Item>();
		foreach (Item i in items)
		{
			i.Remove();
		}
		OnFire = false;
		this.toListen.Push(clipList[2]);
	}

	public override int MustCheck(TimeSpan dt)
	{
		if (OnFire)
			return 0;

		if(DebugMode)
			return (int)dt.TotalSeconds / 1;
		return ((int)dt.TotalHours) / 24;
	}

	public override DateTime GetLastCheck(int i, DateTime last)
	{
		if (DebugMode)
			return last.AddSeconds(i * 1);

		return last.AddHours(i * 24);
	}

	protected override void OnAudioChanged(AudioClip audioClip)
	{
		this.audio.loop = (audioClip == this.clipList[1]);
	}

}
