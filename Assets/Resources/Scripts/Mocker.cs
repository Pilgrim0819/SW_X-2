using UnityEngine;
using System.Collections;
using PilotsXMLCSharp;
using ShipsXMLCSharp;
using System.Collections.Generic;

public class Mocker {

	public void mockPlayerSquadrons()
    {
        Ships ships = XMLLoader.getShips("rebel_ships.xml");
        Pilots pilots = XMLLoader.getPilots("T-65 X-wing_pilots.xml");

        LoadedShip ship1 = new LoadedShip();
        ship1.setShip(ships.Ship[0]);
        ship1.setPilot(pilots.Pilot[0]);
		PlayerDatas.setSelectedShip(ship1.getShip());
		PlayerDatas.addPilotToSquadron(ship1.getPilot());

        LoadedShip ship2 = new LoadedShip();
        ship2.setShip(ships.Ship[0]);
        ship2.setPilot(pilots.Pilot[1]);
		PlayerDatas.setSelectedShip(ship2.getShip());
		PlayerDatas.addPilotToSquadron(ship2.getPilot());

        Ships ships2 = XMLLoader.getShips("imperial_ships.xml");
        Pilots pilots2 = XMLLoader.getPilots("TIE Fighter_pilots.xml");

        LoadedShip ship3 = new LoadedShip();
        ship3.setShip(ships.Ship[0]);
        ship3.setPilot(pilots.Pilot[0]);
    }

}
