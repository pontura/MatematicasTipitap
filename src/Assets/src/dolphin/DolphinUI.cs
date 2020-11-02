using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DolphinUI : MonoBehaviour
{

    List<TextsDolphin.Vuelta> vuelta;

    public DolphinGameManager dolphinGameManager;
    public Image filledImage;

    public PhrasesUI UI_Phrases;
    public WordUI UI_Word;

    public int okWords;
    private IEnumerator ResetRoutine;
    public string title;
    public string toSay;
    public List<string> ok;
    public List<string> wrongWords;
    public DolphinGame dolphinGame;
    public Text wrongFeedback;
    public Text okFeedback;
    public List<string> doneOK;
    public List<string> doneWrong;
    private bool commitError;

    public states state;
    public enum states
    {
        PLAYING,
        WAITING
    }

    void Start()
    {
        wrongFeedback.text = "";
        okFeedback.text = "";

        Data.Instance.wordsUsed.Reset();
        filledImage.fillAmount = 0;
        okWords = 0;
        Events.OnGotWord += OnGotWord;
        Events.OnSayCorrectWord += OnSayCorrectWord;
        Events.OnSayCorrectWord_with_beep += OnSayCorrectWord_with_beep;
        Events.OnTutorialReady += OnTutorialReady;



        Invoke("DelayedOnTutorialReady", 0.2f);
        return;
        if (!Data.Instance.levelsManager.dolphin_IntroPlayed)
        {
            Data.Instance.levelsManager.dolphin_IntroPlayed = true;
            GetComponent<Intro>().Init();
        }
        else
        {
            Invoke("DelayedOnTutorialReady", 0.1f);
        }
    }
    void DelayedOnTutorialReady()
    {
        vuelta = Data.Instance.GetComponent<TextsDolphin>().texts.level_1;
        Data.Instance.GetComponent<TextsDolphin>().ShuffleArr(vuelta);

        GetComponent<Animation>().Stop();
        GetComponent<Animation>().enabled = false;
        Events.OnTutorialReady();
    }
    void OnTutorialReady()
    {
        Data.Instance.wordsUsed.HackToFillDulphinWords();
        Events.StartGame();
        Invoke("GetNextWord", 1);
    }

    public void GetNextWord()
    {
        state = states.PLAYING;
        title = "";

        UI_Phrases.gameObject.SetActive(true);
        UI_Word.gameObject.SetActive(false);
        ok = vuelta[okWords].ok;
        title = vuelta[okWords].title;
        wrongWords = vuelta[okWords].wrong;

        UI_Phrases.Init(title);  
    }
    void OnDestroy()
    {
        Events.OnTutorialReady -= OnTutorialReady;
        Events.OnGotWord -= OnGotWord;
        Events.OnSayCorrectWord -= OnSayCorrectWord;
        Events.OnSayCorrectWord_with_beep -= OnSayCorrectWord_with_beep;
    }

    bool IsOk(string word)
    {
        foreach (string s in ok)
            if (s == word)
                return true;
        return false;
    }
    private void OnGotWord(string text)
    {
        if (ResetRoutine != null)
            StopCoroutine(ResetRoutine);

        if (!IsOk(text))
        {
            Data.Instance.GetComponent<SummaryWords>().Add_UsedWrongWords(text);
            doneWrong.Add(text);
            commitError = true;
            UI_Phrases.GetComponent<Animation>().Play("wrong");

            StartCoroutine(Next());
            Events.OnSoundFX("mistakeWord");
        }
        else
        {
            
            GetOkWord(text);
        }
        SetFields();
    }
    public void GetOkWord(string text)
    {
        okWords++;
        if (text != "")
        {

            if(Random.Range(0,10)<4)
                Data.Instance.GetComponent<SummaryWords>().Add_UsedOkWord(ok[0]);

            doneOK.Add(text);
            UI_Phrases.GetComponent<Animation>().Play("ok");

            Events.OnSoundFX("correctWord");
            //Events.OnVoiceSayFromList("felicitaciones", 0.5f);
        } else
            Data.Instance.GetComponent<SummaryWords>().Add_UsedOkWord(ok[0]);

        if (okWords >= vuelta.Count)
        {
            dolphinGameManager.LevelComplete();
            LevelComplete();
        }
        else
            StartCoroutine(Next());

        Events.OnOkWord(GameData.types.DOLPHIN);


        float totalWordsUsed = (float)Data.Instance.wordsUsed.words.Count;

        float progress = okWords / totalWordsUsed;
        filledImage.fillAmount = progress;

        print("________________totalWordsUsed: " + totalWordsUsed + " okWords: " + okWords + " progress: " + progress);
    }
    void SetFields()
    {
        okFeedback.text = "";
        wrongFeedback.text = "";
        int id = 0;
        foreach(string s in doneOK)
        {
            if(id >0)
                okFeedback.text += ",";
            okFeedback.text += s;
            id++;
        }
        id = 0;
        foreach (string s in doneWrong)
        {
            if (id > 0)
                wrongFeedback.text += ",";
            wrongFeedback.text += s;
            id++;
        }
    }
    IEnumerator Next()
    {
        state = states.WAITING;
        yield return new WaitForSeconds(2.1f);

        if (okWords >= Data.Instance.wordsUsed.words.Count)
        {
            yield return new WaitForSeconds(2f);    
            LevelComplete();
        }
        else
        {
            //UI_Phrases.gameObject.SetActive(false);
            UI_Word.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            GetNextWord();
        }
    }
    void OnSayCorrectWord_with_beep()
    {
        print("__________OnSayCorrectWord " + toSay.ToLower());
        Events.OnVoiceSay("palabras/" + toSay.ToLower() + "BEEP");
    }
    void OnSayCorrectWord()
    {
		//print("__________OnSayCorrectWord " + toSay.ToLower());
       // Events.OnVoiceSay("palabras/" + toSay.ToLower());
		
		//if(UI_Pictures.gameObject)
			//UI_Pictures.Init(toSay);
    }
    void LevelComplete()
    {
        if (!commitError)
            Events.OnPerfect();
        else
            Events.OnGood();

        Data.Instance.GetComponent<SummaryWords>().OnLevelComplete(GameData.types.DOLPHIN, commitError);

        Data.Instance.wordsUsed.Empty();
    }
    public void MainMenu()
    {
        Data.Instance.LoadLevel("MainMenu", false);
    }
}
