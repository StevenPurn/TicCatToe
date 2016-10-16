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
    public static GameObject winPanel, winBackdrop;
    static public bool gameOver;
    public static int winningScore = 15;

    public static Sprite cheeseWinImage;
    public static Sprite catWinImage;

    public void Start()
    {
        cheeseWinImage = Resources.Load<Sprite>("Sprites/DogsWin");
        catWinImage = Resources.Load<Sprite>("Sprites/CatsWin");
        scoreBoardText = GameObject.Find("Scoreboard").GetComponent<Text>();
        winPanel = GameObject.Find("WinPanel");
        winBackdrop = GameObject.Find("WinImage");
        winPanel.SetActive(false);
        winningScore = GameObject.Find("MenuController").GetComponent<ScoreTracker>().winningScore;
        scoreBoardText.text = "To win: " + winningScore.ToString();
        SubscribeToThings();
        gameOver = false;
        ScoreByPlayer[Player.playerOne] = 0;
        ScoreByPlayer[Player.playerTwo] = 0;
        UpdateScoreText(Player.playerOne);
        UpdateScoreText(Player.playerTwo);
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
        GameObject.Find("SFXController").GetComponent<SFXScript>().PlaySFX(SFXScript.AudioClipEnum.score);

        if (ScoreByPlayer[player] >= winningScore)
        {
            gameOver = true;
            StaticCoroutine.DoCoroutine(WaitForWin());
            winBackdrop.GetComponent<Image>().sprite = player == Player.playerOne
                ? cheeseWinImage
                : catWinImage;
            winPanel.GetComponentInChildren<Text>().text = player == Player.playerOne
                ? "Dogs win!"
                : "Cats win!";
            GameObject fireworks = player == Player.playerOne
                ? (GameObject)Resources.Load("Prefabs/Fireworks_Spawn_Dogs_GO")
                : (GameObject)Resources.Load("Prefabs/Fireworks_Spawn_Cats_GO");
            Instantiate(fireworks);
            if (GlobalData.AiPlayer == player)
            {
                GameObject.Find("SFXController").GetComponent<SFXScript>().PlaySFX(SFXScript.AudioClipEnum.lose);
            }else
            {
                GameObject.Find("SFXController").GetComponent<SFXScript>().PlaySFX(SFXScript.AudioClipEnum.win);
            }
        }

    }

    static IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(1.5f);
        winPanel.gameObject.SetActive(true);
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
