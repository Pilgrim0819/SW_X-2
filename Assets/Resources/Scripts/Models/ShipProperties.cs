using UnityEngine;

/*The 3D container uses this model to track which ship instance it holds*/
public class ShipProperties : MonoBehaviour {

    private LoadedShip loadedShip;
    private bool movable = true;
    private int ownerId;

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

    public void setOwnerId(int playerId)
    {
        this.ownerId = playerId;
    }

    public int getOwnerId()
    {
        return this.ownerId;
    }
}
