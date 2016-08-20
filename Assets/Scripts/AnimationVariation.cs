using UnityEngine;
using System.Collections;

public class AnimationVariation : MonoBehaviour {

    public void Awake()
    {
        Animation anim = GetComponent<Animation>();

        foreach (AnimationState state in anim)
        {
            state.speed = Random.Range(0.7f, 1.3f);
        }

    }
}
