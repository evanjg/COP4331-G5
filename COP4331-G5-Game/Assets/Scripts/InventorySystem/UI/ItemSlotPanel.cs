using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemSlotPanel : MonoBehaviour {
	private ItemSlot slot;

	public Text itemNameText;
	public Text countText;
	public Image itemIcon;

	void Awake() {
		UpdateElements();
	}

	void Start() {
		UpdateElements();
	}

	public void SetSlot(ItemSlot slot) {
		if (this.slot != null)
			this.slot.onChanged -= UpdateElements;	
		this.slot = slot;
		this.slot.onChanged += UpdateElements;
	}

	public void UpdateElements() {
		if (slot != null) {
			if (!slot.IsEmpty()) {
				itemNameText.enabled = true;
				itemNameText.text = slot.item.nameEnglish;
				countText.enabled = true;
				countText.text = "x " + slot.count;
				itemIcon.enabled = true;
				itemIcon.sprite = slot.item.icon;
			} else {
				itemNameText.enabled = false;
				countText.enabled = false;
				itemIcon.enabled = false;
			}
		} else {
			itemNameText.enabled = true;
			itemNameText.text = "null";
			countText.enabled = false;
			itemIcon.enabled = false;
			//countText.text = "null";
			//itemIcon.image = null;
		}
	}
}
