using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager : MonoBehaviour
{
    public delegate void WinCheckDelegate();
    public WinCheckDelegate WinCheckEvent;

    public delegate void TurnEndDelegate();
    public TurnEndDelegate turnEndEvent;

    public delegate void StartTurnDelegate();
    public StartTurnDelegate startTurnEvent;

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
        WinCheckEvent();
        turnEndEvent();
        ChangePlayer();
        StartTurn();
    }

    public void StartTurn()
    {
        startTurnEvent();
    }

    void ChangePlayer()
    {
        // Switch from current player to next player's turn
        curPlayer = curPlayer == Player.playerOne ? Player.playerTwo : Player.playerOne;
    }
}
