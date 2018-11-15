using UnityEngine;
using System.Collections;

public class CustomEventFactory {

	public CustomEventBase getCustomEventAction(string action, LoadedShip owner)
    {
        switch(action)
        {
            case "asd":
                return new EventActionWedgeAntilles(owner);
                break;
            default:
                return null;
        }
    }

}
