using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemPlacement : MonoBehaviour {
    Tile[,] boardTiles;
    GameObject turnManager;

    private GameObject catPrefab, cheesePrefab;

    void Start()
    {
        turnManager = GameObject.Find("TurnManager");
        catPrefab = (GameObject)Resources.Load("Prefabs/Cat");
        cheesePrefab = (GameObject)Resources.Load("Prefabs/Cheese");
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
            GameObject instantiatedItem = (GameObject)Instantiate(cheesePrefab, spawnPosition, Quaternion.identity);
            instantiatedItem.GetComponent<TileBehaviour>().TileLocation = tilePosition;
            instantiatedItem.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<BoardLocationDictionary>().SortLayer[tilePosition];
            tValue = TileValue.cheese;
        }
        else
        {
            GameObject instantiatedItem = (GameObject)Instantiate(catPrefab, spawnPosition, Quaternion.identity);
            instantiatedItem.GetComponent<TileBehaviour>().TileLocation = tilePosition;
            instantiatedItem.GetComponent<SpriteRenderer>().sortingLayerName = GetComponent<BoardLocationDictionary>().SortLayer[tilePosition];
            tValue = TileValue.cat;
        }

        //Set value of Tile item in this slot to occupied & cat or cheese
        Tile tile = boardTiles[tilePosition.x, tilePosition.y];
        tile.tileOccupied = true;
        tile.valueOfTile = tValue;

        turnManager.GetComponent<TurnManager>().EndTurn();
    }
}
