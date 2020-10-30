using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WordUI : MonoBehaviour {

    public Text field;

    public void Init(string word)
    {
        field.text = word.ToLower();
    }
    public void Pressed()
    {
        Events.OnSayCorrectWord();
    }
}
