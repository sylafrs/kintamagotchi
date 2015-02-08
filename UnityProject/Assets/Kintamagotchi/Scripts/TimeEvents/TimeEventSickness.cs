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
				MenuManager.Get.MessageBox.SetTextAndShow("Votre ami est en bonne santé grâce à votre assurance maladie");
				GameData.Get.Data.SicknessAssurance--;
				if (GameData.Get.Data.SicknessAssurance <= 0)
				{
					GameData.Get.Data.SicknessAssurance = 0;
					MenuManager.Get.ImgMaladie.SetActive(false);
				}
			}
			else
			{
				Debug.Log("Sickness NOT prevented :'(");
				MenuManager.Get.MessageBox.SetTextAndShow("Votre ami est tombé malade, il faut le soigner (antibiotique ou assurance maladie)");
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
