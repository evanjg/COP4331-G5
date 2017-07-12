﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,100*Time.deltaTime,0);
	    if (transform.position.y >= 0)
	    {
            transform.position = new Vector3(transform.position.x, transform.position.y - 3 * Time.deltaTime, transform.position.z);
	    }
	}
}
