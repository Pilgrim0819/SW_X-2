using UnityEngine;
using System.Collections;
using PilotsXMLCSharp;
using ShipsXMLCSharp;
using System.Collections.Generic;

/*ONLY FOR DEV TESTING!! Pre-loads two players to be used directly in the macth scene (without squadron building...)*/
public class Mocker {

    private List<Vector3> mockPositions = new List<Vector3>();
    private int positionIndex = 0;

    public Mocker()
    {
        //TODO Tune these predefined coordinates!!!
        mockPositions.Add(new Vector3(4300.0f, -500.0f, 4350.0f));
        mockPositions.Add(new Vector3(3800.0f, -500.0f, 4350.0f));
        mockPositions.Add(new Vector3(3300.0f, -500.0f, 4350.0f));
        mockPositions.Add(new Vector3(2800.0f, -500.0f, 4350.0f));
        mockPositions.Add(new Vector3(2300.0f, -500.0f, 4350.0f));
        mockPositions.Add(new Vector3(1800.0f, -500.0f, 4350.0f));
        mockPositions.Add(new Vector3(1300.0f, -500.0f, 4350.0f));
        mockPositions.Add(new Vector3(800.0f, -500.0f, 4350.0f));
        mockPositions.Add(new Vector3(300.0f, -500.0f, 4350.0f));
        mockPositions.Add(new Vector3(-800.0f, -500.0f, 4350.0f));
    }

    public Vector3 getNextMockPosition()
    {
        return mockPositions[positionIndex++];
    }

	public void mockPlayerSquadrons()
    {
        Ships ships = XMLLoader.getShips("rebel_ships.xml");
        Pilots pilots = XMLLoader.getPilots("t65xw_pilots.xml");

        LoadedShip ship1 = new LoadedShip();
        ship1.setShip(ships.Ship[0]);
        ship1.setPilot(pilots.Pilot[0]);
        //Adding event actions to be registered for event handling.
        //TODO Is the input parameter correct/enough??????
        ship1.addEventAction(new EventActionWedgeAntilles(ship1));
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

            //Registering event actions...
            foreach (CustomEventBase eventAction in ship.getEventActions())
            {
                EventActionRegister.registerEventAction(eventAction);
            }
        }

        Player player1 = new Player();
        player1.setChosenSide("Rebels");
        player1.setPlayerName("Human Player");
        player1.setPLayerID(1);
        player1.setSquadron(squadron1);
        player1.setAI(false);

        PlayerDatas.setPlayer(player1);

        PlayerDatas.deleteSquadron();

        Ships ships2 = XMLLoader.getShips("imperial_ships.xml");
        Pilots pilots2 = XMLLoader.getPilots("tief_pilots.xml");

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

        LoadedShip ship6 = new LoadedShip();
        ship6.setShip(ships2.Ship[0]);
        ship6.setPilot(pilots2.Pilot[0]);
        PlayerDatas.setSelectedShip(ship6.getShip());
        PlayerDatas.addPilotToSquadron(ship6.getPilot());

        LoadedShip ship7 = new LoadedShip();
        ship7.setShip(ships2.Ship[0]);
        ship7.setPilot(pilots2.Pilot[0]);
        PlayerDatas.setSelectedShip(ship7.getShip());
        PlayerDatas.addPilotToSquadron(ship7.getPilot());

        LoadedShip ship8 = new LoadedShip();
        ship8.setShip(ships2.Ship[0]);
        ship8.setPilot(pilots2.Pilot[0]);
        PlayerDatas.setSelectedShip(ship8.getShip());
        PlayerDatas.addPilotToSquadron(ship8.getPilot());

        Player player2 = new Player();
        player2.setChosenSide("Empire");
        player2.setPlayerName("AI Player");
        player2.setPLayerID(2);
        player2.setSquadron(PlayerDatas.getSquadron());
        player2.setAI(true);

        MatchDatas.addPlayer(player1);
        MatchDatas.addPlayer(player2);

        PlayerDatas.setPlayerName("Human Player");
    }
}
