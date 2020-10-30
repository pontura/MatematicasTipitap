using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Dashboard : MainClass {

    public DashboardLine dashboardLine;
    public Transform content1;
    public Transform content2;
    public Transform content3;
    public Transform content4;

    public GameObject off1;
    public GameObject off2;
    public GameObject off3;

    public GameObject off4;
    public GameObject off5;
    public GameObject off6;

    public GameObject off7;
    public GameObject off8;
    public GameObject off9;

    public Image progressPerLevel;

    void Start () {
        Data.Instance.stats.TrackEvent("Dashboard");
        Data.Instance.GetComponent<SettingsScreen>().settingsButton.SetActive(false);
        ProgressData data = Data.Instance.GetComponent<ProgressData>();
        Create(content1, data.well1);
        Create(content2, data.well2);
        Create(content3, data.well3);
        Create(content4, data.well4);

        off1.SetActive(true);
        off2.SetActive(true);
        off3.SetActive(true);

        off4.SetActive(true);
        off5.SetActive(true);
        off6.SetActive(true);

        off7.SetActive(true);
        off8.SetActive(true);
        off9.SetActive(true);


        if (Data.Instance.GetComponent<Routes>().unlockedRoute >1)
            off1.SetActive(false);
        if (Data.Instance.GetComponent<Routes>().unlockedRoute >2)
            off2.SetActive(false);
        if (Data.Instance.GetComponent<Routes>().unlockedRoute >3)
            off3.SetActive(false);

        if (Data.Instance.GetComponent<Routes>().unlockedRoute > 4)
            off4.SetActive(false);
        if (Data.Instance.GetComponent<Routes>().unlockedRoute > 5)
            off5.SetActive(false);
        if (Data.Instance.GetComponent<Routes>().unlockedRoute > 6)
            off6.SetActive(false);

        if (Data.Instance.GetComponent<Routes>().unlockedRoute > 7)
            off7.SetActive(false);
        if (Data.Instance.GetComponent<Routes>().unlockedRoute > 8)
            off8.SetActive(false);
        if (Data.Instance.GetComponent<Routes>().unlockedRoute > 9)
            off9.SetActive(false);

        progressPerLevel.fillAmount = Data.Instance.routes.GetTotalPerfectAmount();
    }
	void Create(Transform content, List<string> words)
    {
        foreach (string word in words)
        {
            DashboardLine newLine = Instantiate(dashboardLine);            
            newLine.transform.SetParent(content);
            newLine.Init(word.ToUpper());
            newLine.transform.localScale = Vector3.one;
        }
    }
    public void Close()
    {
        Data.Instance.GetComponent<SettingsScreen>().settingsButton.SetActive(true);
        Data.Instance.LoadLevel("MainMenu", false);
    }
}
