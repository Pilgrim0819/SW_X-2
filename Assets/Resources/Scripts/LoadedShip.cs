﻿using UnityEngine;
using System.Collections;
using PilotsXMLCSharp;
using ShipsXMLCSharp;

public class LoadedShip {

    private Pilot pilot;
    private Ship ship;
	private int pilotId;

	public int getPilotId(){
		return this.pilotId;
	}

	public void setPilotId(int pilotId){
		this.pilotId = pilotId;
	}

    public void setPilot(Pilot pilot)
    {
        this.pilot = pilot;
    }

    public Pilot getPilot()
    {
        return this.pilot;
    }

    public void setShip(Ship ship)
    {
        this.ship = ship;
    }

    public Ship getShip()
    {
        return this.ship;
    }
}