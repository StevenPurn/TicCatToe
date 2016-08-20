using UnityEngine;
using System.Collections;

public class ClearItemFromTile : MonoBehaviour {

    Tile[,] boardTiles;

    public delegate void RemoveItemFromTileDelegate(TileLocation tileLocation);
    public RemoveItemFromTileDelegate removeItemEvent;

    public void RemoveItemFromTile(TileLocation tilePosition)
    {
        boardTiles = GameObject.Find("BoardManager").GetComponent<BoardManager>().BoardTiles;

        Tile tile = boardTiles[tilePosition.x, tilePosition.y];
        tile.tileOccupied = false;
        tile.valueOfTile = TileValue.empty;
        removeItemEvent(tilePosition);
    }

}
