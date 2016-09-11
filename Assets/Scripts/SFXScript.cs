using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.Events;

public class SFXScript : MonoBehaviour {

    public static bool playMusic = true;
    public static bool playSFX = true;

    public enum AudioClipEnum { glassBreak, buttonClick, score, win, lose, placeCheese, placeCat, itemFall };

    public AudioClip glassBreakSFX, buttonClickSFX, scoreSFX, winSFX, loseSFX, placeCheeseSFX, placeCatSFX, itemFallSFX;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
    }


    public void ToggleMusic()
    {
		audioSource.mute = playMusic;
		playMusic = !playMusic;
	}

    public void ToggleSFX()
    {
        playSFX = !playSFX;
    }

    public void PlaySFX(AudioClipEnum soundEffect)
    {
        if(playSFX == false)
        {
            return;
        }
        else
        {
            switch (soundEffect)
            {
                case AudioClipEnum.glassBreak:
                    audioSource.PlayOneShot(glassBreakSFX);
                    break;
                case AudioClipEnum.buttonClick:
                    audioSource.PlayOneShot(buttonClickSFX);
                    break;
                case AudioClipEnum.score:
                    audioSource.PlayOneShot(scoreSFX);
                    break;
                case AudioClipEnum.win:
                    audioSource.PlayOneShot(winSFX);
                    break;
                case AudioClipEnum.lose:
                    audioSource.PlayOneShot(loseSFX);
                    break;
                case AudioClipEnum.placeCat:
                    audioSource.PlayOneShot(placeCatSFX);
                    break;
                case AudioClipEnum.placeCheese:
                    audioSource.PlayOneShot(placeCheeseSFX);
                    break;
                case AudioClipEnum.itemFall:
                    audioSource.PlayOneShot(itemFallSFX);
                    break;
                default:
                    break;
            }
        }
    }
}
