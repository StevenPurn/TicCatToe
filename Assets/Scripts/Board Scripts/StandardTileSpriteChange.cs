using UnityEngine;
using System;
using System.Collections;

public class StandardTileSpriteChange : MonoBehaviour {

    public Func<bool> isOccupied;

    public Sprite occupiedSprite, unoccupiedSprite;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isOccupied())
        {
            GetComponent<SpriteRenderer>().sprite = occupiedSprite;
        }else
        {
            GetComponent<SpriteRenderer>().sprite = unoccupiedSprite;
        }
    }
}
