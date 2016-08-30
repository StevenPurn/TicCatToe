using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

	public int winningScore = 16;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetScore(int scoreToWin){
		winningScore = scoreToWin;
	}
}
