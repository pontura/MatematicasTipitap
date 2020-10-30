using UnityEngine;
using System.Collections;

public class EndingItems : MonoBehaviour {

    public GameObject[] items;

    private int routeID;

    void Start () {
        routeID = Data.Instance.routes.routeID;
        int unlockedItems = 0;
        unlockedItems = Data.Instance.GetComponent<Items>().unlockedItems;

        foreach (GameObject item in items)
            item.SetActive(false);

        if (unlockedItems >= Data.Instance.routes.routeID)
        {
            print("unlockedItems: " + unlockedItems + " + routeID  " + Data.Instance.routes.routeID + " ----> ya gano el premio:");
        }
        else
        {
            int newItemID = unlockedItems + 1;
            int id = 0;
            foreach (GameObject item in items)
            {
                id++;
                if (id == newItemID)
                    item.SetActive(true);
            }

            Invoke("WonItem", 0.2f);
            print("unlockedItems: " + unlockedItems);
        }
	}
    void WonItem()
    {
        Events.WonItem(routeID);
    }
}
