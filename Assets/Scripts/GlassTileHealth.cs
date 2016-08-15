using UnityEngine;
using System.Collections;

public class GlassTileHealth : MonoBehaviour {

    private int curHealth;
    private int maxHealth = 3;
    private Vector2 tileLocation;
    public bool occupied;
    public Sprite sprite1, sprite2, sprite3, sprite4;
    public enum GlassSprite { glassSprite1 = 0, glassSprite2, glassSprite3, glassSprite4 };
    public GlassSprite glassSprite;

    // Use this for initialization
    void Start () {
        curHealth = maxHealth;
        ChangeSprite(curHealth);
        FindObjectOfType<TurnManager>().turnEndEvent += TurnEnd;
	}

    public void SetLoction(Vector2 location)
    {
        tileLocation = location;
    }

    public void TurnEnd()
    {
        if (occupied)
        {
            AdjustHealth(-1);
        }
    }

    public void AdjustHealth(int healthChange)
    {
        curHealth += healthChange;
        if (curHealth > 0)
        {
            ChangeSprite(curHealth);
        }
        else
        {
            occupied = false;
            //Remove item on space, play animation?
            curHealth = maxHealth;
        }
    }

    void ChangeSprite(int sprite)
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();

        if(sprite == (int)GlassSprite.glassSprite1)
        {
            rend.sprite = sprite1;
        }else if (sprite == (int)GlassSprite.glassSprite2)
        {
            rend.sprite = sprite2;
        }else if (sprite == (int)GlassSprite.glassSprite3)
        {
            rend.sprite = sprite3;
        }else
        {
            rend.sprite = sprite4;
        }

    }
}
