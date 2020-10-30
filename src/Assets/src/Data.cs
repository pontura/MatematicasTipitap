using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Data : MonoBehaviour
{
    const string PREFAB_PATH = "Data";
    static Data mInstance = null;
    public LevelsManager levelsManager;
    public UserData userdata;
    public WordsUsed wordsUsed;
    public bool DEBUG;
    public string lastScene;
    public string newScene;
    public Routes routes;
    private float time_ViewingMap = 7.3f;
    public StatsManager stats;

    public static Data Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<Data>();

                if (mInstance == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>(PREFAB_PATH)) as GameObject;
                    mInstance = go.GetComponent<Data>();
                    go.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            return mInstance;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Events.OnLevelComplete(GameData.types.BRIDGE, true);
    }
    public void LoadLevel(string aLevelName, bool showMap)
    {
        this.newScene = aLevelName;
        GetComponent<Transitions>().SetOn(showMap);
        
        if (showMap)
        {
           // Invoke("EmptyDelayed", 2);
            if (Data.Instance.GetComponent<MusicManager>().volume == 0)
                time_ViewingMap = 5.5f ;
            Invoke("LoadDelayed", time_ViewingMap);
        }
        else
            Invoke("LoadDelayed", 0.75f);       
    }
    void EmptyDelayed()
    {
        SceneManager.LoadScene("Empty");
    }
    void LoadDelayed()
    {
         SceneManager.LoadScene(newScene);
    }
    void Awake()
    {
        if (!mInstance)
            mInstance = this;

        else
        {
            Destroy(this.gameObject);
            return;
        }
        PlayerPrefs.DeleteAll();
        DontDestroyOnLoad(this);

        userdata = GetComponent<UserData>();
        levelsManager = GetComponent<LevelsManager>();
        routes = GetComponent<Routes>();
        wordsUsed = GetComponent<WordsUsed>();
        stats = GetComponent<StatsManager>();
    }
}
