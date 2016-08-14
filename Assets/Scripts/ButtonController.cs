using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonController : MonoBehaviour {

    public Scene Scene;

    public void GoToScene(Scene targetScene)
    {
        string sceneName = targetScene.name;
        SceneManager.LoadScene(sceneName);
    }

    public void Test()
    {
        Debug.Log("FUCK THOMAS");
    }
}
