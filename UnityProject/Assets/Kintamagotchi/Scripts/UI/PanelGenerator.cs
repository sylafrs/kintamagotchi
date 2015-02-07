﻿//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************

public enum TypePanel
{
	Shop,
	Inventory,
	Diamonds
}

public class PanelGenerator : MonoBehaviour 
{
#region Script Parameters
	public List<Canvas> ItemsList;
	public ItemShop		ItemsShop;
	public TypePanel	Type;
#endregion

#region Properties
#endregion

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
#endregion

#region Unity Methods
	void OnEnable()
	{
		if (ItemsList.Count == 0 || Type == TypePanel.Inventory)
		{
			GeneratePanel();
		}
	}
#endregion

#region Methods
	public void Clear()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
		transform.DetachChildren();
	}
#endregion

#region Implementation
	private void GeneratePanel()
	{
		Clear();
		switch (Type)
		{
			case TypePanel.Inventory:
				GenerateInventoryPanel();
				break;
			case TypePanel.Shop:
				GenerateShopPanel();
				break;
			case TypePanel.Diamonds:
				GenerateDiamondsPanel();
				break;
			default:
				break;
		}
	}
	private void GenerateDiamondsPanel()
	{
		foreach (DiamondsDesc item in ItemsShop.DiamondsDescList)
		{
			var container = GameObject.Instantiate(Resources.Load("Prefabs/UI/" + Type)) as GameObject;
			SetParent(container, transform);
			var uiValue = container.GetComponent<ItemUIDiamonsValue>();
			if (!uiValue)
			{
				Debug.LogError("no ItemUIValue on prefab " + container.name);
				return;
			}
			uiValue.Name.text = item.Name;
			uiValue.Price.text = item.Price.ToString();
		}
	}
	
	private void GenerateShopPanel()
	{
		foreach(ItemDesc item in ItemsShop.ItemDescList)
		{
			var container = GameObject.Instantiate(Resources.Load("Prefabs/UI/" + Type)) as GameObject;
			SetParent(container, transform);
			var uiValue = container.GetComponent<ItemUIShopValue>();
			if (!uiValue)
			{
				Debug.LogError("no ItemUIValue on prefab " + container.name);
				return;
			}
			uiValue.Name.text = item.Name;
			uiValue.Price.text = item.Price.ToString();
			uiValue.Desc.text = item.Desc;
		}
	}
	private void GenerateInventoryPanel()
	{
		foreach(var item in GameData.Get.Data.Inventory)
		{
			var container = GameObject.Instantiate(Resources.Load("Prefabs/UI/" + Type)) as GameObject;
			SetParent(container, transform);
			var uiValue = container.GetComponent<ItemUIInventoryValue>();
			if (!uiValue)
			{
				Debug.LogError("no ItemUIValue on prefab " + container.name);
				return;
			}
			uiValue.Name.text = item.ItemDetail.Name;
			uiValue.Count.text = item.Number.ToString();
			uiValue.Desc.text = item.ItemDetail.Desc;
		}
	}

	void SetParent(GameObject obj, Transform parent)
	{
		Vector3 position;
		Quaternion rotation;
		Vector3 scale;

		position = obj.transform.position;
		rotation = obj.transform.rotation;
		scale = obj.transform.localScale;
		obj.transform.parent = parent;
		obj.transform.localPosition = position;
		obj.transform.localRotation = rotation;
		obj.transform.localScale = scale;
	}
#endregion
}
