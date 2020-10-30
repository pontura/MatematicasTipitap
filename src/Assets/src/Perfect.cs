using UnityEngine;
using System.Collections;

public class Perfect : MonoBehaviour {

    public GameObject goodPanel;
    public GameObject panel;

	void Start () {
        SetOff();
        Events.OnPerfect += OnPerfect;
        Events.OnGood += OnGood;
    }
	
	void OnDestroy () {
        Events.OnPerfect -= OnPerfect;
        Events.OnGood -= OnGood;
    }
    void OnPerfect()
    {
        panel.SetActive(true);
        Invoke("SetOff", 4);
    }
    void OnGood()
    {
        print("_____GOOOD");
        goodPanel.SetActive(true);
        Invoke("SetOff", 4);
    }
    void SetOff()
    {
        goodPanel.SetActive(false);
        panel.SetActive(false);
    }
}
