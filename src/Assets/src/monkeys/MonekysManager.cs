using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MonekysManager : MonoBehaviour {

    public List<Monkey> monkeys;
    public MoneysUI ui;
    private bool levelComplete;

    private float speed;
    private float standardSpeed = 5;
    private float MultiplierMedium = 0.75f;
    private float MultiplierHard = 0.5f;

	void Start () {
        Invoke("ChangeStates", 4);
        Invoke("Init", 0.1f);
        SetSpeed();
        Events.OnOkWord += OnOkWord;
	}
    void OnDestroy()
    {
        Events.OnOkWord -= OnOkWord;
    }
    public void SetSpeed()
    {
        speed = standardSpeed;
        switch (Data.Instance.userdata.dificult)
        {
            case UserData.dificults.HARD: speed *= MultiplierHard; break;
            case UserData.dificults.MEDIUM: speed *= MultiplierMedium; break;
        }
    }
    void Init()
    {
        levelComplete = false;
        TextsMonkeys.Vuelta vuelta = ui.vuelta;
        int activeID = Random.Range(0, monkeys.Count);
        int id = 0;
        int wrongID = 0;
        foreach (Monkey monkey in monkeys)
        {
            if (activeID == id)
                monkey.Init(vuelta.ok[0], true);
            else
            {
                monkey.Init(vuelta.wrong[wrongID], false);
                wrongID++;
            }
            id++;
        }
    }
    public void OnOkWord(GameData.types type)
    {
        levelComplete = true;
        foreach (Monkey monkey in monkeys)
        {
            if (monkey.state == Monkey.states.ACTIVE)
                monkey.Hide();
        }
        Invoke("Init", 6);
    }
    void ChangeStates()
    {
        Invoke("ChangeStates", 2);
        if (levelComplete) return;
        foreach (Monkey monkey in monkeys)
        {
            int rand = Random.Range(0, 100);

            if (monkey.state == Monkey.states.INACTIVE && rand < 70)
            {
                SwitchWord(monkey);
                monkey.SetActive(speed);
            }
        }
    }
    void SwitchWord(Monkey monkey)
    {
        if (Data.Instance.userdata.dificult != UserData.dificults.HARD)
            return;

        foreach (Monkey otherMonkey in monkeys)
        {
            if (otherMonkey != monkey)
            {
                if (otherMonkey.state == Monkey.states.INACTIVE)
                {
                    string myWord = monkey.word;
                    bool myIsCorrect = monkey.isTheCorrect;
                    monkey.Init(otherMonkey.word, otherMonkey.isTheCorrect);
                    otherMonkey.Init(myWord, myIsCorrect);
                }
            }
        }
    }
}
