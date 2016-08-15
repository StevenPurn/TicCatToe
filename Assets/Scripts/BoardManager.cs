using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour {

    public List<Tile> BoardTiles = new List<Tile>() { };
    private int boardSize = 4;
    public GameObject emptyTileObj, standardTileObj, glassTileObj;
    private GameObject boardObjects;

    // Use this for initialization
    void Start() {

        if (GameObject.Find("BoardObjects") == null)
        {
            boardObjects = (GameObject)Instantiate(Resources.Load("Prefabs/BoardObjects"));
        }
        else
        {
            boardObjects = GameObject.Find("BoardObjects");
        }

        standardTileObj = (GameObject)Resources.Load("Prefabs/StandardTile");
        glassTileObj = (GameObject)Resources.Load("Prefabs/GlassTile");
        emptyTileObj = (GameObject)Resources.Load("Prefabs/EmptyTile");

        CreateBoard();
    }

    void CreateBoard() { 
        TileType typeOfTile = TileType.emptyTile;

        for (int i = 0; i <= boardSize; i++)
        {
            for (int j = 0; j <= boardSize; j++)
            {
                if(i > 0 && i < 4 && j > 0 && j < 4)
                {
                    if(i == j)
                    {
                        typeOfTile = TileType.glassTile;
                    }else if(i == 1 && j == 3)
                    {
                        typeOfTile = TileType.glassTile;
                    }else if(i == 3 && j == 1)
                    {
                        typeOfTile = TileType.glassTile;
                    }else
                    {
                        typeOfTile = TileType.standardTile;
                    }
                }else
                {
                    typeOfTile = TileType.emptyTile;
                }

                AddTile(new Vector2(i, j), typeOfTile, TileValue.empty);
            }
        }
	}

    void AddTile(Vector2 tileLocation, TileType tileType, TileValue tileValue)
    {
        GameObject instantiatedTile = null;

        BoardTiles.Add(new Tile(tileLocation, tileType, tileValue));
        if (tileType == TileType.glassTile)
        {
            instantiatedTile = (GameObject)Instantiate(glassTileObj, boardObjects.transform);
        }
        else if (tileType == TileType.emptyTile)
        {
            instantiatedTile = (GameObject)Instantiate(emptyTileObj, boardObjects.transform);
        }
        else if (tileType == TileType.standardTile)
        {
            instantiatedTile = (GameObject)Instantiate(standardTileObj, boardObjects.transform);
        }

        instantiatedTile.GetComponent<TileLocation>().tileLocation = tileLocation;
    }
}
