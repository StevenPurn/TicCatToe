using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {
    private static int BoardSize = 5;
    public Tile[,] BoardTiles = new Tile[BoardSize,BoardSize];
    public GameObject EmptyTileObj, StandardTileObj, GlassTileObj;
    public Player curPlayer { get; private set; }

    public delegate void WinCheckDelegate();
    public WinCheckDelegate WinCheckEvent;

    public delegate void TurnEndDelegate();
    public TurnEndDelegate turnEndEvent;

    public delegate void StartTurnDelegate();
    public StartTurnDelegate startTurnEvent;

    public delegate void ChangePlayerDelegate();
    public ChangePlayerDelegate changePlayerEvent;

    private GameObject boardObjects;
    private GameObject catPrefab, cheesePrefab;

    void Start()
    {
        catPrefab = (GameObject)Resources.Load("Prefabs/Cat");
        cheesePrefab = (GameObject)Resources.Load("Prefabs/Cheese");

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

        //Set current player to player one (cheese)
        curPlayer = Player.playerOne;
        StartCoroutine(HandleAi());
    }

    void HandleWins(HashSet<Tile> winningTiles)
    {
        foreach (Tile tile in winningTiles)
        {
            tile.valueOfTile = TileValue.empty;
            tile.tileOccupied = false;
            GetComponent<ClearItemFromTile>().RemoveItemFromTileWin(tile.locationOfTile);
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
            var health = instantiatedTile.GetComponentInChildren<GlassTileHealth>();
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

        var tileRemoval =  instantiatedTile.GetComponentInChildren<ListenForTileRemoval>();

        if (tileRemoval != null) {
            tileRemoval.ReplaceTileEvent += AddTile;
        }
    }

    void SetSprite(GameObject tile, TileLocation tileLocation)
    {
        SpriteRenderer[] sr = tile.GetComponentsInChildren<SpriteRenderer>();

        if (tileLocation == new TileLocation(2, 1) || tileLocation == new TileLocation(3, 2))
        {
            sr[0].sprite = Resources.Load<Sprite>("Sprites/Tile_front_v2");
        }
        foreach (var i in sr)
        {
            i.sortingLayerName = GetComponent<BoardLocationDictionary>().SortLayer[tileLocation];
        }
    }

    public void SetPosition(GameObject tile, TileLocation tileLocation)
    {
        float tileX = (BoardSize-1-tileLocation.y - tileLocation.x) * -1.84f;      //Magic numbers depend
        float tileY = (tileLocation.y - tileLocation.x) * 1.22f;                     //on scale of sprites
        tile.transform.position = new Vector2(tileX, tileY);
        tile.GetComponentInChildren<TileBehaviour>().TileLocation = tileLocation;
    }

    void RemoveTile(TileLocation tileLocation)
    {
        Tile tile = BoardTiles[tileLocation.x, tileLocation.y];
        tile.typeOfTile = TileType.emptyTile;
        tile.valueOfTile = TileValue.empty;
    }

    void ChangePlayer()
    {
        // Switch from current player to next player's turn
        curPlayer = curPlayer == Player.playerOne ? Player.playerTwo : Player.playerOne;
        changePlayerEvent();
    }

    TileLocation GetAiMove(Tile[,] boardTiles, Player curPlayer)
    {
        return new TileLocation(2, 2);
    }

    IEnumerator HandleAi()
    {
        if (curPlayer == GlobalData.AiPlayer)
        {
            yield return new WaitForSeconds(2);
            // Given the current board state and the current player, what's a move?
            TileLocation loc = GetAiMove(BoardTiles, curPlayer);
            // Now place an item on that tile.
            PlaceItemIfAvailable(loc);
        }
    }

    public void EndTurn()
    {
        WinCheckEvent();
        turnEndEvent();
        ChangePlayer();
        startTurnEvent();
        StartCoroutine(HandleAi());
    }

    public void PlaceItemIfAvailable(TileLocation tilePosition)
    {
        Tile tile = BoardTiles[tilePosition.x, tilePosition.y];
        if (tile.tileOccupied == false)
        {
            PlaceItem(tilePosition);
        }
    }

    void PlaceItem(TileLocation tileLoc)
    {
        TileValue tValue;
        GameObject instantiatedItem;

        if (curPlayer == Player.playerOne)
        {
            instantiatedItem = (GameObject)Instantiate(cheesePrefab, Vector3.zero, Quaternion.identity);
            tValue = TileValue.cheese;
        }
        else
        {
            instantiatedItem = (GameObject)Instantiate(catPrefab, Vector3.zero, Quaternion.identity);
            tValue = TileValue.cat;
        }
        GetComponent<BoardManager>().SetPosition(instantiatedItem, tileLoc);
        instantiatedItem.GetComponentInChildren<TileBehaviour>().TileLocation = tileLoc;
        instantiatedItem.GetComponentInChildren<SpriteRenderer>().sortingLayerName = GetComponent<BoardLocationDictionary>().SortLayer[tileLoc];

        //Set value of Tile item in this slot to occupied & cat or cheese
        Tile tile = BoardTiles[tileLoc.x, tileLoc.y];
        tile.tileOccupied = true;
        tile.valueOfTile = tValue;

        EndTurn();
    }
}
