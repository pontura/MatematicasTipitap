using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryMinigame : MonoBehaviour
{

    public SummaryButton summaryButton;
    public List<SummaryButton> all;
    public int separation = 100;
    public List<string> allResults;
    public Transform container;
    public Text okContainer;
    public Text wrongcontainer;
    SummaryWords summaryWords;
    int totalErased;
    bool done;

    void Start()
    {

        summaryWords = Data.Instance.GetComponent<SummaryWords>();
        foreach (string s in summaryWords.used_okWords)
            allResults.Add(s);
        foreach (string s in summaryWords.used_wrongWords)
            allResults.Add(s);

        //si no hay errores saltea todo:
        if (allResults.Count == 0)
            Delayed();
        else
        {
            Events.OnSceneLoaded();
            Init();
        }
    }
    void Done()
    {
        done = true;
        Invoke("Delayed", 2);
    }
    void Delayed()
    {
        Data.Instance.GetComponent<SummaryWords>().Next();
    }
    public void Init()
    {
        okContainer.text = "";
        wrongcontainer.text = "";
      

        Utils.ShuffleListTexts(allResults);
        int id = 0;
        foreach(string s in allResults)
        {
            SummaryButton button = Instantiate(summaryButton);
            button.transform.SetParent(container);
            button.transform.localPosition = new Vector3(0, (-1*id)*separation, 0);
            button.Init(this, s);
            all.Add(button);
            id++;
        }
    }
    float dest_y;
    public void SetResult(bool toRight)
    {
        if (done)
            return;

        SummaryButton button = all[0];
        button.SetResult(toRight);

        bool win = false;
        bool isCorrect = IsCorrect(button.field.text);
        if (isCorrect)
            okContainer.text += button.field.text + "\n";
        else
            wrongcontainer.text += button.field.text + "\n";

        print(button.field.text + "    isCorrect: " + isCorrect + " toRight:" + toRight);

        if ((isCorrect && toRight) || (!isCorrect && !toRight))
            win = true;

        if(win)
            Events.OnSoundFX("correctWord");
        else
            Events.OnSoundFX("mistakeWord");

        all.Remove(button);
        totalErased++;
        dest_y = totalErased * separation;
        if (all.Count <= 0)
            Done();
    }
   
    public bool IsCorrect(string text)
    {
        foreach (string s in summaryWords.used_okWords)
            if(text == s)
                return true;
        return false;
    }
    private void Update()
    {
        Vector3 pos = container.transform.localPosition;
        pos.y = dest_y;
        print(dest_y);
        container.transform.localPosition = Vector3.Lerp(container.transform.localPosition, pos, 0.05f) ;
    }
}
