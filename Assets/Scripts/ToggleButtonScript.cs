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

    void Start()
    {
        buttonImg = GetComponent<Image>();
    }

    public void ToggleButton()
    {
        if (toggleOn)
        {
            if (buttonType == TypeOfButton.music)
            {
                Debug.Log("Turn off music");
            }
            else
            {
                Debug.Log("Turn off sound effects");
            }
            buttonImg.sprite = offImg;
        }
        else
        {
            if (buttonType == TypeOfButton.music)
            {
                Debug.Log("Turn on music");
            }
            else
            {
                Debug.Log("Turn on sound effects");
            }
            buttonImg.sprite = onImg;
        }
        toggleOn = !toggleOn;
    }
}
