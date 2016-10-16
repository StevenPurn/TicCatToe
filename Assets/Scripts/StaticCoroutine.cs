using UnityEngine;
using System.Collections;
using System;

public class StaticCoroutine : MonoBehaviour
{
    static public StaticCoroutine instance; //the instance of our class that will do the work

    void Awake()
    { //called when an instance awakes in the game
        instance = this; //set our static reference to our newly initialized instance
    }

    IEnumerator Perform(IEnumerator coroutine, Action onComplete = null)
    {
        onComplete = onComplete ?? delegate { };
        yield return StartCoroutine(coroutine);
        onComplete();
    }

    static public void DoCoroutine(IEnumerator coroutine, Action onComplete = null)
    {
        instance.StartCoroutine(instance.Perform(coroutine, onComplete)); //this will launch the coroutine on our instance
    }

}