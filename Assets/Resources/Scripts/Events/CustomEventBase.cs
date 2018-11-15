using UnityEngine;
using System.Collections;

public abstract class CustomEventBase {

    protected LoadedShip owner;
    protected string name;

    public CustomEventBase(LoadedShip pOwner, string pName)
    {
        owner = pOwner;
        name = pName;
    }

    public abstract void doEventAction();

    public abstract void cleanup();

    public string getName()
    {
        return name;
    }

    public LoadedShip getOwner()
    {
        return owner;
    }

}
