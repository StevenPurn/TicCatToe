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

    private AudioScript audioController;

    void Start()
    {
        buttonImg = GetComponent<Image>();
        audioController = GameObject.Find("AudioController").GetComponent<AudioScript>();
        if(buttonType == TypeOfButton.music)
        {
            toggleOn = AudioScript.playMusic;
        }else
        {
            toggleOn = AudioScript.playSFX;
        }

        SetSprite();
    }

    public void ToggleButton()
    {
        if (buttonType == TypeOfButton.music)
        {
            audioController.ToggleMusic();
        }
        else
        {
            audioController.ToggleSFX();
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
