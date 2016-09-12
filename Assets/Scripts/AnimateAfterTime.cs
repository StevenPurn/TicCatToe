using UnityEngine;
using System.Collections;

public class AnimateAfterTime : MonoBehaviour {

    private float animationTimerActual;
    public float animationTimer;
    public string animationName;
	private Animator anim;

    void Start()
    {
        animationTimerActual = animationTimer;
		anim = GetComponent<Animator> ();
    }

	
	// Update is called once per frame
	void Update () {
        animationTimerActual -= Time.deltaTime;
        if(animationTimerActual <= 0)
        {
			Debug.Log ("Resetting timer & checking whether to blink");
            animationTimerActual = animationTimer;
            if(Random.Range(1,11) > 3)
            {
				Debug.Log ("Should be blinking");
                anim.Play(animationName);
            }
        }
	}
}
