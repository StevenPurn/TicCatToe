using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {

    public void Start()
    {
        FindObjectOfType<WinChecking>().SendWinEvent += UpdateScore;
    }

    void UpdateScore(HashSet<Tile> winningTile)
    {

    }

    public Dictionary<Player, int> ScoreByPlayer = new Dictionary<Player, int>()
    {
        { Player.playerOne, 0 },
        { Player.playerTwo, 0 }
    };

    public void ScoreChange(Player player, int score)
    {
        ScoreByPlayer[player] += score;
    }

    public int GetScore(Player player)
    {
        return ScoreByPlayer[player];
    }

}
