using UnityEngine;
using System.Collections;

public class ToggleDisable : MonoBehaviour {
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
			toggleOn = !MusicScript.playMusic;
		}else
		{
			toggleOn = !SFXScript.playSFX;
		}

		SetActive();
	}

	private void SetActive()
	{
		gameObject.SetActive (toggleOn);
	}
}
