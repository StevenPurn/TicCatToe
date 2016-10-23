using UnityEngine;
using System.Collections;

public class DeactivateAfterTime : MonoBehaviour {

	private float deactivateTimeActual;
	public float deactivateTimer;

	// Use this for initialization
	void Start () {
		deactivateTimeActual = deactivateTimer;
	}
	
	// Update is called once per frame
	void Update () {
		deactivateTimeActual -= Time.deltaTime;
		if (deactivateTimeActual <= 0) {
			this.gameObject.SetActive (false);
		}
	}
}
