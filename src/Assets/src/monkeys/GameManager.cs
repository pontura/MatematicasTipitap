using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class GameManager : MainClass
{
    int currentBananaThrownIndex;
    public SlingShot slingshot;
    [HideInInspector]
    public static GameState CurrentGameState = GameState.Start;

    public GameObject bird;

   // private List<GameObject> Bricks;
    public List<GameObject> Bananas;
    private List<GameObject> Monkeys;

    public Animator tutorialAnim;

    // Use this for initialization
    bool started;
   void Start()
    {
        Events.OnTutorialReady += OnTutorialReady;
        Events.OnLevelComplete += OnLevelComplete;
       // Invoke("OnTutorialReady", 0.1f);
    }
   void OnDestroy()
   {
       Events.OnTutorialReady -= OnTutorialReady;
        Events.OnLevelComplete -= OnLevelComplete;
    }
    void OnLevelComplete(GameData.types t, bool a)
    {
        started = false;
        Invoke("DelayReady", 1);
    }
    void DelayReady()
    {
        transform.localPosition = new Vector3(1000, 0, 0);
    }
   void OnTutorialReady()
    {
        //tutorialAnim.Stop();
        slingshot.OnTutorialReady();
        CurrentGameState = GameState.Start;
        slingshot.enabled = false;
        Monkeys = new List<GameObject>(GameObject.FindGameObjectsWithTag("Monkey"));
        slingshot.BananaThrown -= Slingshot_BananaThrown; slingshot.BananaThrown += Slingshot_BananaThrown;

        AnimateBananaThrownToSlingshot();
        started = true;
        GetComponent<Animator>().enabled = false;
        Events.AvatarsIdle();
    }
    // Update is called once per frame
    void Update()
    {
        if (!started) return;
        switch (CurrentGameState)
        {
            case GameState.Start:
                if (Input.GetMouseButtonUp(0))
                {
                    AnimateBananaThrownToSlingshot();
                }
                break;
            case GameState.BananaThrownMovingToSlingshot:
                //do nothing
                break;
            case GameState.Playing:
                if (slingshot.slingshotState == SlingshotState.BananaFlying &&
                    (BricksBananasPigsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 2f))
                {
                    slingshot.enabled = false;
                   // AnimateCameraToStartPosition();
                    CurrentGameState = GameState.BananaThrownMovingToSlingshot;
                    ShootAgain();
                }
                break;
            case GameState.Won:
            case GameState.Lost:
                if (Input.GetMouseButtonUp(0))
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
                break;
            default:
                break;
        }
    }


    private bool AllPigsDestroyed()
    {
        return Monkeys.All(x => x == null);
    }


    void ShootAgain()
    {
        Events.MonkeysShoot();
        slingshot.slingshotState = SlingshotState.Idle;
        AnimateBananaThrownToSlingshot();
        return;
        if (AllPigsDestroyed())
        {
            CurrentGameState = GameState.Won;
        }
        else if (currentBananaThrownIndex == Bananas.Count - 1)
        {
            CurrentGameState = GameState.Lost;
        }
        else
        {
            
            currentBananaThrownIndex++;
            AnimateBananaThrownToSlingshot();
        }
    }

    void AnimateBananaThrownToSlingshot()
    {
        Events.ResetTimeToSayNotPlaying();
        GameObject newBananaThrown = Instantiate(bird);
        newBananaThrown.transform.localPosition = new Vector3(0, 0, -1);
        Bananas.Add(newBananaThrown);
        CurrentGameState = GameState.BananaThrownMovingToSlingshot;
        newBananaThrown.transform.positionTo
            (Vector2.Distance(newBananaThrown.transform.position / 10,
            slingshot.BananaWaitPosition.transform.position) / 10, //duration
            slingshot.BananaWaitPosition.transform.position). //final position
                setOnCompleteHandler((x) =>
                        {
                            x.complete();
                            x.destroy(); //destroy the animation
                            CurrentGameState = GameState.Playing;
                            slingshot.enabled = true; //enable slingshot
                            //current bird is the current in the list
                            slingshot.BananaToThrow = newBananaThrown;
                        });
    }
    private void Slingshot_BananaThrown(object sender, System.EventArgs e)
    {
    }
    bool BricksBananasPigsStoppedMoving()
    {
        return true;
    }

}
