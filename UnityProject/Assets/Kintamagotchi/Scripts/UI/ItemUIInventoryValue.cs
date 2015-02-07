//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//******************************************************************************
public class ItemUIInventoryValue : MonoBehaviour 
{
#region Script Parameters
	public Text		Name;
	public Text		Count;
	public Text		Desc;
	public Image	Sprite;
#endregion

#region Methods
	public void BuyItem()
	{
		ShopManager.Get.UseItem(Name.text);
	}
#endregion

}
