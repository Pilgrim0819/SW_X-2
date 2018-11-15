using UnityEngine;
using System.Collections;
using System;

public class EventActionWedgeAntilles : CustomEventBase {

    public EventActionWedgeAntilles(LoadedShip owner) : base(owner, owner.getPilot().Name)
    {
        
    }

	public override void doEventAction()
    {
        if (MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getActiveShip().Equals(owner))
        {
            if (owner.getTarget().getShip().Agility - 1 > 0)
            {
                owner.getTarget().getShip().Agility--;
            }
        }
    }

    public override void cleanup()
    {
        if (MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getActiveShip().Equals(owner))
        {
            owner.getTarget().getShip().Agility++;
        }
    }

}
