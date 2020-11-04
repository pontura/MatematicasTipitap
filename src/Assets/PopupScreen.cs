using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupScreen : MonoBehaviour
{
    public GameObject panel;
    public Text field;

    public void Start()
    {
        panel.SetActive(false);
    }
    public void Init(string text)
    {
        panel.SetActive(true);
        field.text = text;
        Invoke("Delayed", 2);
    }
    void Delayed()
    {
        Time.timeScale = 0;
    }
    public void Close()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }
}
