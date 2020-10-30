using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainClass : MonoBehaviour {

	void OnEnable () {
        Invoke("Done", 0.5f);
    }
    public void Done()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Metrics.OnView(sceneName);

        Events.OnSceneLoaded();
    }
}
