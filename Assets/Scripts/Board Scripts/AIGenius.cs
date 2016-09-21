using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Threading;
using System.Collections;

public static class AIGenius
{
    private static int maxDepth = 3;
    private static Dictionary<int, TileLocation> randomSpawnPoints;
    public static BoardManager boardManager;
    public static TileLocation? result;
    private static Thread activeThread;

    public static IEnumerator CalculateMove(Tile[,] boardTiles, Player curPlayer)
    {
        yield return new WaitForSeconds(0f);
        boardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
        randomSpawnPoints = GameObject.Find("BoardManager").GetComponent<BoardLocationDictionary>().RandomSpawnPoints;

        GameState gameState = new GameState
        {
            boardTiles = boardTiles,
            curPlayer = curPlayer,
            scoreByPlayer = ScoreManager.ScoreByPlayer,
        };

        activeThread = new Thread(() => {
            result = GetScoredMove(gameState).location.Value;
        });
        activeThread.Start();
        while (result == null)
        {
            yield return new WaitForSeconds(0.5f);
        }
        boardManager.PlaceItemIfAvailable(result.Value);
        result = null;
    }

    private static GameState GetGameState(GameState gameState, TileLocation move, TileLocation? spawn = null)
    {
        GameState newGameState;
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, gameState);
            ms.Position = 0;
            newGameState = (GameState)formatter.Deserialize(ms);
        }
        if (spawn != null)
        {
            newGameState.boardTiles[spawn.Value.x, spawn.Value.y].typeOfTile = TileType.glassTile;
        }
        newGameState.boardTiles[move.x, move.y].valueOfTile = newGameState.curPlayer == Player.playerOne ? TileValue.cheese : TileValue.cat;
        HashSet<Tile> winningTiles = WinChecking.CheckWins(newGameState.boardTiles);
        newGameState.scoreByPlayer[newGameState.curPlayer] += winningTiles.Count;

        foreach (Tile tile in newGameState.boardTiles)
        {
            if (tile.typeOfTile == TileType.glassTile)
            {
                tile.tileHealth += GlassTileHealth.HEALTH_DECREMENT;
                if (tile.tileHealth <= 0)
                {
                    tile.valueOfTile = TileValue.empty;
                    tile.tileOccupied = false;
                    
                    if (randomSpawnPoints.ContainsValue(tile.locationOfTile))
                    {
                        tile.typeOfTile = TileType.emptyTile;
                    }
                    else
                    {
                        tile.tileHealth = GlassTileHealth.MAX_HEALTH;
                    }
                }
            }
        }

        foreach (Tile tile in winningTiles)
        {
            newGameState.boardTiles[tile.locationOfTile.x, tile.locationOfTile.y].valueOfTile = TileValue.empty;
            newGameState.boardTiles[tile.locationOfTile.x, tile.locationOfTile.y].tileOccupied = false;
        }

        newGameState.curPlayer = newGameState.curPlayer == Player.playerOne ? Player.playerTwo : Player.playerOne;

        return newGameState;
    }

    private static ScoredMove GetScoredMove(GameState gameState, int depth = 0)
    {
        Player humanPlayer = GlobalData.AiPlayer == Player.playerOne ? Player.playerTwo : Player.playerOne;
        Player aiPlayer = (Player)GlobalData.AiPlayer;
        int bestValue = gameState.curPlayer == GlobalData.AiPlayer ? int.MaxValue : int.MinValue;
        TileLocation? bestLocation = null;

        if (gameState.scoreByPlayer[humanPlayer] >= ScoreManager.winningScore)
        {
            bestValue = int.MaxValue - depth;
        }
        else if (gameState.scoreByPlayer[aiPlayer] >= ScoreManager.winningScore)
        {
            bestValue = int.MinValue + depth;
        }
        else if (depth >= maxDepth)
        {
            bestValue = GetScoreForGameState(gameState);
        }
        else
        {
            foreach (var tileLocation in GetPossibleMoves(gameState.boardTiles))
            {
                int? boardValue = null;
                if (!bestLocation.HasValue)
                {
                    bestLocation = tileLocation;
                }
                GameState nextGameState = GetGameState(gameState, tileLocation);
                boardValue = GetScoredMove(nextGameState, depth + 1).value;
 
                if(gameState.curPlayer == GlobalData.AiPlayer)
                {
                    if (bestValue > boardValue.Value)
                    {
                        bestValue = boardValue.Value;
                        bestLocation = tileLocation;
                    }
                }else
                {
                    if (bestValue < boardValue.Value)
                    {
                        bestValue = boardValue.Value;
                        bestLocation = tileLocation;
                    }
                }
            }
        }

        return new ScoredMove
        {
            value = bestValue,
            location = bestLocation,
        };
    }

    private static IEnumerable<TileLocation> GetPossibleMoves(Tile[,] boardTiles)
    {
        List<TileLocation> possibleMoves = new List<TileLocation>();

        foreach (var tile in boardTiles)
        {
            if (tile.valueOfTile == TileValue.empty && TileType.emptyTile != tile.typeOfTile)
            {
                possibleMoves.Add(tile.locationOfTile);
            }
        }
        return possibleMoves;
    }

    private static int GetScoreForGameState(GameState gameState)
    {
        int boardScore = 0;
        Player humanPlayer = GlobalData.AiPlayer == Player.playerOne ? Player.playerTwo : Player.playerOne;
        TileValue aiTileValue = GlobalData.AiPlayer == Player.playerOne ? TileValue.cheese : TileValue.cat;
        boardScore += (gameState.scoreByPlayer[GlobalData.AiPlayer.Value] - gameState.scoreByPlayer[humanPlayer]) * -10000;
        foreach (var tile in gameState.boardTiles)
        {
            if (tile.valueOfTile != TileValue.empty)
            {
                if (tile.typeOfTile == TileType.glassTile)
                {
                    boardScore += (int)((tile.valueOfTile == aiTileValue ? -10  : 10) * tile.tileHealth/GlassTileHealth.MAX_HEALTH);
                }
                else if (tile.typeOfTile == TileType.standardTile)
                {
                    boardScore += tile.valueOfTile == aiTileValue ? -30 : 30;
                }
            }
        }
        return boardScore;
    }
}

[Serializable]
class GameState
{
    [SerializeField]
    public Tile[,] boardTiles;
    [SerializeField]
    public Player curPlayer;
    [SerializeField]
    public Dictionary<Player, int> scoreByPlayer;

    public GameState()
    {
    }
}

class ScoredMove
{
    public int value;
    public TileLocation? location;

    public ScoredMove()
    { }
}