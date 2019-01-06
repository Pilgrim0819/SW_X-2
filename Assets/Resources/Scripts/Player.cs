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

    public bool isDefeated()
    {
        bool result = true;

        foreach (LoadedShip ship in squadron)
        {
            // TODO DO NOT USE the hull value!!! Make another variable for ships which holds the CURRENT hull value!!
            if (ship.getShip().Hull != 0)
            {
                result = false;
                break;
            }
        }

        return result;
    }

    public LoadedShip getNextShip(int currentLevel, bool ascending)
    {
        foreach (LoadedShip ship in squadron)
        {
            if (ascending)
            {
                if (ship.getPilot().Level >= currentLevel && !ship.isHasBeenActivatedThisRound())
                {
                    return ship;
                }
            } else
            {
                if (ship.getPilot().Level <= currentLevel && !ship.isHasBeenActivatedThisRound())
                {
                    return ship;
                }
            }
        }

        return null;
    }
}
