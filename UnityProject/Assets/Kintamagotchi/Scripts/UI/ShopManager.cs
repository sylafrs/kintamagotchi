//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************
public class ShopManager : MonoBehaviour 
{
#region Script Parameters
	public GameObject	PanelShop;
	public GameObject	PanelInventory;
	public GameObject	PanelDiamonds;
	public GameObject	PanelGlobal;
	public ItemShop		ItemsShop;
#endregion

#region Static
	private static ShopManager mInstance;
	public static ShopManager Get { get { return mInstance; } }
#endregion

#region Unity Methods
	void Awake()
	{
		if (mInstance != null && mInstance != this)
		{
			DestroyImmediate(this.gameObject, true);
			return;
		}
		DontDestroyOnLoad(this);
		mInstance = this;
	}
#endregion

#region Methods
	public void ShowInventory()
	{
		PanelShop.SetActive(false);
		PanelDiamonds.SetActive(false);
		PanelInventory.SetActive(true);
	}

	public void ShowShop()
	{
		PanelShop.SetActive(true);
		PanelDiamonds.SetActive(false);
		PanelInventory.SetActive(false);
	}

	public void ShowDiamonds()
	{
		if (!PanelGlobal.activeSelf)
		{
			InteractionManager.instance.enabled = false;
			PanelGlobal.SetActive(true);
		}
		PanelShop.SetActive(false);
		PanelDiamonds.SetActive(true);
		PanelInventory.SetActive(false);
	}

	public void SwitchVisibility()
	{
		PanelGlobal.SetActive(!PanelGlobal.activeSelf);
		InteractionManager.instance.enabled = !PanelGlobal.activeSelf;
		
		if(PanelGlobal.activeSelf)
		{
			ShowInventory();
		}
	}

	public void BuyItem(string name)
	{
		Debug.Log("achat effectué " + name);
	}
#endregion
}
