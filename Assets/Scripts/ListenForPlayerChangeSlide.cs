using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ListenForPlayerChangeSlide : MonoBehaviour
{

    public Animation anim;
    private int animSpeed;
    public string animName;

    void Start()
    {
        anim = GetComponent<Animation>();
        animSpeed = 1;
        FindObjectOfType<TurnManager>().changePlayerEvent += PlayAnimation;
    }

    public void PlayAnimation()
    {
        anim[animName].speed = animSpeed;
        anim[animName].time = anim[animName].length;
        anim.Play(animName);
        animSpeed = -animSpeed;
    }
}
