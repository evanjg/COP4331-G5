using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour {
	private Text text;

	void Awake() {
		text = GetComponent<Text>();
	}
	
	void Update () {
		text.text = "" + Player.instance.playerData.score;
	}
}
