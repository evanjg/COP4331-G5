using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class PegHole : MonoBehaviour, IDropHandler {
	public Peg myPeg;
	public PegEvent onPegRemoved;
	public PegEvent onPegPlaced;
	//public Vector2 coord = Vector2.zero;
	public int holeX;
	public int holeY;
	public IMoveValidator moveValidator;
	public IMoveExecutor moveExecutor;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RemovePeg() {
		Peg p = myPeg;
		myPeg = null;
		if (p != null)
			onPegRemoved.Invoke(p);
	}

	public bool HasPeg() {
		return myPeg != null;
	}

	public void SetPeg(Peg p) {
		myPeg = p;
		if (p != null) {
			p.SetPegHole(this);
			onPegPlaced.Invoke(p);
		}
	}

	public void OnDrop(PointerEventData eventData) {
		if (myPeg == null && Peg.DRAG_PEG) {
			PegHole holeSource = Peg.DRAG_PEG.myHole;

			Move move = new Move(
				holeSource.holeX, holeSource.holeY,
				holeX - holeSource.holeX, holeY - holeSource.holeY
			);
			if (moveValidator != null && !moveValidator.IsValidMove(move)) {
				Debug.Log("Not a valid move!");
				return;
			}

			if (moveExecutor != null) {
				moveExecutor.ExecuteMove(move);
			}
			/*if (Peg.DRAG_PEG.myHole)
				Peg.DRAG_PEG.myHole.RemovePeg();

			SetPeg(Peg.DRAG_PEG);*/
		}
	}
}
