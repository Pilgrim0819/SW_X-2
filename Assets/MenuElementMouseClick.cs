using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuElementMouseClick : MonoBehaviour {

    public GameObject currentObject;
    public string side;

    private int onMouseOverOffsetY = 250;

    private void OnMouseEnter()
    {
        Vector3 position = currentObject.transform.position;
        position.y += onMouseOverOffsetY;

        currentObject.transform.position = position;
    }

    private void OnMouseExit()
    {
        Vector3 position = currentObject.transform.position;
        position.y -= onMouseOverOffsetY;

        currentObject.transform.position = position;
    }

    private void OnMouseDown()
    {
        PlayerDatas.setChosenSide(side);
        SceneManager.LoadScene("scene3", LoadSceneMode.Single);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
