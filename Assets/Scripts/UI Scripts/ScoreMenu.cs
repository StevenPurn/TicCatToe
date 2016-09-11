using UnityEngine;
using System.Collections;

public class ScoreMenu : MonoBehaviour {

	public GameObject titleImage;
	public Animator anim;
	public string animName;
	private int animSpeed;
    public GameObject singlePlayerButton;
    public GameObject vsButton;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		animSpeed = 1;
	}

	public void PlayAnimation(){
		anim.SetFloat(animName, animSpeed);
		anim.Play(animName);
		animSpeed = -animSpeed;
		titleImage.SetActive(false);
        singlePlayerButton.SetActive(false);
        vsButton.SetActive(false);
	}
}
