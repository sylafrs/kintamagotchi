//******************************************************************************
// Authors: Frédéric SETTAMA
//    
//******************************************************************************

using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

[Serializable]
public class EventCheck
{
	public string EventChecked;
	public DateTime LastCheckTime = DateTime.Now;
}

[System.Serializable]
public class GameDataItem
{
	public ItemDesc ItemDetail;
	public int Number;
}

[System.Serializable]
public class SaveData
{
	public int					Level = 1;
	public int					Exp = 0;
	public int					Diamonds = 25;
	public List<EventCheck>		EventChecks = new List<EventCheck>();
	public List<GameDataItem>	Inventory;
	public float				MoralLastInteraction = 0;
	public float				Moral = 0.4f;
	public string[]				Spots = new string[4];

	public EventCheck GetEventCheckByName(string name)
	{
		foreach (EventCheck e in EventChecks)
		{
			if (e.EventChecked == name)
				return e;
		}

		return null;
	}
}

//******************************************************************************
public class GameData : MonoBehaviour
{
#region Static
	private static GameData mInstance;
	public static GameData Get { get{ return mInstance; } }
#endregion

#region Fields
	// Public ------------------------------------------------------------------
	public SaveData		Data;
#endregion

#region Unity Methods
	void Awake()
	{
		if(mInstance != null && mInstance != this)
		{
			DestroyImmediate(this.gameObject, true);
			return;
		}
		DontDestroyOnLoad (this);
		mInstance = this;
		Load();
	}

	void OnApplicationQuit()
	{
		Save();
	}

#endregion

#region Methods
	public void Load()
	{
		var dataPath = Path.Combine(Application.persistentDataPath, "SaveData.xml");
		if(File.Exists(dataPath))
		{
			Data = Serialization.FromFile<SaveData>(dataPath);
		}
		else
		{
			Data = new SaveData();
		}
	}

	public void Save()
	{
		var dataPath = Path.Combine(Application.persistentDataPath, "SaveData.xml");
		Serialization.ToFile<SaveData>(Data, dataPath);
	}

	public GameDataItem GetItem(string name)
	{
		if (GameData.Get.Data.Inventory == null)
			return null;
		return Data.Inventory.Find(x => x.ItemDetail.Name.Equals(name));
	}

	public void AddItem(GameDataItem item)
	{
		if (GameData.Get.Data.Inventory == null)
			Data.Inventory = new List<GameDataItem>();
		Data.Inventory.Add(item);
	}

	public void UpdateCountItem(GameDataItem item)
	{
		foreach (GameDataItem it in Data.Inventory)
		{
			if (it.ItemDetail == item.ItemDetail)
			{
				it.Number++;
				return;
			}
		}
	}

	public void DecreaseCountItem(ItemDesc item)
	{
		foreach (GameDataItem it in Data.Inventory)
		{
			if (it.ItemDetail == item)
			{
				it.Number--;
				if (it.Number == 0)
				{
					Data.Inventory.Remove(it);
				}
				return;
			}
		}
	}
#endregion

	public int MaxExp
	{
		get
		{
			switch (Data.Level)
			{
				case 1:
					return 150;
				case 2:
					return 175;
				case 3:
					return 200;
				case 4:
					return 225;
				case 6:
					return 250;
			}

			return 250;
		}
	}

	public void CheckXP()
	{
		while (Data.Exp > MaxExp)
		{
			Data.Exp -= MaxExp;
			Data.Level++;
		}
	}

	public void CheckMorale()
	{
		Data.Moral = Mathf.Clamp01(Data.Moral);

		if(Data.Moral < 0.2f)
		{
			// SAD :'(
		}
		else if(Data.Moral < 0.45f)
		{
			// NEUTRAL :|
		}
		else
		{
			// HAPPY ! :D
		}
	}
}
