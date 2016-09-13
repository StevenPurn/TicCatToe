using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GlassTileHealth : MonoBehaviour
{
    private float curHealth;
    public const float HEALTH_DECREMENT = -.5f;
    public const float MAX_HEALTH = 4f;
    public string animName;
    public Func<bool> isOccupied;
    public Sprite sprite1, sprite2, sprite3, sprite4, sprite5;
    public enum GlassSprite { glassSprite1 = 1, glassSprite2, glassSprite3, glassSprite4, glassSprite5 };
    public GlassSprite glassSprite;
    public BoardManager boardManager;
    public TileLocation tileLocation;
    private Text healthText;

    // Use this for initialization
    void Start()
    {
        curHealth = MAX_HEALTH;
        ChangeSprite(curHealth);
        boardManager = GameObject.Find("BoardManager").GetComponent<BoardManager>();
        boardManager.turnEndEvent += TurnEnd;

        healthText = GetComponentInChildren<Text>();

        tileLocation = GetComponent<TileBehaviour>().TileLocation;
    }

    public void TurnEnd()
    {
        if (isOccupied())
        {
            AdjustHealth(HEALTH_DECREMENT);
        }
    }

    public void AdjustHealth(float healthChange)
    {
        curHealth += healthChange;
        boardManager.BoardTiles[tileLocation.x, tileLocation.y].tileHealth = curHealth;

        if (curHealth <= 0)
        {
            boardManager.turnEndEvent -= TurnEnd;
			GetComponent<TileTap> ().enabled = false;
            StartCoroutine(WaitForAnimation());
        }else
        {
            ChangeSprite(curHealth);
        }
    }

    void ChangeSprite(float sprite)
    {
        if(GetComponent<SpriteRenderer>() == null)
        {
            return;
        }
        SpriteRenderer rend = GetComponent<SpriteRenderer>();

        if ((int)Mathf.Ceil(sprite) == (int)GlassSprite.glassSprite1)
        {
            rend.sprite = sprite1;
        }
        else if ((int)Mathf.Ceil(sprite) == (int)GlassSprite.glassSprite2)
        {
            rend.sprite = sprite2;
        }
        else if ((int)Mathf.Ceil(sprite) == (int)GlassSprite.glassSprite3)
        {
            rend.sprite = sprite3;
        }
        else if ((int)Mathf.Ceil(sprite) == (int)GlassSprite.glassSprite4)
        {
            rend.sprite = sprite4;
        }
        else
        {
            if (isOccupied())
            {
                rend.sprite = sprite4;
            }
        }
    }

    IEnumerator WaitForAnimation()
    {
        GameObject.Find("BoardManager").GetComponent<ClearItemFromTile>().RemoveItemFromTileLose(tileLocation);
        Animator anim = GetComponent<Animator>();
        anim.enabled = true;
        anim.Play(animName);
        GameObject.Find("SFXController").GetComponent<SFXScript>().PlaySFX(SFXScript.AudioClipEnum.glassBreak);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        GetComponent<ListenForTileRemoval>().RemoveTile(tileLocation);
    }
}
