using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        this.gameObject.SetActive(false);
	}
	
    //Toggles display of options menu when optionsButton is pressed
	public void OnButtonPress()
    {
        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
        else
            this.gameObject.SetActive(true);
    }
}
