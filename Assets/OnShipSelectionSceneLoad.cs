﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OnShipSelectionSceneLoad : MonoBehaviour {

    public Text testObj;

    private JSONLoader jsonLoader = new JSONLoader();

	// Use this for initialization
	void Start () {
        string chosenSide = PlayerDatas.getChosenSide();
        Ships shipsCollection = new Ships();

        switch (chosenSide)
        {
            case "Rebels":
                shipsCollection = jsonLoader.getShips("rebel_ships.json");
                break;
            case "Empire":
                shipsCollection = jsonLoader.getShips("imperial_ships.json");
                break;
        }

        string testString = chosenSide + " ships (" + shipsCollection.ships.Length + "):";

        foreach(Ship ship in shipsCollection.ships)
        {
            testString += " " + ship.shipName;
        }

        testObj.text = testString;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
