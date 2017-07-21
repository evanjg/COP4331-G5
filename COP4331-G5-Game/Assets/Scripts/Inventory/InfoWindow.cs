using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour {
	public InventoryPanel inventoryPanel;

	public ItemSlot slot;
	public Text title;
	public Text description;
	public Image icon;
	public Text activation;
	public Button activateButton;
	public Text activateButtonText;
	public Button closeButton;

	public Player activator;

	void Awake() {
		activator = Player.instance;
	}
	
	void Start() {
		Close();
		closeButton.onClick.AddListener(Close);

		Setup();
		inventoryPanel.onInventoryInitialized.AddListener(Setup);

		activateButton.onClick.AddListener(DoActivation);

	}

	private void Setup() {
		if (inventoryPanel != null) {
			foreach (ItemSlotPanel slotPanel in inventoryPanel.itemSlotPanels) {
				slotPanel.onSelected.AddListener(() => {
					if (slotPanel.slot != null && !slotPanel.slot.IsEmpty()) {
						Show(slotPanel.slot);
					}
				});
				slotPanel.onAltSelected.AddListener(() => {
					if (slotPanel.slot != null && !slotPanel.slot.IsEmpty()) {
						((ActivatableItem) slotPanel.slot.item).Activate(activator, slotPanel.slot);
					}
				});
			}
		}
	}

	private void DoActivation() {
		Close();
		((ActivatableItem) slot.item).Activate(activator, slot);
	}

	public void Show(ItemSlot slot) {
		this.slot = slot;
		SetItem(slot.item);
		gameObject.SetActive(true);
	}

	public void Close() {
		gameObject.SetActive(false);
	}

	public void SetItem(Item item) {
		title.text = item.title;
		icon.sprite = item.icon;
		description.text = item.description;
		if (item is ActivatableItem) {
			ActivatableItem aitem = (ActivatableItem) item;
			activateButton.gameObject.SetActive(true);
			activation.gameObject.SetActive(true);
			activateButtonText.text = aitem.useVerb;
			activation.text = aitem.activeEffectDescription;
		} else {
			activateButton.gameObject.SetActive(false);
			activation.gameObject.SetActive(false);
		}
	}
}
