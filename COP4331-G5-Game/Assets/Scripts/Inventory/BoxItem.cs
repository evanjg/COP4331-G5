using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
[CreateAssetMenu(fileName="NewBoxItem.asset")]
public class BoxItem : ActivatableItem {
	public static string SCENE_PUZZLE = "Scenes/puzzle";

	[Header("Box")]
	[Tooltip("The items to give when the box is opened")]
	public ItemCount[] contents;
	[Tooltip("The drop table to evaluate when the box is opened")]
	public DropTable dropTable;

	public override bool IsActivatable(IItemActivator activator) {
		return true;
	}

	public override void Activate(IItemActivator activator, ItemSlot slot) {
		base.Activate(activator, slot);
		Player player = (Player) activator;
		//player.inventory.GiveItems(contents);

		// Play the puzzle
		player.playerData.boxType = this;
		SceneManager.LoadScene(SCENE_PUZZLE);
	}
}
