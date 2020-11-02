using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMainMenuButtons : MonoBehaviour
{
    public Transform container;
    public MapMainButton button;

    void Start()
    {
        Events.OnSceneLoaded();
        for(int a = 1; a<3+1; a++)
        {
            MapMainButton b = Instantiate(button);
            b.transform.SetParent(container);
            b.Init(this, a);
            b.transform.localScale = Vector3.one;
        }
    }
    public void OnClicked(int id)
    {
        Data.Instance.routes.RouteSelected(id);
        Data.Instance.levelsManager.LoadNextGame(false);
        Data.Instance.levelsManager.RouteSelected(id);
    }
}
