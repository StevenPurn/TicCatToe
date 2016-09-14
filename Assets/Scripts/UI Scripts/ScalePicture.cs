using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScalePicture : MonoBehaviour {

	private RectTransform rectT;

	// Use this for initialization
	void Start () {
		rectT = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
		float scaleX = rectT.localScale.x;
		scaleX += 0.03f * Time.deltaTime;
		float scaleY = rectT.localScale.y;
		scaleY += 0.03f * Time.deltaTime;
		rectT.localScale = new Vector3 (scaleX, scaleY, 1.0f);
	}
}
