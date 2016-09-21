using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour {
    private static int BoardSize = 5;
    public Tile[,] BoardTiles = new Tile[BoardSize,BoardSize];
    public GameObject EmptyTileObj, StandardTileObj, GlassTileObj;
    public Player curPlayer { get; private set; }
	public Text aiText;

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

    private SFXScript sfxScript;


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
        sfxScript = GameObject.Find("SFXController").GetComponent<SFXScript>();
		aiText = GameObject.Find ("AIThinking").GetComponent<Text> ();

        CreateBoard();
        FindObjectOfType<SpawnRandomTiles>().AddTileEvent += AddTile;
        FindObjectOfType<SpawnRandomTiles>().RemoveTileEvent += RemoveTile;
        FindObjectOfType<WinChecking>().SendWinEvent += HandleWins;

        //Set current player to player one (cheese)
        curPlayer = Player.playerOne;
        HandleAi();
    }

    void HandleWins(HashSet<Tile> winningTiles)
    {
        ScoreManager.UpdateScore(winningTiles);

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
            var spriteChange = instantiatedTile.GetComponentInChildren<StandardTileSpriteChange>();
            spriteChange.isOccupied = () => BoardTiles[tileLocation.x, tileLocation.y].tileOccupied;
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

        /*if (tileLocation == new TileLocation(2, 1) || tileLocation == new TileLocation(3, 2))
        {
            sr[0].sprite = Resources.Load<Sprite>("Sprites/Tile_front_v2");
        }*/
        foreach (var i in sr)
        {
            i.sortingLayerName = GetComponent<BoardLocationDictionary>().SortLayer[tileLocation];
        }
    }

    public void SetPosition(GameObject tile, TileLocation tileLocation)
    {
        float tileX = (BoardSize-1-tileLocation.y - tileLocation.x) * -1.91f;            //Magic numbers depend
        float tileY = (tileLocation.y - tileLocation.x) * 1.092f;                       //on scale of sprites
        tile.transform.position = new Vector2(tileX, tileY);
        tile.GetComponentInChildren<TileBehaviour>().TileLocation = tileLocation;
    }

    void RemoveTile(TileLocation tileLocation)
    {
        Tile tile = BoardTiles[tileLocation.x, tileLocation.y];
        tile.typeOfTile = TileType.emptyTile;
        tile.valueOfTile = TileValue.empty;
		TileBehaviour[] tileBehaviour  = FindObjectsOfType<TileBehaviour>();

		foreach (TileBehaviour tileB in tileBehaviour) {
			if (tileB.TileLocation == tileLocation) {
				Destroy(tileB.gameObject);
			}
		}

    }

    void ChangePlayer()
    {
        // Switch from current player to next player's turn
        curPlayer = curPlayer == Player.playerOne ? Player.playerTwo : Player.playerOne;
        changePlayerEvent();
    }

    private void HandleAi()
    {
		aiText.gameObject.SetActive (false);	
        if (curPlayer == GlobalData.AiPlayer && !ScoreManager.gameOver)
        {
            // Given the current board state and the current player, what's a move?
			aiText.gameObject.SetActive(true);
            StartCoroutine(AIGenius.CalculateMove(BoardTiles, curPlayer));
            // Place an item on that tile.

        }
    }

    public void EndTurn()
    {
        HandleWins(WinChecking.CheckWins(BoardTiles));
        turnEndEvent();
        ChangePlayer();
        startTurnEvent();
        HandleAi();
    }

	public void PlaceItemIfAvailable(TileLocation tilePosition, GameObject gameObj = null)
    {
        Tile tile = BoardTiles[tilePosition.x, tilePosition.y];
        if (tile.tileOccupied == false)
        {
            PlaceItem(tilePosition, gameObj);
        }
    }

	void PlaceItem(TileLocation tileLoc, GameObject gameObj)
    {
        TileValue tValue;
        GameObject instantiatedItem;

        if (curPlayer == Player.playerOne)
        {
            instantiatedItem = (GameObject)Instantiate(cheesePrefab, Vector3.zero, Quaternion.identity);
            tValue = TileValue.cheese;
            sfxScript.PlaySFX(SFXScript.AudioClipEnum.placeCheese);
        }
        else
        {
            instantiatedItem = (GameObject)Instantiate(catPrefab, Vector3.zero, Quaternion.identity);
            tValue = TileValue.cat;
            sfxScript.PlaySFX(SFXScript.AudioClipEnum.placeCat);
        }
        GetComponent<BoardManager>().SetPosition(instantiatedItem, tileLoc);
        instantiatedItem.GetComponentInChildren<TileBehaviour>().TileLocation = tileLoc;
        instantiatedItem.GetComponentInChildren<SpriteRenderer>().sortingLayerName = GetComponent<BoardLocationDictionary>().SortLayer[tileLoc];

		TileBehaviour[] tileBehaviour  = FindObjectsOfType<TileBehaviour>();

		int counter = 0;

		foreach (TileBehaviour tileB in tileBehaviour) {
			if (tileB.TileLocation == tileLoc) {
				instantiatedItem.transform.parent = tileB.gameObject.transform;
				instantiatedItem.transform.localPosition = Vector3.zero;
			}
		}

        //Set value of Tile item in this slot to occupied & cat or cheese
        Tile tile = BoardTiles[tileLoc.x, tileLoc.y];
        tile.tileOccupied = true;
        tile.valueOfTile = tValue;

        EndTurn();
    }
}
