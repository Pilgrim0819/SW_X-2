using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using PilotsXMLCSharp;

public class AddPilotToSquardon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject currentObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Pilot pilot = new Pilot();
        //TODO: get pilot datas
        PlayerDatas.addPilotToSquadron(pilot);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
