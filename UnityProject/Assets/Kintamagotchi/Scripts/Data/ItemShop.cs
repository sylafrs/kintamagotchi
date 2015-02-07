//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************

[System.Serializable]
public class ItemDesc
{
	public string	Name;
	public string	Sprite;
	public string	Desc;
	public int		Price;
}

public class ItemShop : MonoBehaviour 
{
#region Script Parameters
	public List<ItemDesc> ItemDescList;
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
	}
#endregion
}
