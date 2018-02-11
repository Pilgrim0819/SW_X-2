using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuAction : MonoBehaviour {

    public void MENU_ACTION_rotateTo(GameObject target)
    {
        SceneManager.LoadScene("Scene 2", LoadSceneMode.Single);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
