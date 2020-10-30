using UnityEngine;
using System.Collections;

public class FroggerCamera : MonoBehaviour {

    public float newX;
    public FroggerGame game;
	
    public void SetX(float _newX)
    {
        if (_newX > (game.panels.GetComponentsInChildren<FroggerPanel>().Length-2) * game.distance) return;
        this.newX = _newX;
    }
	void LateUpdate () {
        Vector3 newPos = new Vector3(newX, 0,-10);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, 0.1f);
	}
}
