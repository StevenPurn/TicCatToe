using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour {

    public Dictionary<Player, int> ScoreByPlayer = new Dictionary<Player, int>()
    {
        { Player.playerOne, 0 },
        { Player.playerTwo, 0 }
    };

    public void ScoreChange(Player player, int score)
    {
        ScoreByPlayer[player] += score;
        //Event
    }

    public int GetScore(Player player)
    {
        return ScoreByPlayer[player];
    }

}
