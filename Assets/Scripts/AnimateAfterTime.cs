﻿using UnityEngine;
using System.Collections;

public class AnimateAfterTime : MonoBehaviour {

    private float animationTimerActual;
    public float animationTimer;
    public string animationName;

    void Start()
    {
        animationTimerActual = animationTimer;
    }

	
	// Update is called once per frame
	void Update () {
        animationTimerActual -= Time.deltaTime;
        if(animationTimerActual <= 0)
        {
            animationTimerActual = animationTimer;
            if(Random.Range(1,11) > 3)
            {
                Animator anim = GetComponent<Animator>();
                anim.Play(animationName);
            }
        }
	}
}
