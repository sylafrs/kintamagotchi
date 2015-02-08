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

	public GameObject[] Slots;
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

	void OnApplicationPause(bool pause)
	{
#if (UNITY_IPHONE || UNITY_ANDROID)
		if (pause)
			Save();
#endif
	}

#endregion

#region Properties
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

		this.LoadItems();
	}

	public void Save()
	{
		var dataPath = Path.Combine(Application.persistentDataPath, "SaveData.xml");
		Serialization.ToFile<SaveData>(Data, dataPath);
	}

	public ItemDesc GetItem(string name)
	{
		if (GameData.Get.Data.Inventory == null)
			return null;

		GameDataItem item = Data.Inventory.Find(x => x.ItemDetail.Name.Equals(name));
		if(item != null)	
			return item.ItemDetail;
		
		List<Item> itemsInScene = new List<Item>(GameObject.FindObjectsOfType<Item>());
		Item itemInScene = itemsInScene.Find(x => x.ItemDesc.Name.Equals(name));
		if (itemInScene != null)
			return itemInScene.ItemDesc;

		return null;
	}

	public void AddItem(GameDataItem item)
	{
		if (GameData.Get.Data.Inventory == null)
			Data.Inventory = new List<GameDataItem>();
		Data.Inventory.Add(item);
	}

	public void UpdateCountItem(ItemDesc item)
	{
		foreach (GameDataItem it in Data.Inventory)
		{
			if (it.ItemDetail == item)
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
			if (it.ItemDetail.Name == item.Name)
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
	
	public void CheckXP()
	{
		while (Data.Exp > MaxExp)
		{
			Data.Exp -= MaxExp;
			Data.Level++;
			GameObject.FindObjectOfType<Monster>().OnLvlUp();
		}
	}

	public void CheckMorale()
	{
		Data.Moral = Mathf.Clamp01(Data.Moral);

		if(Data.Moral < 0.2f)
		{
			// SAD :'(
			GameObject.FindObjectOfType<Monster>().OnSad();
		}
		else if(Data.Moral < 0.45f)
		{
			// NEUTRAL :|
			GameObject.FindObjectOfType<Monster>().OnNeutral();
		}
		else
		{
			// HAPPY ! :D
			GameObject.FindObjectOfType<Monster>().OnIsHappy();
		}
	}

	private void LoadItems()
	{
		ItemDesc itemDesc;
		GameObject mItemGrab;
		ItemShop shop = this.GetComponent<ItemShop>();
		Item item;
		for (int i = 0; i < this.Data.Spots.Length; i++ )
		{
			string name = this.Data.Spots[i];
			if(!string.IsNullOrEmpty(name))
			{
				mItemGrab = GameObject.Instantiate(Resources.Load("Prefabs/Item/Cube")) as GameObject;
				itemDesc = shop.GetItem(name);
				item = mItemGrab.GetComponent<Item>();
				item.ItemDesc = itemDesc;
				item.usedSlot = (eObjectType)(i + 1);
				item.PlaceToSlot(Slots[i]);
			}
		}			
	}

#endregion
}
