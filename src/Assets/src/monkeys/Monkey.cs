using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Monkey : MonoBehaviour
{
    public Animation anim;
    public TextMesh field;
    private float ChangeSpriteHealth;
    private Collider2D collider;
    public bool isTheCorrect;
    public states state;
    public string word;

    public enum states
    {
        INACTIVE,
        ACTIVE,
        GOOD,
        BAD
    }

    void Start()
    {
        collider = GetComponent<Collider2D>();
        state = states.INACTIVE;
        Hide();
    }
    public void Init(string _word, bool isTheCorrect)
    {
        this.word = _word;
        this.isTheCorrect = isTheCorrect;
        this.field.text = _word;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        if (col.gameObject.tag == "Banana")
        {
            Destroy(col.gameObject);
            EventsMonkey.OnGotBanana(field.text);

            if (isTheCorrect)
                Good();
            else
                Bad();
        }
    }
    public void SetActive(float time)
    {
        if (state == states.ACTIVE) return;
        if (state == states.GOOD) return;
        if (state == states.BAD) return;
        collider.enabled = true;
        state = states.ACTIVE;
        GetComponent<Animation>().Play("monkeyShow");
        //Events.OnSoundFXSecondary("monkeyMove");
        Invoke("Hide", time);
    }
    public void Hide()
    {
        collider.enabled = false;
        if (state == states.INACTIVE) return;
        if (state == states.GOOD) return;
        if (state == states.BAD) return;
        state = states.INACTIVE;
        //Events.OnSoundFXSecondary("monkeyMove");
        GetComponent<Animation>().Play("monkeyHide");
    }
    void Good()
    {
        if (state == states.GOOD) return;
        state = states.GOOD;
        GetComponent<Animation>().Play("monkeyWin");
        Invoke("Reset", 2);
    }
    void Bad()
    {
        if (state == states.BAD) return;
        state = states.BAD;
        GetComponent<Animation>().Play("monkeyLose");
        Invoke("Reset", 2);
    }
    void Reset()
    {
        collider.enabled = false;
        state = states.INACTIVE;
    }

}
