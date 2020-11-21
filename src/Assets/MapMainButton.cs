using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapMainButton : MonoBehaviour
{
    public Text field;
    public int id;
    MapMainMenuButtons mapMainMenuButtons;
    public GameObject locked;

    public void Init(MapMainMenuButtons mapMainMenuButtons, int id)
    {
        this.id = id;
        this.mapMainMenuButtons = mapMainMenuButtons;
        field.text = id.ToString();
    }

    public void OnClicked()
    {
        mapMainMenuButtons.OnClicked(id);
    }
    public void SetLocked(bool isLocked)
    {
        locked.SetActive(isLocked);
    }
}
