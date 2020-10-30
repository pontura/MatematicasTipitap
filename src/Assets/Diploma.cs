using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Diploma : MonoBehaviour
{
    public int diploma;
    public GameObject panel;
    public Texture2D tex;

    void Start()
    {
        SetOff();
        Events.WinDiploma += WinDiploma;
        diploma = PlayerPrefs.GetInt("diploma", 0);
    }
    
    void OnDestroy()
    {
        Events.WinDiploma -= WinDiploma;
    }
    void OnEnable()
    {
       // WinDiploma();
    }
    void WinDiploma()
    {
        if (diploma == 1) return;

        if (!Data.Instance.routes.CheckIfAllPerfect()) return;

        Data.Instance.stats.TrackEvent("Gana_Diploma");

        print("win diploma");
        PlayerPrefs.SetInt("diploma", 1);
        diploma = 1;
        Open();
    }
    public void Open()
    {
        panel.SetActive(true);
        Invoke("OnShare", 2);
    }
    void OnShare()
    {
        //print("OnEnable");

        //int width = 2047;
        //int height = 1150;

        //tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //tex.Apply();

        //string destination = Path.Combine(Application.persistentDataPath, "diploma.png");
        //Debug.Log(destination);

        //byte[] bytes = tex.EncodeToPNG();
        //Object.Destroy(tex);

        //File.WriteAllBytes(destination, bytes);

        //Data.Instance.GetComponent<NativeShare>().Share("Pikala" + "the game", destination, destination, "");

        //Invoke("SetOff", 4);
    }
    public void SetOff()
    {
        panel.SetActive(false);
    }
}
