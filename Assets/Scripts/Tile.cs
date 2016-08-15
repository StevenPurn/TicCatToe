using UnityEngine;
using System.Collections;

public class Tile {

    public TileType typeOftile;
    public TileValue valueOfTile;
    public Vector2 locationOfTile;
    public SortingLayer sortLayer;
    public Vector2 worldSpaceLocation;

    public Tile(Vector2 tileLocation, TileType tileType, TileValue tileValue)
    {
        this.typeOftile = tileType;
        this.valueOfTile = tileValue;
        this.locationOfTile = tileLocation;
    }

}
