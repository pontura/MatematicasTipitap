using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Lane : MonoBehaviour {

    public int id;

    public void AddObject(GameObject go, Vector3 pos)
    {
        go.transform.SetParent(transform);
        go.transform.localPosition = pos;
    }

}
