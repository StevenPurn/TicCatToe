using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {
    
	public void GoToScene (string targetScene) {
        SceneManager.LoadScene(targetScene);
	}
}
