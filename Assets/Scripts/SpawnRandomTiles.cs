using UnityEngine;
using System.Collections;

public class SpawnRandomTiles : MonoBehaviour {
    //Check for less than 2 randomly spawned tiles
    //Generate random location, check if occupied
    //if so, pick next location & spawn
    //Add random tile to boardList
    //Make sure random tile is removed from boardlist
    //and tile is reset in the list when deleted
    Tile[,] boardTiles;
    private int numRandomTiles;

    public delegate void AddTileDelegate(TileLocation tileLocation, TileType tileType, TileValue tileValue);
    public AddTileDelegate AddTileEvent;
    public delegate void RemoveTileDelegate(TileLocation tileLocation);
    public RemoveTileDelegate RemoveTileEvent;

    void Start()
    {
        numRandomTiles = 0;
        FindObjectOfType<TurnManager>().startTurnEvent += CheckForRandomTiles;
        FindObjectOfType<ClearItemFromTile>().removeTileEvent += ReduceRandomTileCount;
    }

    public void ReduceRandomTileCount(TileLocation tileLoc)
    {
        if (GetComponent<BoardLocationDictionary>().RandomSpawnPoints.ContainsValue(tileLoc))
        { 
            numRandomTiles -= 1;
        }
    }

    public void CheckForRandomTiles()
    {
        if(numRandomTiles < 2)
        {
            SpawnRandomTile(Random.Range(1,9));
        }
    }

    private void SpawnRandomTile(int randomKey)
    {
        
        boardTiles = GameObject.Find("BoardManager").GetComponent<BoardManager>().BoardTiles;
        TileLocation tileLoc = GetComponent<BoardLocationDictionary>().RandomSpawnPoints[randomKey];

        if (boardTiles[tileLoc.x, tileLoc.y].tileOccupied == true)
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
        RemoveTileEvent(tileLoc);
        AddTileEvent(tileLoc, TileType.glassTile, TileValue.empty);
        numRandomTiles += 1;
    }

}
