using UnityEngine;
using System.Collections;

public class ClearItemFromTile : MonoBehaviour {

    Tile[,] boardTiles;

    public delegate void RemoveItemFromTileWinDelegate(TileLocation tileLocation);
    public RemoveItemFromTileWinDelegate removeItemWinEvent;

    public delegate void RemoveItemFromTileLoseDelegate(TileLocation tileLocation);
    public RemoveItemFromTileLoseDelegate removeItemLoseEvent;

    public void RemoveItemFromTileWin(TileLocation tilePosition)
    {
        boardTiles = GameObject.Find("BoardManager").GetComponent<BoardManager>().BoardTiles;

        Tile tile = boardTiles[tilePosition.x, tilePosition.y];
        tile.tileOccupied = false;
        tile.valueOfTile = TileValue.empty;
        removeItemWinEvent(tilePosition);
    }

    public void RemoveItemFromTileLose(TileLocation tilePosition)
    {
        boardTiles = GameObject.Find("BoardManager").GetComponent<BoardManager>().BoardTiles;

        Tile tile = boardTiles[tilePosition.x, tilePosition.y];
        tile.tileOccupied = false;
        tile.valueOfTile = TileValue.empty;
        removeItemLoseEvent(tilePosition);
    }
}
