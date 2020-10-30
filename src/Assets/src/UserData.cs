using UnityEngine;
using System.Collections;

public class UserData : MonoBehaviour {

    public dificults dificult;
    public enum dificults
    {
        EASY,
        MEDIUM,
        HARD
    }
	void Start () {
	
	}
    public void ChangeDificult(dificults newDificult)
    {
        dificult = newDificult;
    }
}
