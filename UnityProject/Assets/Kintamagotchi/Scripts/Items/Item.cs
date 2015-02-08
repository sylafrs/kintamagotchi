using UnityEngine;

public class Item : MonoBehaviour 
{
	[HideInInspector]
	public eObjectType usedSlot;

	public Vector3 OriginalScale;
	public float offset;

	[HideInInspector]
	public ItemDesc		ItemDesc;
		
	public virtual bool CanUse { get { return true; } }

	public virtual void Use(eObjectType slotUsed, GameObject slot)
	{
		this.UpdateStatus();

		this.usedSlot = slotUsed;
		if (slotUsed != eObjectType._NO_TYPE_)
		{
			this.RemovePrevious(slotUsed);
			GameData.Get.Data.Spots[(int)slotUsed - 1] = this.ItemDesc.Name;
		}
		if (ItemDesc.Type == TypeItem.Consommable || ItemDesc.Type == TypeItem.Assurance)
			GameObject.FindObjectOfType<Monster>().OnObjectDropped();
		if (ItemDesc.Type == TypeItem.Meuble)
			GameObject.FindObjectOfType<Monster>().OnHappyBecauseNewMeubleAdded();
		if(slot)
			PlaceToSlot(slot);
	}

	protected void RemovePrevious(eObjectType slot)
	{
		string itemName = GameData.Get.Data.Spots[(int)slot - 1];
		if (!string.IsNullOrEmpty(itemName))
		{
			Item[] items = GameObject.FindObjectsOfType<Item>();
			for(int i = 0; i < items.Length; i++)
			{
				if(items[i] != this && items[i].usedSlot == this.usedSlot)
				{
					GameObject.Destroy(items[i].gameObject);
				}
			}
		}
	}

	protected void UpdateStatus()
	{
		GameData.Get.Data.Exp += ItemDesc.XP;
		GameData.Get.Data.Moral += 0.01f * ItemDesc.Morale;

		GameData.Get.CheckXP();
		GameData.Get.CheckMorale();
	}

	public virtual void PlaceToSlot(GameObject slot)
	{
		this.transform.position = slot.transform.position + slot.transform.up * offset;
		this.transform.rotation = slot.transform.rotation;
		this.transform.localScale = this.OriginalScale;
		slot.GetComponent<cObject>().hasActivatedObject = true;
	}
}
