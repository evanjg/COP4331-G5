using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Pawstop : MonoBehaviour {
	public static int BOX_COOLDOWN = 60 * 4;

	public GeoPoint geoPoint;

	[Tooltip("Pawstop active radius in feet")]
	public int activeRadius;

	public UnityEvent onActivate = new UnityEvent();
	public UnityEvent onDeactivate = new UnityEvent();
	public bool isActive = false;
	public double distance = 0.0f;
	public bool isBoxAvailable = false;

	private Collider col;
	private Animator anim;

	public UnityEvent onBoxCollected = new UnityEvent();
	public ParticleSystem particleCollect;

	void Awake() {
		col = GetComponent<Collider>();
		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		isBoxAvailable = isBoxAvailable || GetTimeSinceLastVisit() >= BOX_COOLDOWN;
		anim.SetBool("Active", isActive);
		anim.SetBool("BoxAvailable", isBoxAvailable);
	}

	void OnMouseDown() {
		if (isActive) {
			if (isBoxAvailable) CollectBox();
		}
	}

	public void SetIsActive(bool isActive) {
		bool wasActive = this.isActive;
		if (!wasActive && isActive) {
			onActivate.Invoke();
		} else if (wasActive && !isActive) {
			onDeactivate.Invoke();
		}
		this.isActive = isActive;
	}

	public void CollectBox() {
		geoPoint.lastVisit = PawstopManager.GetTimestamp();
		isBoxAvailable = false;
		particleCollect.Stop();
		particleCollect.Play();
		onBoxCollected.Invoke();
	}

	public int GetTimeSinceLastVisit() {
		Debug.Assert(geoPoint != null, "geoPoint is null oh darn");
		if (geoPoint == null) return 0;
		return PawstopManager.GetTimestamp() - geoPoint.lastVisit;
	}

	public void Setup(GeoPoint geoPoint) {
		this.geoPoint = geoPoint;
	}

}
