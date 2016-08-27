using UnityEngine;
using System.Collections;

public class ListenForTileRemoval : MonoBehaviour {

    public delegate void ReplaceTileDelegate(TileLocation tileLocation, TileType tileType, TileValue tileValue);
    public ReplaceTileDelegate ReplaceTileEvent;

    public delegate void ReduceRandomSpawnTileCount();
    public ReduceRandomSpawnTileCount ReduceRandomSpawnTileCountEvent;

    void Start()
    {
        FindObjectOfType<SpawnRandomTiles>().RemoveTileEvent += RemoveTile;
    }

    public void RemoveTile(TileLocation tileLocation)
    {
        if (tileLocation == GetComponent<TileBehaviour>().TileLocation)
        {

            var boardManager = GameObject.Find("BoardManager");

            if (!boardManager.GetComponent<BoardLocationDictionary>().RandomSpawnPoints.ContainsValue(tileLocation))
            {
                ReplaceTileEvent(tileLocation, TileType.glassTile, TileValue.empty);
            }
            else
            {
                if (boardManager.GetComponent<BoardManager>().BoardTiles[tileLocation.x, tileLocation.y].typeOfTile == TileType.glassTile)
                {
                    GameObject.Find("BoardManager").GetComponent<SpawnRandomTiles>().ReduceRandomTileCount(tileLocation);
                }
            }
            FindObjectOfType<SpawnRandomTiles>().RemoveTileEvent -= RemoveTile;
            Destroy(this.transform.parent.gameObject);
        }
    }
}
