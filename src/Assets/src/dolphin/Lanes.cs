using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lanes : MonoBehaviour {

    //public GameObject background;
   // public List<GameObject> backgrounds;
    public Lane[] all;
    public int laneActiveID = 3;
   // public GameObject enemy;

	void Start () {
        //Events.OnPoolAllItemsInScene += OnPoolAllItemsInScene;
	}
    void OnDestroy()
    {
      //  Events.OnPoolAllItemsInScene -= OnPoolAllItemsInScene;
    }
   
    public Lane GetActivetLane()
    {
        return all[laneActiveID];
    }
   
    public void AddObjectToLane(DolphinObject obj, int laneId, int _x, DolphinObjectSettings settings )
    {
        sortInLayersByLane(obj.gameObject, laneId);

        obj.transform.SetParent(all[laneId].transform);
        obj.transform.localPosition = new Vector3(_x, 0, 0);
        obj.Init(settings, laneId);

    }
    public void sortInLayersByLane(GameObject go, int laneId)
    {
         SpriteRenderer[] renderers = go.GetComponentsInChildren<SpriteRenderer>(true);
         foreach (SpriteRenderer sr in renderers)
             sr.sortingLayerName = "lane" + laneId;
    }
    //public void changeEnemyLane(Enemy enemy, Lane lane)
    //{
    //    enemy.transform.SetParent(lane.transform);
    //    sortInLayersByLane(enemy.gameObject, lane.id);

    //    Vector2 pos = enemy.transform.localPosition;  
    //    pos.y = 0;
    //    enemy.transform.localPosition = pos;

    //    enemy.laneId = lane.id;
    //}
    public bool TryToChangeLane(bool up)
    {
        if (up && laneActiveID < all.Length - 1)
        {
            laneActiveID++;
            return true;
        } else if (!up && laneActiveID > 0)
        {
            laneActiveID--;
            return true;
        }
        return false;
    }
}
