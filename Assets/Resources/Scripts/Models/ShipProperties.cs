using UnityEngine;
using System.Collections;
using PilotsXMLCSharp;
using ShipsXMLCSharp;

/*The 3D container uses this model to track which ship instance it holds*/
public class ShipProperties : MonoBehaviour {

    private LoadedShip loadedShip;
    private bool movable = true;

    public void setLoadedShip(LoadedShip ship)
    {
        this.loadedShip = ship;
    }

    public LoadedShip getLoadedShip()
    {
        return this.loadedShip;
    }

    public bool isMovable()
    {
        return movable;
    }

    public void setMovable(bool m)
    {
        movable = m;
    }
}
