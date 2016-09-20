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
        BoardManager boardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();

		if (ScoreManager.gameOver == false && boardManager.curPlayer != GlobalData.AiPlayer) 
		{
			TileLocation curLocation = GetComponent<TileBehaviour> ().TileLocation;
			GameObject.Find ("BoardManager").GetComponent<BoardManager> ().PlaceItemIfAvailable (curLocation, this.gameObject);
		}
    }
}
