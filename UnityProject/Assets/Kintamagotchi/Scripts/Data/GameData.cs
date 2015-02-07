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
	public struct Item
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
#endregion
}
