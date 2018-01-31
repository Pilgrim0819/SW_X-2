using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using ShipsXMLCSharp;

public class OnShipSelectionSceneLoad : MonoBehaviour {

    private JSONLoader jsonLoader = new JSONLoader();

	// Use this for initialization
	void Start () {
        string chosenSide = PlayerDatas.getChosenSide();
        Ships ships = new Ships();

        switch (chosenSide)
        {
            case "Rebels":
                ships = XMLLoader.getShips("rebel_ships.xml");
                break;
            case "Empire":
                ships = XMLLoader.getShips("imperial_ships.xml");
                break;
        }

        foreach(ShipsXMLCSharp.Ship ship in ships.Ship)
        {
            
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
