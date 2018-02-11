using UnityEngine;
using System.Collections;
using PilotsXMLCSharp;
using ShipsXMLCSharp;
using System.Collections.Generic;

public class PlayerDatas {
    private static Pilots pilots = new Pilots();
    private static Ship selectedShip;
    private static string chosenSide;
    private static string chosenShip;
    private static int pointsToSpend = 100;
    private static List<LoadedShip> squadron = new List<LoadedShip>();
    private static int currentPilotId = 1;

    public static void setChosenSide(string side)
    {
        chosenSide = side;
    }

    public static string getChosenSide()
    {
        return chosenSide;
    }

    public static void setChosenShip(string ship)
    {
        chosenShip = ship;
    }

    public static string getChosenShip()
    {
        return chosenShip;
    }

    public static Pilots getPilots()
    {
        return pilots;
    }

    public static void setSelectedShip(Ship ship)
    {
       selectedShip = ship;
    }

    public static Ship getSelectedShip()
    {
        return selectedShip;
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

}
