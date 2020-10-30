using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhrasesUI : MonoBehaviour {

    public Text field;

    public void Init(string word)
    {
        field.text = word;
    }
    public void Pressed()
    {
        Events.OnSayCorrectWord_with_beep();
    }
}
