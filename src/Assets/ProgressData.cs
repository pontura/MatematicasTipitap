using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProgressData : MonoBehaviour {

    public List<string> well1;
    public List<string> well2;
    public List<string> well3;
    public List<string> well4;

    public List<string> lastWrongWords;

    private int gameID;

    void Start () {        
        Events.OnAddWordToList += OnAddWordToList;
        Events.OnAddFinalWordToList += OnAddFinalWordToList;
        Events.OnAddWrongWord += OnAddWrongWord;
        Events.ResetApp += ResetApp;
        GetSavedWords();
    }
    void OnDestroy()
    {
        Events.OnAddWordToList -= OnAddWordToList;
        Events.OnAddFinalWordToList -= OnAddFinalWordToList;
        Events.OnAddWrongWord -= OnAddWrongWord;
        Events.ResetApp -= ResetApp;
    }
    void ResetApp ()
    {
        well1.Clear();
        well2.Clear();
        well3.Clear();
        well4.Clear();
    }
    void GetSavedWords()
    {
        for (int a = 0; a < 4; a++)
        {
            for (int b = 0; b < 40; b++)
            {
                string wordIdName = "well_" + (int)(a + 1) + "_" + (int)(b + 1);
               // print(wordIdName);
                string word = PlayerPrefs.GetString(wordIdName, "");
                if (word != "")
                    AddWordTo(a + 1, word);
            }
        }
    }
    bool  AddWordTo(int arrId, string word)
    {
        bool newWordAdded = false;
        switch(arrId)
        {
            case 1: newWordAdded = AddNewWord(well1, word); break;
            case 2: newWordAdded = AddNewWord(well2, word); break;
            case 3: newWordAdded = AddNewWord(well3, word); break;
            case 4: newWordAdded = AddNewWord(well4, word); break;
        }
        return newWordAdded;
    }
    bool AddNewWord(List<string> arr, string word)
    {
        foreach (string thisWord in arr)
            if (thisWord == word) return false;
        arr.Add(word);
       
        return true;
       
    }
    void OnAddWrongWord(string word)
    {
        foreach (string thisWord in well4)
            if (thisWord == word) return;
        well4.Add(word);
        string wordIdName = "well_4_" + well4.Count;
        print("new " + wordIdName + " : " + word);
        
        if (well4.Count > 5)
            well4.RemoveAt(0);
        for (int a = 1; a < well4.Count+1; a++)
            PlayerPrefs.SetString("well_4_" + a, well4[a-1]);
    }
    void OnAddWordToList(GameData.types type, int id)
    {
        string word = "";

        switch (type)
        {
            //case GameData.types.DOLPHIN:
            //    gameID = 1;
            //    word = Data.Instance.GetComponent<TextsMonkeys>().vueltas[id].title;
            //    break;
            //case GameData.types.MONKEY:
            //    gameID = 1;
            //    word = Data.Instance.GetComponent<TextsMonkeys>().vueltas[id].title;
            //    break;
            case GameData.types.FROGGER:
                gameID = 2;
                word = Data.Instance.GetComponent<TextsFrogger>().GetVuelta()[id].ok;
                break;
            case GameData.types.BRIDGE:
                gameID = 3;
              //  word = Data.Instance.GetComponent<TextsBridge>().vueltas[id].palabra;
                break;
        }

        if (AddWordTo(gameID, word) == true)
        {
            int arrLength = GetArrLength(gameID);
            string wordIdName = "well_" + gameID + "_" + arrLength;
            print("new " + wordIdName);
            PlayerPrefs.SetString(wordIdName, word);
        }
    }
    int GetArrLength(int gameID)
    {
        switch(gameID)
        {
            case 1: return well1.Count;
            case 2: return well2.Count;
            default: return well3.Count;
        }
    }
    void OnAddFinalWordToList(GameData.types type, string word)
    {
        int gameID = 1;
        switch (type)
        {
            case GameData.types.MONKEY:
                gameID = 1;
                break;
            case GameData.types.FROGGER:
                gameID = 2;
                break;
            case GameData.types.BRIDGE:
                gameID = 3;
                break;
        }
        if (AddWordTo(gameID, word) == true)
        {
            int arrLength = GetArrLength(gameID);
            string wordIdName = "well_" + gameID + "_" + arrLength;
            print("new " + wordIdName);
            PlayerPrefs.SetString(wordIdName, word);
        }
    }

}
