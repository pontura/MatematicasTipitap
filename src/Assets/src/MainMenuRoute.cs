using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuRoute : MonoBehaviour {

    public MainMenuRoutePoint[] routePoints;
    public GameObject button;

    public void Init(int activeID, bool perfect, bool toBePlayed)
    {
        //print("MAP : Init  " + activeID + "            perfect: " + perfect);
        //routePoints[activeID].Init(perfect);
        //button.SetActive(true);
        //if (toBePlayed)
        //{
        //    print(activeID + " - " + perfect + " - " + toBePlayed);
        //    routePoints[activeID].ToBePlayed();
        //}
    }
    public void ResetPlayedPoints()
    {
        button.SetActive(true);
        foreach (MainMenuRoutePoint point in routePoints)
            point.SetUnPlayed();
    }
    public void SetOn(int activeID, bool isMainMenu)
    {
        button.SetActive(true);

        gameObject.SetActive(true);
        foreach (MainMenuRoutePoint point in routePoints)
            point.SetOff();

        //  string sceneName = SceneManager.GetActiveScene().name;
        // if (sceneName != "Map")
        // {
        int id = 0;
        foreach (MainMenuRoutePoint point in routePoints)
        {
            if (id < activeID)
            {
                point.SetPlayed();
            }
            else
            {
                point.SetUnPlayed();
            }
            id++;
        }
      //  }

       

        if (!isMainMenu && activeID > 0)
            routePoints[activeID - 1].SetOn();
    }
    public void SetOff()
    {
        button.SetActive(false);
        gameObject.SetActive(false);
    }
}
