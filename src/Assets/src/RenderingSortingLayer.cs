using UnityEngine;
using System.Collections;

public class RenderingSortingLayer : MonoBehaviour {
     public int SortLayer = 0;
    // public int SortingLayerID = SortingLayer.GetLayerValueFromName("Default");
     // Use this for initialization
     void Start () {
         Renderer renderer = this.gameObject.GetComponent<Renderer>();
         if(renderer != null)
         {
             renderer.sortingLayerName = "laneOver";
             renderer.sortingOrder = 0;
           //  renderer.sortingLayerID = SortingLayerID;
         }
     }
     
 }
 