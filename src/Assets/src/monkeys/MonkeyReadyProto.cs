using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MonkeyReadyProto : MonoBehaviour {

	void Start () {
        Invoke("Done", 2);
	}
    void Done()
    {
        SceneManager.LoadScene("Game_Monkey");
    }
}
