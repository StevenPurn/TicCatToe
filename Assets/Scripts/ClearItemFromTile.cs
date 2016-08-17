using UnityEngine;
using System.Collections;

public class ClearItemFromTile : MonoBehaviour {

    Tile[,] boardTiles;

    public delegate void RemoveItemFromTileDelegate(TileLocation tileLocation);
    public RemoveItemFromTileDelegate removeTileEvent;

    public void RemoveItemFromTile(TileLocation tilePosition)
    {
        boardTiles = GameObject.Find("BoardManager").GetComponent<BoardManager>().BoardTiles;

        Tile tile = boardTiles[tilePosition.x, tilePosition.y];
        if (tile.tileOccupied == true)
        {
            tile.tileOccupied = false;
            tile.valueOfTile = TileValue.empty;
        }
        removeTileEvent(tilePosition);
    }

}
