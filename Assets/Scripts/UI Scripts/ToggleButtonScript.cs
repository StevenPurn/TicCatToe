using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleButtonScript : MonoBehaviour {
    public enum TypeOfButton { soundEffects, music };
    public TypeOfButton buttonType;
    public Image buttonImg;
    public Sprite onImg;
    public Sprite offImg;
    public bool toggleOn = true;

    private SFXScript sfxController;
	private MusicScript musicController;

    void Start()
    {
        buttonImg = GetComponent<Image>();
        musicController = GameObject.Find("AudioController").GetComponent<MusicScript>();
		sfxController = GameObject.Find ("SFXController").GetComponent<SFXScript> ();
        if(buttonType == TypeOfButton.music)
        {
            toggleOn = MusicScript.playMusic;
        }else
        {
            toggleOn = SFXScript.playSFX;
        }

        SetSprite();
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
        SetSprite();
    }

    private void SetSprite()
    {
        if (toggleOn)
        {
            buttonImg.sprite = onImg;
        }
        else
        {
            buttonImg.sprite = offImg;
        }
    }
}
