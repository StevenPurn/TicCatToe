using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileBehaviour))]
public class ListenForItemRemoval : MonoBehaviour {

	void Awake() {
        FindObjectOfType<ClearItemFromTile>().removeItemEvent += RemoveItem;
    }

    void RemoveItem(TileLocation tileLocation)
    {
        if(tileLocation == GetComponent<TileBehaviour>().TileLocation)
        {
            FindObjectOfType<ClearItemFromTile>().removeItemEvent -= RemoveItem;
            Destroy(this.gameObject);
        }
    }
}
