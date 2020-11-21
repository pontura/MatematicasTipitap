using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScreen : MonoBehaviour
{
    GameData gameData;
    public GameObject[] selva;
    public GameObject[] matematicas;
    public GameObject[] active;
    int id = 0;

    void Start()
    {        
        gameData = Data.Instance.routes.GetActualGame();
        Events.OnSceneLoaded();
        Init();
    }
    private void Init()
    {
        Reset();
        switch (gameData.type)
        {         
            case GameData.types.MATEMATICAS:
                active = matematicas;
                break;
            default:
                active = selva;
                break;
        }
        SetOn();
    }
    private void Reset()
    {
        foreach (GameObject go in selva)
            go.SetActive(false);
        foreach (GameObject go in matematicas)
            go.SetActive(false);
    }
    void SetOn()
    {
        active[id].SetActive(true);
    }
    public void Next()
    {
        id++;
        if (id >= active.Length)
            Events.OnLevelComplete(gameData.type, true);
        else
            SetOn();

        //Data.Instance.LoadLevel("Map", false);
    }
}
