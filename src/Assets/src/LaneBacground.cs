using UnityEngine;
using System.Collections;

public class LaneBacground : MonoBehaviour {

    public Transform hero;
    public float LoopWidth;
    private float _x;
    public int qty;

    void Start()
    {
        _x = transform.localPosition.x;
    }
	void Update () {
        if (hero.transform.localPosition.x - LoopWidth > _x)
        {
            _x += (LoopWidth * qty);
            transform.localPosition = new Vector3(_x, 0, 0);            
        }
	}
}
