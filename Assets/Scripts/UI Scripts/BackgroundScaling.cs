using UnityEngine;
using System.Collections;

public class BackgroundScaling : MonoBehaviour {

	// Use this for initialization
	void FixedUpdate () {
        ResizeSprite();
	}
	
	void ResizeSprite () {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if(sr == null)
        {
            return;
        }

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float cameraHeight = Camera.main.orthographicSize * 2.0f;
        float cameraWidth = cameraHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3( cameraWidth / width, cameraHeight / height,1.0f);
	}
}
