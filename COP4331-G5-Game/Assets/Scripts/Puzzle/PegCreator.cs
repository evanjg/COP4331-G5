using UnityEngine;
using System.Collections;

public class PegCreator : MonoBehaviour {
	public Peg pegPrefab;
	public PegHole targetHole;

	void Start() {
		CreatePegAtTarget();
	}

	public void CreatePegAtTarget() {
		if (targetHole.myPeg == null) {
			Peg p = Instantiate<Peg>(pegPrefab);

			targetHole.SetPeg(p);
		}
	}
}
