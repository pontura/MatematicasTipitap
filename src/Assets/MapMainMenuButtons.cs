using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMainMenuButtons : MonoBehaviour
{
    public Transform container;
    public MapMainButton button;
    public GameObject[] maps;
    bool done;

    void Start()
    {
        Events.OnSceneLoaded();
        SetMap(1);
    }
    public void OnClicked(int id)
    {
        if (done)
            return;
        done = true;

        Data.Instance.routes.RouteSelected(id);
        Data.Instance.levelsManager.LoadNextGame(false);
        Data.Instance.levelsManager.RouteSelected(id);
    }
    public void SetMap(int id)
    {
        foreach (GameObject go in maps)
            go.SetActive(false);
        maps[id-1].SetActive(true);

        Utils.RemoveAllChildsIn(container);
        for (int a = 1; a < 13; a++)
        {
            MapMainButton b = Instantiate(button);
            b.transform.SetParent(container);
            b.Init(this, a);
            b.transform.localScale = Vector3.one;
            if (a < 4 && id == 1)
                b.SetLocked(false);
            else
                b.SetLocked(true);
        }
    }
}
