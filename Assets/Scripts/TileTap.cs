using UnityEngine;
using System.Collections;


[RequireComponent(typeof(TileLocation))]
public class TileTap : MonoBehaviour {
    
    void OnMouseUpAsButton()
    {
        Vector2 curLocation = GetComponent<TileLocation>().tileLocation;
        GameObject.Find("BoardManager").GetComponent<ItemPlacement>().CheckTileAvailability(curLocation, transform.position);
    }
}
