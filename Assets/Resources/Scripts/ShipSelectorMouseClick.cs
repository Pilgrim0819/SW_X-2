using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using ShipsXMLCSharp;

public class ShipSelectorMouseClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    public GameObject currentObject;
    private Ship ship;

    public void setShip(Ship ship)
    {
        this.ship = ship;
    }

    public Ship getShip()
    {
        return this.ship;
    }

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
        LoadPilotsForSelector pilotLoader = new LoadPilotsForSelector();
        string shipName = currentObject.transform.Find("Ship Name").gameObject.GetComponent<UnityEngine.UI.Text>().text;
        PlayerDatas.setSelectedShip(ship);
        
        SceneManager.LoadScene("Scene 4", LoadSceneMode.Single);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
