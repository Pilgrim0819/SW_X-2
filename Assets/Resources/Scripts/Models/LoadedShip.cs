using PilotsXMLCSharp;
using ShipsXMLCSharp;
using System;
using System.Collections.Generic;

/*This represents a whole ship with a pilot and upgrades*/
[System.Serializable]
public class LoadedShip {

    private Pilot pilot;
    private Ship ship;
	private int pilotId;
    private bool alive = true;
    private bool hasBeenActivatedThisRound = false;
    private bool beforeAction = false;
    private int numOfActions = 0;
    private LoadedShip target;
    private List<CustomEventBase> eventActions = new List<CustomEventBase>();
    private Maneuver plannedManeuver;
    private List<IToken> tokens = new List<IToken>();
    private List<string> prevActions = new List<string>();

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
        this.alive = alive;
    }

    public bool isAlive()
    {
        return this.alive;
    }

    public LoadedShip getTarget()
    {
        return this.target;
    }

    public void setTarget(LoadedShip pTarget)
    {
        this.target = pTarget;
    }

    public void setHasBeenActivatedThisRound(bool value)
    {
        this.hasBeenActivatedThisRound = value;
    }

    public bool isHasBeenActivatedThisRound()
    {
        return this.hasBeenActivatedThisRound;
    }

    public void addToken(IToken token)
    {
        this.tokens.Add(token);
    }

    public void removeToken(IToken token)
    {
        this.tokens.Remove(token);
    }

    public int getTokenIdByType(Type clazz)
    {
        int result = 0;

        foreach (IToken t in this.tokens)
        {
            if (t.GetType() == clazz)
            {
                result = t.getId();
            }
        }

        return result;
    }

    public void removeTokenById(Type clazz, int id)
    {
        foreach (IToken t in this.tokens)
        {
            if (t.GetType() == clazz && t.getId() == id)
            {
                tokens.Remove(t);
            }
        }
    }

    public List<IToken> getTokens()
    {
        return this.tokens;
    }

    public bool isBeforeAction()
    {
        return this.beforeAction;
    }

    public void setBeforeAction(bool beforeAction)
    {
        this.beforeAction = beforeAction;
    }

    public void registerPreviousAction(string action)
    {
        this.prevActions.Add(action);
    }

    public List<string> getPreviousActions()
    {
        return this.prevActions;
    }

    public void deletePreviousActions()
    {
        this.prevActions.Clear();
    }

    public void setNumOfActions(int val)
    {
        this.numOfActions = val;
    }

    public int getNumOfActions()
    {
        return this.numOfActions;
    }

    public void addAction()
    {
        this.numOfActions++;
    }

    public void removeAction()
    {
        this.numOfActions--;
    }
}
