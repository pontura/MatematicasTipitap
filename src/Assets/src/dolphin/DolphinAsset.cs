using UnityEngine;
using System.Collections;

public class DolphinAsset : MonoBehaviour {

    public Animator anim;
    public states state;
	//public Avatar nene;
	//public Avatar nena;
    public ParticleSystem particles;
    private AvatarsManager manager;

    public enum states
    {
        DOLPHIN_RUN
    }
    void Awake()
    {
        manager = GetComponent<AvatarsManager>();
        Events.StartGame += StartGame;
        Events.OnDolphinCrash += OnDolphinCrash;
        Events.OnDolphinJump += OnDolphinJump;
        Events.OnOkWord += OnOkWord;
    }
    void OnDestroy()
    {
        Events.StartGame -= StartGame;
        Events.OnDolphinCrash -= OnDolphinCrash;
        Events.OnDolphinJump -= OnDolphinJump;
        Events.OnOkWord -= OnOkWord;
    }
    void StartGame()
    {
       // Run();
    }
    void OnDolphinCrash()
    {
        Crash();
    }
    void OnHeroCelebrate()
    {
        Celebrate();
    }
    void OnDolphinJump()
    {
        anim.Play("dolphin_jump",0,0);
        Events.OnSoundFX("dolphinJump");
    }
    public void Tutorial()
    {
        print("Tutorial");
        anim.Play("introDolphin", 0, 0);
    }
    public void ResetState()
    {
    }
    public void EndAnimation()
    {
    }
    void Crash()
    {
    }
    public void Run()
    {
        anim.Play("dolphin_run");
    }
	void DolphinRun()
	{
		state = states.DOLPHIN_RUN;
        manager.nene.BoyDolphinRun();
        manager.nena.GirlDolphinRun();
	}
    void OnOkWord(GameData.types type)
    {
        particles.Play();
    }
    void Celebrate()
    {

    }
    public void Dash()
    {

    }
    public void Die()
    {

    }
    public void ResetAnimation()
    {

    }
}
