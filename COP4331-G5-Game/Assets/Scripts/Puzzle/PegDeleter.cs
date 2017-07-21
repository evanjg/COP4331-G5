using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PegHole))]
public class PegDeleter : MonoBehaviour {
	public PegHole targetHole;

	public void DeletePegFromTarget() {
		if (targetHole.myPeg) {
			targetHole.myPeg.DestroyPeg();
		}
	}
}
