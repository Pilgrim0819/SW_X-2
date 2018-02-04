using UnityEngine;
using System.Collections;
using PilotsXMLCSharp;

public class PlayerDatas {
    private static string chosenSide;
    private static string chosenShip;
    private static Pilots pilots = new Pilots();

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
        pilots.Pilot.Add(pilot);
    }

    public static void removePilotFromSquadron(Pilot pilot)
    {
        pilots.Pilot.Remove(pilot);
    }

}
