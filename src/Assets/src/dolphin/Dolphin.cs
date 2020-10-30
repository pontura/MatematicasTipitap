using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Dolphin : MonoBehaviour
{

    [SerializeField]
    DolphinAsset _dolphinAsset;

    public DolphinAsset dolphinAsset;

    float timeToCrossLane;

    public GameObject container;
    public GameObject heroContainer;
    public GameObject powerUpsContainer;
    private BoxCollider2D collider;

    public bool CantMoveUp;
    public bool CantMoveDown;

    public actions action;
    public enum actions
    {
        PLAYING,
        CHANGING_LANE,
    }

    void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }
    public void Init()
    {
        timeToCrossLane = 0.1f;
    }
    void Start()
    {
        Events.OnTutorialReady += OnTutorialReady;
        transform.localScale = new Vector3(0.52f, 0.52f, 0.52f);

        dolphinAsset = Instantiate(_dolphinAsset) as DolphinAsset;
        dolphinAsset.transform.SetParent(heroContainer.transform);

        dolphinAsset.transform.localPosition = Vector3.zero;
        Idle();
        dolphinAsset.Tutorial();
    }
    void OnDestroy()
    {
        Events.OnTutorialReady -= OnTutorialReady;
    }
    void OnTutorialReady()
    {
        dolphinAsset.Run();
    }
    public void OnSetHeroState(bool show)
    {
        if (!show)
            heroContainer.transform.localPosition = new Vector3(-1000, 0, 0);
        else
            heroContainer.transform.localPosition = Vector3.zero;
    }
    public void MoveUP()
    {
        Move(2f, true);
    }
    public void MoveDown()
    {
        Move(-2f, true);
    }
    public void Idle()
    {
        print("IDLE");
        dolphinAsset.Run();
    }
    public void GotoCenterOfLane()
    {
        Vector3 pos = transform.localPosition;
        pos.y = 0;
        transform.localPosition = pos;
        container.transform.localPosition = Vector3.zero;
    }
    private void Move(float _y, bool firstStep)
    {
        if (action == actions.CHANGING_LANE) return;
        Events.OnDolphinJump();
        CantMoveUp = false;
        CantMoveDown = false;

        Events.OnChangeLane();
        action = actions.CHANGING_LANE;
        Events.OnSoundFX("changeLane");
        TweenParms parms = new TweenParms();
        parms.Prop("localPosition", new Vector3(0, _y, 0));

        parms.Ease(EaseType.Linear);

        parms.OnComplete(OnChangeLaneComplete);

        HOTween.To(container.transform, timeToCrossLane, parms);
    }
    void OnChangeLaneComplete()
    {
        Events.OnChangeLaneComplete();
        action = actions.PLAYING;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        DolphinWord word = other.GetComponent<DolphinWord>();
        if (word != null)
        {
            Events.OnGotWord(word.word);
            word.Pool();
            if (word.isCorrectWord)
            {

            }
            else
            {
                Events.OnDolphinCrash();
            }
        }
        else
        {
            DolphinObject obstacle = other.GetComponent<DolphinObject>();
            if (obstacle != null)
            {
                obstacle.Crashed();
                Events.OnDolphinCrash();
            }
        }
    }
}
