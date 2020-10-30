using UnityEngine;
using System.Collections;

public class TimeNoPlayingAudio : MonoBehaviour {

    bool shoutUp;
    public float seconds;
    float timeToSayIt = 50f;

	void Start () {
        shoutUp = true;
        Events.ResetTimeToSayNotPlaying += ResetTimeToSayNotPlaying;
        Events.OnSwipe += OnSwipe;
        Events.OnTutorialReady += OnTutorialReady;
        Events.OnLevelComplete += OnLevelComplete;
    }
    void OnDestroy()
    {
        Events.ResetTimeToSayNotPlaying -= ResetTimeToSayNotPlaying;
        Events.OnSwipe -= OnSwipe;
        Events.OnTutorialReady -= OnTutorialReady;
        Events.OnLevelComplete -= OnLevelComplete;
    }
    void OnLevelComplete(GameData.types t, bool d)
    {
        ResetTimeToSayNotPlaying();
    }
    void OnSwipe(SwipeDetector.directions dir)
    {
        ResetTimeToSayNotPlaying();
    }
    void OnSwipe()
    {
        ResetTimeToSayNotPlaying();
    }
    void Update()
    {
        if (!shoutUp)
            seconds += Time.deltaTime;
        if (seconds > timeToSayIt)
            Say();
    }
    public void ResetTimeToSayNotPlaying()
    {
        seconds = 0;
    }
    void OnTutorialReady()
    {
        EnableAudio();
    }
    void EnableAudio()
    {
        shoutUp = false;
    }
    void OnDisable()
    {
        shoutUp = true;
    }	
	void Say() {
        ResetTimeToSayNotPlaying();
        Events.OnVoiceSayFromList("timeNoPlaying", 0);
        EnableAudio();
    }
}
