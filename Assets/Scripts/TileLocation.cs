using UnityEngine;
using System.Collections;

public class TileLocation : MonoBehaviour {

    public Vector2 tileLocation;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if(GetComponent<SpriteRenderer>() == null)
        {
            return;
        }
        if(tileLocation == new Vector2(2,1) || tileLocation == new Vector2(3,2))
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Tile_front");
        }

        GameObject boardManager = GameObject.Find("BoardManager");
        transform.position = boardManager.GetComponent<BoardLocationDictionary>().BoardLocation[tileLocation];
        sr.sortingLayerName = boardManager.GetComponent<BoardLocationDictionary>().SortLayer[tileLocation];
    }
}
