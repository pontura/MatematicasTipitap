using UnityEngine;
using System.Collections;

public class DolphinObstacle : DolphinObject
{
    public override void OnCrashed() 
    {
        GetComponent<Animator>().Play("crash");
        Invoke("Pool", 3);
    } 
}
