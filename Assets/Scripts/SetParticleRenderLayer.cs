﻿using UnityEngine;
using System.Collections;

public class SetParticleRenderLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystemRenderer>().sortingLayerName = transform.parent.GetComponent<SpriteRenderer>().sortingLayerName;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<ParticleSystemRenderer>().sortingLayerName = transform.parent.GetComponent<SpriteRenderer>().sortingLayerName;
    }
}
