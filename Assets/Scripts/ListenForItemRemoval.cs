using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileBehaviour))]
public class ListenForItemRemoval : MonoBehaviour {

	void Start () {
        FindObjectOfType<ClearItemFromTile>().removeTileEvent += RemoveItem;
    }

    void RemoveItem(TileLocation tileLocation)
    {
        if(tileLocation == GetComponent<TileBehaviour>().TileLocation)
        {
            FindObjectOfType<ClearItemFromTile>().removeTileEvent -= RemoveItem;
            Destroy(this.gameObject);
        }
    }
}
