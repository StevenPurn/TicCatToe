using UnityEngine;
using System.Collections;

public class PlayAnimationOnStart : MonoBehaviour {

    Animation introAnim;

	// Use this for initialization
	void Awake () {

        introAnim = GetComponent<Animation>();
        introAnim.Play("Cat_intro");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
