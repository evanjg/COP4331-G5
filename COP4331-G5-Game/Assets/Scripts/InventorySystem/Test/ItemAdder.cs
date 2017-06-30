using UnityEngine;
using System.Collections;

public class ItemAdder : MonoBehaviour {
	public Backpack backpack;
	public Item item;
	public int count;

	public void AddItems() {
		backpack.inventory.AddItem(count, item);
	}
}
