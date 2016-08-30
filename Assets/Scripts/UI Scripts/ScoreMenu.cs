using UnityEngine;
using System.Collections;

public class ScoreMenu : MonoBehaviour {

	public Animator anim;
	public string animName;
	private int animSpeed;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		animSpeed = 1;
	}

	public void PlayAnimation(){
		anim.SetFloat(animName, animSpeed);
		anim.Play(animName);
		animSpeed = -animSpeed;
	}
}
