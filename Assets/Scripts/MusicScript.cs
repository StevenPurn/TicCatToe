using UnityEngine;
using System.Collections;

public class MusicScript : MonoBehaviour {

    public static bool playMusic = true;

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
		playMusic = !playMusic;
		audioSource.enabled = playMusic;
	}
}
