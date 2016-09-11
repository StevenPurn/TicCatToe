using UnityEngine;
using System.Collections;

public class ScoreTracker : MonoBehaviour {

	public int winningScore = 16;

	public void SetScore(int scoreToWin){
		winningScore = scoreToWin;
	}
}
