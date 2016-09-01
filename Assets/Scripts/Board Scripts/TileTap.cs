using UnityEngine;

[RequireComponent(typeof(TileBehaviour))]
public class TileTap : MonoBehaviour
{
    public bool gameOver;

    void Update()
    {
    }

    void OnMouseUpAsButton()
    {
        ScoreManager scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        BoardManager boardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();

        if (scoreManager.gameOver == false && boardManager.curPlayer != GlobalData.AiPlayer)
        {
            TileLocation curLocation = GetComponent<TileBehaviour>().TileLocation;
            GameObject.Find("BoardManager").GetComponent<BoardManager>().PlaceItemIfAvailable(curLocation);
        }
    }
}
