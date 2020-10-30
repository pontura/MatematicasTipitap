using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DashboardLine : MonoBehaviour {

    public Text field;

	public void Init(string _text) {
        field.text = _text;
    }

}
