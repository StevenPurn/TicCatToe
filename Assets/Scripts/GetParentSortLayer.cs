using UnityEngine;
using System.Collections;

public class GetParentSortLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string sortLayer = transform.parent.GetComponent<SpriteRenderer>().sortingLayerName;
        if (GetComponent<ParticleSystem>() != null)
        {
            GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = sortLayer;
        }else if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = sortLayer;
        }
    }
}
