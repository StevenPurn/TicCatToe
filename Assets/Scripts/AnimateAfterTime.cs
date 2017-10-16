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
            animationTimerActual = animationTimer;
            if(Random.Range(1,11) > 7)
            {
                anim.Play(animationName, -1, 0f);
            }
        }
	}
}
