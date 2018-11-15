using UnityEngine;
using System.Collections;

public class EventActionRegister {

    public static void registerEventAction(CustomEventBase eventAction)
    {
        switch(eventAction.getName())
        {
            //TODO Mapping of pilot/upgrade names and event action classes needed!!!!
            case "Wedge Antilles":
                DelegatesHandler.onBeforeAttack += eventAction.doEventAction;
                break;
        }
    }

}
