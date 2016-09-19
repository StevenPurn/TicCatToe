using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SFXVolumeSlider : MonoBehaviour {

	private AudioSource aSource;

	// Use this for initialization
	void Start () {
		aSource = GameObject.Find ("SFXController").GetComponent<AudioSource> ();
		GetComponent<Slider> ().value = aSource.volume;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnSliderMove(Slider slider){
		aSource.volume = slider.value;
	}
}
