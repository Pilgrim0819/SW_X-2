using UnityEngine;
using System.Collections;

public class ShipSelectorMouseClick : MonoBehaviour {

    public GameObject currentObject;
    public string ship;

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
        LoadPilotsForSelector pilotLoader = new LoadPilotsForSelector();
        PlayerDatas.setChosenShip(ship);

        pilotLoader.loadPilotsCards();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
