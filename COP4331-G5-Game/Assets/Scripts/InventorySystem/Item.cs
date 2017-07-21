using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName="NewItem.asset")]
public class Item : ScriptableObject, ISerializationCallbackReceiver {
	public static Dictionary<string, Item> allItems = new Dictionary<string, Item>();

	[Header("Technical")]
	[Tooltip("A unique identifier for this item (format: some-item-name)")]
	public string id;
	[Tooltip("The sorting index for this item (for sorted inventories)")]
	public int sortingIndex = 1;
	[Tooltip("The maximum number of this item that can go in one slot")]
	public int stackLimit = 1;

	[Space]
	[Header("Flavor")]
	[Tooltip("The icon of the item")]
	public Sprite icon;
	[Tooltip("Name of the item in English")]
	public string title;
	[Tooltip("Description of the item in English")]
	public string description;

	public Item() {
	}

	public void OnAfterDeserialize() {
		Debug.Log("After deserialize");
		Debug.Log("Item " + id + " deserialized");
		allItems.Add(id, this);
	}

	public void OnBeforeSerialize() {

	}
}
