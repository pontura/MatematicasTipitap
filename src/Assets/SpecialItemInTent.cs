using UnityEngine;
using System.Collections;

public class SpecialItemInTent : MonoBehaviour {

    public GameObject available;
    public GameObject locked;

    public void Init(bool unlocked) {
	    if(unlocked)
        {
            available.SetActive(true);
            locked.SetActive(false);
        }
        else
        {
            available.SetActive(false);
            locked.SetActive(true);
        }
	}
}
