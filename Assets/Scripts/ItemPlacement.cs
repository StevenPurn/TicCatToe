using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemPlacement : MonoBehaviour {
    Tile[,] boardTiles;
    GameObject turnManager;

    void Start()
    {
        turnManager = GameObject.Find("TurnManager");
    }

    public void PlaceItemIfAvailable(TileLocation tilePosition, Vector2 spawnPosition)
    {
        boardTiles = GameObject.Find("BoardManager").GetComponent<BoardManager>().BoardTiles;

        Tile tile = boardTiles[tilePosition.x, tilePosition.y];
        if (tile.tileOccupied == false)
        {
            PlaceItem(tilePosition, spawnPosition);
        }
    }

    void PlaceItem(TileLocation tilePosition, Vector2 spawnPosition)
    {
        TileValue tValue;

        if (turnManager.GetComponent<TurnManager>().curPlayer == Player.playerOne)
        {
            Instantiate(Resources.Load("Prefabs/Cheese"), new Vector3(spawnPosition.x, spawnPosition.y), Quaternion.identity);
            tValue = TileValue.cheese;
        }
        else
        {
            Instantiate(Resources.Load("Prefabs/Cat"), new Vector3(spawnPosition.x, spawnPosition.y), Quaternion.identity);
            tValue = TileValue.cat;
        }

        //Set value of Tile item in this slot to occupied & cat or cheese
        Tile tile = boardTiles[tilePosition.x, tilePosition.y];
        tile.tileOccupied = true;
        tile.valueOfTile = tValue;

        turnManager.GetComponent<TurnManager>().EndTurn();
    }
}
