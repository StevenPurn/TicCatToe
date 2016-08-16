using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{

    public delegate void TurnEndDelegate();
    public TurnEndDelegate turnEndEvent;

    //Should be static
    public Player curPlayer;

    // Use this for initialization
    void Start()
    {
        //Set current player to player one (cheese)
        curPlayer = Player.playerOne;
    }

    public void EndTurn()
    {
        //call win condition checker
        //Call Event that starts:
        //Subtract health from all active glass tiles
        //FindObjectsOfType<GlassTileHealth>().AdjustHealth(-1);
        //Instantiate random glass tile if less than two additional glass tiles
        turnEndEvent();
        ChangePlayer();
    }

    void ChangePlayer()
    {
        // Switch from current player to next player's turn
        curPlayer = curPlayer == Player.playerOne ? Player.playerTwo : Player.playerOne;
    }
}
