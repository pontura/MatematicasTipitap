using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BridgeGame : MainClass
{
    List<TextsBridge.Vuelta> vuelta;

    public BridgeScenary bridgeScenary;
    public float bridgeScenary_x_separation;
    public BridgeScenary bridgeScenaryActive;
    public Transform bridgeScenaryActiveTransform;
    public Transform slotsContainer;
    public Transform itemsContainer;
    public Transform gameContainer;
    public Camera cam;
    public float cam_speed;

    public states state;
    public enum states
    {
        LOADING,
        INTRO,
        PLAYING,
        READY,
        JUMPING,
        SCROLL
    }
    public Slot slot;

    public BridgeItem item;

    public int addLetters;

    public List<string> ok;
    private AvatarsManager avatarsManager;
    private float speedJump = 7;
    private float timeBetweenJumps = 0.6f;
    private bool commitError;
    public int wordID;
    private float _x = -5.4f;
    public BridgeUI ui;
    public Animation anim;

    void Start()
    {
        avatarsManager = GetComponent<AvatarsManager>();
        switch (Data.Instance.userdata.dificult)
        {
            case UserData.dificults.EASY:
                addLetters = 0; break;
            case UserData.dificults.MEDIUM:
                addLetters = 1; break;
            case UserData.dificults.HARD:
                addLetters = 2; break;
        }
        AddNewScene();
        GetComponent<DragManager>().enabled = true;
        Invoke("DelayedOnTutorialReady", 0.2f);
        return;

        GetComponent<DragManager>().enabled = false;
        Events.OnTutorialReady += OnTutorialReady;
    }
    void DelayedOnTutorialReady()
    {
        if(Data.Instance.routes.routeID == 1)
            vuelta = Data.Instance.GetComponent<TextsBridge>().texts.level_1;
        else
            vuelta = Data.Instance.GetComponent<TextsBridge>().texts.level_2;
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().enabled = false;
       // Events.OnTutorialReady();
        Invoke("Restart", 0.1f);
    }
    void OnDestroy()
    {
        Events.OnTutorialReady -= OnTutorialReady;
    }
    void OnTutorialReady()
    {
        GetComponent<DragManager>().enabled = true;
        anim.enabled = false;
    }
    void AddNewScene()
    {
        bridgeScenaryActive = Instantiate(bridgeScenary);
        bridgeScenaryActive.transform.SetParent(gameContainer);
        bridgeScenaryActive.transform.position = new Vector2(wordID * bridgeScenary_x_separation, 0);
    }
    void Restart()
    {
        int vueltaID = 0;
        TextsBridge.Vuelta vuelta = Data.Instance.GetComponent<TextsBridge>().texts.level_1[vueltaID];
        avatarsManager.Walk();
        Delayed();
        ui.Init(vuelta.title);
    }
    
    void Delayed()
    {
        List<string> wordsToUse = new List<string>();
        state = states.INTRO;
        wordsToUse.Clear();

        slotsContainer = bridgeScenaryActive.SlotContainer;
        itemsContainer = bridgeScenaryActive.itemsContainer;
        ok = vuelta[wordID].ok;
        int totalSlots = ok.Count;
        float separation = 9 / (float)totalSlots;
        float scales = 0.8f;
        for (int a = 0; a < totalSlots; a++)
        {
            Slot newSlot = Instantiate(slot);
            newSlot.transform.SetParent(slotsContainer);
            newSlot.transform.localPosition = new Vector2(separation * a, 0);
            newSlot.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 6) - 3);
            newSlot.transform.localScale = new Vector3(scales, scales, scales);
            newSlot.id = a + 1;
            wordsToUse.Add(ok[a].ToString());
        }

        //for (int a = 0; a < addLetters; a++)
        //    wordsToUse.Add(letters[Random.Range(0, letters.Length)]);

        Utils.ShuffleListTexts(wordsToUse);

        //separation = 9 / (float)wordsToUse.Count;

        for (int a = 0; a < wordsToUse.Count; a++)
        {
            BridgeItem newItem = Instantiate(item);
            newItem.transform.SetParent(itemsContainer);
            newItem.transform.localPosition = new Vector2(separation * a, 0);
            newItem.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 10) - 5);
            newItem.SetActualSize();
            newItem.id = a + 1;
            newItem.Init(wordsToUse[a]);
        }
    }
    public bool CheckToFill(BridgeItem item)
    {
        
        float dist = 1000;
        Slot snapSlot = null;
        foreach (Slot slot in bridgeScenaryActive.SlotContainer.GetComponentsInChildren<Slot>())
        {

            float newDist = Vector2.Distance(slot.transform.position, item.transform.position);
           // print(newDist + " " + slot.letter);
            if (newDist < 1 && (slot.letter == null || slot.letter == ""))
            {
                dist = newDist;
                snapSlot = slot;
            }
        }

        if (snapSlot)
        {
            item.slotAttached = snapSlot;
            item.transform.position = snapSlot.transform.position;
            item.transform.localEulerAngles = snapSlot.transform.localEulerAngles;
            snapSlot.SetLetter(item.letter);
            CheckGameStatus();
            return true;
        }
        else
            return false;
    }
    void StartGame()
    {
        Events.ResetTimeToSayNotPlaying();
        state = states.PLAYING;
        avatarsManager.Idle();
    }

    void CheckGameStatus()
    {
        bool wrong = false;
        int id = 0;
        int totalWordsInGame = 0;
        foreach (Slot slot in bridgeScenaryActive.SlotContainer.GetComponentsInChildren<Slot>())
        {
            print("slot.letter " + slot.letter);
            if (slot.letter != "")
                totalWordsInGame++;
            else
                return;
            if (slot.letter != ok[id])
            {
                //GetItemInSlot(slot).SetWrong(true);
                wrong = true;               
            }
            id++;
        }
        if(totalWordsInGame == ok.Count && wrong)
        {
            Events.OnSoundFX("mistakeWord");
            
            return;
        }

        Events.OnAddWordToList(GameData.types.BRIDGE, Data.Instance.levelsManager.bridges);

        wordID++;
        Events.OnOkWord(GameData.types.BRIDGE);

        StartCoroutine(Jumps());
        return;
        
        //if (totalWordsInGame == word.Length)
        //{
        //    if (wrong)
        //    {
        //        wrongWordDone = true;
        //        commitError = true;
                
        //    }
        //    id = 0;
        //    foreach (Slot slot in bridgeScenaryActive.SlotContainer.GetComponentsInChildren<Slot>())
        //    {
        //        if (slot.letter != "" + word[id])
        //        {
        //            GetItemInSlot(slot).SetWrong(true);
        //        }
        //        id++;
        //    }
        //}
    }
    void TakeItemOutFromSlot(Slot slot)
    {
        foreach (BridgeItem item in bridgeScenaryActive.itemsContainer.GetComponentsInChildren<BridgeItem>())
        {
            if (item.slotAttached && item.slotAttached.id == slot.id)
            {
                slot.EmptyLetters();
                item.slotAttached = null;
                item.transform.position = item.originalPosition;
            }
        }
    }
    private BridgeItem GetItemInSlot(Slot slot)
    {
        foreach (BridgeItem item in bridgeScenaryActive.itemsContainer.GetComponentsInChildren<BridgeItem>())
        {
            if (item.slotAttached && item.slotAttached.id == slot.id)
            {
                return item;
            }
        }
        return null;
    }

    IEnumerator Jumps()
    {
        yield return new WaitForSeconds(0.5f);
        state = states.JUMPING;
        foreach (Slot slot in bridgeScenaryActive.SlotContainer.GetComponentsInChildren<Slot>())
        {
            _x = slot.transform.position.x - 0.4f;
            print(_x);
            avatarsManager.Jump();
            yield return new WaitForSeconds(timeBetweenJumps);
        }
        _x += 1.7f;
        avatarsManager.Jump();

        if (Data.Instance.levelsManager.vueltas == vuelta.Count)
            Events.OnGameReady();

        yield return new WaitForSeconds(0.2f);
        _x = 13.7f + (bridgeScenary_x_separation * (wordID - 1));
        avatarsManager.Run();
        if (Data.Instance.levelsManager.vueltas == vuelta.Count)
        {
            Events.OnVoiceSayFromList("felicitaciones", 0.8f);
            Invoke("LevelComplete", 3);
            state = states.JUMPING;
            _x += 4f;
        }
        else
        {
            AddNewScene();
            state = states.SCROLL;
        }
        yield return null;
    }
    void LevelComplete()
    {

        if (!commitError)
            Events.OnPerfect();
        else
            Events.OnGood();
        Events.OnLevelComplete(GameData.types.BRIDGE, commitError);
    }
    bool skipGame;
    void SkipGame()
    {
        if (skipGame)
            return;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Events.OnLevelComplete(GameData.types.BRIDGE, true);
            skipGame = true;
        }
    }
    void Update()
    {
        SkipGame();
        if (state == states.INTRO)
        {
            Vector2 pos = avatarsManager.nene.transform.position;
            if (pos.x < _x)
                pos.x += Time.deltaTime * (speedJump *2);
            else
                StartGame();
            UpdatePositions(pos);
        }
        else
        if (state == states.SCROLL)
        {
            if (cam.transform.position.x < bridgeScenary_x_separation * wordID)
                cam.transform.position = new Vector3(cam.transform.position.x + (Time.deltaTime * cam_speed), 0, -10);
            else
            {
                Restart();
            }
        }
        if (state == states.JUMPING || state == states.SCROLL)
        {
            Vector2 pos = avatarsManager.nene.transform.position;
            if (pos.x < _x)
                pos.x += Time.deltaTime * speedJump;

            UpdatePositions(pos);
        }
    }
    void UpdatePositions(Vector2 pos)
    {
        avatarsManager.nene.transform.position = pos;
        pos.y = avatarsManager.nena.transform.position.y;
        pos.x += 0.8f;
        avatarsManager.nena.transform.position = pos;
    }
}
