using UnityEngine;
using System.Collections;


[RequireComponent(typeof(TileLocation))]
public class TileTap : MonoBehaviour {
    
    void OnMouseUpAsButton()
    {
        Debug.Log(GetComponent<TileLocation>().tileLocation);
    }
}
