using UnityEngine;
using System.Collections;

public class DolphinManager : MonoBehaviour {

    public Dolphin dolphin;
    public Lanes lanes;
    private bool playing;
    void Start()
    {
        Events.OnSwipe += OnSwipe;
        Events.OnChangeLaneComplete += OnChangeLaneComplete;
        Events.OnLevelComplete += OnLevelComplete;
    }
    public void Init()
    {        
        OnChangeLaneComplete();
        dolphin.Init();

        Invoke("Delay", 0.2f);        
    }
    void Delay()
    {
        playing = true;
        lanes.sortInLayersByLane(dolphin.gameObject, lanes.GetActivetLane().id);
    }
    void OnDestroy()
    {
        Events.OnSwipe -= OnSwipe;
        Events.OnChangeLaneComplete -= OnChangeLaneComplete;
        Events.OnLevelComplete -= OnLevelComplete;
    }
    void OnLevelComplete(GameData.types t, bool b)
    {
        playing = false;
    }
    public void UpdatePosition(float _x)
    {
       Vector3 pos = dolphin.transform.position;
       pos.x = _x;
       dolphin.transform.position = pos;
    }
    private float lastKeyPressedTime;
    void OnSwipe(SwipeDetector.directions direction)
    {
        if (!playing) return;
        if (DolphinGame.Instance.dolphinGameManager.state == DolphinGameManager.states.TUTORIAL) return;
        if (DolphinGame.Instance.dolphinGameManager.state == DolphinGameManager.states.FINISH) return;
        if (DolphinGame.Instance.state != DolphinGame.states.PLAYING) return;

        float Diff = (Time.time - lastKeyPressedTime);
        lastKeyPressedTime = Time.time;
        if (Diff < 0.1f) return;

        switch (direction)
        {
            case SwipeDetector.directions.UP:
                if (!dolphin.CantMoveUp && lanes.TryToChangeLane(true))
                    dolphin.MoveUP(); 
                break;
            case SwipeDetector.directions.DOWN:
                if (!dolphin.CantMoveDown && lanes.TryToChangeLane(false))
                    dolphin.MoveDown(); 
               break;
        }
    }
    //void OnChangeingLane()
    //{
    //    if (lanes.GetActivetLane())
    //    {
    //        //dolphin.transform.parent = lanes.GetActivetLane().gameObject.transform;
    //        //dolphin.GotoCenterOfLane();
    //    }
    //}
    void OnChangeLaneComplete()
    {
        dolphin.transform.parent = lanes.GetActivetLane().gameObject.transform;
        dolphin.GotoCenterOfLane();
        lanes.sortInLayersByLane(dolphin.gameObject, lanes.GetActivetLane().id);
    }
    
}
