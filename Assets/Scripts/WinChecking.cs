using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class WinChecking : MonoBehaviour
{
    public delegate void WinCheckDelegate(HashSet<Tile> winningTilesHomie);
    public WinCheckDelegate SendWinEvent;

    public Tile[,] BoardTiles;
    private TileLocation[] startingLocationHor = {
        new TileLocation(0, 0),
        new TileLocation(1, 0),
        new TileLocation(2, 0),
        new TileLocation(3, 0),
        new TileLocation(4, 0),
    };
    private TileLocation[] startingLocationVer = {
        new TileLocation(0, 0),
        new TileLocation(0, 1),
        new TileLocation(0, 2),
        new TileLocation(0, 3),
        new TileLocation(0, 4),
    };
    private TileLocation[] startingLocationDiagUp = {
        new TileLocation(2, 0),
        new TileLocation(3, 0),
        new TileLocation(4, 0),
        new TileLocation(4, 1),
        new TileLocation(4, 2),
    };
    private TileLocation[] startingLocationDiagDown = {
        new TileLocation(0, 2),
        new TileLocation(0, 1),
        new TileLocation(0, 0),
        new TileLocation(1, 0),
        new TileLocation(2, 0),
    };

    void Start()
    {
        var boardManager = FindObjectOfType<BoardManager>();
        boardManager.WinCheckEvent += SendWins;
        BoardTiles = boardManager.BoardTiles;
    }

    public void SendWins()
    {
        var wins = CheckWins();
        if (wins.Count > 0)
        {
            SendWinEvent(wins);
        }
    }

    HashSet<Tile> CheckWins()
    {
        HashSet<Tile> winTiles = new HashSet<Tile>();

        winTiles.UnionWith(CheckWinsInDirection(startingLocationDiagDown, new Vector2(1, 1)));
        winTiles.UnionWith(CheckWinsInDirection(startingLocationDiagUp, new Vector2(-1, 1)));
        winTiles.UnionWith(CheckWinsInDirection(startingLocationVer, new Vector2(1, 0)));
        winTiles.UnionWith(CheckWinsInDirection(startingLocationHor, new Vector2(0, 1)));

        return winTiles;
    }

    HashSet<Tile> CheckWinsInDirection(TileLocation[] startLocations, Vector2 moveDirection)
    {
        var successes = new HashSet<Tile>();
        var results = new HashSet<TileLocation>();
        foreach (TileLocation tileLocation in startLocations)
        {
            TileLocation curTile = tileLocation;
            TileValue? tileValue = null;
            while (IsInBoard(curTile))
            {
                if (BoardTiles[curTile.x, curTile.y].valueOfTile == tileValue && tileValue != TileValue.empty)
                {
                    results.Add(curTile);
                }
                else
                {
                    if (results.Count <= 2)
                    {
                        results.Clear();
                        results.Add(curTile);
                        tileValue = BoardTiles[curTile.x, curTile.y].valueOfTile;
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
                successes.UnionWith(from tileLoc in results select BoardTiles[tileLoc.x, tileLoc.y]);
            }
        }

        return successes;
    }

    private bool IsInBoard(TileLocation tileLocation)
    {
        return tileLocation.x >= 0 && tileLocation.x <= 4 &&
            tileLocation.y >= 0 && tileLocation.y <= 4;
    }
}
