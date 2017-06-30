using UnityEngine;
using System.Collections;

public class InventoryPanel : MonoBehaviour {
	public Backpack backpack;
	private Inventory inventory;
	private ItemSlotPanel[] itemSlotPanels;

	public ItemSlotPanel itemSlotPanelPrefab;

	void Start () {
		SetInventory(backpack.inventory);
	}

	public void SetInventory(Inventory inventory) {
		// TODO: clean up old slots here
		this.inventory = inventory;
		itemSlotPanels = new ItemSlotPanel[this.inventory.slotCount];
		for (int i = 0; i < this.inventory.slotCount; i++) {
			ItemSlotPanel panel = Instantiate<ItemSlotPanel>(itemSlotPanelPrefab);
			panel.transform.SetParent(transform);
			panel.SetSlot(inventory.slots[i]);
			itemSlotPanels[i] = panel;
		}
	}
}
