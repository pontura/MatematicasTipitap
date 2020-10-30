using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FroggerSignal : MonoBehaviour {

    public int vueltaID;
    public TextMesh field;
    public bool isOk;
    public string word;

    public void Init(string word, bool isOk, int vueltaID) 
    {
        this.vueltaID = vueltaID;
        GetComponent<Animation>().Play("Idle");
        GetComponent<Animation>()["Idle"].normalizedTime = Random.Range(0, 10) / 10;
        this.word = word;
        this.isOk = isOk;        
        Colorize();

    }
    public void Break()
    {
        GetComponent<Animation>().Play("Break");
        field.text = "";
    }
    public void Rebuild()
    {
        if (!GetComponent<Animation>()) return;
        GetComponent<Animation>().Play("Idle");
        Colorize();
    }
    public void CatchWord()
    {
        field.text = "";
    }
    void Colorize()
    {
        if (word == null || word.Length<1) return;
        string terminacion = word.Substring(word.Length - 2);
        string firstPart = word.Substring(0, word.Length - 2);
        field.richText = true;
        field.text = firstPart + "<color=YELLOW>" + terminacion + "</color>";
    }
}
