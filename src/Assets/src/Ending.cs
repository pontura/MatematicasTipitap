using UnityEngine;
using System.Collections;

public class Ending : MainClass {

    public EndingItems endingItems;
    public Transform container;
    private int unlockedItems;
    private string audioName = "";
    public bool isHe;
    public GameObject readySignal;
    bool winDiploma;
    public AvatarsManager avatarsManager;
    bool winPremio;

    public override void Done() { }
    void Start() {

        Data.Instance.LoadLevel("Map", false);
        return;
        Data.Instance.stats.TrackEvent("fin_recorrido", Data.Instance.routes.routeID);

        Data.Instance.GetComponent<SettingsScreen>().settingsButton.SetActive(false);

        avatarsManager = GetComponent<AvatarsManager>();
        readySignal.SetActive(false);
        int routeID = Data.Instance.routes.routeID;
        int count;

        unlockedItems = Data.Instance.GetComponent<Items>().unlockedItems;

        if (unlockedItems >= Data.Instance.routes.routeID)
        {
            Ready();
            return;
        }
        count = Data.Instance.GetComponent<Items>().items.Count;
        audioName = Data.Instance.GetComponent<Items>().items[unlockedItems].audio;
        isHe = Data.Instance.GetComponent<Items>().items[unlockedItems].isHe;


        EndingItems newItems = Instantiate(endingItems);
        newItems.transform.SetParent(container);
        newItems.transform.localPosition = Vector3.zero;

        Events.OnMusic("carefree");
        Events.UnlockNextRoute();
        if (unlockedItems < count)
        {
            Events.OnSoundFX("arco iris regalo");
            Invoke("DiceRegalo", 10);
            winPremio = true;
        }
        else
        {
            Invoke("Continue", 3);
            winPremio = false;
        }
    } 
    void OnDestroy()
    {
        Data.Instance.GetComponent<SettingsScreen>().settingsButton.SetActive(true);
    }
    public void DiceRegalo()
    {
        if(isHe)
            avatarsManager.BoyTalks();
        else
            avatarsManager.GirlTalk();

        print("Ending: da premio: " + audioName);
        Events.OnSoundFX("premios/" + audioName);
        Invoke("Tent", 3);
        Invoke("ResetFelicita", 2);
       
        Data.Instance.stats.TrackEvent("premio_" + audioName);
    }
    void ResetFelicita()
    {
        avatarsManager.Idle();
    }
    public void Tent()
    {
        Data.Instance.LoadLevel("Tent", false);
    }
    public void Continue()
    {
        if(winPremio || winDiploma)
            Data.Instance.LoadLevel("Tent", false);
        else
            Data.Instance.LoadLevel("Map", false);
    }
    void Ready()
    {
        winDiploma = true;
        readySignal.SetActive(true);
        Events.WinDiploma();
        GetComponent<Animation>().Stop();
        GetComponent<Animation>().enabled = false;
        GetComponent<AvatarsManager>().Idle();
    }
}
