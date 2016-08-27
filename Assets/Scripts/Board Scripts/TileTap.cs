using UnityEngine;
using System.Collections;


[RequireComponent(typeof(TileBehaviour))]
public class TileTap : MonoBehaviour {

    public bool gameOver;

    void OnMouseUpAsButton()
    {
        ScoreManager scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        if (scoreManager.gameOver == false)
        {
            TileLocation curLocation = GetComponent<TileBehaviour>().TileLocation;
            GameObject.Find("BoardManager").GetComponent<ItemPlacement>().PlaceItemIfAvailable(curLocation, transform.position);
        }
    }
}
