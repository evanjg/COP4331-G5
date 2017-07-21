using UnityEngine;

public class Backpack : MonoBehaviour {
	public int slotCount = 8;
	public Inventory inventory;

	public Item startItem;
	public int startItemCount;

	void Awake() {
		inventory = new Inventory(slotCount);
		if (startItem != null) {
			inventory.AddItem(startItemCount, startItem);
		}
	}

	void Update() {
	
	}
}
