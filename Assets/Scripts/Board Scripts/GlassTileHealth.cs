using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GlassTileHealth : MonoBehaviour
{
    private int curHealth;
    public const int HEALTH_DECREMENT = -1;
    public const int MAX_HEALTH = 8;
    public string animName;
    public Func<bool> isOccupied;
    public Sprite sprite1, sprite2, sprite3, sprite4, sprite5, sprite6, sprite7, sprite8;
	public enum GlassSprite { glassSprite1 = 1, glassSprite2, glassSprite3, glassSprite4, glassSprite5, glassSprite6, glassSprite7, glassSprite8 };
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
        tileLocation = GetComponent<TileBehaviour>().TileLocation;
        boardManager.BoardTiles[tileLocation.x, tileLocation.y].tileHealth = curHealth;

        healthText = GetComponentInChildren<Text>();
    }

    public void TurnEnd()
    {
        if (isOccupied())
        {
            AdjustHealth(HEALTH_DECREMENT);
        }
    }

    public void AdjustHealth(int healthChange)
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

	void ChangeSprite(int sprite)
    {
        if(GetComponent<SpriteRenderer>() == null)
        {
            return;
        }
        SpriteRenderer rend = GetComponent<SpriteRenderer>();

		if (sprite == (int)GlassSprite.glassSprite1)
        {
            rend.sprite = sprite1;
			Animator anim = GetComponent<Animator>();
			anim.enabled = true;
			anim.Play("GlassTileShake");
        }
		else if (sprite == (int)GlassSprite.glassSprite2)
        {
            rend.sprite = sprite2;
        }
		else if (sprite == (int)GlassSprite.glassSprite3)
        {
            rend.sprite = sprite3;
        }
		else if (sprite == (int)GlassSprite.glassSprite4)
        {
            rend.sprite = sprite4;
		}else if (sprite == (int)GlassSprite.glassSprite5)
		{
			rend.sprite = sprite5;
		}
		else if (sprite == (int)GlassSprite.glassSprite6)
		{
			rend.sprite = sprite6;
		}
		else if (sprite == (int)GlassSprite.glassSprite7)
		{
			rend.sprite = sprite7;
		}
        else
        {
            if (isOccupied())
            {
                rend.sprite = sprite8;
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
