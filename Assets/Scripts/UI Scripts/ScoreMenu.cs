using UnityEngine;
using System.Collections;

public class ScoreMenu : MonoBehaviour {

	public GameObject titleImage;
	public Animator anim;
	public string animName;
	private int animSpeed;
    public GameObject singlePlayerButton;
    public GameObject vsButton;
	public GameObject playerSelect;

	// Use this for initialization
	void Start () {
		if (GetComponent<Animator> () != null) {
			anim = GetComponent<Animator> ();
		}
		animSpeed = 1;
	}

	public void PlayAnimation(){
		if (animName != "") {
			anim.SetFloat (animName, animSpeed);
			anim.Play (animName);
			animSpeed = -animSpeed;
		}
		if (titleImage != null) {
			titleImage.SetActive (false);
		}
		if (singlePlayerButton != null) {
			singlePlayerButton.SetActive (false);
		}
		if (vsButton != null) {
			vsButton.SetActive (false);
		}
		if (playerSelect != null) {
			playerSelect.SetActive (false);
		}
	}
}
