using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryButton : MonoBehaviour
{
    public Text field;
    bool pressed;
    SummaryMinigame summaryMinigame;
    float initial_x;
    bool isRight;
    bool isDone;

    public void Init(SummaryMinigame summaryMinigame, string text)
    {
        this.summaryMinigame = summaryMinigame;
        field.text = text;
    }
    //public void OnPointerDown()
    //{
    //    initial_x = Input.mousePosition.x;
    //    pressed = true;
    //}
    public void SetResult(bool isRight)
    {
        if (isDone)
            return;

        isDone = true;
        if (isRight)
            GetComponent<Animation>().Play("okFinal");
        else
            GetComponent<Animation>().Play("wrongFinal");
        Invoke("Delayed", 1);
    }
    void Delayed()
    {
        Destroy(this.gameObject);
    }
    //private void Update()
    //{
    //    if (!pressed)
    //        return;
    //    if (Input.mousePosition.x > initial_x && !isRight)
    //    {
    //        isRight = true;
    //        GetComponent<Animation>().Play("ok");
    //    } else if (Input.mousePosition.x < initial_x && isRight)
    //    {
    //        isRight = false;
    //        GetComponent<Animation>().Play("wrong");
    //    }

    //}
}
