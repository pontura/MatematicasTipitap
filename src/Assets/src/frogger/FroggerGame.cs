using UnityEngine;
using System.Collections;

public class FroggerGame : MainClass {

    private int vuelta;
    private int qtyWords;

    public FroggerPanel frogger_up;
    public FroggerPanel frogger_down;
    public FroggerPanel frogger_sand;
    public FroggerPanel frogger_finish;

    public GameObject panels;
    public int distance = 4;

    private int _x;
    

	void Start () {
        Events.OnTutorialReady += OnTutorialReady;
        vuelta = Data.Instance.levelsManager.frogger;
        qtyWords = Data.Instance.routes.GetTotalWordsOfActiveGame();

        if (qtyWords > 0)
            AddPanel(frogger_up);
        if (qtyWords > 1)
            AddPanel(frogger_down);
        if (qtyWords > 2)
            AddPanel(frogger_up);
        if (qtyWords > 3)
        {
            AddPanel(frogger_sand);
            AddPanel(frogger_down);
        }
        if (qtyWords > 4)
            AddPanel(frogger_up);
        if (qtyWords > 5)
            AddPanel(frogger_down);
        if (qtyWords > 6)
            AddPanel(frogger_up);
        if (qtyWords > 7)
        {
            AddPanel(frogger_sand);
            AddPanel(frogger_down);
        }
        if (qtyWords > 8)
            AddPanel(frogger_up);

        AddPanel(frogger_finish);

        GetComponent<FroggerController>().Init();
    }
    void OnDestroy()
    {
        Events.OnTutorialReady -= OnTutorialReady;
    }
    void OnTutorialReady()
    {
        GetComponent<FroggerController>().StartGame();
        GetComponent<Animation>().Stop();
        Events.AvatarsIdle();
    }
    void AddPanel(FroggerPanel panel)
    {
         _x += distance;
        FroggerPanel newPanel = Instantiate(panel);
        newPanel.transform.SetParent(panels.transform);
        newPanel.transform.localPosition = new Vector2(_x, 0);       
    }
}
