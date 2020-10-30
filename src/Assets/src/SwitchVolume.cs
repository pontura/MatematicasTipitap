using UnityEngine;
using System.Collections;

public class SwitchVolume : MonoBehaviour {

    public GameObject on;
    public GameObject off;

    public bool isOn;

    void Start()
    {
       float vol = Data.Instance.GetComponent<MusicManager>().volume;
       if (vol == 0)
           SetOn(false);
       else
           SetOn(true);
    }
	public void SetOn(bool isOn) {

        if (isOn)
        {
            on.SetActive(true);
            off.SetActive(false);
        }
        else
        {
            on.SetActive(false);
            off.SetActive(true);
        }
        this.isOn = isOn;
	}
    public void Switch()
    {
        isOn = !isOn;
        SetOn(isOn);
        Data.Instance.GetComponent<MusicManager>().SwitchMusic();
    }
}
