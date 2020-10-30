using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BridgeUI : MonoBehaviour
{
    public Text field;
    public Animation anim;
    public Animator tutorialAnim;
    private TextsBridge textsBridge;

    void Awake()
    {
        textsBridge = Data.Instance.GetComponent<TextsBridge>();
    }
    void Start()
    {       
        Events.OnOkWord += OnOkWord;
        Events.OnGameReady += OnGameReady;
        Events.OnTutorialReady += OnTutorialReady;

        //if (!Data.Instance.levelsManager.bridge_IntroPlayed)
        //{
        //    Data.Instance.levelsManager.bridges = 1;
        //    //pictureUI.gameObject.SetActive(false);
        //    Data.Instance.levelsManager.bridge_IntroPlayed = true;
        //    GetComponent<Intro>().Init();
        //}
        //else
        //{
            Invoke("DelayedOnTutorialReady", 0.1f);
       // }
    }
    public void Init(string title)
    {
        field.text = title;
    }
    void DelayedOnTutorialReady()
    {
        Events.OnTutorialReady();
    }
    void OnDestroy()
    {
        Events.OnOkWord -= OnOkWord;
        Events.OnGameReady -= OnGameReady;
        Events.OnTutorialReady -= OnTutorialReady;
    }
    void OnTutorialReady()
    {       
        Invoke("Delayed", 0.1f);
    }
    void Delayed()
    {
        anim.Play("uiStart");
        int vuelta = Data.Instance.levelsManager.bridges; 
    }
    void OnGameReady()
    {
        gameObject.SetActive(false);
    }
    private void OnOkWord(GameData.types type)
    {
        anim.Play("ok");
        Events.OnSoundFX("correctWord");
    }
    public void MainMenu()
    {
        Data.Instance.LoadLevel("MainMenu", false);
    }
}
