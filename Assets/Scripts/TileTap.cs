using UnityEngine;
using System.Collections;


[RequireComponent(typeof(TileBehaviour))]
public class TileTap : MonoBehaviour {
    void OnMouseUpAsButton()
    {
        TileLocation curLocation = GetComponent<TileBehaviour>().TileLocation;
        GameObject.Find("BoardManager").GetComponent<ItemPlacement>().PlaceItemIfAvailable(curLocation, transform.position);
    }
}
