using UnityEngine;
using System.Collections;

public class DolphinGame : MainClass
{
        
    private states lastState;
    public states state;
    public DolphinManager dolphinManager;
    public DolphinGameManager dolphinGameManager;
    public DolphinsAreasManager areasManager;
    public DolphinsLevelManager dolphinsLevelManager;
    
    public enum states
    {
        PAUSED,
        PLAYING,
        ENDED
    }

    static DolphinGame mInstance = null;

    public static DolphinGame Instance
    {
        get
        {
            if (mInstance == null)
            {
               // Debug.LogError("Algo llama a Game antes de inicializarse");
            }
            return mInstance;
        }
    }
	void Awake () {
        mInstance = this;
    }
    void Start  ()
    {
       // Data.Instance.wordsUsed.Shuffle();
        Events.OnGamePaused += OnGamePaused;
        Events.StartGame += StartGame;
        

        dolphinManager = GetComponent<DolphinManager>();
        dolphinManager.Init();
        dolphinGameManager = GetComponent<DolphinGameManager>();
        dolphinGameManager.Init();
        dolphinsLevelManager = GetComponent<DolphinsLevelManager>();
        areasManager = GetComponent<DolphinsAreasManager>();

        OnGamePaused(false);
    }
    void OnDestroy()
    {
        Events.OnGamePaused -= OnGamePaused;
        Events.StartGame -= StartGame;
    }
    
    void StartGame()
    {
        state = states.PLAYING;
    }
    void OnGameOver()
    {
        state = states.PAUSED;       
    }
    
    void OnGameRestart()
    {
        Data.Instance.LoadLevel("MainMenu", false); 
        OnGamePaused(false);
    }
    void OnGamePaused(bool paused)
    {
        if (paused)
        {
            lastState = state;
            Time.timeScale = 0;
            state = states.PAUSED;
        }
        else
        {
            state = lastState;
            Time.timeScale = 1;
        }
    }
    
}
