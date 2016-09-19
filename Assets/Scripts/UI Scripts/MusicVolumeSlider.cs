using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour {

	private AudioSource aSource;

	// Use this for initialization
	void Start () {
		aSource = GameObject.Find ("AudioController").GetComponent<AudioSource> ();
		GetComponent<Slider> ().value = aSource.volume;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnSliderMove(Slider slider){
		aSource.volume = slider.value;
	}
}
