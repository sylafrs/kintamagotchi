//******************************************************************************
// Authors: Frédéric SETTAMA
//    
//******************************************************************************

using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

//******************************************************************************
public class GameData : MonoBehaviour
{
	[System.Serializable]
	public class Item
	{
		public ItemDesc		ItemDetail;
		public int			Number;
	}

	[System.Serializable]
	public class SaveData
	{
		public int							Level = 1;
		public float						Exp = 0;
		public int							Diamonds = 25;
		public Dictionary<string, DateTime> EventChecks;
		public List<Item>					Inventory;
		public float						MoralLastInteraction = 0;
		public float						Moral = 0.4f;
	}

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

	public Item GetItem(string name)
	{
		if (GameData.Get.Data.Inventory == null)
			return null;
		return Data.Inventory.Find(x => x.ItemDetail.Name.Equals(name));
	}

	public void AddItem(Item item)
	{
		if (GameData.Get.Data.Inventory == null)
			Data.Inventory = new List<Item>();
		Data.Inventory.Add(item);
	}

	public void UpdateCountItem(Item item)
	{
		foreach (Item it in Data.Inventory)
		{
			if (it.ItemDetail == item.ItemDetail)
			{
				it.Number++;
				return;
			}
		}
	}
#endregion
}
