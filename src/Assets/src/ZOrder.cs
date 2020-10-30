using UnityEngine;
using System.Collections;

public class ZOrder : MonoBehaviour {

    public int z_order_value;

	void Start () {
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
            spriteRenderer.sortingOrder += z_order_value;
	}
}
