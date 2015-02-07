//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//******************************************************************************
public class ItemUIShopValue : MonoBehaviour 
{
#region Script Parameters
	public Text		Name;
	public Text		Price;
	public Text		Desc;
	public Image	Sprite;

#endregion

#region Methods
	public void BuyItem()
	{
		ShopManager.Get.BuyItem(Name.text);
	}
#endregion
}
