using UnityEngine;
using System.Text;
using System.IO;
using SimpleJSON;

public class Texts :MonoBehaviour {

    public void Load(string jsonURL)
    {
        Encoding utf8 = Encoding.UTF8;

        TextAsset file;
        file = Resources.Load(jsonURL) as TextAsset;
        LoadDataMinigames(file.text);
    }
    public virtual void LoadDataMinigames(string json_data) {  }

}
