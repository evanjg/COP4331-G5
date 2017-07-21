using System;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class ItemSlot {
	public string itemId;
	[NonSerialized]
	public Item item = null;
	public int count = 0;
	public ItemData data = null;

	public event Action onChanged;

	public void SetValues(int count, Item item, ItemData data) {
		this.count = count;
		this.item = item;
		if (item != null) {
			this.itemId = item.id;
		} else {
			this.itemId = "";
		}
		
		this.data = data;

		if (onChanged != null)
			onChanged();
	}

	public ItemSlot() {
		Clear();
	}

	/*
	public ItemSlot(SerializationInfo info, StreamingContext context) {
		Clear();
		Debug.Log("Constructing from serialization");
		string itemId = (string) info.GetValue("item-id", typeof(string));
		if (itemId != null && Item.allItems.ContainsKey(itemId)) {
			item = Item.allItems[itemId];
			count = (int) info.GetValue("item-count", typeof(int));
		} else if (itemId != null) {
			Debug.Log("Unknown item id " + itemId);
		} else {
			Debug.Log("No item id " + itemId);
		}
	}

	public void GetObjectData(SerializationInfo info, StreamingContext context) {
		Debug.Log("Get object data");
		info.AddValue("item-id", item.id);
		info.AddValue("item-count", count);
	}*/

	public void Clear() {
		SetValues(0, null, null);
	}

	public void SetCount(int count) {
		SetValues(count, this.item, this.data);
	}

	public void SetItem(Item item) {
		SetValues(this.count, item, this.data);
	}

	public void SetData(ItemData data) {
		SetValues(this.count, this.item, data);
	}
	
	public bool IsEmpty() {
		return item == null && count == 0;
	}

	public bool HasItem(Item item) {
		return this.item == item;
	}

	public bool HasData(ItemData data) {
		return this.data == data;
	}
}
