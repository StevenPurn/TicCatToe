using UnityEngine;
using System.Collections;

public class AnimationVariation : MonoBehaviour {

	public Animation startAnim;

    public void Awake()
    {

		Animator anim = GetComponent<Animator>();

		StartCoroutine (WaitForAnimation(anim));

		anim.speed = Random.Range(0.7f, 1.3f);

    }

	IEnumerator WaitForAnimation(Animator anim)
	{
		Debug.Log (anim.GetCurrentAnimatorStateInfo(0));
		Debug.Log (anim.GetCurrentAnimatorClipInfo(0));
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);	
		GetComponent<TileTap> ().enabled = true;
		anim.speed = 1;
	}
}
