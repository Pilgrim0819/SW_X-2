using UnityEngine;
using System.Collections;
using System;

public class OnShipClick : MonoBehaviour {

    public static Action<LoadedShip> onShipSelection { get; internal set; }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            target = hit.collider.gameObject;
            Debug.Log("Selected ship and pilot: " + target.GetComponent<ShipProperties>().getShip().ShipName + " - " + target.GetComponent<ShipProperties>().getPilot().Name);
        }

        return target;
    }
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject target = hit.transform.GetChild(0).gameObject;
                LoadedShip activeShip = new LoadedShip();

                //TODO Show basic ship info (level, attack, agility, shield, hull, pilot talent, upgrade slots(JUST text!!), actions (just image/text))

                //IF the selected ship is the current player's
                foreach (LoadedShip ship in MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getSquadron())
                {
                    if (ship.getPilot().Equals(target.GetComponent<ShipProperties>().getPilot()))
                    {
                        //TODO Show extra ship info (actions (clickable), maneuvers (clickable ONLY in planning phase or in case of extra movement!!), upgrades (can be activated))

                        //Set as active ship
                        activeShip.setShip(target.GetComponent<ShipProperties>().getShip());
                        activeShip.setPilot(target.GetComponent<ShipProperties>().getPilot());

                        MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].setActiveShip(activeShip);
                    }
                }
            }
        }
    }
}
