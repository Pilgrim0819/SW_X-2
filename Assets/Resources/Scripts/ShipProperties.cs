using UnityEngine;
using System.Collections;
using PilotsXMLCSharp;
using ShipsXMLCSharp;

public class ShipProperties : MonoBehaviour {

    private Ship ship;
    private Pilot pilot;

    public void setShip(Ship ship)
    {
        this.ship = ship;
    }

    public Ship getShip()
    {
        return this.ship;
    }

    public void setPilot(Pilot pilot)
    {
        this.pilot = pilot;
    }

    public Pilot getPilot()
    {
        return this.pilot;
    }
}
