using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestCases : MonoBehaviour
{
    bool petMenuButtonPressed;
    bool optionsMenuButtonPressed;
    bool animationsEnabled;
    float soundValue;

	// Use this for initialization
	void Start ()
    {
        petMenuButtonPressed = false;
        optionsMenuButtonPressed = false;
        animationsEnabled = false;
        soundValue = 100;
	}

    //Prints statement to console if options button is registered as being pressed
    public void OptionsButtonPressed()
    {
        this.optionsMenuButtonPressed = true;
        Debug.Log("Options button successfully pressed.");

        this.AllButtonsFunctional();
    }

    //Prints statement to console if pet menu button is registered as being pressed
    public void PetMenuButtonPressed()
    {
        this.petMenuButtonPressed = true;
        Debug.Log("Pet menu button successfully pressed.");

        this.AllButtonsFunctional();
    }

    public void SoundLevelAdjusted(float sliderValue)
    {
        this.soundValue = sliderValue;
        if (this.soundValue < 50)
            Debug.Log("Sound value below 50.");
    }

    public void UITransitionEnabled(bool toggle)
    {
        animationsEnabled = toggle;
        if (animationsEnabled)
            Debug.Log("Animations have been enabled.");
    }

    //Statement that declares if all buttons register as functional if all booleans are rendered true.
    void AllButtonsFunctional()
    {
        if (petMenuButtonPressed && optionsMenuButtonPressed)
        {
            Debug.Log("All buttons are functional!");
        }
    }

}
