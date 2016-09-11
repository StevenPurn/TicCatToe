using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileBehaviour))]
public class ListenForItemRemoval : MonoBehaviour {

    public string winAnimName;
    public string loseAnimName;

	void Awake() {
        FindObjectOfType<ClearItemFromTile>().removeItemWinEvent += RemoveItemWin;
        FindObjectOfType<ClearItemFromTile>().removeItemLoseEvent += RemoveItemLose;
    }

    void RemoveItemWin(TileLocation tileLocation)
    {
        if(tileLocation == GetComponent<TileBehaviour>().TileLocation)
        {
            FindObjectOfType<ClearItemFromTile>().removeItemWinEvent -= RemoveItemWin;
            FindObjectOfType<ClearItemFromTile>().removeItemLoseEvent -= RemoveItemLose;
            StartCoroutine(WaitForAnimation(winAnimName));
        }
    }

    void RemoveItemLose(TileLocation tileLocation)
    {
        if (tileLocation == GetComponent<TileBehaviour>().TileLocation)
        {
            GameObject.Find("SFXController").GetComponent<SFXScript>().PlaySFX(SFXScript.AudioClipEnum.itemFall);
            FindObjectOfType<ClearItemFromTile>().removeItemWinEvent -= RemoveItemWin;
            FindObjectOfType<ClearItemFromTile>().removeItemLoseEvent -= RemoveItemLose;
            StartCoroutine(WaitForAnimation(loseAnimName));
        }
    }


    IEnumerator WaitForAnimation( string animName)
    {
        Animator anim = GetComponent<Animator>();
        anim.Play(animName);
        yield return new WaitForSeconds (anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(transform.parent.gameObject);
    }
}
