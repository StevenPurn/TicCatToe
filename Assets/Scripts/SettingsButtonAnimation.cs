using UnityEngine;
using System.Collections;

public class SettingsButtonAnimation : MonoBehaviour {

    public Animation anim;
    private int animSpeed;
	public string animName;
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
		anim[animName].speed = animSpeed;
		if (animSpeed < 0)
		{
			anim[animName].time = anim[animName].length;
		}
		anim.Play(animName);
		animSpeed = -animSpeed;
    }
}
