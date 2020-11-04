using UnityEngine;
using System.Collections;

public class MapInData : MonoBehaviour
{
    public Canvas canvas;

    public Map map1;
    public Map map2;
    public Map map3;
    public int mapID;

    void Start()
    {
        map1.gameObject.SetActive(false);
        map2.gameObject.SetActive(false);
        map3.gameObject.SetActive(false);

        Events.OnShowMap += OnShowMap;
    }
    void OnDestroy()
    {
        Events.OnShowMap -= OnShowMap;
    }
    void OnShowMap(bool showIt)
    {
        if (showIt)
        {
            canvas.enabled = true;
            SetActiveMap();
        }
        else
        {
            switch (mapID)
            {
                case 0: map1.GetComponent<Animation>().Play("map_off"); break;
                case 1: map2.GetComponent<Animation>().Play("map_off"); break;
                case 2: map3.GetComponent<Animation>().Play("map_off"); break;
            }
        }
    }
    void SetActiveMap()
    {
        map1.gameObject.SetActive(false);
        map2.gameObject.SetActive(false);
        map3.gameObject.SetActive(false);

        switch (mapID)
        {
            case 0: map1.gameObject.SetActive(true); map1.Init(); break;
            case 1: map2.gameObject.SetActive(true); map2.Init(); break;
            case 2: map3.gameObject.SetActive(true); map3.Init(); break;
        }
    }
}
