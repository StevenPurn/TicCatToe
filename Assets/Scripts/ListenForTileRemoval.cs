using UnityEngine;
using System.Collections;

public class ListenForTileRemoval : MonoBehaviour {

    public delegate void ReplaceTileDelegate(TileLocation tileLocation, TileType tileType, TileValue tileValue);
    public ReplaceTileDelegate ReplaceTileEvent;

    void Start()
    {
        FindObjectOfType<SpawnRandomTiles>().RemoveTileEvent += RemoveTile;
    }

    public void RemoveTile(TileLocation tileLocation)
    {
        if (tileLocation == GetComponent<TileBehaviour>().TileLocation)
        {
            Debug.Log("(" + tileLocation.x + ", " + tileLocation.y + ") == (" + GetComponent<TileBehaviour>().TileLocation.x + ", " + GetComponent<TileBehaviour>().TileLocation.y + ")");
            if (!GameObject.Find("BoardManager").GetComponent<BoardLocationDictionary>().RandomSpawnPoints.ContainsValue(tileLocation))
            {
                ReplaceTileEvent(tileLocation, TileType.glassTile, TileValue.empty);
            }

            FindObjectOfType<SpawnRandomTiles>().RemoveTileEvent -= RemoveTile;
            Destroy(this.gameObject);
        }
    }
}
