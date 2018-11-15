using UnityEngine;
using System.Collections;
using System;

public class EventActionExpertHandling : CustomEventBase
{

    public EventActionExpertHandling(LoadedShip owner) : base(owner, "Expert handling")
    {

    }

    public override void doEventAction()
    {
        //SHOW barrel roll direction choser
        //Do barrel roll (move ship)

        bool hasBarrelRoll = false;

        foreach (string action in owner.getShip().Actions.Action)
        {
            if (action.Equals("barrel roll"))
            {
                hasBarrelRoll = true;
            }
        }

        if (!hasBarrelRoll)
        {
            //Add a stress token
        }
    }

    public override void cleanup()
    {
        //SHOW prompt to remove a target lock if wanted...
    }
}
