using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {
    private static int BoardSize = 5;
    public Tile[,] BoardTiles = new Tile[BoardSize,BoardSize];
    public GameObject EmptyTileObj, StandardTileObj, GlassTileObj;

    private GameObject boardObjects;

    void Start() {
        if (GameObject.Find("BoardObjects") == null)
        {
            boardObjects = (GameObject)Instantiate(Resources.Load("Prefabs/BoardObjects"));
        }
        else
        {
            boardObjects = GameObject.Find("BoardObjects");
        }

        StandardTileObj = (GameObject)Resources.Load("Prefabs/StandardTile");
        GlassTileObj = (GameObject)Resources.Load("Prefabs/GlassTile");
        EmptyTileObj = (GameObject)Resources.Load("Prefabs/EmptyTile");

        CreateBoard();
        FindObjectOfType<SpawnRandomTiles>().AddTileEvent += AddTile;
        FindObjectOfType<SpawnRandomTiles>().RemoveTileEvent += RemoveTile;
        FindObjectOfType<WinChecking>().SendWinEvent += HandleWins;
    }

    void HandleWins(HashSet<Tile> winningTiles)
    {
        foreach (Tile tile in winningTiles)
        {
            tile.valueOfTile = TileValue.empty;
            tile.tileOccupied = false;
            GetComponent<ClearItemFromTile>().RemoveItemFromTile(tile.locationOfTile);
        }
    }

    void CreateBoard() {
        TileType typeOfTile = TileType.emptyTile;

        for (int x = 0; x < BoardSize; x++)
        {
            for (int y = 0; y < BoardSize; y++)
            {
                if (x > 0 && x < BoardSize - 1 && y > 0 && y < BoardSize - 1)
                {
                    if (x == y)
                    {
                        typeOfTile = TileType.glassTile;
                    }
                    else if (x == 1 && y == 3)
                    {
                        typeOfTile = TileType.glassTile;
                    }
                    else if(x == 3 && y == 1)
                    {
                        typeOfTile = TileType.glassTile;
                    }
                    else
                    {
                        typeOfTile = TileType.standardTile;
                    }
                }
                else
                {
                    typeOfTile = TileType.emptyTile;
                }

                AddTile(new TileLocation(x, y), typeOfTile, TileValue.empty);
            }
        }
	}

    void AddTile(TileLocation tileLocation, TileType tileType, TileValue tileValue)
    {
        GameObject instantiatedTile = null;

        BoardTiles[tileLocation.x, tileLocation.y] = new Tile(tileLocation, tileType, tileValue);
        if (tileType == TileType.glassTile)
        {
            instantiatedTile = (GameObject)Instantiate(GlassTileObj, boardObjects.transform);
            SetSprite(instantiatedTile, tileLocation);
            var health = instantiatedTile.GetComponent<GlassTileHealth>();
            health.isOccupied = () => BoardTiles[tileLocation.x, tileLocation.y].tileOccupied;
        }
        else if (tileType == TileType.emptyTile)
        {
            instantiatedTile = (GameObject)Instantiate(EmptyTileObj, boardObjects.transform);
        }
        else if (tileType == TileType.standardTile)
        {
            instantiatedTile = (GameObject)Instantiate(StandardTileObj, boardObjects.transform);
            SetSprite(instantiatedTile, tileLocation);
        }

        SetPosition(instantiatedTile, tileLocation);

        var tileRemoval =  instantiatedTile.GetComponent<ListenForTileRemoval>();

        if (tileRemoval != null) {
            tileRemoval.ReplaceTileEvent += AddTile;
        }
    }

    void SetSprite(GameObject tile, TileLocation tileLocation)
    {
        SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();

        if (tileLocation == new TileLocation(2, 1) || tileLocation == new TileLocation(3, 2))
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Tile_front");
        }
        sr.sortingLayerName = GetComponent<BoardLocationDictionary>().SortLayer[tileLocation];
    }

    void SetPosition(GameObject tile, TileLocation tileLocation)
    {
        float tileX = (BoardSize-1-tileLocation.y - tileLocation.x) * -1.5f;    //Magic numbers depend
        float tileY = (tileLocation.y - tileLocation.x) * 0.95f;                //on scale of sprites
        tile.transform.position = new Vector2(tileX, tileY);
        tile.GetComponent<TileBehaviour>().TileLocation = tileLocation;
    }

    void RemoveTile(TileLocation tileLocation)
    {
        Tile tile = BoardTiles[tileLocation.x, tileLocation.y];
        tile.typeOfTile = TileType.emptyTile;
        tile.valueOfTile = TileValue.empty;
    }
}
