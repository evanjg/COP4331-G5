using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour {
	public static string INVENTORY_SCENE = "Scenes/inventory";

	// Initial value of all pegs
	public static int PEG_INITIAL_VALUE = 10;

	// Points awarded for solving the puzzle (leaving only one peg on the board)
	public static int SOLVE_REWARD = 6;

	// Value increase of peg after jump
	public static int PEG_VALUE_INCREASE = 4;

	public PegSolitaireBoard board;
	private PlayerData playerData;


	public PuzzleSoundManager soundManager;

	void Awake() {
		soundManager = GetComponentInChildren<PuzzleSoundManager>();
	}

	void Start () {
		playerData = Player.instance.playerData;
		ResetScore();
		board.onNoValidMoves.AddListener(EndOfGame);
		board.onPegDidJump.AddListener(PegDidJump);
		
		board.onInitialized.AddListener(InitializePegs);
		if (board.isInitialized) InitializePegs();
	}
	
	void Update () {
		
	}

	public void InitializePegs() {
		// Set all pegs' values to initial
		List<Peg> pegs = board.GetPegs();
		Debug.Log("Initializing " + pegs.Count + " pegs");
		foreach (Peg peg in pegs) {
			peg.value = PEG_INITIAL_VALUE;
		}
	}

	public void PegDidJump(Peg jumper) {
		AwardPoints(jumper.value);
		jumper.value += PEG_VALUE_INCREASE;
		soundManager.pegJump.Play();
	}

	public void EndOfGame() {
		List<Peg> pegs = board.GetPegs();
		if (pegs.Count == 1) {
			soundManager.puzzleSolved.Play();
			AwardPoints(pegs[0].value);
			AwardPoints(SOLVE_REWARD);
		} else {
			soundManager.puzzleComplete.Play();
			//Debug.Log("Puzzle finished with " + pegs.Count + " pegs remaining");
		}
		Debug.Log("Puzzle finished with " + playerData.score + " points");
		Invoke("ReturnToInventory", 1);
		//ReturnToInventory();
	}

	public void ReturnToInventory() {
		SceneManager.LoadScene(INVENTORY_SCENE);
	}

	public void ResetScore() {
		playerData.score = 0;
	}

	public void AwardPoints(int points) {
		playerData.score += points;
	}

}
