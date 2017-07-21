using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PegSolitaireBoard : PegHoleMatrix {
	public Peg pegPrefab;
	public int pegCount = 8;
	public PegEvent onPegJumped = new PegEvent();
	public PegEvent onPegDidJump = new PegEvent();
	public UnityEvent onNoValidMoves = new UnityEvent();
	
	void Start () {
		Generate();
		CreatePuzzle();
		onPegJumped.AddListener(PegJumped);
		onPegDidJump.AddListener(PegDidJump);
	}
	
	void Update () {
	
	}

	private void PegJumped(Peg jumped) {
		PegHole hole = jumped.myHole;
		hole.RemovePeg();
		GameObject.Destroy(jumped.gameObject);

		if (GetValidMoves().Count == 0)
			onNoValidMoves.Invoke();
	}

	private void PegDidJump(Peg jumper) {
		Debug.Log("Peg with value " + jumper.value + " did jump");
	}

	public void CreatePuzzle() {
		//AddRandomPegs();
		/*
			An anti-move is like a move, but in reverse. Instead of removing a peg from the board,
			it creates a peg.

			The puzzle creation algorithm is as follows:
			1)	Start with empty board with all holes deactivated.
			2)	Activate the center hole and put a peg in it.
			3)	Consider all possible anti-moves. Choose one, and execute it. If there are none, or pegCount pegs, stop.

			We could potentially make puzzles more interesting by using a preference for holes we've already used.
			Do this by encorporating a cost for an anti-move: the number of holes would we have to make active if we 
			were to use this anti-move. Select a random 0-cost move first, if there are none, 1-cost, and so on.

			Additionally, we could keep performing anti-moves (and thus adding pegs to the board) until we have reached
			at least one hole on each side of the board (the top, bottom, left and right). This will guarantee the generated
			puzzle has a minimum width/height.
		*/

		// Deactivate all holes
		for (int x = 0; x < boardDimensions.x; x++)
			for (int y = 0; y < boardDimensions.y; y++)
				GetPegHole(x, y).gameObject.SetActive(false);

		// Add a singular peg to the center hole
		PegHole centerHole = GetPegHole((int) boardDimensions.x / 2, (int) boardDimensions.y / 2);
		centerHole.gameObject.SetActive(true);
		AddPegToHole(centerHole);
		for (int i = 0; i < pegCount - 1; i++) {
			List<Move> antimoves = GetValidAntiMoves();
			if (antimoves.Count == 0) break;
			Move move = antimoves[Random.Range(0, antimoves.Count - 1)];
			ExecuteAntiMove(move);
		}
	}

	public void AddPegToHole(PegHole hole) {
		Peg peg = Instantiate<Peg>(pegPrefab);
		hole.SetPeg(peg);
		peg.SetColor(Color.HSVToRGB(Random.Range(0f, 100f)/100f, 1f, 1f));
	}

	public void ExecuteAntiMove(Move move) {
		PegHole holeSource = GetPegHole(move.x, move.y);
		Peg peg = holeSource.myPeg;
		holeSource.RemovePeg();

		PegHole holeJump = GetPegHole(move.x + move.dx / 2, move.y + move.dy / 2);
		holeJump.gameObject.SetActive(true);
		AddPegToHole(holeJump);

		PegHole holeDest = GetPegHole(move.x + move.dx, move.y + move.dy);
		holeDest.gameObject.SetActive(true);
		holeDest.SetPeg(peg);
	}

	public List<Move> GetValidAntiMoves() {
		List<Move> antimoves = new List<Move>();
		for (int x = 0; x < boardDimensions.x; x++) {
			for (int y = 0; y < boardDimensions.y; y++) {
				ConsiderAntiMove(antimoves, x, y, -2, 0);
				ConsiderAntiMove(antimoves, x, y, 2, 0);
				ConsiderAntiMove(antimoves, x, y, 0, -2);
				ConsiderAntiMove(antimoves, x, y, 0, 2);
			}
		}
		return antimoves;
	}

	public void ConsiderAntiMove(List<Move> antimoves, int x, int y, int dx, int dy) {
		int x2 = x + dx, y2 = y + dy, cost = 0;
		if (x2 < 0 || x2 >= boardDimensions.x) return;
		if (y2 < 0 || y2 >= boardDimensions.y) return;
		PegHole holeSource = GetPegHole(x, y);
		if (!holeSource.HasPeg()) return;
		PegHole holeJump = GetPegHole(x + dx / 2, y + dy / 2);
		if (holeJump.HasPeg()) return;
		if (!holeJump.gameObject.activeSelf) cost++;
		PegHole holeDest = GetPegHole(x2, y2);
		if (holeDest.HasPeg()) return;
		if (!holeDest.gameObject.activeSelf) cost++;

		Move move = new Move(x, y, dx, dy);
		move.cost = cost;
		antimoves.Add(move);
	}

	public List<Move> GetValidMoves() {
		List<Move> moves = new List<Move>();
		for (int x = 0; x < boardDimensions.x; x++) {
			for (int y = 0; y < boardDimensions.y; y++) {
				ConsiderMove(moves, x, y, -2, 0);
				ConsiderMove(moves, x, y, 2, 0);
				ConsiderMove(moves, x, y, 0, -2);
				ConsiderMove(moves, x, y, 0, 2);
			}
		}
		return moves;
	}

	public void ConsiderMove(List<Move> moves, int x, int y, int dx, int dy) {
		int x2 = x + dx, y2 = y + dy;
		if (x2 < 0 || x2 >= boardDimensions.x) return;
		if (y2 < 0 || y2 >= boardDimensions.y) return;
		PegHole holeSource = GetPegHole(x, y);
		if (!holeSource.gameObject.activeSelf || !holeSource.HasPeg()) return;
		PegHole holeJump = GetPegHole(x + dx / 2, y + dy / 2);
		if (!holeJump.gameObject.activeSelf || !holeJump.HasPeg()) return;
		PegHole holeDest = GetPegHole(x2, y2);
		if (!holeDest.gameObject.activeSelf || holeDest.HasPeg()) return;

		moves.Add(new Move(x, y, dx, dy));
	}

	public void AddRandomPegs() {
		for (int i = 0; i < pegCount; i++) {
			PegHole hole = null;
			do {
				hole = GetRandomPegHole();
			} while (hole == null || hole.HasPeg());
			AddPegToHole(hole);
		}
	}

	public override bool IsValidMove(Move move) {
		// The move must be from a valid peg hole
		if (!IsValidPegHole(move.x, move.y)) return false;

		// It's not a move unless you actually move somewhere
		if (move.dx == 0 && move.dy == 0) return false;

		// You can only move horizontally or vertically 2 spaces exactly
		if (!(
			(move.dx == 0 && Mathf.Abs(move.dy) == 2) ||
			(move.dy == 0 && Mathf.Abs(move.dx) == 2))) return false;

		// The end hole must be on the board
		int x2 = move.x + move.dx, y2 = move.y + move.dy;
		if (!IsValidPegHole(x2, y2)) return false;

		// Get the source/destination holes
		PegHole holeSource = GetPegHole(move.x, move.y);
		PegHole holeDest = GetPegHole(x2, y2);

		// Both holes must exist and be active
		if (holeSource == null || holeDest == null) return false;
		if (!holeSource.gameObject.activeSelf || !holeDest.gameObject.activeSelf) return false;

		// There must be an active hole with a peg between the start hole and end hole
		PegHole jumpHole = GetPegHole(move.x + move.dx / 2, move.y + move.dy / 2);
		if (jumpHole == null || !jumpHole.gameObject.activeSelf) return false;
		if (!jumpHole.HasPeg()) return false;

		return true;
	}

	public override void ExecuteMove(Move move) {
		Peg pegJumper = GetPegHole(move.x, move.y).myPeg;

		// Move the peg
		base.ExecuteMove(move);
		// Also remove the jumped peg
		PegHole holeJumped = GetPegHole(move.x + move.dx / 2, move.y + move.dy / 2);
		Peg pegJumped = holeJumped.myPeg;
		onPegDidJump.Invoke(pegJumper);
		onPegJumped.Invoke(pegJumped);
	}
}

