using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class InventoryPanel : MonoBehaviour {
	public Backpack backpack;
	private Inventory inventory;
	public ItemSlotPanel[] itemSlotPanels;

	public ItemSlotPanel itemSlotPanelPrefab;

	public UnityEvent onInventoryInitialized = new UnityEvent();

	void Start () {
		if (backpack != null) SetInventory(backpack.inventory);
		if (Player.instance.playerData.inventory != null) {
			SetInventory(Player.instance.playerData.inventory);
		} else {
			Debug.Log("No inventory");
			Player.instance.playerData.onLoaded.AddListener(() => {
				Debug.Log("Okay it was loaded");
				SetInventory(Player.instance.playerData.inventory);
			});
		}
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
		onInventoryInitialized.Invoke();
	}
}
