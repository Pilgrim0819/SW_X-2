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

        List<LoadedShip> squadron1 = new List<LoadedShip>();
        foreach (LoadedShip ship in PlayerDatas.getSquadron())
        {
            squadron1.Add(ship);
        }

        Player player1 = new Player();
        player1.setChosenSide("Rebels");
        player1.setPlayerName("Player Number 1");
        player1.setSquadron(squadron1);

        PlayerDatas.deleteSquadron();

        Ships ships2 = XMLLoader.getShips("imperial_ships.xml");
        Pilots pilots2 = XMLLoader.getPilots("TIE Fighter_pilots.xml");

        LoadedShip ship3 = new LoadedShip();
        ship3.setShip(ships2.Ship[0]);
        ship3.setPilot(pilots2.Pilot[0]);
        PlayerDatas.setSelectedShip(ship3.getShip());
        PlayerDatas.addPilotToSquadron(ship3.getPilot());

        LoadedShip ship4 = new LoadedShip();
        ship4.setShip(ships2.Ship[0]);
        ship4.setPilot(pilots2.Pilot[1]);
        PlayerDatas.setSelectedShip(ship4.getShip());
        PlayerDatas.addPilotToSquadron(ship4.getPilot());

        LoadedShip ship5 = new LoadedShip();
        ship5.setShip(ships2.Ship[0]);
        ship5.setPilot(pilots2.Pilot[2]);
        PlayerDatas.setSelectedShip(ship5.getShip());
        PlayerDatas.addPilotToSquadron(ship5.getPilot());

        Player player2 = new Player();
        player2.setChosenSide("Empire");
        player2.setPlayerName("Player Number 2");
        player2.setSquadron(PlayerDatas.getSquadron());

        MatchDatas.addPlayer(player1);
        MatchDatas.addPlayer(player2);

        PlayerDatas.setPlayerName("Player Number 2");
    }

}
