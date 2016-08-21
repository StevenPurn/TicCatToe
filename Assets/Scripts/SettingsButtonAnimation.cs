using UnityEngine;
using System.Collections;

public class SettingsButtonAnimation : MonoBehaviour {

    public Animation anim;
    private int animSpeed;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animation>();
        animSpeed = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayAnimation()
    {
        Debug.Log("Animation Called");
        anim["SettingsButton"].speed = animSpeed;
        anim.Play();
        animSpeed *= -1;
    }
}
