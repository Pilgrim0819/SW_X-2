using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

    private string playerName;
    private List<LoadedShip> squadron = new List<LoadedShip>();
    private string chosenSide;
    private List<int> lastDiceResults = new List<int>();
    private LoadedShip activeShip;
    private bool hasInitiative = false;
    private int playerID;

    public void setPLayerID(int id)
    {
        playerID = id;
    }

    public int getPlayerID()
    {
        return playerID;
    }

    public void setInitiative()
    {
        hasInitiative = true;
    }

    public bool getHasInitiative()
    {
        return hasInitiative;
    }

    public void setPlayerName(string name)
    {
        playerName = name;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public void setChosenSide(string side)
    {
        chosenSide = side;
    }

    public string getChosenSide()
    {
        return chosenSide;
    }

    public void setSquadron(List<LoadedShip> ships)
    {
        squadron = ships;
    }

    public List<LoadedShip> getSquadron()
    {
        return squadron;
    }

    public void setLastDiceResults(List<int> results)
    {
        lastDiceResults = results;
    }

    public List<int> getLastDiceResults()
    {
        return lastDiceResults;
    }

    public void setActiveShip(LoadedShip ship)
    {
        Debug.Log("Active ship is set to: " + ship.getShip().ShipName + " - " + ship.getPilot().Name);
        this.activeShip = ship;
    }

    public LoadedShip getActiveShip()
    {
        return this.activeShip;
    }

    public int getCumulatedSquadPoints()
    {
        int result = 0;

        foreach(LoadedShip ship in squadron)
        {
            result += ship.getPilot().Cost;
        }

        return result;
    }
}
