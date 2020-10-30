using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FroggerUI : MonoBehaviour
{
    public WordUI ui;
    public PicturesUI pictureUI;
    private IEnumerator ResetRoutine;
    private string ok;
    public int secs;
    public Animation anim;
    public TextsFrogger texts;
    private string ok_terminacion;
    public FroggerController controller;
    public Animation tutorialAnim;
    public Animation tutorialAnim2;

    void Start()
    {
        pictureUI.gameObject.SetActive(false);
        texts = Data.Instance.GetComponent<TextsFrogger>();

        Events.OnGotWord += OnGotWord;
        Events.OnSayCorrectWord += OnSayCorrectWord;
        Events.OnSayCorrectWordReal += OnSayCorrectWordReal;
        Events.OnTutorialReady += OnTutorialReady;

        ui.gameObject.SetActive(true);

        if (!Data.Instance.levelsManager.frogger_IntroPlayed)
        {
            //ui.gameObject.SetActive(false);
            Data.Instance.levelsManager.frogger_IntroPlayed = true;
            GetComponent<Intro>().Init();
        }
        else
        {
            StartCoroutine(WaitUntilReady());
            Invoke("DelayedOnTutorialReady", 0.1f);
        }
        
    }
    public void Jump()
    {
        controller.Jump();
    }
    void DelayedOnTutorialReady()
    {
        Events.OnTutorialReady();
    }
    void OnTutorialReady()
    {
        tutorialAnim2.Stop();
        tutorialAnim.Stop();
        tutorialAnim.enabled = false;
        StartCoroutine(WaitUntilReady());
        GetComponent<Intro>().panel.SetActive(false);
    }
    IEnumerator WaitUntilReady()
    {
        while (texts.GetVueltaByLevel() == null)
            yield return new WaitForEndOfFrame();
        Init();
    }
    void Init()
    {
        pictureUI.gameObject.SetActive(false);
        TextsFrogger.Vuelta vuelta = texts.GetVueltaByLevel();
        ok = vuelta.ok;
        ok_terminacion = vuelta.terminacion;
        ReplaceOkWordWith(ok_terminacion);
    }
    void OnDestroy()
    {
        Events.OnGotWord -= OnGotWord;
        Events.OnSayCorrectWord -= OnSayCorrectWord;
        Events.OnSayCorrectWordReal -= OnSayCorrectWordReal;
        Events.OnTutorialReady -= OnTutorialReady;
    }
    void ResetWord()
    {
        pictureUI.gameObject.SetActive(false);
        ReplaceOkWordWith(ok_terminacion);
    }
    private string lastWord;
    bool wasWrong;
    private void OnGotWord(string text)
    {
        wasWrong = false;
        string terminacion = text.Substring(text.Length - 2);

        print(terminacion + " OnGotWord   " + text + " ok_terminacion: " + ok_terminacion);

        if (ResetRoutine != null)
            StopCoroutine(ResetRoutine);

        ReplaceOkWordWith(text);

        if (terminacion.ToUpper() != ok_terminacion)
        {           
            if (ResetRoutine != null) StopCoroutine(ResetRoutine);
            wasWrong = true;
            ResetRoutine = ResetNextWord();
            StartCoroutine(ResetRoutine);
            anim.Play("wrong");
            Events.OnSoundFX("Splash");
            Events.OnAddWrongWord(text);
        }
        else
        {
            this.lastWord = text;
            anim.Play("ok");
            Events.OnSoundFX("correctWord");
            Events.OnAddFinalWordToList(GameData.types.FROGGER, text);
            pictureUI.gameObject.SetActive(true);
            pictureUI.Init(text);
            ResetRoutine = ResetNextWord();
            StartCoroutine(ResetRoutine);
        }
    }
    void OnSayCorrectWord()
    {
        if (lastWord != "")
        Events.OnVoiceSay("terminaciones/" + ok_terminacion);
    }
    void OnSayCorrectWordReal()
    {
        if (lastWord != "")
            Events.OnVoiceSay("palabras/" + lastWord);
    }
    IEnumerator ResetNextWord()
    {
        yield return new WaitForSeconds(0.2f);
        if (!wasWrong)
            OnSayCorrectWordReal();
        yield return new WaitForSeconds(2);
        ResetWord();
        anim.Play("uiStart");
    }
    private void ReplaceOkWordWith(string NewWord)
    {
        ui.Init(NewWord.ToUpper());
    }
    public void MainMenu()
    {
        Data.Instance.LoadLevel("MainMenu", false);
    }
}
