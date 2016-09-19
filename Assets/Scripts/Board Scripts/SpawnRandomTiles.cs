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
    public static int maxRandomTiles = 3;

    private bool firstRandSpawn = true;

    void Start()
    {
        FindObjectOfType<BoardManager>().startTurnEvent += CheckForRandomTiles;
        firstRandSpawn = true;
    }

    public void ReduceRandomTileCount(TileLocation tileLoc)
    {
        curRandomTiles.Remove(tileLoc);
    }

    public void CheckForRandomTiles()
    {
        if (curRandomTiles.Count < maxRandomTiles )
        {
            SpawnRandomTile(Random.Range(1,9));
            if (firstRandSpawn)
            {
                SpawnRandomTile(Random.Range(1, 9));
                firstRandSpawn = false;
            }
        }
    }

    private void SpawnRandomTile(int randomKey)
    {
        boardTiles = GameObject.Find("BoardManager").GetComponent<BoardManager>().BoardTiles;
        TileLocation tileLoc = GetComponent<BoardLocationDictionary>().RandomSpawnPoints[randomKey];

		while (boardTiles[tileLoc.x, tileLoc.y].typeOfTile == TileType.glassTile)
        {
			randomKey += 1;
			if (randomKey > 8) {
				randomKey = 1;
			}
			tileLoc = GetComponent<BoardLocationDictionary>().RandomSpawnPoints[randomKey];
        }
			
        //Don't need to remove from the array because it will be overwritten?
        curRandomTiles.Add(tileLoc);
        RemoveTileEvent(tileLoc);
		AddTileEvent(tileLoc, TileType.glassTile, TileValue.empty);
    }

}
