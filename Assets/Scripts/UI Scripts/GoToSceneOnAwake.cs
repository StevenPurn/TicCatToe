using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToSceneOnAwake : MonoBehaviour {

	public string sceneTarget;
	public float delayTime = 5.0f;

	// Use this for initialization
	void Awake () {
		StartCoroutine(GoToScene(sceneTarget));
	}

	IEnumerator GoToScene(string sceneTarget){
		yield return new WaitForSeconds (delayTime);
		SceneManager.LoadScene (sceneTarget);
	}
}
