using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScoreUI : MonoBehaviour {

    public Player player;
    private Text scoreText;
    private int curScore;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        curScore = FindObjectOfType<ScoreManager>().GetScore(player);
	}

    void UpdateText()
    {
        scoreText.text = curScore.ToString();
    }
}
