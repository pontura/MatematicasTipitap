using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class dificultPanel : MonoBehaviour {

    public Text field;
    
	void Start () {
        SetText(); 
	}
    public void Switch()
    {
        switch (Data.Instance.userdata.dificult)
        {
            case UserData.dificults.EASY: Data.Instance.userdata.ChangeDificult(UserData.dificults.MEDIUM); break;
            case UserData.dificults.MEDIUM: Data.Instance.userdata.ChangeDificult(UserData.dificults.HARD); break;
            case UserData.dificults.HARD: Data.Instance.userdata.ChangeDificult(UserData.dificults.EASY); break;
        }
        SetText();
    }
    void SetText()
    {
        field.text = Data.Instance.userdata.dificult.ToString();
    }
}
