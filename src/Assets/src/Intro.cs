using UnityEngine;
using System.Collections;

public class Intro : MainClass {

    public GameObject panel;
    public int DelayToStart;

    void Start()
    {
        Events.OnTutorialReady += OnTutorialReady;
    }
    void OnDestroy()
    {
        Events.OnTutorialReady -= OnTutorialReady;
    }
    public void Init()
    {
        Invoke("StartByTime", DelayToStart+1);
        panel.SetActive(true);
        GetComponent<AudioSource>().Play();
    }
    public void StartGame()
    {
        GetComponent<AudioSource>().Stop();
        Events.OnTutorialReady();
        panel.SetActive(false);
    }
    public void StartByTime()
    {
        GetComponent<AudioSource>().Stop();
        Events.OnTutorialReady();
    }
    void OnTutorialReady()
    {
        GetComponent<AudioSource>().Stop();
        panel.SetActive(false);
    }
}
