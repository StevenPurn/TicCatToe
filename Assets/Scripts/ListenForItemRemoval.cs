using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileBehaviour))]
public class ListenForItemRemoval : MonoBehaviour {

    public string animName;

	void Awake() {
        FindObjectOfType<ClearItemFromTile>().removeItemEvent += RemoveItem;
    }

    void RemoveItem(TileLocation tileLocation)
    {
        if(tileLocation == GetComponent<TileBehaviour>().TileLocation)
        {
            FindObjectOfType<ClearItemFromTile>().removeItemEvent -= RemoveItem;
            Animator anim = GetComponent<Animator>();
            anim.Play(animName);
            Destroy(this.gameObject);
        }
    }
}
