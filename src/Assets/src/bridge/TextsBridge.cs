using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

public class TextsBridge : Texts
{

    public TextsData texts;

    [Serializable]
    public class TextsData
    {
        public List<Vuelta> level_1;
        public List<Vuelta> level_2;
    }

    [Serializable]
    public class Vuelta
    {
        public string title;
        public List<string> ok;
    }

    void Start()
    {
        Load("bridge");
    }
   
    public override void LoadDataMinigames(string json_data)
    {
        texts = JsonUtility.FromJson<TextsData>(json_data);

    }
}
