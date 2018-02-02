using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class ShipSelectorMouseClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public GameObject currentObject;

    private int onMouseOverOffsetY = 250;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color background = new Color(255,255,255,255);
        Image img = currentObject.GetComponent<Image>();
        img.color = background;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color background = new Color(255, 255, 255, 163);
        Image img = currentObject.GetComponent<Image>();
        img.color = background;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Pointer click event!!");
        LoadPilotsForSelector pilotLoader = new LoadPilotsForSelector();
        string shipName = currentObject.transform.Find("Ship Name").gameObject.GetComponent<UnityEngine.UI.Text>().text;
        PlayerDatas.setChosenShip(shipName);

        pilotLoader.loadPilotsCards();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
