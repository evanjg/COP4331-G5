using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGiver : MonoBehaviour {
	public DropTable dropTable;

	public void GiveABox() {
		ItemCount itemCount = dropTable.GetRandomItemCount();
		Debug.Assert(itemCount != null, "Item count was null!?");
		Debug.Assert(Player.instance != null, "Player is null");
		Debug.Assert(itemCount.item != null, "Item was null!?");
		Debug.Log("Player got item: " + itemCount.item.title + " x " + itemCount.count);
		Player.instance.playerData.inventory.AddItem(itemCount.count, itemCount.item);
	}
}
