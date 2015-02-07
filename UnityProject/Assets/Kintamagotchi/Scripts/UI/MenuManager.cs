//******************************************************************************
// Authors: Frederic SETTAMA  
//******************************************************************************

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//******************************************************************************
public class MenuManager : MonoBehaviour 
{
#region Script Parameters
	public GameObject	PanelShop;
	public GameObject	PanelInventory;
	public GameObject	PanelDiamonds;
	public GameObject	PanelGlobal;
	public ItemShop		ItemsShop;
	public BoxUI		DialogBox;
	public BoxUI		MessageBox;
	public Text			DiamondsLabel;
	public GameObject	ImgMaladie;
	public GameObject	ImgHabitation;
#endregion

#region Static
	private static MenuManager mInstance;
	public static MenuManager Get { get { return mInstance; } }
#endregion

#region Fields
	private ItemDesc		mItemToBuy;
	private DiamondsDesc	mDiamondsToBuy;
	private const string	CONFIRMATION_MSG = "Voulez-vous vraiment acheter ";
	private const string	ERROR_MSG = "Solde insuffisant";
	private const string	ALREADY_BUY = "Objet déjà acheté";
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

	void Start()
	{
		UpdateDiamonds();
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

	public void BuyItem(string name, TypePanel type)
	{
		if (type == TypePanel.Shop)
			mItemToBuy = ItemsShop.GetItem(name);
		else if (type == TypePanel.Diamonds)
			mDiamondsToBuy = ItemsShop.GetDiamonds(name);
		DialogBox.SetTextAndShow(CONFIRMATION_MSG+name);
	}

	public void ValidatedBuy()
	{
		bool ret = false;

		if (mItemToBuy != null)
			ret = BuyItem();
		else if (mDiamondsToBuy != null)
			ret = BuyDiamonds();
		if (!ret)
			MessageBox.SetTextAndShow(ERROR_MSG);
		mItemToBuy = null;
		mDiamondsToBuy = null;
	}

	public void UseItem(string name)
	{

	}
#endregion

#region Implementations
	private bool BuyItem()
	{
		GameData.Item	item;
		int				diamonds = GameData.Get.Data.Diamonds;

		if (diamonds < mItemToBuy.Price)
			return false;

		item = GameData.Get.GetItem(mItemToBuy.Name);
		if (item != null)
		{
			if (mItemToBuy.Type == TypeItem.Consommable)
			{
				GameData.Get.Data.Diamonds = diamonds - mItemToBuy.Price;
				GameData.Get.UpdateCountItem(item);
				UpdateDiamonds();
			}
			else
				MessageBox.SetTextAndShow(ALREADY_BUY);
		}
		else
		{
			GameData.Get.Data.Diamonds = diamonds - mItemToBuy.Price;
			item = new GameData.Item();
			item.ItemDetail = mItemToBuy;
			item.Number = 1;
			GameData.Get.AddItem(item);
			UpdateDiamonds();
		}
		return true;
	}

	private bool BuyDiamonds()
	{
		//Transfert compte vers epargne

		GameData.Get.Data.Diamonds += mDiamondsToBuy.Value;
		UpdateDiamonds();
		return true;
	}

	private void UpdateDiamonds()
	{
		DiamondsLabel.text = GameData.Get.Data.Diamonds.ToString();
	}
#endregion
}
