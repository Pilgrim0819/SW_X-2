using UnityEngine;
using System.Collections;
using PilotsXMLCSharp;
using System.Collections.Generic;

public class PlayerDatas {
    private static string chosenSide;
    private static string chosenShip;
    private static Pilots pilots = new Pilots();
    private static int pointsToSpend = 100;

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

    public static void addPilotToSquadron(Pilot pilot)
    {
        bool canAddPilot = true;

        if (pilots.Pilot == null || pilots.Pilot.Capacity == 0)
        {
            pilots.Pilot = new List<Pilot>();
        }

        if (pilot.Unique)
        {
            foreach (Pilot p in pilots.Pilot)
            {
                if (p.Name.Equals(pilot.Name))
                {
                    canAddPilot = false;
                }
            }
        }

        if ((getCumulatedSquadPoints() + pilot.Cost) > pointsToSpend)
        {
            canAddPilot = false;
        }

        if (canAddPilot)
        {
            pilots.Pilot.Add(pilot);
        } else
        {
            //TODO show error messages!
            Debug.Log("Unique pilot already added or squad point limit would be exceeded!");
        }
    }

    public static void removePilotFromSquadron(Pilot pilot)
    {
        if (pilots.Pilot != null && pilots.Pilot.Capacity > 0) {
            pilots.Pilot.Remove(pilot);
        }
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

        if (pilots.Pilot != null && pilots.Pilot.Capacity > 0)
        {
            foreach (Pilot pilot in pilots.Pilot)
            {
                total += System.Convert.ToInt32(pilot.Cost);
            }
        }

        return total;
    }

}
