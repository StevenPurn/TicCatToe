using UnityEngine;
using System.Collections;

public class MainMenuButtonToggle : MonoBehaviour {

	public GameObject mainMenuButton;

	public void ToggleMainMenuButton(){
		if (mainMenuButton.activeSelf) {
			mainMenuButton.SetActive (false);
		}
		else
		{
		mainMenuButton.SetActive (true);
		}
	}
}
