using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChangerManuAction : MonoBehaviour {

    public string sceneToLoad;

    public void changeSceneAction()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
