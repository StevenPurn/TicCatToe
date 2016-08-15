using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

    Player curPlayer;

	// Use this for initialization
	void Start () {
        //Set current player to player one (cheese)
        curPlayer = Player.playerOne;
	}

    public void EndTurn()
    {
        //call win condition checker

        //Subtract health from all active glass tiles
        //FindObjectsOfType<GlassTileHealth>().AdjustHealth(-1);
        //Instantiate random glass tile if less than two additional glass tiles
        ChangePlayer();
    }

    void ChangePlayer()
    {
        //Switch from current player to next player's turn
        if(curPlayer == Player.playerOne)
        {
            curPlayer = Player.playerTwo;
        }else
        {
            curPlayer = Player.playerOne;
        }
    }
}
