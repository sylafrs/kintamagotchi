﻿//******************************************************************************
// Authors: Frédéric SETTAMA
//    
//******************************************************************************

using UnityEngine;
using System.IO;
using System.Collections.Generic;

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
		public int			Level = 1;
		public float		Exp = 0;
		public int			Diamonds = 25;
		public List<Item>	Inventory;
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
		return Data.Inventory.Find(x => x.ItemDetail.Name.Equals(name));
	}

	public void AddItem(Item item)
	{
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
