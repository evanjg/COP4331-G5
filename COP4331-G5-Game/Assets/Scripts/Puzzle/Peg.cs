using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Peg : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static Peg DRAG_PEG;
	public PegHole myHole;
	public Vector3 dragOffset = Vector3.zero;
	Collider2D col;
	SpriteRenderer spr;

	public int value = 0;

	void Awake () {
		col = GetComponent<Collider2D>();
		spr = GetComponentInChildren<SpriteRenderer>();
	}
	
	void Start () {
		MoveToMyHole();
	}

	void Update () {
	
	}

	public void SetColor(Color c) {
		spr.color = c;
	}

	public void OnBeginDrag(PointerEventData eventData) {
		Peg.DRAG_PEG = this;
		transform.SetParent(null, true);

		Vector3 p = Camera.main.ScreenToWorldPoint(eventData.position);
		p.z = 0;
		dragOffset = transform.localPosition - p;
		col.enabled = false;
	}

	public void OnDrag(PointerEventData eventData) {
		Vector3 p = Camera.main.ScreenToWorldPoint(eventData.position);
		p.z = 0;
		transform.localPosition = p + dragOffset;
	}

	public void OnEndDrag(PointerEventData eventData) {
		MoveToMyHole();
		col.enabled = true;
	}

	public void MoveToMyHole() {
		if (myHole != null) {
			transform.SetParent(myHole.transform);
			transform.localPosition = new Vector3(0.0f, 0.0f, -0.1f);
		}
	}

	public void SetPegHole(PegHole newHole) {
		myHole = newHole;
		MoveToMyHole();
	}

	public void DestroyPeg() {
		if (myHole != null) {
			myHole.RemovePeg();
		}
		Destroy(gameObject);
	}
}
