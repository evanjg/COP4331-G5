using UnityEngine;
using System.Collections;

public class ItemRemover : MonoBehaviour {
	public Backpack backpack;
	public Item item;
	public int count;

	public void RemoveItems() {
		backpack.inventory.RemoveItem(count, item);
	}
}
