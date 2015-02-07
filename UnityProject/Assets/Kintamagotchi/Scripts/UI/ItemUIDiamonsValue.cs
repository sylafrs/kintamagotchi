//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//******************************************************************************
public class ItemUIDiamonsValue : MonoBehaviour 
{
#region Script Parameters
	public Text		Name;
	public Text		Price;
#endregion

#region Methods
	public void BuyItem()
	{
		MenuManager.Get.BuyItem(Name.text, TypePanel.Diamonds);
	}
#endregion

}
