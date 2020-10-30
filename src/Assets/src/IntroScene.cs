using UnityEngine;
using System.Collections;

public class IntroScene : MonoBehaviour {

    void Start()
    {
        Invoke("Delayed", 0.005f);
    }
    void Delayed()
    {
        Events.OnMusic("playful");
    }
    public void StartGame()
    {
        Data.Instance.LoadLevel("MainMenu", false);
    }
}
