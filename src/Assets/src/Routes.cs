using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Routes : MonoBehaviour {

    public int unlockedRoute;
    public int actualGameData;
    public int gameID;
    public int routeID;

    public List<GameData> route1;
    public List<GameData> route2;
    public List<GameData> route3;

    public List<GameData> route4;
    public List<GameData> route5;
    public List<GameData> route6;

    public List<GameData> route7;
    public List<GameData> route8;
    public List<GameData> route9;

    void Start()
    {
        Events.UnlockNextRoute += UnlockNextRoute;
        unlockedRoute = PlayerPrefs.GetInt("unlockedRoute", 1);
        //int id = 0;
        //foreach (GameData gameData in route1)
        //{
        //    if (PlayerPrefs.GetInt("level_1_" + id) == 1)
        //        gameData.perfect = true;
        //    id++;
        //}
        //id = 0;
        //foreach (GameData gameData in route2)
        //{
        //    if (PlayerPrefs.GetInt("level_2_" + id) == 1)
        //        gameData.perfect = true;
        //    id++;
        //}
        //id = 0;
        //foreach (GameData gameData in route3)
        //{
        //    if (PlayerPrefs.GetInt("level_3_" + id) == 1)
        //        gameData.perfect = true;
        //    id++;
        //}

        //id = 0;
        //foreach (GameData gameData in route4)
        //{
        //    if (PlayerPrefs.GetInt("level_4_" + id) == 1)
        //        gameData.perfect = true;
        //    id++;
        //}
        //id = 0;
        //foreach (GameData gameData in route5)
        //{
        //    if (PlayerPrefs.GetInt("level_5_" + id) == 1)
        //        gameData.perfect = true;
        //    id++;
        //}
        //id = 0;
        //foreach (GameData gameData in route6)
        //{
        //    if (PlayerPrefs.GetInt("level_6_" + id) == 1)
        //        gameData.perfect = true;
        //    id++;
        //}

        //id = 0;
        //foreach (GameData gameData in route7)
        //{
        //    if (PlayerPrefs.GetInt("level_7_" + id) == 1)
        //        gameData.perfect = true;
        //    id++;
        //}
        //id = 0;
        //foreach (GameData gameData in route8)
        //{
        //    if (PlayerPrefs.GetInt("level_8_" + id) == 1)
        //        gameData.perfect = true;
        //    id++;
        //}
        //id = 0;
        //foreach (GameData gameData in route9)
        //{
        //    if (PlayerPrefs.GetInt("level_9_" + id) == 1)   
        //        gameData.perfect = true;
        //    id++;
        //}
        Events.ResetApp += ResetApp;
    }
    void OnDestroy()
    {
        Events.ResetApp -= ResetApp;
        Events.UnlockNextRoute -= UnlockNextRoute;
    }
    void UnlockNextRoute()
    {
        unlockedRoute++;
        if(unlockedRoute<10)
            PlayerPrefs.SetInt("unlockedRoute", unlockedRoute);
    }
    void ResetApp()
    {
        unlockedRoute = 1;
        actualGameData = 0;
        ResetMap();

        foreach (GameData gameData in route1)
            gameData.perfect = false;
        foreach (GameData gameData in route2)
            gameData.perfect = false;
        foreach (GameData gameData in route3)
            gameData.perfect = false;

        foreach (GameData gameData in route4)
            gameData.perfect = false;
        foreach (GameData gameData in route5)
            gameData.perfect = false;
        foreach (GameData gameData in route6)
            gameData.perfect = false;

        foreach (GameData gameData in route7)
            gameData.perfect = false;
        foreach (GameData gameData in route8)
            gameData.perfect = false;
        foreach (GameData gameData in route9)
            gameData.perfect = false;
    }
    public void SetPerfect(bool isPerfect)
    {
        if (isPerfect)
        {
          //  print("New Perfect:   " + "level_" + routeID + "_" + gameID);
            PlayerPrefs.SetInt("level_" + routeID + "_" + gameID, 1);
            GetActualGame().perfect = isPerfect;
        }
    }
    public int GetTotalGamesInActualRoute()
    {
        return GetActualGamesInRoute().Count;
    }
    public List<GameData> GetActualGamesInRoute()
    {
        switch (routeID)
        {
            case 1: return route1;
            case 2: return route2;
            case 3: return route3;
            case 4: return route4;
            case 5: return route5;
            case 6: return route6;
            case 7: return route7;
            case 8: return route8;
            default: return route9;
        }
    }
    public void RouteSelected(int routeID)
    {
        gameID = Data.Instance.levelsManager.GetActualGame(routeID);
        this.routeID = routeID;
    }
    public void NextLevel()
    {
        gameID++;
    }
    public GameData GetActualGame()
    {
        switch (routeID)
        {
            case 1: return route1[gameID];
            case 2: return route2[gameID];
            case 3: return route3[gameID];
            case 4: return route4[gameID];
            case 5: return route5[gameID];
            case 6: return route6[gameID];
            case 7: return route7[gameID];
            case 8: return route8[gameID];
            default: return route9[gameID];
        }
    }
    public int GetTotalWordsOfActiveGame()
    {
        return GetActualGame().wordsQty;
    }
    public void ResetMap()
    {
        routeID = 0;
        gameID = 1;
    }
    public float GetTotalPerfectAmount()
    {
        int totalData = 0;
        int totalPerfect = 0;
        foreach (GameData data in route1 )
        {
            totalData++;
            if (data.perfect)
                totalPerfect++;
        }
        foreach (GameData data in route2)
        {
            totalData++;
            if (data.perfect)
                totalPerfect++;
        }
        foreach (GameData data in route3)
        {
            totalData++;
            if (data.perfect)
                totalPerfect++;
        }

        foreach (GameData data in route4)
        {
            totalData++;
            if (data.perfect)
                totalPerfect++;
        }
        foreach (GameData data in route5)
        {
            totalData++;
            if (data.perfect)
                totalPerfect++;
        }
        foreach (GameData data in route6)
        {
            totalData++;
            if (data.perfect)
                totalPerfect++;
        }

        foreach (GameData data in route7)
        {
            totalData++;
            if (data.perfect)
                totalPerfect++;
        }
        foreach (GameData data in route8)
        {
            totalData++;
            if (data.perfect)
                totalPerfect++;
        }
        foreach (GameData data in route9)
        {
            totalData++;
            if (data.perfect)
                totalPerfect++;
        }

        return (float)totalPerfect / (float)totalData;
    }
    public bool CheckIfAllPerfect()
    {
        foreach (GameData gd in route1)
            if (!gd.perfect) return false;
        foreach (GameData gd in route2)
            if (!gd.perfect) return false;
        foreach (GameData gd in route3)
            if (!gd.perfect) return false;
        foreach (GameData gd in route4)
            if (!gd.perfect) return false;
        foreach (GameData gd in route5)
            if (!gd.perfect) return false;
        foreach (GameData gd in route6)
            if (!gd.perfect) return false;
        foreach (GameData gd in route7)
            if (!gd.perfect) return false;
        foreach (GameData gd in route8)
            if (!gd.perfect) return false;
        foreach (GameData gd in route9)
            if (!gd.perfect) return false;
        return true;
    }
}
