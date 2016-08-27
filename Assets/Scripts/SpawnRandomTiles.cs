using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class SpawnRandomTiles : MonoBehaviour {

    Tile[,] boardTiles;
    private HashSet<TileLocation> curRandomTiles= new HashSet<TileLocation>();

    public delegate void AddTileDelegate(TileLocation tileLocation, TileType tileType, TileValue tileValue);
    public AddTileDelegate AddTileEvent;
    public delegate void RemoveTileDelegate(TileLocation tileLocation);
    public RemoveTileDelegate RemoveTileEvent;

    void Start()
    {
        FindObjectOfType<BoardManager>().startTurnEvent += CheckForRandomTiles;
    }

    public void ReduceRandomTileCount(TileLocation tileLoc)
    {
        curRandomTiles.Remove(tileLoc);
    }

    public void CheckForRandomTiles()
    {
        if (curRandomTiles.Count < 4 )
        {
            SpawnRandomTile(Random.Range(1,9));
        }
    }

    private void SpawnRandomTile(int randomKey)
    {
        boardTiles = GameObject.Find("BoardManager").GetComponent<BoardManager>().BoardTiles;
        TileLocation tileLoc = GetComponent<BoardLocationDictionary>().RandomSpawnPoints[randomKey];

        if (boardTiles[tileLoc.x, tileLoc.y].typeOfTile != TileType.emptyTile)
        {
            var prevRand = randomKey;
            do
            {
                randomKey = Random.Range(1, 9);
            }
            while (randomKey == prevRand);

            tileLoc = GetComponent<BoardLocationDictionary>().RandomSpawnPoints[randomKey];
        }
        //Don't need to remove from the array because it will be overwritten?
        curRandomTiles.Add(tileLoc);
        RemoveTileEvent(tileLoc);
        AddTileEvent(tileLoc, TileType.glassTile, TileValue.empty);
    }

}
