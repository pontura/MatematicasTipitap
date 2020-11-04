using UnityEngine;
using System.Collections;

public class LevelsManager : MonoBehaviour {

    public int[] levelsProgress;
    public int vueltas = 0;

    public int monkeys = 0;
    public int frogger = 0;
    public int bridges = 0;

    public bool introPlayed;
    public bool frogger_IntroPlayed;
    public bool monkey_IntroPlayed;
    public bool bridge_IntroPlayed;
    public bool dolphin_IntroPlayed;

    void Start()
    {
        monkeys = PlayerPrefs.GetInt("monkeys", 0);
        frogger = PlayerPrefs.GetInt("frogger", 0);
        bridges = PlayerPrefs.GetInt("bridges", 0);

        if (PlayerPrefs.GetInt("frogger_IntroPlayed") == 1) frogger_IntroPlayed = true;
        if (PlayerPrefs.GetInt("monkey_IntroPlayed") == 1)  monkey_IntroPlayed = true;
        if (PlayerPrefs.GetInt("bridge_IntroPlayed") == 1)  bridge_IntroPlayed = true;
        

        Events.OnLevelComplete += OnLevelComplete;
        Events.OnOkWord += OnOkWord;
        Events.ResetApp += ResetApp;
        Events.DebugWinLevel += DebugWinLevel;

        int unlockedRoute = PlayerPrefs.GetInt("unlockedRoute", 1);
        if(unlockedRoute>1)
        {
            introPlayed = true;
            frogger_IntroPlayed = true;
            monkey_IntroPlayed = true;
            bridge_IntroPlayed = true;
            dolphin_IntroPlayed = true;
        }
        LoadSavedProgress();
    }
    void OnDestroy()
    {
        Events.OnLevelComplete -= OnLevelComplete;
        Events.OnOkWord -= OnOkWord;
        Events.ResetApp -= ResetApp;
        Events.DebugWinLevel -= DebugWinLevel;
    }
    public void RouteSelected(int routeID)
    {
        switch (routeID)
        {
            case 1: bridges = 1; monkeys = 0; break;
            case 2: bridges = 8; monkeys = 8; break;
            case 3: bridges = 16; monkeys = 16; break;
            case 4: bridges = 22; monkeys = 22; break;
            case 5: bridges = 0; monkeys = 2; break;
            case 6: bridges = 7; monkeys = 10; break;
            case 7: bridges = 15; monkeys = 18; break;
            case 8: bridges = 21; monkeys = 26; break;
            case 9: bridges = 29; monkeys = 4; break;
        }
        if(levelsProgress[routeID-1] >=3)
        {
            bridges += 4;
            monkeys += 4;
            if (bridges > 29) bridges = bridges - 30;
            if (monkeys > 29) monkeys = monkeys - 30;
        }
        print("_______bridges: " + bridges + " monkeys: " + monkeys);
        
        GetComponent<WordsUsed>().RutaSelected(routeID);
    }
    void ResetApp()
    {
        vueltas = 0;
        monkeys = 0;
        frogger = 0;
        bridges = 0;

        introPlayed = false;
        frogger_IntroPlayed = false;
        bridge_IntroPlayed = false;
        dolphin_IntroPlayed = false;
        monkey_IntroPlayed = false;

        for(int a = 0; a<9; a++)
        {
            levelsProgress[a] = 0;
        }
    }
    void OnOkWord(GameData.types type)
    {
        vueltas++;
        switch (type)
        {
            case GameData.types.MONKEY: 
                monkeys++;
                //if (monkeys >= GetComponent<TextsMonkeys>().vueltas.Count)
                //    monkeys = 0;
                PlayerPrefs.SetInt("monkeys", monkeys);
                PlayerPrefs.SetInt("monkey_IntroPlayed", 1);
                break;
            case GameData.types.FROGGER:
               // frogger++;
               // if (frogger >= GetComponent<TextsFrogger>().vueltas.Count)
                 //   frogger = 0;
                PlayerPrefs.SetInt("frogger", frogger);
                PlayerPrefs.SetInt("frogger_IntroPlayed", 1);
                break;
            case GameData.types.BRIDGE: 
                bridges++;
                //if (bridges >= GetComponent<TextsBridge>().vueltas.Count)
                //    bridges = 0;
                PlayerPrefs.SetInt("bridges", bridges);
                PlayerPrefs.SetInt("bridge_IntroPlayed", 1);
                break;
            case GameData.types.DOLPHIN: 
                break;
        }
    }
    void OnLevelComplete(GameData.types type, bool commitError)
    {
        AddProgressToLevel();
        //if (commitError)
        //{
        //    if(Random.Range(0,10)<5)
        //        Events.OnSoundFX("Luisa NO nivel completado");
        //    else
        //        Events.OnSoundFX("Daniel NO nivel completado");
        //}
        //else
        //{
        //    if (Random.Range(0, 10) < 5)
        //        Events.OnSoundFX("Luisa nivel completado");
        //    else
        //        Events.OnSoundFX("Daniel nivel completado");
        //}
        Data.Instance.routes.SetPerfect(!commitError);
        vueltas = 0;

        if (Data.Instance.routes.gameID+1 >= Data.Instance.routes.GetTotalGamesInActualRoute())
            EndSceneOn();
        else
        {
            Data.Instance.routes.NextLevel();
            LoadNextGame(true);
        }
    }
    void DebugWinLevel()
    {
        Data.Instance.routes.gameID = 6;
        OnLevelComplete(GameData.types.DOLPHIN, true);
    }
    public void LoadNextGame(bool showMap)
    {
        Events.OnMusic("marimba");
       GameData gameData =  Data.Instance.routes.GetActualGame();
      //// bool showMap = true;
      // if (Data.Instance.routes.gameID == 0)
      //     showMap = false;

       switch(gameData.type)
       {
           case GameData.types.BRIDGE:
                   Data.Instance.LoadLevel("Game_Bridge", showMap); 
               break;
           case GameData.types.DOLPHIN:
                   Data.Instance.LoadLevel("Game_Dolphin", showMap); 
               break;
           case GameData.types.MONKEY:
                   Data.Instance.LoadLevel("Game_Monkey", showMap); 
               break;
           case GameData.types.FROGGER:
                   Data.Instance.LoadLevel("Game_Frogger", showMap); 
               break;
       }
    }
    void EndSceneOn()
    {
        int routeID = Data.Instance.routes.routeID;
        switch (routeID)
        {
            case 4: routeID = 1; break;
            case 5: routeID = 2; break;
            case 6: routeID = 3; break;
            case 7: routeID = 1; break;
            case 8: routeID = 2; break;
            case 9: routeID = 3; break;
        }
        Data.Instance.LoadLevel("Ending" + routeID, true);
    }
    void AddProgressToLevel()
    {
        int routeID = Data.Instance.routes.routeID - 1;
        levelsProgress[routeID]++;

        if (levelsProgress[routeID] == 7)
            levelsProgress[routeID] = 0;

        PlayerPrefs.SetInt("levelsProgress_" + routeID, levelsProgress[routeID]);
    }
    void LoadSavedProgress()
    {
        for (int a = 0; a < 9; a++)
        {
            levelsProgress[a] = PlayerPrefs.GetInt("levelsProgress_" + a, 0);
        }
    }
    public int GetActualGame(int routeID)
    {
        print(routeID);
        return levelsProgress[routeID - 1];
    }
}
