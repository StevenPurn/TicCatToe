using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{

    static Text playerOneText;
    static Text playerTwoText;
    public static Text scoreBoardText;
    public static GameObject winPanel;
    static public bool gameOver;
    public static int winningScore = 15;

    public static Sprite cheeseWinImage;
    public static Sprite catWinImage;

    public void Start()
    {
        scoreBoardText = GameObject.Find("Scoreboard").GetComponent<Text>();
        winPanel = GameObject.Find("WinPanel");
        winPanel.SetActive(false);
        winningScore = GameObject.Find("MenuController").GetComponent<ScoreTracker>().winningScore;
        scoreBoardText.text = "To win: " + winningScore.ToString();
        SubscribeToThings();
        gameOver = false;
    }

    public void SubscribeToThings()
    {
        playerOneText = GameObject.Find("PlayerOneScoreText").GetComponent<Text>();
        playerTwoText = GameObject.Find("PlayerTwoScoreText").GetComponent<Text>();
        UpdateScoreText(Player.playerOne);
        UpdateScoreText(Player.playerTwo);
    }

    public static void UpdateScore(HashSet<Tile> winningTiles)
    {
        Player? curPlayer = null;

        foreach (Tile tile in winningTiles)
        {
            curPlayer = tile.valueOfTile == TileValue.cat
                ? Player.playerTwo
                : Player.playerOne;
            break;
        }

        if (winningTiles.Count > 0)
        {
            ScoreChange((Player)curPlayer, winningTiles.Count);
        }
    }

    public static Dictionary<Player, int> ScoreByPlayer = new Dictionary<Player, int>()
    {
        { Player.playerOne, 0 },
        { Player.playerTwo, 0 }
    };

    public static void ScoreChange(Player player, int score)
    {
        ScoreByPlayer[player] += score;
        UpdateScoreText(player);

        if (ScoreByPlayer[player] >= winningScore)
        {
            gameOver = true;
            winPanel.gameObject.SetActive(true);
            winPanel.GetComponentInChildren<Image>().sprite = player == Player.playerOne
                ? cheeseWinImage
                : catWinImage;
            winPanel.GetComponentInChildren<Text>().text = player == Player.playerOne
                ? "Cheese wins!"
                : "Cats win!";
        }

    }

    public void SetWinningScore(int newWinningScore)
    {
        winningScore = newWinningScore;
    }

    public int GetScore(Player player)
    {
        return ScoreByPlayer[player];
    }

    static void UpdateScoreText(Player player)
    {
        if (player == Player.playerOne)
        {
            playerOneText.text = ScoreByPlayer[player].ToString();
        }
        else
        {
            playerTwoText.text = ScoreByPlayer[player].ToString();
        }
    }

}
