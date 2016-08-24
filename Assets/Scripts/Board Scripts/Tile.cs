using UnityEngine;
using System.Collections;

public class Tile {
    public TileType typeOfTile;
    public TileValue valueOfTile;
    public TileLocation locationOfTile;
    public SortingLayer sortLayer;
    public Vector2 worldSpaceLocation;
    public bool tileOccupied = false;

    public Tile(TileLocation tileLocation, TileType tileType, TileValue tileValue)
    {
        this.typeOfTile = tileType;
        this.valueOfTile = tileValue;
        this.locationOfTile = tileLocation;
    }

    public void SetTileStatus(TileValue tileValue, bool occupied)
    {
        this.valueOfTile = tileValue;
        this.tileOccupied = occupied;
    }
}
