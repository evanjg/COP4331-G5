using UnityEngine;
using System.Collections;

public class PegRecolor : MonoBehaviour {
	public PegHole targetHole;
	public Color color;

	public void RecolorPegAtTarget() {
		if (targetHole.myPeg != null) {
			targetHole.myPeg.SetColor(color);
		}
	}
}