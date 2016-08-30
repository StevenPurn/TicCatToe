using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToSceneOnAwake : MonoBehaviour {

	public string sceneTarget;

	// Use this for initialization
	void Awake () {
		SceneManager.LoadScene (sceneTarget);
	}
}
