using UnityEngine;
using System.Collections;
using PilotsXMLCSharp;
using ShipsXMLCSharp;
using System.Collections.Generic;

public class PlayerDatas {
    private static Ship selectedShip;
    private static Pilot selectedPilot;
    private static string chosenSide;
    private static int pointsToSpend = 100;
    private static List<LoadedShip> squadron = new List<LoadedShip>();
    private static int currentPilotId = 1;
    private static List<int> lastDiceResults = new List<int>();
    private static float dt = 0.0f;
    private static string playerName;

    public static int numberOfDice = 5;

    public static void setPlayerName(string name)
    {
        playerName = name;
    }

    public static string getPlayerName()
    {
        return playerName;
    }

    public static void setChosenSide(string side)
    {
        chosenSide = side;
    }

    public static string getChosenSide()
    {
        return chosenSide;
    }

    public static void setSelectedShip(Ship ship)
    {
       selectedShip = ship;
    }

    public static Ship getSelectedShip()
    {
        return selectedShip;
    }

    public static void setSelectedPilot(Pilot pilot)
    {
        selectedPilot = pilot;
    }

    public static Pilot getSelectedPilot()
    {
        return selectedPilot;
    }

    public static void addPilotToSquadron(Pilot pilot)
    {
        bool canAddPilot = true;
		bool duplicate = false;

		foreach (LoadedShip ls in squadron)
		{
			if (ls.getPilot().Name.Equals(pilot.Name))
			{
				duplicate = true;
			}
		}

		if (pilot.Unique && duplicate) {
			canAddPilot = false;
		}

        if ((getCumulatedSquadPoints() + pilot.Cost) > pointsToSpend)
        {
            canAddPilot = false;
        }

        if (canAddPilot)
        {
            //pilots.Pilot.Add(pilot);

            LoadedShip ls = new LoadedShip();
            ls.setShip(selectedShip);
            ls.setPilot(pilot);
			ls.setPilotId (currentPilotId);

            squadron.Add(ls);

            currentPilotId++;
        } else
        {
            //TODO show error messages!
            Debug.Log("Unique pilot already added or squad point limit would be exceeded!");
        }
    }

	public static void removePilotFromSquadron(Pilot pilot, int pilotId)
    {
        //TODO Test if only one ship gets deleted when pilot is not unique!!
        LoadedShip shipToRemove = new LoadedShip();

        foreach (LoadedShip ls in squadron)
        {
			if (ls.getPilot().Name.Equals(pilot.Name) && ls.getPilotId() == pilotId)
            {
                shipToRemove = ls;
                break;
            }
        }

        squadron.Remove(shipToRemove);
    }

    public static void setPointsToSpend(int points)
    {
        pointsToSpend = points;
    }

    public static int getPointsToSpend()
    {
        return pointsToSpend;
    }

    public static int getCumulatedSquadPoints()
    {
        int total = 0;

        if (squadron != null && squadron.Capacity > 0)
        {
            foreach (LoadedShip ls in squadron)
            {
                total += System.Convert.ToInt32(ls.getPilot().Cost);
            }
        }

        return total;
    }

    public static List<LoadedShip> getSquadron()
    {
        return squadron;
    }

    public static void deleteSquadron()
    {
        squadron.Clear();
    }

    public static void addDiceResult(int result)
    {
        lastDiceResults.Add(result);
    }

    public static List<int> getDiceResults()
    {
        return lastDiceResults;
    }

    public static void deleteDiceResults()
    {
        lastDiceResults = new List<int>();
    }

    public static void addDeltaTime(float time)
    {
        dt += time;
    }

    public static float getDeltaTime()
    {
        return dt;
    }

    public static void resetDeltaTime()
    {
        dt = 0.0f;
    }
}
