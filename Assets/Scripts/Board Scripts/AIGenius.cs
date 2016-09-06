using UnityEngine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class AIGenius
{
    //Glass tile health
    //What is the score?
    //What will the score be in each scenario?
    //What do I want for dinner?
    //What's the best Pixar movie?
    private static int maxDepth = 2;
    private static Dictionary<int, TileLocation> randomSpawnPoints;

    public static TileLocation GetMove(Tile[,] boardTiles, Player curPlayer)
    {
        randomSpawnPoints = GameObject.Find("BoardManager").GetComponent<BoardLocationDictionary>().RandomSpawnPoints;

        GameState gameState = new GameState
        {
            boardTiles = boardTiles,
            curPlayer = curPlayer,
            scoreByPlayer = ScoreManager.ScoreByPlayer,
        };

        return GetScoredMove(gameState).location.Value;
    }

    private static IEnumerable<GameState> GetGameStatesForMove(GameState gameState, TileLocation tileLocation)
    {
        int curRandTiles = 0;
        foreach (var spawnLoc in randomSpawnPoints)
        {
            if (gameState.boardTiles[spawnLoc.Value.x, spawnLoc.Value.y].typeOfTile == TileType.glassTile)
            {
                curRandTiles += 1;
            }
        }

        if (curRandTiles >= SpawnRandomTiles.maxRandomTiles)
        {
            return new List<GameState> { GetGameStateForMoveSpawn(gameState, tileLocation) };
        }

        List<GameState> gameStates = new List<GameState>();

        foreach (var spawnLoc in randomSpawnPoints)
        {
            if (gameState.boardTiles[spawnLoc.Value.x, spawnLoc.Value.y].typeOfTile == TileType.emptyTile)
            {
                gameStates.Add(GetGameStateForMoveSpawn(gameState, tileLocation, spawnLoc.Value));
            }
        }
        return gameStates;
    }

    private static GameState GetGameStateForMoveSpawn(GameState gameState, TileLocation move, TileLocation? spawn = null)
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

        foreach (Tile tile in winningTiles)
        {
            newGameState.boardTiles[tile.locationOfTile.x, tile.locationOfTile.y].valueOfTile = TileValue.empty;
            newGameState.boardTiles[tile.locationOfTile.x, tile.locationOfTile.y].tileOccupied = false;
        }

        return newGameState;
    }

    private static ScoredMove GetScoredMove(GameState gameState, int depth = 0)
    {
        Player humanPlayer = GlobalData.AiPlayer == Player.playerOne ? Player.playerTwo : Player.playerOne;
        Player aiPlayer = (Player)GlobalData.AiPlayer;
        int bestValue = int.MinValue;
        TileLocation? bestLocation = null;

        if (gameState.scoreByPlayer[humanPlayer] >= ScoreManager.winningScore)
        {
            Debug.Log("Human win");
            bestValue = int.MaxValue;
        }
        else if (gameState.scoreByPlayer[aiPlayer] >= ScoreManager.winningScore)
        {
            Debug.Log("AI win");
            bestValue = int.MinValue;
        }
        else if (depth >= maxDepth)
        {
            Debug.Log(gameState.scoreByPlayer[humanPlayer] - gameState.scoreByPlayer[aiPlayer]);
            bestValue = gameState.scoreByPlayer[humanPlayer] - gameState.scoreByPlayer[aiPlayer];
        }
        else
        {
            foreach (var tileLocation in GetPossibleMoves(gameState.boardTiles))
            {
                int? boardValue = null;
                IEnumerable<GameState> possibleGameStates = GetGameStatesForMove(gameState, tileLocation);
                foreach (var possibleGameState in possibleGameStates)
                {
                    var moveScore = GetScoredMove(possibleGameState, depth + 1).value;
                    if (!boardValue.HasValue)
                    {
                        boardValue = moveScore;
                    }
                    else
                    {
                        boardValue = gameState.curPlayer == GlobalData.AiPlayer
                            ? Mathf.Max(boardValue.Value, moveScore)
                            : Mathf.Min(boardValue.Value, moveScore);
                    }
                }
                bestValue = gameState.curPlayer == GlobalData.AiPlayer
                    ? Mathf.Min(bestValue, boardValue.Value)
                    : Mathf.Max(bestValue, boardValue.Value);
                bestLocation = tileLocation;
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