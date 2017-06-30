using System;

public class ItemSlot {
	public Item item = null;
	public int count = 0;
	public ItemData data = null;

	public event Action onChanged;

	public void SetValues(int count, Item item, ItemData data) {
		this.count = count;
		this.item = item;
		this.data = data;

		if (onChanged != null)
			onChanged();
	}

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
