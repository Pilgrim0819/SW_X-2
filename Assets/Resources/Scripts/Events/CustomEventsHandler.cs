using UnityEngine;
using System.Collections;

public class CustomEventsHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        OnShipClick.onShipSelection += this.shipSelected;
	}

    void OnDisable()
    {
        OnShipClick.onShipSelection -= this.shipSelected;
    }

    public void shipSelected(LoadedShip ship)
    {
        Debug.Log("Custom event triggered!");

        GUIHandler guiHandler = new GUIHandler();

        MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].setActiveShip(ship);

        guiHandler.showActiveShipsCard(ship);
    }
}
