using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Events.OnLevelComplete();
        //}
        //if (Input.GetKeyDown(KeyCode.A))
        //    Events.OnPowerUp(PowerupManager.types.CHUMBO);
        //else if (Input.GetKeyDown(KeyCode.S))
        //    Events.OnPowerUp(PowerupManager.types.MOTO);
        //else if (Input.GetKeyDown(KeyCode.D))
        //    Events.OnPowerUp(PowerupManager.types.GIL);
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Events.OnSwipe(SwipeDetector.directions.UP);
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Events.OnSwipe(SwipeDetector.directions.DOWN);
        else if (Input.GetKeyDown(KeyCode.Space))
            Events.OnSwipe(SwipeDetector.directions.RIGHT);

        //RaycastHit hit;
        //Ray ray;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    ray = UICamera.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider != null)
        //        {
        //            Events.OnUIClicked(hit.collider.gameObject);
        //            return;
        //        }
        //    }
        //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider.tag == "Hero")
        //        { }
        //        }

        //}
    }
}
