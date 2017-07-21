using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

public class ItemSlotPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	public static float LONG_PRESS_TIME = 1.0f;
	public ItemSlot slot;

	public Text itemNameText;
	public Text countText;
	public Image itemIcon;

	private bool isPressed = false;
	private float pressTime = 0.0f;

	public UnityEvent onSelected = new UnityEvent();
	public UnityEvent onAltSelected = new UnityEvent();

	void Awake() {
		UpdateElements();
	}

	void Start() {
		UpdateElements();
		transform.localScale = Vector3.one;
	}

	void Update() {
		if (isPressed) {
			pressTime += Time.deltaTime;
			if (pressTime >= LONG_PRESS_TIME) {
				isPressed = false;
				Handheld.Vibrate();
				onAltSelected.Invoke();
			}
		}
	}

	public void SetSlot(ItemSlot slot) {
		if (this.slot != null)
			this.slot.onChanged -= UpdateElements;	
		this.slot = slot;
		this.slot.onChanged += UpdateElements;
	}

	public void UpdateElements() {
		if (itemNameText == null) return;
		
		if (slot != null) {
			if (!slot.IsEmpty()) {
				itemNameText.enabled = true;
				itemNameText.text = " " + slot.item.title + " ";
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

	public void OnPointerDown(PointerEventData eventData) {
		isPressed = true;
		pressTime = 0.0f;
	}

	public void OnPointerUp(PointerEventData eventData) {
		isPressed = false;
		if (pressTime < LONG_PRESS_TIME) {
			onSelected.Invoke();
		}
	}
}
