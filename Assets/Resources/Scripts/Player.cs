using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

    private string playerName;
    private List<LoadedShip> squadron = new List<LoadedShip>();
    private string chosenSide;
    private List<int> lastDiceResults = new List<int>();
    private LoadedShip activeShip;
    private LoadedShip selectedShip;
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
        this.activeShip = ship;
    }

    public LoadedShip getActiveShip()
    {
        return this.activeShip;
    }

    public void setSelectedShip(LoadedShip ship)
    {
        this.selectedShip = ship;
    }

    public LoadedShip getSelectedhip()
    {
        return this.selectedShip;
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

    //TODO make it a List, so every ship with the same level (if not yet used!) will be returned
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
