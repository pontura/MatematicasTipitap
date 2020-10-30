using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {

    public int id;
    public string letter;

    public void SetLetter(string letterNew)
    {
        Events.OnSoundFX("BridgeWoodPop");
        letter = letterNew;
    }
    public void EmptyLetters()
    {
        letter = "";
    }
}
