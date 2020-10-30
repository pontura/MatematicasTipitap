using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FroggerController : MonoBehaviour {

    public states state;
    public enum states
    {
        INTRO,
        IDLE,
        JUMPING,
        DEAD,
        READY
    }
    public GameObject world;
    public float _x;
    private float distance = 4;
    public int activePanelID;
    public FroggerCamera cam;
    public GameObject characters;
    private float jumpSpeed = 15f;
    public GameObject snapperPlatform;
    private GameObject startingSnapperPlatform;
    private AvatarsManager avatarsManager;
    public FroggerPanel[] panels;
    public bool commitError;
    public ParticleSystem particles;

    public GameObject cascadaPattern;
    public GameObject cascadaContainer;

    public void StartGame()
    {
        state = states.IDLE;
    }
    public void Init()
    {
        avatarsManager = GetComponent<AvatarsManager>();
        avatarsManager.Idle();
        startingSnapperPlatform = snapperPlatform;
        activePanelID = 0;
        Invoke("Delayed", 0.1f);
    }
    List<int> usedIds;
    int GetVueltaByControlledRandom(List<TextsFrogger.Vuelta> vuelta)
    {
        bool exists = false;
        int id = Random.Range(0, vuelta.Count);
        foreach (int idVuelta in usedIds)
            if (id == idVuelta) exists = true;
        if (exists)
            return GetVueltaByControlledRandom(vuelta);

        usedIds.Add(id);
        return id;
    }
    void Delayed()
    {
        usedIds = new List<int>();
        panels = GetComponent<FroggerGame>().panels.GetComponentsInChildren<FroggerPanel>();
        int id = 0;
        foreach (FroggerPanel panel in panels)
        {
            id++;
            if (id < panels.Length)
                AddCascada();

            if (panel.hasWords)
            {
                List<TextsFrogger.Vuelta> vueltas = null;

                switch (Data.Instance.routes.routeID)
                {
                    case 1: vueltas = Data.Instance.GetComponent<TextsFrogger>().vueltas1; break;
                    case 4: vueltas = Data.Instance.GetComponent<TextsFrogger>().vueltas1; break;
                    case 7: vueltas = Data.Instance.GetComponent<TextsFrogger>().vueltas1; break;

                    case 5: vueltas = Data.Instance.GetComponent<TextsFrogger>().vueltas2; break;
                    case 8: vueltas = Data.Instance.GetComponent<TextsFrogger>().vueltas2; break;
                    case 2: vueltas = Data.Instance.GetComponent<TextsFrogger>().vueltas2; break;

                    default: vueltas = Data.Instance.GetComponent<TextsFrogger>().vueltas3; break;
                }
                TextsFrogger.Vuelta vuelta = vueltas[GetVueltaByControlledRandom(vueltas)];

                List<string> newList = new List<string>();
                Utils.ShuffleListTexts(vuelta.wrong);

                if (Random.Range(0, 10) < 5)
                {
                    newList.Add(vuelta.ok);
                    newList.Add(vuelta.wrong[0]);
                }
                else
                {
                    newList.Add(vuelta.wrong[0]);
                    newList.Add(vuelta.ok);                    
                }
                newList.Add(vuelta.wrong[1]);
                if (Random.Range(0, 10) < 5)
                {
                    newList.Add(vuelta.ok);
                    newList.Add(vuelta.wrong[2]);
                }
                else
                {
                    newList.Add(vuelta.wrong[2]);
                    newList.Add(vuelta.ok);
                }                
                int wordID = 0;
                foreach (FroggerSignal signal in panel.GetComponentsInChildren<FroggerSignal>())
                {
                    bool isCorrect = false;
                    if (newList[wordID] == vuelta.ok) isCorrect = true;
                    signal.Init(newList[wordID], isCorrect, vuelta.id);
                    wordID++;
                }
            }
        }
    }
    int cascadaNum;
    float cascadaWidth = 7.9f;
    bool addCascadaPair;
    void AddCascada()
    {
        addCascadaPair = !addCascadaPair;
        if (!addCascadaPair) return;
        GameObject cascada = Instantiate(cascadaPattern);
        cascada.transform.SetParent(cascadaContainer.transform);
        cascada.transform.localScale = Vector2.one;
        cascada.transform.localPosition = new Vector2(cascadaWidth * cascadaNum, 0);
        cascadaNum++;
    }
    public void Jump()
    {
        if (state != states.IDLE) return;
        state = states.JUMPING;
        activePanelID++;
        Events.OnJump();
        Events.ResetTimeToSayNotPlaying();

        cam.SetX(activePanelID * distance);
        GetNewSnap();
    }
	void Update () {
        if (state == states.DEAD) return;
        if (state == states.INTRO) return;
        //if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
        //{
           
        //}
        if (state == states.JUMPING)
        {
            _x = characters.transform.localPosition.x + (Time.deltaTime * jumpSpeed);
            if (characters.transform.localPosition.x >= activePanelID * distance)
            {
                _x = activePanelID * distance;
                if (activePanelID == panels.Length)
                {
                    state = states.READY;
                    Events.OnVoiceSayFromList("felicitaciones", 0);
                    Invoke("LevelComplete", 3);
                }
                else
                    state = states.IDLE;
            }
        }
        characters.transform.localPosition = new Vector2(_x, snapperPlatform.transform.localPosition.y);
	}
    void LevelComplete()
    {
        if (!commitError)
            Events.OnPerfect();
        else
            Events.OnGood();

        Events.OnLevelComplete(GameData.types.FROGGER, commitError);
    }
    void GetNewSnap()
    {
        float _lastY = 10;
        int id = 0;
        foreach (FroggerPanel panel in panels)
        {
            id++;
            panel.SetInactive();

            if (id == activePanelID)
            {
                print(id + " activePanelID: " + activePanelID);
                panel.SetActive();
                foreach (FroggerSignal signal in panel.GetComponentsInChildren<FroggerSignal>())
                {
                    float _y = Mathf.Abs(characters.transform.localPosition.y - signal.transform.localPosition.y);
                    if (_y < _lastY)
                    {
                        snapperPlatform = signal.gameObject;
                        _lastY = _y;
                        avatarsManager.Jump();
                    }
                }
            }
        }
        if(panels[activePanelID-1].hasWords)
            Invoke("CheckWord", 0.32f);
       
        
    }
    void CheckWord()
    {
        FroggerSignal signal = snapperPlatform.GetComponent<FroggerSignal>();
        bool isCorrect = signal.isOk;
        if (isCorrect)
        {
            particles.Play();
            signal.CatchWord();
            Events.OnOkWord(GameData.types.FROGGER);
          //  Events.OnAddWordToList(GameData.types.FROGGER, signal.vueltaID);
        }
        else
        {
            commitError = true;
            signal.Break();
            snapperPlatform = null;
            state = states.DEAD;
            characters.transform.localPosition = new Vector3(-100, 0, 0);
            Invoke("Revive", 3);
        }
        Events.OnGotWord(signal.word);
    }
    void Revive()
    {        
        _x = 0;
        cam.SetX(_x);
        snapperPlatform = startingSnapperPlatform;
        activePanelID = 0;
        state = states.IDLE;
    }

}
