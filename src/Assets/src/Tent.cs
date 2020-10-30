using UnityEngine;
using System.Collections;

public class Tent : MainClass
{
    public SpecialItemInTent special1;
    public SpecialItemInTent special2;
    public SpecialItemInTent special3;

    public SpecialItemInTent diploma;

    void Start () {

        Events.OnMusic("carefree");

        if (Data.Instance.GetComponent<Diploma>().diploma == 0)
            diploma.Init(false);
        else diploma.Init(true);

        if (Data.Instance.GetComponent<Items>().unlockedItems > 2)
            special1.Init(true);
        else special1.Init(false);

        if (Data.Instance.GetComponent<Items>().unlockedItems > 5)
            special2.Init(true);
        else special2.Init(false);

        if (Data.Instance.GetComponent<Items>().unlockedItems > 8)
            special3.Init(true);
        else special3.Init(false);

    }
    public void BackPressed()
    {
        Data.Instance.LoadLevel("MainMenu", false);
    }
    public void Map()
    {
        Data.Instance.LoadLevel("Map", false);
    }
    public void DiplomaClicked()
    {
        if (Data.Instance.GetComponent<Diploma>().diploma == 0)
            Events.OnVoiceSay("Explicando diploma");
        else
            Data.Instance.GetComponent<Diploma>().Open();
    }
}
