﻿//******************************************************************************
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
	public GameObject		PanelShop;
	public GameObject		PanelInventory;
	public GameObject		PanelDiamonds;
	public GameObject		PanelGlobal;
	public GameObject		PanelCheat;
	public GameObject		PanelCredits;
	public Button			ButtonInventory;
	public Button			ButtonShop;
	public ItemShop			ItemsShop;
	public BoxUI			DialogBox;
	public BoxUI			MessageBox;
	public Text				DiamondsLabel;
	public GameObject		ImgMaladie;
	public GameObject		ImgHabitation;
	public Slider			Exp;
	public GameObject[]		SlotsRenderer = new GameObject[4];
	public float			FadeSpeed = 0.1f;
	public List<AudioClip>	ClipList;
	public Color			SelectedColor = new Color(169, 102, 51);
#endregion

#region Static
	private static MenuManager mInstance;
	public static MenuManager Get { get { return mInstance; } }
#endregion

#region Fields
	private ItemDesc		mItemToBuy;
	private DiamondsDesc	mDiamondsToBuy;
	private GameObject		mItemGrab = null;
	private bool			mDropItem = true;
	private AudioSource		mActualSound;
	private AudioClip		mNextClip;
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
		mActualSound = GetComponent<AudioSource>();
		UpdateDiamonds();
		UpdateExp();
	}

	void Update()
	{
		ChecksInteractionPossibility();
		playSound();
		UpdateDiamonds();
		UpdateExp();
		if (mItemGrab)
		{
			if (UpdatePosItemGrab())
			{
				Destroy(mItemGrab);
				mItemGrab = null;
				mDropItem = true;
				return;
			}
			if (mDropItem)
				DropItem();
		}
	}
#endregion

#region Methods

	public void ShowCredits()
	{
		if (PanelCheat.activeSelf)
		{
			PanelCheat.gameObject.SetActive(false);
			PanelCredits.gameObject.SetActive(true);
		}
		else
		{
			PanelCredits.gameObject.SetActive(false);
		}
	}
	public void ShowInventory()
	{
		PanelShop.SetActive(false);
		PanelDiamonds.SetActive(false);
		PanelInventory.SetActive(true);
		ButtonInventory.gameObject.SetActive(true);
		ButtonShop.gameObject.SetActive(true);
		ButtonInventory.image.color = SelectedColor;
		ButtonShop.image.color = Color.white;;
		ClearPopup();
	}

	public void ShowShop()
	{
		PanelShop.SetActive(true);
		PanelDiamonds.SetActive(false);
		PanelInventory.SetActive(false);
		ButtonInventory.gameObject.SetActive(true);
		ButtonShop.gameObject.SetActive(true);
		ButtonInventory.image.color = Color.white;
		ButtonShop.image.color = SelectedColor;
		ClearPopup();
	}

	public void ShowDiamonds()
	{
		if (!(PanelGlobal.activeSelf && (PanelShop.activeSelf || PanelInventory.activeSelf)))
		{
			PanelGlobal.SetActive(!PanelGlobal.activeSelf);
		}
		if (PanelGlobal.activeSelf)
		{
			mNextClip = ClipList[0];
			PanelCheat.gameObject.SetActive(false);
			PanelShop.SetActive(false);
			PanelDiamonds.SetActive(true);
			PanelInventory.SetActive(false);
			ButtonInventory.gameObject.SetActive(false);
			ButtonShop.gameObject.SetActive(false);
		}
		ClearPopup();
		ChecksInteractionPossibility();
	}

	public void ChecksInteractionPossibility()
	{
		InteractionManager.instance.enabled = !PanelGlobal.activeSelf &&
											  !MessageBox.gameObject.activeSelf &&
											  !DialogBox.gameObject.activeSelf &&
											  !PanelCheat.gameObject.activeSelf;
	}

	public void SwitchVisibility()
	{
		if (!(PanelGlobal.activeSelf  && PanelDiamonds.activeSelf))
		{
			PanelGlobal.SetActive(!PanelGlobal.activeSelf);
		}
		if(PanelGlobal.activeSelf)
		{
			PanelCredits.gameObject.SetActive(false);
			PanelCheat.gameObject.SetActive(false);
			mNextClip = ClipList[0];
			ShowInventory();
		}
		ChecksInteractionPossibility();
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
		string prefabName = name.Replace(" ", "_").Replace("é", "e");
		mItemGrab = GameObject.Instantiate(Resources.Load("Prefabs/Item/" + prefabName)) as GameObject;
		ItemDesc itemDesc = ItemsShop.GetItem(name);
		mItemGrab.GetComponent<Item>().ItemDesc = itemDesc;
		mDropItem = false;
		PanelGlobal.SetActive(false);
		ShowSlot(itemDesc);
	}

	public void DropItem()
	{
		cObject cObj;
		GameObject obj;

		ShowSlot();
		obj = InteractionManager.instance.FindObjectTouched(GetMousePosition());
		if (obj)
		{
			Item i = mItemGrab.GetComponent<Item>();
	
			if (CheckItemToCollider(obj) && i.CanUse)
			{
				eObjectType typeSlot = eObjectType._NO_TYPE_;
				if (cObj = obj.GetComponent<cObject>())
					typeSlot = cObj.pType;
				i.Use(typeSlot, obj);
				GameData.Get.DecreaseCountItem(i.ItemDesc);
				
				if(i.ItemDesc.Type != TypeItem.Meuble)
				{
					GameObject.Destroy(mItemGrab);
				}
				else
				{
					foreach (GameObject carton in cObj.Cartons)
					{
						carton.renderer.enabled = false;
					}
				}

				mItemGrab = null;
				InteractionManager.instance.enabled = true;
				return;
			}
		}
		Destroy(mItemGrab);
		mItemGrab = null;
		InteractionManager.instance.enabled = true;
	}

	public void CheatDance()
	{
		Monster.instance.SendEvent("OnDance");
	}

	public void CheatFire()
	{
		GameData.Get.GetComponent<TimeEventFire>().Launch();
	}

	public void CheatSick()
	{
		GameData.Get.GetComponent<TimeEventSickness>().Launch();
	}

	public void CheatReset()
	{
		GameData.Get.Data = new SaveData();
		GameData.Get.Data.Spots = new string[4];
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif

	}
