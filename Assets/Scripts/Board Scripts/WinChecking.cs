using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class WinChecking : MonoBehaviour
{
    public delegate void WinCheckDelegate(HashSet<Tile> winningTilesHomie);
    public WinCheckDelegate SendWinEvent;

    private static TileLocation[] startingLocationHor = {
        new TileLocation(0, 0),
        new TileLocation(1, 0),
        new TileLocation(2, 0),
        new TileLocation(3, 0),
        new TileLocation(4, 0),
    };
    private static TileLocation[] startingLocationVer = {
        new TileLocation(0, 0),
        new TileLocation(0, 1),
        new TileLocation(0, 2),
        new TileLocation(0, 3),
        new TileLocation(0, 4),
    };
    private static TileLocation[] startingLocationDiagUp = {
        new TileLocation(2, 0),
        new TileLocation(3, 0),
        new TileLocation(4, 0),
        new TileLocation(4, 1),
        new TileLocation(4, 2),
    };
    private static TileLocation[] startingLocationDiagDown = {
        new TileLocation(0, 2),
        new TileLocation(0, 1),
        new TileLocation(0, 0),
        new TileLocation(1, 0),
        new TileLocation(2, 0),
    };

    public static HashSet<Tile> CheckWins(Tile[,] boardTiles)
    {
        HashSet<Tile> winTiles = new HashSet<Tile>();

        winTiles.UnionWith(CheckWinsInDirection(boardTiles, startingLocationDiagDown, new Vector2(1, 1)));
        winTiles.UnionWith(CheckWinsInDirection(boardTiles, startingLocationDiagUp, new Vector2(-1, 1)));
        winTiles.UnionWith(CheckWinsInDirection(boardTiles, startingLocationVer, new Vector2(1, 0)));
        winTiles.UnionWith(CheckWinsInDirection(boardTiles, startingLocationHor, new Vector2(0, 1)));

        return winTiles;
    }

    static HashSet<Tile> CheckWinsInDirection(Tile[,] boardTiles, TileLocation[] startLocations, Vector2 moveDirection)
    {
        var successes = new HashSet<Tile>();
        var results = new HashSet<TileLocation>();
        foreach (TileLocation tileLocation in startLocations)
        {
            TileLocation curTile = tileLocation;
            TileValue? tileValue = null;
            while (IsInBoard(curTile))
            {
                if (boardTiles[curTile.x, curTile.y].valueOfTile == tileValue && tileValue != TileValue.empty)
                {
                    results.Add(curTile);
                }
                else
                {
                    if (results.Count <= 2)
                    {
                        results.Clear();
                        results.Add(curTile);
                        tileValue = boardTiles[curTile.x, curTile.y].valueOfTile;
                    }
                    else
                    {
                        break;
                    }
                }
                curTile = new TileLocation(curTile.x + (int)moveDirection.x, curTile.y + (int)moveDirection.y);
            }

            if (results.Count >= 3)
            {
                successes.UnionWith(from tileLoc in results select boardTiles[tileLoc.x, tileLoc.y]);
            }
        }

        return successes;
    }

    private static bool IsInBoard(TileLocation tileLocation)
    {
        return tileLocation.x >= 0 && tileLocation.x <= 4 &&
            tileLocation.y >= 0 && tileLocation.y <= 4;
    }
}
