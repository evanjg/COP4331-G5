using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName="NewBoostItem.asset")]
public class BoostItem : ActivatableItem {
	[Space]
	[Header("Boost Item")]
	[Tooltip("The value of this boosting item")]
	public int boostValue = 1;

	[Tooltip("This boost item only usable on what pet? (Leave blank for any pet)")]
	public Pet requiredPet;

	public override bool IsActivatable(IItemActivator activator) {
		return true;
	}

	public override void Activate(IItemActivator activator, ItemSlot slot) {
		base.Activate(activator, slot);

		Player player = (Player) activator;

		if (requiredPet != null && player.playerData.pet != requiredPet) {
			// Exchange for rare candy?
			return;
		}

		player.playerData.AwardExperience(boostValue);
	}
}