#endregion

#region Implementations
	private bool CheckItemToCollider(GameObject obj)
	{
		ItemDesc itemDesc = mItemGrab.GetComponent<Item>().ItemDesc;
		switch (itemDesc.Type)
		{
			case TypeItem.Consommable:
				if (obj.CompareTag("Player"))
					return true;
				break;
			case TypeItem.Assurance:
				if (obj.CompareTag("Player"))
					return true;
				break;
			case TypeItem.Meuble:
				cObject cObj = obj.GetComponent<cObject>();
				if (!cObj)
					break;
				if (itemDesc.Slots.Contains(cObj.pType))
					return true;
				break;
			default:
				break;
		}
		return false;
	}
	private Vector3 GetMousePosition()
	{
#if !FORCE_MOUSE
		if (Input.touchSupported)
		{
			if (Input.touchCount > 0)
			{
				Touch t = Input.GetTouch(0);
				if (t.phase == TouchPhase.Moved)
					mDropItem = false;
				else if (t.phase == TouchPhase.Ended)
					mDropItem = true;
				return t.position;
			}
		}
		else
#endif
		{
			if (Input.GetMouseButtonUp(0))
				mDropItem = true;
			else if (Input.GetMouseButton(0))
				mDropItem = false;
			return Input.mousePosition;
		}
		return Vector3.zero;
	}

	private Vector3 GetMousePositionToWorld(Vector3 mousePos)
	{
		mousePos.z = Camera.main.nearClipPlane + 4f;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
		return mousePos;
	}

	private bool UpdatePosItemGrab()
	{
		Vector3 mousePos = GetMousePosition();
		if (mousePos == Vector3.zero)
			return true;
		mItemGrab.transform.position = GetMousePositionToWorld(mousePos);
		if (mItemGrab.transform.position == Vector3.zero)
			return true;
		return false;
	}

	private bool BuyItem()
	{
		ItemDesc	item;
		int			diamonds = GameData.Get.Data.Diamonds;

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
				mNextClip = ClipList[1];
			}
			else
				MessageBox.SetTextAndShow(ALREADY_BUY);
		}
		else
		{
			GameData.Get.Data.Diamonds = diamonds - mItemToBuy.Price;
			GameDataItem newItem = new GameDataItem();
			newItem.ItemDetail = mItemToBuy;
			newItem.Number = 1;
			GameData.Get.AddItem(newItem);
			UpdateDiamonds();
			mNextClip = ClipList[1];
		}
		return true;
	}

	private bool BuyDiamonds()
	{
		//Transfert compte vers epargne

		GameData.Get.Data.Diamonds += mDiamondsToBuy.Value;
		UpdateDiamonds();
		mNextClip = ClipList[2];
		return true;
	}

	private void UpdateDiamonds()
	{
		DiamondsLabel.text = GameData.Get.Data.Diamonds.ToString();
	}

	private void UpdateExp()
	{
		Exp.value = GameData.Get.Data.Exp / (float)GameData.Get.MaxExp;
	}

	private void ClearPopup()
	{
		mItemToBuy = null;
		mDiamondsToBuy = null;
		MessageBox.gameObject.SetActive(false);
		DialogBox.gameObject.SetActive(false);
	}

	public void ShowSlot(ItemDesc item = null)
	{
		foreach (GameObject obj in SlotsRenderer)
		{
			cObject o = obj.GetComponent<cObject>();
			if (item != null && item.Type == TypeItem.Meuble && item.Slots.Contains(o.pType))
				obj.renderer.enabled = true;
			else
				obj.renderer.enabled = false;

			if (item != null)
			{
				foreach (GameObject carton in o.Cartons)
				{
					carton.renderer.enabled = !obj.renderer.enabled && !HasItemAt(o);
				}
			}
			else
			{
				foreach (GameObject carton in o.Cartons)
				{
					carton.renderer.enabled = !HasItemAt(o);
				}
			}
		}
		
	}

	private bool HasItemAt(cObject o)
	{
		int type = (int)o.pType;
		if (type == 0)
			return false;

		return GameData.Get.Data.Spots[type - 1] != null;
	}


	bool fade()
	{
		mActualSound.volume -= Time.deltaTime * FadeSpeed;
		if (mActualSound.volume <= 0.0f)
		{
			mActualSound.Stop();
			return true;
		}
		return false;
	}

	private void playSound()
	{
		if (mActualSound.isPlaying)
			fade();
		if (mActualSound.volume <= 0.0f || !mActualSound.isPlaying)
		{
			mActualSound.volume = 1.0f;
			mActualSound.clip = mNextClip;
			mNextClip = null;
			mActualSound.Play();
		}
	}

	public void ShowMaladie()
	{
		ImgMaladie.SetActive(!ImgMaladie.activeSelf);
	}

	public void ShowHabitation()
	{
		ImgHabitation.SetActive(!ImgHabitation.activeSelf);
	}

	public void ToogleCheatMenu()
	{
		PanelCheat.gameObject.SetActive(!PanelCheat.gameObject.activeSelf);
		if (PanelCheat.gameObject.activeSelf)
		{
			PanelCredits.SetActive(false);
			PanelGlobal.SetActive(false);
		}
	}

#endregion

	
}
