using UnityEngine;
using System.Collections;

public class Avatar : MonoBehaviour {

    public Animator anim;
    public AvatarCustomizator customizator;
    public string DefaultAnim;

	void Start () {
	    if(DefaultAnim != "")
            anim.Play(DefaultAnim);
	}
    public void MonkeyActiveGomera(bool isActive)
    {
       // print("MonkeyActiveGomera " + isActive);
        if (isActive)
        {
            anim.Play("boy_monkey_rotate", 0, 0);
            anim.speed = 1;
        }
        else
            anim.speed = 0;
    }
    public void MonkeyShoot()
    {
        anim.CrossFade("girl_monkey_shoot", 0.15f);
    }
	public void BoyDolphinRun()
	{
		anim.CrossFade("boy_dolphin_run", 0.15f);
	}
	public void GirlDolphinRun()
	{
		anim.CrossFade("girl_dolphin_run", 0.15f);
	}
    public void MonkeyIdle()
    {
        anim.Play("girl_monkey_idle", 0, 0);
    }
	public void GirlDolphinWin()
	{
		anim.Play("girl_dolphin_win");
	}
	public void BoyDolphinWin()
	{
		anim.Play("boy_dolphin_win");
	}
	public void GirlDolphinCrash()
	{
		anim.Play("girl_dolphin_crash");
	}
	public void BoyDolphinCrash()
	{
		anim.Play("boy_dolphin_crash");
	}


    public void GirlJump()
    {
        anim.Play("girl_jump", 0, 0);
    }
    public void GirlIdle()
    {
        anim.CrossFade("girl_idle", 0.15f);
    }
    public void GirlWalk()
    {
        anim.Play("girl_walk", 0, 0);
    }
    public void GirlRun()
    {
        anim.Play("girl_run", 0, 0); ;
    }

    public void BoylJump()
    {
        anim.Play("boy_jump", 0, 0);
    }
    public void BoyIdle()
    {
        anim.CrossFade("boy_idle", 0.15f);
    }
    public void BoyWalk()
    {
        anim.Play("boy_walk");
    }
    public void BoyRun()
    {
        anim.Play("boy_run");
    }
    public void BoyIntro1()
    {
        anim.CrossFade("boy_intro1", 0.15f);
    }
    public void BoyIntro2()
    {
        anim.CrossFade("boy_intro2", 0.02f);
    }
	public void BoyIntro1Talk()
	{
		anim.CrossFade("boy_intro1_talk", 0.15f);
	}
	public void BoyIntro2Talk()
	{
		anim.CrossFade("boy_intro2_talk", 0.15f);
	}
	public void GirlTalk()
	{
		anim.CrossFade("girl_talk", 0.15f);
	}
	public void BoyIntro2Idle()
	{
		anim.CrossFade("boy_intro2_idle", 0.15f);
	}
	public void BoyStopTalkingIntro1()
	{
		anim.CrossFade("boy_intro1", 0.15f);
	}
	public void BoyStopTalkingIntro2()
	{
		anim.CrossFade("boy_intro2_idle", 0.15f);
	}
	public void GirlStopTalking()
	{
		anim.CrossFade("girl_idle", 0.15f);
	}
	public void GirlGenericTalk()
	{
		anim.Play("genericTalk",0,0);
	}
	public void BoyGenericTalk()
	{
		anim.Play("genericTalk",0,0);
	}
	public void GenericShut()
	{
		anim.Play("genericShut",0,0);
	}
	public void BoyMonkeyTalk()
	{
		anim.CrossFade("boy_monkey_talk", 0.15f);
	}
	public void GirlMonkeyTalk()
	{
		anim.CrossFade("girl_monkey_talk", 0.15f);
	}
	public void GirlMonkeyIdle()
	{
		anim.CrossFade("girl_monkey_idle", 0.15f);
	}
	public void BoyMonkeyIdle()
	{
		anim.CrossFade("boy_monkey_idle", 0.15f);
	}
	public void BoyTalk()
	{
		anim.CrossFade("boy_talk", 0.15f);
	}
	public void BoyDolphinIdle()
	{
        print("BoyDolphinIdle");
		anim.CrossFade("boy_dolphin_idle", 0.15f);
	}
	public void BoyDolphinTalk()
	{
        print("BoyDolphinTalk");
        anim.CrossFade("boy_dolphin_talk", 0.15f);
	}
	public void GirlDolphinIdle()
	{
		anim.CrossFade("girl_dolphin_idle", 0.15f);
	}
	public void GirlDolphinTalk()
	{
		anim.CrossFade("girl_dolphin_talk", 0.15f);
	}

}
