using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOpener : MonoBehaviour {

	void Start () {
		PlayerData playerData = Player.instance.playerData;
		if (playerData.boxType) {
			BoxItem box = playerData.boxType;
			int numberOfItems = GetItemCountForScore(playerData.score);

			playerData.boxType = null;
			playerData.score = 0;

			for (int i = 0; i < numberOfItems; i++) {
				ItemCount itemCount = box.dropTable.GetRandomItemCount();
				Player.instance.playerData.inventory.AddItem(itemCount.count, itemCount.item);
			}
		}
	}

	public static int GetItemCountForScore(int score) {
		int count = 1;
		if (score >= 70) count++;
		if (score >= 120) count++;
		return count;
	}
}
