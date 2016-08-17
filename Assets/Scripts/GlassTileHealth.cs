using UnityEngine;
using System.Collections;
using System;

public class GlassTileHealth : MonoBehaviour
{
    private float curHealth;
    private const float MAX_HEALTH = 3;
    public Func<bool> isOccupied;
    public Sprite sprite1, sprite2, sprite3, sprite4;
    public enum GlassSprite { glassSprite1 = 0, glassSprite2, glassSprite3, glassSprite4 };
    public GlassSprite glassSprite;

    // Use this for initialization
    void Start()
    {
        curHealth = MAX_HEALTH;
        ChangeSprite(curHealth);
        FindObjectOfType<TurnManager>().turnEndEvent += TurnEnd;
    }

    public void TurnEnd()
    {
        if (isOccupied())
        {
            AdjustHealth(-.5f);
        }
    }

    public void AdjustHealth(float healthChange)
    {
        curHealth += healthChange;

        if (curHealth <= 0)
        {
            FindObjectOfType<TurnManager>().turnEndEvent -= TurnEnd;
            TileLocation tileLocation = GetComponent<TileBehaviour>().TileLocation;
            GameObject.Find("BoardManager").GetComponent<ClearItemFromTile>().RemoveItemFromTile(tileLocation);
            GetComponent<ListenForTileRemoval>().RemoveTile(tileLocation);
            //if not random spawn tile add back a new glass tile
        }

        ChangeSprite(curHealth);
    }

    void ChangeSprite(float sprite)
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();

        if ((int)sprite == (int)GlassSprite.glassSprite1)
        {
            rend.sprite = sprite1;
        }
        else if ((int)sprite == (int)GlassSprite.glassSprite2)
        {
            rend.sprite = sprite2;
        }
        else if ((int)sprite == (int)GlassSprite.glassSprite3)
        {
            rend.sprite = sprite3;
        }
        else
        {
            rend.sprite = sprite4;
        }
    }
}
