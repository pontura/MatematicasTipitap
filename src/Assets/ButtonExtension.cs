using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonExtension : MonoBehaviour {

    void OnEnable()
    {
        foreach (Button btn in GetComponentsInChildren<Button>())
        {
            btn.onClick.AddListener(TaskOnClick);
        }
    }
    void OnDisable()
    {
        foreach (Button btn in GetComponentsInChildren<Button>())
        {
            btn.onClick.RemoveListener(TaskOnClick);
        }
    }
    void TaskOnClick()
    {
        Events.OnButtonClick();
    }
}
