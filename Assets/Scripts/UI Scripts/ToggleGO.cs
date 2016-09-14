using UnityEngine;
using System.Collections;

public class ToggleGO : MonoBehaviour {

	// Use this for initialization
	public void ToggleGameObject(GameObject go){
		if (go.activeSelf == true) {
			go.SetActive (false);
		} else {
			go.SetActive (true);
		}
	}
}
