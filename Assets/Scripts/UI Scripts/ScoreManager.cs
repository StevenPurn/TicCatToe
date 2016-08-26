﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {

    Text playerOneText;
    Text playerTwoText;

    public void Start()
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