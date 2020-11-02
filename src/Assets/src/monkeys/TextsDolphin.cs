using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

public class TextsDolphin : Texts {

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
        public List<string> wrong;
    }

    void Start () {
        Load("dolphin");
	}
    public override void LoadDataMinigames(string json_data)
    {
        texts = JsonUtility.FromJson<TextsData>(json_data);
    }
    public void ShuffleArr(List<Vuelta> arr)
    {
        if (arr.Count < 2) return;
        for (int a = 0; a < 100; a++)
        {
            var id = UnityEngine.Random.Range(1, arr.Count);
            var value1 = arr[0];
            var value2 = arr[id];
            arr[0] = value2;
            arr[id] = value1;
        }
    }
}
