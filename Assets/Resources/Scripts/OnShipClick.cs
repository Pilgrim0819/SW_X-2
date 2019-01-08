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
                GameObject target = hit.transform.gameObject;
                LoadedShip clickedShip = new LoadedShip();

                //TODO Show basic ship info (level, attack, agility, shield, hull, pilot talent, upgrade slots(JUST text!!), actions (just image/text))

                int playerIndex = 0;

                foreach (Player player in MatchDatas.getPlayers())
                {
                    foreach (LoadedShip ship in player.getSquadron())
                    {
                        if (ship.getPilot().Equals(target.GetComponent<ShipProperties>().getPilot()))
                        {
                            clickedShip.setShip(target.GetComponent<ShipProperties>().getShip());
                            clickedShip.setPilot(target.GetComponent<ShipProperties>().getPilot());

                            MatchDatas.getPlayers()[playerIndex].setSelectedShip(clickedShip);

                            // TODO IF! this part is needed, get the player ID who clicked on the ship!!
                            if (player.getPlayerID() == 1)
                            {
                                player.setActiveShip(clickedShip);
                            }
                        }
                    }

                    playerIndex++;
                }
            }
        }
    }
}
