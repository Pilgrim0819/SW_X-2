using UnityEngine;
using System.Collections;

public class MenuAction : MonoBehaviour {

    public GameObject[] cameras;

    public void MENU_ACTION_rotateTo(int phase)
    {
        GameObject camera1 = GameObject.Find("Main Camera");
        Vector3 pos = camera1.transform.position;
        pos.z += 1000;

        camera1.transform.position = pos;

        GameObject mainMenu = GameObject.Find("Main Menu");
        mainMenu.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
