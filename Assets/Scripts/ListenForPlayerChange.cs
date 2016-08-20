using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ListenForPlayerChange : MonoBehaviour {

    private Image playerImage;
    public float alphaValue;

	// Use this for initialization
	void Start () {
        playerImage = GetComponent<Image>();
        FindObjectOfType<TurnManager>().changePlayerEvent += ChangePlayer;
	}
	
	public void ChangePlayer() {

        Color imageColor = playerImage.color;

        if (playerImage.color.a > alphaValue)
        {
            imageColor.a = alphaValue;
        }
        else
        {
            imageColor.a = 255;
        }

        playerImage.color = imageColor;
    }
}
