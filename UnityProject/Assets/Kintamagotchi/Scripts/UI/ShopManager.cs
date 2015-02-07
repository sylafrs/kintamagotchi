//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//******************************************************************************
public class ShopManager : MonoBehaviour 
{
#region Script Parameters
	public GameObject	PanelShop;
	public GameObject	PanelInventory;
	public GameObject	PanelGlobal;
#endregion

#region Static
#endregion

#region Properties
#endregion

#region Fields
	// Const -------------------------------------------------------------------

	// Private -----------------------------------------------------------------
#endregion

#region Methods
	public void ShowInventory()
	{
		PanelShop.SetActive(false);
		PanelInventory.SetActive(true);
	}

	public void ShowShop()
	{
		PanelShop.SetActive(true);
		PanelInventory.SetActive(false);
	}

	public void Show()
	{
		if (PanelGlobal.activeSelf)
			PanelGlobal.SetActive(false);
		else
		{
			PanelGlobal.SetActive(true);
			ShowInventory();
		}

	}
#endregion
}
