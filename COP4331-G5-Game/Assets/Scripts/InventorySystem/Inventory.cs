using UnityEngine;
using System;
using System.Collections.Generic;

public class Inventory {
	public int slotCount;
	public ItemSlot[] slots;

	public Inventory(int slotCount) {
		this.slotCount = slotCount;
		InitSlots();
	}

	public Inventory() : this(8) {

	}

	private void InitSlots() {
		slots = new ItemSlot[slotCount];
		for (int i = 0; i < slotCount; i++) {
			slots[i] = new ItemSlot();
		}
	}

	public int CountItem(Item item) {
		int count = 0;
		for (int i = 0; i < slots.Length; i++)
			if (slots[i].HasItem(item))
				count += slots[i].count;
		return count;
	}

	public int CountItem(Item item, ItemData data) {
		int count = 0;
		for (int i = 0; i < slots.Length; i++)
			if (slots[i].HasItem(item) && slots[i].HasData(data))
				count += slots[i].count;
		return count;
	}

	public bool HasItem(Item item, int count) {
		for (int i = 0; i < slots.Length; i++) {
			if (slots[i].HasItem(item))
				count -= slots[i].count;
			if (count <= 0) return true;
		}
		return false;
	}

	public bool HasItem(Item item) {
		return HasItem(item, 1);
	}

	public bool HasItem(Item item, ItemData data, int count) {
		for (int i = 0; i < slots.Length; i++) {
			if (slots[i].HasItem(item) && slots[i].HasData(data))
				count -= slots[i].count;
			if (count <= 0) return true;
		}
		return false;
	}

	public bool HasItem(Item item, ItemData data) {
		return HasItem(item, data, 1);
	}

	public int GetSpaceFor(Item item) {
		int space = 0;
		for (int i = 0; i < slots.Length; i++)
			if (slots[i].HasItem(item) || slots[i].item == null)
				space += item.stackLimit - slots[i].count;
		return space;
	}

	public int AddItem(int count, Item item) {
		int itemsAdded = 0;
		for (int i = 0; i < slots.Length; i++) {
			ItemSlot slot = slots[i];
			if (slot.IsEmpty() || slot.HasItem(item)) {
				int itemsToAdd = Math.Min(count, item.stackLimit - slot.count);
				slot.SetValues(slot.count + itemsToAdd, item, slot.data);
				count -= itemsToAdd;
				if (count == 0)
					break;
			}
		}
		return itemsAdded;
	}

	// TODO: Don't repeat myself and use some ItemSlotFilter delegate
	public int AddItem(int count, Item item, ItemData data) {
		int itemsAdded = 0;
		for (int i = 0; i < slots.Length; i++) {
			ItemSlot slot = slots[i];
			if (slot.IsEmpty() || (slot.HasItem(item) && slot.HasData(data))) {
				int itemsToAdd = Math.Min(count, item.stackLimit - slot.count);
				slot.SetValues(slot.count + itemsToAdd, item, data);
				count -= itemsToAdd;
				if (count == 0)
					break;
			}
		}
		return itemsAdded;
	}

	public int AddItem(Item item) {
		return AddItem(1, item);
	}

	public int AddItem(Item item, ItemData data) {
		return AddItem(1, item, data);
	}

	public int RemoveItem(int count, Item item) {
		int itemsRemoved = 0;
		// Iterate over slots in reverse
		for (int i = slots.Length - 1; i >= 0; i--) {
			ItemSlot slot = slots[i];
			if (slot.HasItem(item)) {
				int itemsToRemove = Math.Min(slot.count, count);
				if (slot.count == itemsToRemove) {
					slot.Clear();
				} else {
					slot.SetValues(slot.count - itemsToRemove, slot.item, slot.data);
				}
				count -= itemsToRemove;
				itemsRemoved += itemsToRemove;
				if (count == 0)
					break;
			}
		}
		return itemsRemoved;
	}

	// TODO: Don't repeat myself and use some ItemSlotFilter delegate
	public int RemoveItem(int count, Item item, ItemData data) {
		int itemsRemoved = 0;
		// Iterate over slots in reverse
		for (int i = slots.Length - 1; i >= 0; i--) {
			ItemSlot slot = slots[i];
			if (slot.HasItem(item) && slot.HasData(data)) {
				int itemsToRemove = Math.Min(slot.count, count);
				if (slot.count == itemsToRemove) {
					slot.Clear();
				} else {
					slot.SetValues(slot.count - itemsToRemove, slot.item, slot.data);
				}
				count -= itemsToRemove;
				itemsRemoved += itemsToRemove;
				if (count == 0)
					break;
			}
		}
		return itemsRemoved;
	}

	public int RemoveItem(Item item) {
		return RemoveItem(1, item);
	}

	public int RemoveItem(Item item, ItemData data) {
		return RemoveItem(1, item, data);
	}
}
