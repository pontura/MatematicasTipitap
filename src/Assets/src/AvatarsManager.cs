using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AvatarsManager : MonoBehaviour {

    public Avatar nene;
    public Avatar nena;
    public string initState;

	void Start () {
        return;
        if (initState == "Idle") Idle();

        Events.OnAvatarChangeCloth += OnAvatarChangeCloth;
        Events.MonkeysShoot += MonkeysShoot;
        Events.MonkeysAiming += MonkeysAiming;
        Events.AvatarsIdle += AvatarsIdle;

        Invoke("Customize", 0.3f);
	}
    void AvatarsIdle()
    {
        Invoke("AvatarsIdleDelayed", 0.1f);
    }
    void AvatarsIdleDelayed()
    {
        if (SceneManager.GetActiveScene().name == "Game_Monkey")
        {
            nene.BoyMonkeyIdle();
            nena.GirlMonkeyIdle();
        }
        else
        {
            nene.BoyIdle();
            nena.GirlIdle();
        }
    }
    void Customize()
    {
        AvatarStyles.Styles neneStyles = Data.Instance.GetComponent<AvatarStyles>().nene;
        AvatarStyles.Styles nenaStyles = Data.Instance.GetComponent<AvatarStyles>().nena;

        nene.customizator.AddItem(AvatarCustomizator.partsType.HATS, neneStyles.hats);
        nene.customizator.AddItem(AvatarCustomizator.partsType.BODY, neneStyles.body);
        nene.customizator.AddItem(AvatarCustomizator.partsType.GLASSES, neneStyles.glasses);

        nena.customizator.AddItem(AvatarCustomizator.partsType.HATS, nenaStyles.hats);
        nena.customizator.AddItem(AvatarCustomizator.partsType.BODY, nenaStyles.body);
        nena.customizator.AddItem(AvatarCustomizator.partsType.GLASSES, nenaStyles.glasses);

        ReorderLayers();
    }
    void OnAvatarChangeCloth(AvatarCustomizator.partsType type, string _sexo, int id)
    {
        Avatar avatar;
        if (_sexo == "nene")
            avatar = nene;
        else avatar = nena;

        switch (type)
        {
            case AvatarCustomizator.partsType.BODY:
                avatar.customizator.AddItem(AvatarCustomizator.partsType.BODY, id);
                break;
            case AvatarCustomizator.partsType.GLASSES:
                avatar.customizator.AddItem(AvatarCustomizator.partsType.GLASSES, id);
                break;
            default:
                avatar.customizator.AddItem(AvatarCustomizator.partsType.HATS, id);
                break;
        }
    }
    void ReorderLayers()
    {
        foreach (SpriteRenderer sp in nene.GetComponentsInChildren<SpriteRenderer>())
            sp.sortingLayerName = "laneOverAll_nene";

        foreach (SpriteRenderer sp in nena.GetComponentsInChildren<SpriteRenderer>())
            sp.sortingLayerName = "laneOverAll";
    }
    void OnDestroy()
    {
        Events.MonkeysShoot -= MonkeysShoot;
        Events.MonkeysAiming -= MonkeysAiming;
        Events.OnAvatarChangeCloth -= OnAvatarChangeCloth;
        Events.AvatarsIdle -= AvatarsIdle;
    }
    bool isMoving;
    void MonkeysAiming(bool _isMoving)
    {
        if (_isMoving == isMoving) return;
        isMoving = _isMoving;
        nene.MonkeyActiveGomera(_isMoving);
    }
    public void MonkeysShoot()
    {
        nena.MonkeyShoot();
        MonkeysAiming(false);
    }
    public void Jump()
    {
        nene.BoylJump();
        nena.GirlJump();
        ReorderLayers();
    }
    public void Walk()
    {
        nene.BoyWalk();
        nena.GirlWalk();
        ReorderLayers();
    }
    public void Run()
    {
        nene.BoyRun();
        nena.GirlRun();
        ReorderLayers();
    }
    public void Idle()
    {
        nene.BoyIdle();
        nena.GirlIdle();
        ReorderLayers();
    }
    public void BoyIntro1()
    {
        nene.BoyIntro1();
        ReorderLayers();
    }
    public void BoyIntro2()
    {
        nene.BoyIntro2();
        ReorderLayers();
    }
	public void BoyIntro1Talk()
	{
		nene.BoyIntro1Talk();
		ReorderLayers();
	}
	public void BoyIntro2Talk()
	{
		nene.BoyIntro2Talk();
		ReorderLayers();
	}
	public void GirlTalk()
	{
		nena.GirlTalk();
		ReorderLayers();
	}
	public void GirlIdle()
	{
		nena.GirlIdle();
		ReorderLayers();
	}
	public void BoyIntro2Idle()
	{
		nene.BoyIntro2Idle();
		ReorderLayers();
	}
	public void StopTalkingIntro1()
	{
		nene.BoyStopTalkingIntro1();
		nena.GirlStopTalking();
		ReorderLayers();
	}
	public void StopTalkingIntro2()
	{
		nene.BoyStopTalkingIntro2();
		nena.GirlStopTalking();
		ReorderLayers();
	}
	public void BoyGenTalk()
	{
		nene.BoyGenericTalk();
		ReorderLayers();
	}
	public void GirlGenTalk()
	{
		nene.GirlGenericTalk();
		ReorderLayers();
	}
	public void GenShut()
	{
		nene.GenericShut();
		nena.GenericShut();
		ReorderLayers();
	}
	public void BoyMonkeyTalks()
	{
		nene.BoyMonkeyTalk();
		ReorderLayers();
	}
	public void GirlMonkeyTalks()
	{
		nena.GirlMonkeyTalk();
		ReorderLayers();
	}
	public void MonkeyShut()
	{
		nena.GirlMonkeyIdle();
		nene.BoyMonkeyIdle();
		ReorderLayers();
	}
	public void BoyTalks()
	{
		nene.BoyTalk();
		ReorderLayers();
	}
	public void BoyShuts()
	{
		nene.BoyIdle();
		ReorderLayers();
	}

    public void BoyDolphinTalks()
	{
        print("__BoyDolphinTalks");
		nene.BoyDolphinTalk();
		ReorderLayers();
	}
	public void GirlDolphinTalks()
	{
        print("____GirlDolphinTalks");
        nena.GirlDolphinTalk();
		ReorderLayers();
	}
	public void DolphinShut()
	{
		nena.GirlDolphinIdle();
		nene.BoyDolphinIdle();
		ReorderLayers();
	}
}
