using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleButtonScript : MonoBehaviour {
    public enum TypeOfButton { soundEffects, music };
    public TypeOfButton buttonType;
    public bool toggleOn = true;

    private SFXScript sfxController;
	private MusicScript musicController;

    void Start()
    {
        musicController = GameObject.Find("AudioController").GetComponent<MusicScript>();
		sfxController = GameObject.Find ("SFXController").GetComponent<SFXScript> ();
        if(buttonType == TypeOfButton.music)
        {
            toggleOn = MusicScript.playMusic;
        }else
        {
            toggleOn = SFXScript.playSFX;
        }
    }

    public void ToggleButton()
    {
        if (buttonType == TypeOfButton.music)
        {
            musicController.ToggleMusic();
        }
        else
        {
            sfxController.ToggleSFX();
        }
        toggleOn = !toggleOn;
    }
}
