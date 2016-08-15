using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    public List<Tile> BoardTiles = new List<Tile>() { };
    private int boardSize = 4;

    // Use this for initialization
    void Start () {
        for (int i = 0; i <= boardSize; i++)
        {
            for (int j = 0; j <= boardSize; j++)
            {
                AddTile(new Vector2(i, j),TileType.emptyTile,TileValue.empty);
            }
        }
	}

    void AddTile(Vector2 tileLocation, TileType tileType, TileValue tileValue)
    {
        BoardTiles.Add(new Tile(tileLocation, tileType, tileValue));
        //Instantiate Tiles
    }

    // Update is called once per frame
    void Update()
    {
    }
}
