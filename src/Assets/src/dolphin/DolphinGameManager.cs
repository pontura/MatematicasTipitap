using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DolphinGameManager : MonoBehaviour {
    
    public float distance;
    public int score;

    public states state;
    public enum states
    {
        TUTORIAL,
        ACTIVE,        
        ENDING,
        ENDED,
        FINISH
    }

    public float speed;
    public float realSpeed = 0.3f;

    public GameObject container;
    public DolphinManager dolphinManager;
    private DolphinsLevelManager levelsManager;
    public MainCamera camera;
    public List<BackgroundScrolleable> backgroundsScrolleable;
    public ParticleSystem explotion;

    private float SPEED_ACCELERATION = 0.008f;
    public float DEFAULT_SPEED = 0.09f;

    void Start()
    {
        foreach (BackgroundScrolleable go in backgroundsScrolleable)
            go.gameObject.SetActive(false); 

        if(Data.Instance.routes.routeID == 1)
        {
            backgroundsScrolleable[0].gameObject.gameObject.SetActive(true);
            backgroundsScrolleable[1].gameObject.gameObject.SetActive(true);
            backgroundsScrolleable[2].gameObject.gameObject.SetActive(true);
            backgroundsScrolleable[3].gameObject.gameObject.SetActive(true);
        }
        else if (Data.Instance.routes.routeID == 2)
        {
            backgroundsScrolleable[4].gameObject.gameObject.SetActive(true);
            backgroundsScrolleable[5].gameObject.gameObject.SetActive(true);
        } else
        {
            backgroundsScrolleable[6].gameObject.gameObject.SetActive(true);
            backgroundsScrolleable[7].gameObject.gameObject.SetActive(true);
        }
    }
    public void Init()
    {
        Events.StartGame += StartGame;
        Events.OnDolphinCrash += OnDolphinCrash;

        dolphinManager = GetComponent<DolphinManager>();
        dolphinManager.Init();

        levelsManager = GetComponent<DolphinsLevelManager>();
        levelsManager.Init();

        Events.OnStartCountDown();

        score = 0;

        camera.UpdatePosition(distance);
        dolphinManager.UpdatePosition(distance);

        if (PlayerPrefs.GetString("tutorialReady") != "true")
            DEFAULT_SPEED = 0.055f;

    }
    void OnDestroy()
    {
        Events.StartGame -= StartGame;
        Events.OnDolphinCrash -= OnDolphinCrash;
    }
    public void LevelComplete()
    {
        state = states.FINISH;
    }
    public void OnScoreAdd(int _score)
    {
        score += _score;
        Events.OnRefreshScore(score);
    }
    void StartGame()
    {
        speed = DEFAULT_SPEED;
        //state = states.ACTIVE;
        //Invoke("goOn", 0.2f);
        goOn();
        EachSecond();
    }
    void OnHeroDie()
    {
        if (state == states.FINISH) return;
        state = states.ENDING;
        DolphinGame.Instance.state = DolphinGame.states.ENDED;
        Events.OnSoundFX("warningPopUp");
     //   Events.OnMusicChange("gameOverTemp");              
    }
    void AllPooled()
    {
      //  Application.LoadLevel("04_Game");
    }
    void OnDolphinCrash()
    {
        realSpeed = 0f;
        Events.OnSoundFX("dolphinCrash");
    }
    void OnResetSpeed()
    {
        speed = DEFAULT_SPEED;
    }
    void goOn()
    {
        state = states.ACTIVE;
        Events.OnDolphinJump();
    }
    void EachSecond()
    {
        Invoke("EachSecond", 5);

        if (state == states.FINISH) return;

        DEFAULT_SPEED += (distance/1000)*SPEED_ACCELERATION;
    }
    void Update()
    {
       
        if (DolphinGame.Instance ==null ) return;

        if (DolphinGame.Instance.state == DolphinGame.states.ENDED)
        {
            realSpeed -= 0.001f;
            if (realSpeed <= 0)
                state = states.ENDED;
        }
        else
            realSpeed += 0.0005f;

        if (realSpeed > speed)
            realSpeed = speed;
        else if (realSpeed < 0)
            realSpeed = 0;

        float _speed = (realSpeed*100)*Time.smoothDeltaTime;
        distance += _speed;

        if (state != states.ENDED)
        {
            dolphinManager.UpdatePosition(distance);

            if (DolphinGame.Instance.state == DolphinGame.states.ENDED)
                return;

            if (state != states.FINISH)
            {
                foreach (BackgroundScrolleable bgScrolleable in backgroundsScrolleable)
                    bgScrolleable.UpdatePosition(distance, _speed);
                camera.UpdatePosition(distance);

                if (state != states.ENDING && state != states.TUTORIAL)
                    levelsManager.CheckForNewLevel(distance);
            }            
        }        
	}
    void OnExplotion()
    {
        if (state == states.FINISH) return;
        Dolphin dolphin = dolphinManager.dolphin;
        ParticleSystem ps = Instantiate(explotion) as ParticleSystem;
        ps.transform.SetParent(dolphin.transform.parent.transform);
        ps.transform.localScale = Vector3.one;
        Vector3 newPos = dolphin.transform.localPosition;
        newPos.y += 3;
        ps.transform.localPosition = newPos;
        ps.Play();
    }
}
