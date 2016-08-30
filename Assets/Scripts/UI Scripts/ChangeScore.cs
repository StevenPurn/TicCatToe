using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeScore : MonoBehaviour {

	public Text winningScoreText;
	public int winningScore;

	private GameObject menuController;

	// Use this for initialization
	void Start () {
		winningScore = 16;
		menuController = GameObject.Find ("MenuController");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AdjustScore(int scoreChange){
		winningScore += scoreChange;

		if (winningScore <= 0) {
			winningScore = 1;
		}

		UpdateScoreText ();
		UpdateMenuController ();
	}

	public void UpdateMenuController(){
		menuController.GetComponent<ScoreTracker> ().SetScore (winningScore);
	}

	public void UpdateScoreText(){
		winningScoreText.text = winningScore.ToString ();
	}
}
