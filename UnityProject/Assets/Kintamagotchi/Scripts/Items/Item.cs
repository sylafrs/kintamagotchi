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
			Monster.instance.OnObjectDropped();
		
		if (ItemDesc.Type == TypeItem.Meuble)
			Monster.instance.OnHappyBecauseNewMeubleAdded();
		
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
					items[i].Remove();
				}
			}
		}
	}

	protected void Remove()
	{
		GameObject.Destroy(this.gameObject);
		GameData.Get.Data.Spots[(int)this.usedSlot - 1] = null;
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
		if (!slot.GetComponent<cObject>())
			return;
		slot.GetComponent<cObject>().hasActivatedObject = true;
	}

	public void Accident()
	{
		if (GameData.Get.Data.MaterialAssurance)
		{
			GameData.Get.Data.Diamonds += this.ItemDesc.Price;
		}

		this.Remove();
	}
}
