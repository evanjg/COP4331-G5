using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

	public GameObject target;
	Vector3 offset;

	public float TurnSpeed = .00001f;

	[HideInInspector]
	private float lastRotation = 0f;


	// Use this for initialization
	void Start () {
		offset = target.transform.position - transform.position;	
	}

	// Update is called once per frame
	void LateUpdate () {


		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = currentAngle;

		/*
		if (target.transform.eulerAngles.y != lastRotation) {
			lastRotation = target.transform.eulerAngles.y;
			desiredAngle = target.transform.eulerAngles.y;
		}
*/
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
			Vector2 deltaTouch = Input.GetTouch (0).deltaPosition;
			float vel = deltaTouch.magnitude / Time.deltaTime;

			if (deltaTouch.y >= 0) {
				transform.Rotate (0, vel * Time.deltaTime, 0);
			}
			if (deltaTouch.y < 0) {
				transform.Rotate (0, -vel * Time.deltaTime, 0);
			}
			desiredAngle = transform.eulerAngles.y;
		}

		// For Testing only: Rotates the camera using the 'q' and 'e' keys
		if (Input.GetKey (KeyCode.Q)) {
			transform.Rotate (0, -3.9f* Time.deltaTime, 0);
			desiredAngle = transform.eulerAngles.y;

		}
		if (Input.GetKey (KeyCode.E)) {
			transform.Rotate (0, 3.9f*Time.deltaTime, 0);
			desiredAngle = transform.eulerAngles.y;

		}

		float angle = Mathf.LerpAngle (currentAngle, desiredAngle, Time.deltaTime * 1);

		if (!(desiredAngle == currentAngle)) {
			Quaternion rotation = Quaternion.Euler (0, angle, 0);
			transform.position = target.transform.position - (rotation * offset);
			transform.LookAt (target.transform);
		}
	}
}
