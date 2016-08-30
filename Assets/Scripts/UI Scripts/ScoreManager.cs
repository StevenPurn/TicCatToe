using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    Text playerOneText;
    Text playerTwoText;
	public Text scoreBoardText;
    public GameObject winPanel;
    public bool gameOver;
	private int winningScore = 15;

	public void Start(){
		winningScore = GameObject.Find ("MenuController").GetComponent<ScoreTracker> ().winningScore;
		scoreBoardText.text = "To win: " + winningScore.ToString ();
		SubscribeToThings ();
	}

    public void SubscribeToThings()
    {
        FindObjectOfType<WinChecking>().SendWinEvent += UpdateScore;
        playerOneText = GameObject.Find("PlayerOneScoreText").GetComponent<Text>();
        playerTwoText = GameObject.Find("PlayerTwoScoreText").GetComponent<Text>();
        UpdateScoreText(Player.playerOne);
        UpdateScoreText(Player.playerTwo);
    }
		
    void UpdateScore(HashSet<Tile> winningTiles)
    {
        Player curPlayer = Player.playerOne;

        foreach (Tile tile in winningTiles)
        {
            if(tile.valueOfTile == TileValue.cat)
            {
                curPlayer = Player.playerTwo;
            }
            else
            {
                curPlayer = Player.playerOne;
            }
        }

        ScoreChange(curPlayer, winningTiles.Count);
    }

    public Dictionary<Player, int> ScoreByPlayer = new Dictionary<Player, int>()
    {
        { Player.playerOne, 0 },
        { Player.playerTwo, 0 }
    };

    public void ScoreChange(Player player, int score)
    {
        ScoreByPlayer[player] += score;
        UpdateScoreText(player);

        if(ScoreByPlayer[player] >= winningScore)
        {
            gameOver = true;
            winPanel.gameObject.SetActive(true);
            winPanel.GetComponentInChildren<Text>().text = player == Player.playerOne
                ? "Cheeses prevail!"
                : "CATS!";
            winPanel.GetComponentInChildren<ButtonSpriteSetter>().SetButtonSprite(player);
        }

    }

    public void SetWinningScore(int winningScore)
    {
        this.winningScore = winningScore;
    }

    public int GetScore(Player player)
    {
        return ScoreByPlayer[player];
    }

    void UpdateScoreText(Player player)
    {
        if(player == Player.playerOne)
        {
            playerOneText.text = ScoreByPlayer[player].ToString();
        }
        else
        {
            playerTwoText.text = ScoreByPlayer[player].ToString();
        }
    }

}
