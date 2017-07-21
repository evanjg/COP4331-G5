using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPDisplay : MonoBehaviour {
	public Text level;
	public Text totalXP;
	public Text nextLevelXP;
	public Slider slider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		level.text = "Lv. " + Player.instance.playerData.level;
		totalXP.text = "" + Player.instance.playerData.experience + " XP";
		nextLevelXP.text = Player.instance.playerData.experienceToNextLevel + " XP to next level";
		slider.value = Player.instance.playerData.percentToNextLevel;
	}
}
