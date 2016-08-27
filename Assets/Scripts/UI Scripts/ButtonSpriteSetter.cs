using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSpriteSetter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void SetButtonSprite (Player player) {
	    if(player == Player.playerOne)
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/b2");
        }
        else
        {
            GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/b1");
        }
	}
}
