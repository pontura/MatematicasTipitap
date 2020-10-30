using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Transitions : MonoBehaviour {

    public Canvas canvas;
    public GameObject panel;
    public Image image;
    private bool isOn;
    private float speed = 1.5f;
    public GameObject wheel;
    
    private bool showMap;
    public MusicManager musicManager;

	void Start () {
        musicManager = GetComponent<MusicManager>();
        Events.OnSceneLoaded += OnSceneLoaded;
        panel.SetActive(false);
        canvas.enabled = false;
	}
    void OnSceneLoaded()
    {
        Invoke("SetOff", 0.7f);
    }
    public void SetOn(bool showMap)
    {
        wheel.SetActive(false);
        this.showMap = showMap;
        isOn = true;
        image.fillOrigin = 0;
        panel.SetActive(true);
        canvas.enabled = true;
        StartCoroutine(IsOn());
    }
    IEnumerator IsOn()
    {
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * speed;
            if(!showMap)
                image.fillAmount = i;
            yield return new WaitForEndOfFrame();
        }
        wheel.SetActive(true);
        if (showMap)
        {
            if(musicManager.volume > 0)
                musicManager.OnVolumeChanged(0.07f);
            Events.OnShowMap(true);
        }

        yield return null;
    }
    public void SetOff()
    {        
        if (!isOn) return;
        
        isOn = false;
        image.fillOrigin = 1;
        StartCoroutine(IsOff());
    }
    IEnumerator IsOff()
    {
        wheel.SetActive(false);
        if (showMap)
        {
            Events.OnShowMap(false);
            if (musicManager.volume > 0)
                musicManager.OnVolumeChanged(0.2f);
        }
        float i = 1;
        while (i > 0)
        {
            i -= Time.deltaTime * speed;
            if (!showMap)
                image.fillAmount = i;
            yield return new WaitForEndOfFrame();
        }
        panel.SetActive(false);
        yield return null;  
    }
   
}
