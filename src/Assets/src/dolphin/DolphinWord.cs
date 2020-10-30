using UnityEngine;
using System.Collections;

public class DolphinWord : DolphinObject {

    public TextMesh field;

    void Start()
    {
        Events.OnOkWord += OnOkWord;
    }
    void OnDestroy()
    {
        Events.OnOkWord -= OnOkWord;
    }
    void OnOkWord(GameData.types type)
    {
        //Pool();
    }
    public override void OnInit(DolphinObjectSettings settings, int laneId) 
    {
        field.text = settings.word;
        isCorrectWord = settings.isCorrect;
        Renderer renderer = field.gameObject.GetComponent<Renderer>();
        renderer.sortingLayerName = "laneOver";
        if (settings.word == "")
        {
            Invoke("Pool", 0.1f);
        }
    }
}
