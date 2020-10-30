using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummaryWords : MonoBehaviour
{
    public GameData.types type;
    public bool commitError;

    public List<string> used_okWords;
    public List<string> used_wrongWords;

    public List<string> all_okWords;
    public List<string> all_wrongWords;

    public void OnLevelComplete(GameData.types type, bool commitError)
    {
        this.type = type;
        this.commitError = commitError;
        Data.Instance.LoadLevel("Summary", false);
    }
    public void Next()
    {
        Events.OnLevelComplete(type, commitError);
    }
    public void Reset()
    {
        used_okWords.Clear();
        used_wrongWords.Clear();

        all_okWords.Clear();
        all_wrongWords.Clear();
    }
    public void Add_UsedOkWord(string word)
    {
        used_okWords.Add(word);
    }
    public void Add_UsedWrongWords(string word)
    {
        used_wrongWords.Add(word);
    }
    public void Add_AllOkWord(string word)
    {
        all_okWords.Add(word);
    }
    public void Add_AllWrongWord(string word)
    {
        all_wrongWords.Add(word);
    }
}
