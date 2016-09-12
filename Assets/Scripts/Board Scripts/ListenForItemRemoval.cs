using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileBehaviour))]
public class ListenForItemRemoval : MonoBehaviour {

    public string winAnimName;
    public string loseAnimName;
	public string idleAnimName;
	private Animator anim;
	public float animDelayTime = 3.0f;
	private float currentAnimTime;
	private int animHash;

	void Awake() {
        FindObjectOfType<ClearItemFromTile>().removeItemWinEvent += RemoveItemWin;
        FindObjectOfType<ClearItemFromTile>().removeItemLoseEvent += RemoveItemLose;
		anim = GetComponent<Animator> ();
		currentAnimTime = animDelayTime;
		animHash = Animator.StringToHash ("idleTrigger");
    }

	void Update ()
	{
		currentAnimTime -= Time.deltaTime;

		if (currentAnimTime <= 0) {
			currentAnimTime = animDelayTime;
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName(winAnimName) && !anim.GetCurrentAnimatorStateInfo(0).IsName (loseAnimName)) {
				PlayAnimation(idleAnimName);
			}
		}
	}

    void RemoveItemWin(TileLocation tileLocation)
    {
        if(tileLocation == GetComponent<TileBehaviour>().TileLocation)
        {
            FindObjectOfType<ClearItemFromTile>().removeItemWinEvent -= RemoveItemWin;
            FindObjectOfType<ClearItemFromTile>().removeItemLoseEvent -= RemoveItemLose;
			anim.SetBool ("win", true);
			anim.SetBool ("lose", false);
			anim.SetBool ("idle", false);
			StartCoroutine(WaitForAnimation(winAnimName, true));
        }
    }

    void RemoveItemLose(TileLocation tileLocation)
    {
        if (tileLocation == GetComponent<TileBehaviour>().TileLocation)
        {
            GameObject.Find("SFXController").GetComponent<SFXScript>().PlaySFX(SFXScript.AudioClipEnum.itemFall);
            FindObjectOfType<ClearItemFromTile>().removeItemWinEvent -= RemoveItemWin;
            FindObjectOfType<ClearItemFromTile>().removeItemLoseEvent -= RemoveItemLose;
			anim.SetBool ("win", false);
			anim.SetBool ("lose", true);
			anim.SetBool ("idle", false);
			StartCoroutine(WaitForAnimation(loseAnimName, true));
        }
    }

	void PlayAnimation(string animName){
		anim.Play(animName);
	}


	IEnumerator WaitForAnimation( string animName, bool destroyObj)
	{
		anim.Play (animName);
		yield return new WaitForSeconds (anim.GetCurrentAnimatorStateInfo (0).length);
		anim.Stop ();
		if (destroyObj) 
		{
			Destroy (transform.parent.gameObject);
    
		}
	}
}
