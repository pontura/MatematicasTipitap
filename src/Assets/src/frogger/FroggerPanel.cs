using UnityEngine;
using System.Collections;

public class FroggerPanel : MonoBehaviour {

    public FroggerSignal[] froggerSignals;
    public bool goesUp;
    private float speed;
    private float standardSpeed = 1f;
    private float activeSpeed = 0.5f;
    private float MultiplierMedium = 1.4f;
    private float MultiplierHard = 1.8f;

    public bool hasWords;

	void Start () {
        SetInactive();
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasWords) return;
        foreach (FroggerSignal signal in froggerSignals)
        {
            Vector2 pos = signal.transform.localPosition;
            if (goesUp)
            {
                pos.y += Time.deltaTime * speed;
                if (pos.y > 6)
                {
                    pos.y = -4;
                    signal.Rebuild();
                }
            }
            else
            {
                pos.y -= Time.deltaTime * speed;
                if (pos.y < -4)
                {
                    pos.y = 6;
                    signal.Rebuild();
                }
            }
            signal.transform.localPosition = pos;
        }
	}
    public void SetActive()
    {
        speed = activeSpeed;
        switch (Data.Instance.userdata.dificult)
        {
            case UserData.dificults.HARD: speed *= MultiplierHard; break;
            case UserData.dificults.MEDIUM: speed *= MultiplierMedium; break;
        }
    }
    public void SetInactive()
    {
        speed = standardSpeed;
        switch (Data.Instance.userdata.dificult)
        {
            case UserData.dificults.HARD: speed *= MultiplierHard; break;
            case UserData.dificults.MEDIUM: speed *= MultiplierMedium; break;
        }
    }
}
