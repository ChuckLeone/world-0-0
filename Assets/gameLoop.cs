using UnityEngine;
using System.Collections;

public class gameLoop : MonoBehaviour {

	[SerializeField] private GameObject objToActivate;
	[SerializeField] private GameObject startBox;
	[SerializeField] private GameObject startMusic;
	[SerializeField] private GameObject gameLevel;

	public int playerScore = 0;
	public int cpuScore = 0;

	// Use this for initialization
	void Start () {
		startBox.SetActive (true);
		gameLevel.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if(playerScore == 2) {
			//player wins
			//gameState.playerWins;
			startBox.SetActive(false);
		}
		if (cpuScore == 2){
			//player loses
			//gameState.gameOver;
			startBox.SetActive(false);
		}
	}

	public int updatePlayerScore () {
		playerScore += 1;
		return playerScore;
	}

	public int updateCpuScore () {
		cpuScore += 1;
		return cpuScore;
	}

	void startGame () {
		startBox.SetActive (false);
		gameLevel.SetActive (true);
		startMusic.SetActive (true);
		Invoke("dropBall", 4);

	}

	// drops a ball to start game
	void dropBall () {
		objToActivate.SetActive (true);
	}
}
