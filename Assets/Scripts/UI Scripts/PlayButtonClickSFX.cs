using UnityEngine;
using System.Collections;

public class PlayButtonClickSFX : MonoBehaviour {

    public void PlaySFX()
    {
        GameObject.Find("SFXController").GetComponent<SFXScript>().PlaySFX(SFXScript.AudioClipEnum.buttonClick);
    }
}
