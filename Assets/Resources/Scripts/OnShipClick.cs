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

                int playerIndex = 0;

                foreach (Player player in MatchDatas.getPlayers())
                {
                    foreach (LoadedShip ship in player.getSquadron())
                    {
                        if (ship.getPilot().Equals(target.GetComponent<ShipProperties>().getLoadedShip().getPilot()))
                        {
                            clickedShip.setShip(target.GetComponent<ShipProperties>().getLoadedShip().getShip());
                            clickedShip.setPilot(target.GetComponent<ShipProperties>().getLoadedShip().getPilot());

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
