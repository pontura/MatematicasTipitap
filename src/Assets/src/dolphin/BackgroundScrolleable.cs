using UnityEngine;
using System.Collections;

public class BackgroundScrolleable : MonoBehaviour {

    public bool followAvatar;
    public float _width;
    public float speed;
    private float _x;

	void Start () {
        _x = 0;
	}

	public void UpdatePosition (float distance, float _speed) {
        _x -= _speed/speed;
        Vector3 pos = transform.localPosition;
      
        if (followAvatar) 
            pos.x = distance;
        else 
        {
              pos.x = distance + _x;
              if (distance - pos.x > _width)
              {
                  _x = 0;
                  pos.x = distance;
              }
        }
        
        transform.localPosition = pos;
	}
}
