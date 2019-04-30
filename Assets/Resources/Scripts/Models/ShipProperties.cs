using UnityEngine;
using System.Collections;
using PilotsXMLCSharp;
using ShipsXMLCSharp;
using System.Collections.Generic;

/*The 3D container uses this model to track which ship instance it holds*/
public class ShipProperties : MonoBehaviour {

    private LoadedShip loadedShip;
    private bool movable = true;
    private List<IToken> tokens = new List<IToken>();

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
        return this.movable;
    }

    public void setMovable(bool m)
    {
        this.movable = m;
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
