using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SwipeDetector : MonoBehaviour
{
    private float minSwipeDistY = 15;
    private float minSwipeDistX = 20;

    private float startPosY;
    private float startPosX;

   // public Text debugText;

    public enum directions
    {
        NONE,
        UP,
        DOWN,
        RIGHT,
        LEFT
    }

    private float newTime;
    private bool touched;
    private bool movedByTime;
    private float timeSinceTouch;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    movedByTime = false;
                    newTime = 0;
                    touched = true;
                    startPosY = touch.position.y;
                    startPosX = touch.position.x;
                    break;
                case TouchPhase.Ended:
                    newTime = 0;
                    touched = false;
                    if (!movedByTime)
                        Move(touch.position.x, touch.position.y);
                    break;
            }

            if (touched)
            {
                newTime += Time.deltaTime;
            }

            if (newTime > 0.06f && touched && Mathf.Abs(touch.position.y - startPosY) > minSwipeDistY)
            {
                Move(touch.position.x, touch.position.y);
                movedByTime = true;                
                newTime = -0.32f;
            }


        }
    }
    void Move(float touchFinalPositionX, float touchFinalPositionY)
    {
        float diffY = startPosY - touchFinalPositionY;
        float diffX = startPosX - touchFinalPositionX;

        //if (debugText)
        //    debugText.text = "Y: " + diffY + " x: " + diffX;

        if (Mathf.Abs(diffY) > minSwipeDistY)
        {
            if (diffY < 0)
                Swipe(directions.UP);
            else
                Swipe(directions.DOWN);            
        }
        else
        {
           // debugText.text += " RIGHT";
            Swipe(directions.RIGHT);
        }
        
    }
    void Swipe(directions direction)
    {
        switch (direction)
        {
            case directions.UP:
                Events.OnSwipe(directions.UP); break;
            case directions.DOWN:
                Events.OnSwipe(directions.DOWN); break;
            case directions.RIGHT:               
                Events.OnSwipe(directions.RIGHT); break;
        }
    }
}