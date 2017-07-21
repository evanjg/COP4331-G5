using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class PegHoleMatrix : MonoBehaviour, IMoveValidator, IMoveExecutor {
	public PegHole holePrefab;
	public Vector2 boardDimensions = new Vector2(5, 5);
	public Vector3 pegHoleSpacing = new Vector3(.2f, .2f, 0f);
	public Vector3 pegHoleSize = new Vector3(1f, 1f, 0f);
	public Dictionary<Vector2, PegHole> holes;

	public MoveEvent onMakingMove = new MoveEvent();
	public MoveEvent onMoveMade = new MoveEvent();
	public UnityEvent onInitialized = new UnityEvent();
	public bool isInitialized = false;

	void Start () {
		Generate();
	}
	
	void Update () {
	
	}

	public void Generate() {
		holes = new Dictionary<Vector2, PegHole>();
		GameObject cont = new GameObject("PegHoleMatrix");
		cont.transform.SetParent(transform, false);
		for (int x = 0; x < boardDimensions.x; x++) {
			for (int y = 0; y < boardDimensions.y; y++) {
				PegHole hole = Instantiate<PegHole>(holePrefab);
				hole.name = "(" + x + ", " + y + ")";
				hole.transform.SetParent(cont.transform, false);
				hole.transform.localPosition = new Vector3(
					(pegHoleSize.x + pegHoleSpacing.x) * x,
					(pegHoleSize.y + pegHoleSpacing.x) * y,
					0f
				);
				holes.Add(new Vector2(x, y), hole);
				hole.holeX = x;
				hole.holeY = y;
				hole.moveValidator = this;
				hole.moveExecutor = this;
			}
		}
		cont.transform.localPosition = new Vector3(
			(boardDimensions.x - 1) * pegHoleSpacing.x +
				boardDimensions.x * pegHoleSize.x,
			(boardDimensions.y - 1) * pegHoleSpacing.y +
				boardDimensions.y * pegHoleSize.y,
			0f
		) * -.5f + (Vector3) (pegHoleSize * .5f);
		isInitialized = true;
		onInitialized.Invoke();
	}

	public List<Peg> GetPegs() {
		List<Peg> pegs = new List<Peg>();
		for (int x = 0; x < boardDimensions.x; x++) {
			for (int y = 0; y < boardDimensions.y; y++) {
				PegHole hole = GetPegHole(x, y);
				if (hole.HasPeg())
					pegs.Add(hole.myPeg);
			}
		}
		return pegs;
	}

	public int CountPegs() {
		int count = 0;
		for (int x = 0; x < boardDimensions.x; x++)
			for (int y = 0; y < boardDimensions.y; y++)
				if (GetPegHole(x, y).HasPeg())
					count++;
		return count;
	}

	public bool IsValidPegHole(int x, int y) {
		return x >= 0 && y >= 0 && x < boardDimensions.x && y < boardDimensions.y;
	}

	public PegHole GetPegHole(Vector2 coord) {
		Debug.Assert(holes != null, "PegHoles not initialized");
		return holes[coord];
	}

	public PegHole GetPegHole(int x, int y) {
		return GetPegHole(new Vector2(x, y));
	}

	public PegHole GetRandomPegHole() {
		return GetPegHole(
			Random.Range(0, (int) boardDimensions.x),
			Random.Range(0, (int) boardDimensions.y)
		);
	}

	public virtual bool IsValidMove(Move move) {
		Debug.Log("Defaulting");
		return true;
	}

	public virtual void ExecuteMove(Move move) {
		PegHole holeSource = GetPegHole(move.x, move.y);
		PegHole holeDest = GetPegHole(move.x + move.dx, move.y + move.dy);
		Peg peg = holeSource.myPeg;

		onMakingMove.Invoke(move);

		holeSource.RemovePeg();
		holeDest.SetPeg(peg);

		onMoveMade.Invoke(move);
	}
}
