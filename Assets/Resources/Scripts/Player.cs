﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player {

    private string playerName;
    private List<LoadedShip> squadron = new List<LoadedShip>();
    private string chosenSide;
    private List<int> lastDiceResults = new List<int>();

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
}
