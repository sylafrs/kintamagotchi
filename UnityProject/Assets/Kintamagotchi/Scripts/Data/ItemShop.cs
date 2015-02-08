//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************

public enum TypeItem
{
	Consommable,
	Meuble,
	Assurance
}

[System.Serializable]
public class ItemDesc
{
	public string			Name;
	public string			Sprite;
	public string			Desc;
	public int				Price;
	public TypeItem			Type;
	public eObjectType[]	Slots;
	public int				Morale;
	public int				XP;
}

[System.Serializable]
public class DiamondsDesc
{
	public string	Name;
	public float	Price;
	public int		Value;
}

public class ItemShop : MonoBehaviour 
{
#region Script Parameters
	public List<ItemDesc>		ItemDescList;
	public List<DiamondsDesc>	DiamondsDescList;
#endregion

#region Static
#endregion

#region Properties
#endregion

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
#endregion

#region Unity Methods
	void Awake()
	{
		Load();
	}
#endregion

#region Methods
	public ItemDesc GetItem(string name)
	{
		ItemDesc ret = ItemDescList.Find(x => x.Name.Equals(name));
		return ret;
	}

	public DiamondsDesc GetDiamonds(string name)
	{
		DiamondsDesc ret = DiamondsDescList.Find(x => x.Name.Equals(name));
		return ret;
	}
#endregion

#region Implementation
	private void Load()
	{
		TextAsset shopItems = Resources.Load<TextAsset>("xml/ShopItems");
		if (shopItems == null)
		{
			Debug.LogError("ShopItems.xml not found in folder xml");
		}
		ItemDescList = Serialization.DeserialiseFromTextAsset<List<ItemDesc>>(shopItems);
		TextAsset shopDiamonds = Resources.Load<TextAsset>("xml/ShopDiamonds");
		if (shopDiamonds == null)
		{
			Debug.LogError("ShopDiamonds.xml not found in folder xml");
		}
		DiamondsDescList = Serialization.DeserialiseFromTextAsset<List<DiamondsDesc>>(shopDiamonds);
	}
#endregion
}
