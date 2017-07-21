using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	public float speed = 6.0F;
	public float gravity = 20.0F;

	public float TurnSpeed = 100f;

	
	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;
	public Compass compass = new Compass();
	GameObject player;

    public GameObject prefab;

	void Start(){
		
		// Store reference to attached component
		compass.enabled = true;
		controller = GetComponent<CharacterController>();
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		// Use input up and down for direction, multiplied by speed
		moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= speed;

		

		// Rotates the player to face the direction of the phone
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation,
	        Quaternion.Euler(0, 180 + Input.compass.magneticHeading, 0), Time.deltaTime*0.5f);
        
		// For Testing only: Rotates the player using the 'n' and 'm' keys
		if(Input.GetKey(KeyCode.N)) {
			player.transform.Rotate (0, -TurnSpeed*Time.deltaTime , 0);
		}
		if (Input.GetKey(KeyCode.M)) {
			player.transform.Rotate (0, TurnSpeed*Time.deltaTime , 0);
		}

	    if (Input.GetKeyDown(KeyCode.H))
	    {
	        Instantiate(prefab, controller.transform.position + new Vector3(5,5,0), Quaternion.identity);
	    }

		// Move Character Controller
		if(moveDirection.magnitude > 0.001)
			controller.Move(moveDirection * Time.deltaTime);

		// Makes sure that the player is stuck to the ground.
		controller.transform.position = new Vector3(controller.transform.position.x, 0, controller.transform.position.z);
	}
}
