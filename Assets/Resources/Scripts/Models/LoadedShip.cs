using PilotsXMLCSharp;
using ShipsXMLCSharp;
using System.Collections.Generic;

/*This represents a whole ship with a pilot and upgrades*/
[System.Serializable]
public class LoadedShip {

    private Pilot pilot;
    private Ship ship;
	private int pilotId;
    private bool alive = true;
    private bool hasBeenActivatedThisRound = false;
    private LoadedShip target;
    private List<CustomEventBase> eventActions = new List<CustomEventBase>();
    private Maneuver plannedManeuver;
    private List<IToken> tokens = new List<IToken>();

    public void setPlannedManeuver(Maneuver m) {
        this.plannedManeuver = m;
    }

    public Maneuver getPlannedManeuver()
    {
        return this.plannedManeuver;
    }

    public List<CustomEventBase> getEventActions()
    {
        return eventActions;
    }

    public void addEventAction(CustomEventBase eventAction)
    {
        this.eventActions.Add(eventAction);
    }

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

    public void setAlive(bool alive)
    {
        alive = alive;
    }

    public bool isAlive()
    {
        return alive;
    }

    public LoadedShip getTarget()
    {
        return target;
    }

    public void setTarget(LoadedShip pTarget)
    {
        target = pTarget;
    }

    public void setHasBeenActivatedThisRound(bool value)
    {
        hasBeenActivatedThisRound = value;
    }

    public bool isHasBeenActivatedThisRound()
    {
        return hasBeenActivatedThisRound;
    }

    public void addToken(IToken token)
    {
        this.tokens.Add(token);
    }

    public void removeToken(IToken token)
    {
        tokens.Remove(token);
    }
}
