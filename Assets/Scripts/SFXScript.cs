using UnityEngine;
using System.Collections;

public class SFXScript : MonoBehaviour {

    public static bool playMusic = true;
    public static bool playSFX = true;

    public enum AudioClipEnum { glassBreak, buttonClick, score, win };

    public AudioClip glassBreakSFX, buttonClickSFX, scoreSFX, winSFX;

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
                default:
                    break;
            }
        }
    }
}
