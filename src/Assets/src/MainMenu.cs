using UnityEngine;
using System.Collections;

public class MainMenu : MainClass {

    public Animation tutorialAnim;
    public GameObject tutorial;

    void Start()
    {
        Map();
        return;
        if (!Data.Instance.levelsManager.introPlayed)
        {
            Data.Instance.levelsManager.introPlayed = true;            
        }else
        {            
            tutorialAnim.Stop();
            tutorialAnim.enabled = false;
            tutorial.SetActive(false);

            GetComponent<AudioSource>().Stop();
            GetComponent<Animation>().enabled = false;
        }
        Events.OnMusic("playful");
    }
    public void Tent()
    {
        Data.Instance.LoadLevel("Tent", false);
    }
    public void Map()
    {
        Data.Instance.LoadLevel("Map", false);
    }
    public void ReplayTutorial()
    {
        Data.Instance.levelsManager.introPlayed = false;
        Data.Instance.LoadLevel("MainMenu", false);
    }
}
