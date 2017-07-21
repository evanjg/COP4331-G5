using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ActivatableItem : Item {
	[Space]
	[Header("Activatable Effect")]
	[Tooltip("Destroy the item when activated?")]
	public bool destroyOnActivation = true;
	[Tooltip("A description of the active effect in English")]
	public string activeEffectDescription = "";
	[Tooltip("The conditions that must be met in order to activate the item")]
	public string activeEffectCondition = "";
	[Tooltip("A verb for this item's usage (i.e/ Use, Eat, Drink, Activate)")]
	public string useVerb = "Use";

	public abstract bool IsActivatable(IItemActivator activator);
	public virtual void Activate(IItemActivator activator, ItemSlot slot) {
		if (destroyOnActivation) {
			if (slot.count > 1) {
				slot.SetCount(slot.count - 1);
			} else {
				slot.Clear();
			}
		}
	}
}
