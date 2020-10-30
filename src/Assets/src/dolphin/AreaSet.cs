using UnityEngine;
using System.Collections;

public class AreaSet : MonoBehaviour
{

    public int competitionsPriority;
    public bool randomize = true;
    public DolphinLevel[] levels;
    public int distance;

    [HideInInspector]
    public int id = 0;

    public Vector3 getCameraOrientation()
    {
        return new Vector3(0, 0, 0);
    }

    public DolphinLevel GetLevel()
    {
        DolphinLevel level;

        Random.seed = (int)System.DateTime.Now.Ticks;

        if (randomize)
            level = levels[Random.Range(0, levels.Length)];
        else
            level = levels[id];

        if (id < levels.Length - 1)
            id++;

        return level;
    }
}
