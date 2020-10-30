using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SettingsScreen : MonoBehaviour {

    public GameObject settingsButton;
    public GameObject popup;
    public GameObject popupMainMenu;

    void Start()
    {
        popup.SetActive(false);
    }
    public void OpenSettings()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "Map" || SceneManager.GetActiveScene().name == "Tent")
            popupMainMenu.SetActive(true);
        else
            popup.SetActive(true);

        Time.timeScale = 0;
    }
    public void Close()
    {
        popup.SetActive(false);
        popupMainMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Exit()
    {
        GetComponent<Routes>().ResetMap();
        Data.Instance.LoadLevel("MainMenu", false);
        Close();
    }
    public void ResetApp()
    {
        Data.Instance.GetComponent<ConfirmPopup>().Open(ResetAppWithConformation);       
    }
    public  void ResetAppWithConformation()
    {
        Events.ResetApp();
        PlayerPrefs.DeleteAll();
        Data.Instance.LoadLevel("MainMenu", false);
        Close();
    }
    public void SwitchMusic()
    {
        GetComponent<MusicManager>().SwitchMusic();
    }
    public void Dashboard()
    {
        Data.Instance.LoadLevel("Dashboard", false);
        Close();
    }
}
