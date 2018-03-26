using UnityEngine;
using System.Collections;

public class OnShipClick : MonoBehaviour {

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
                LoadedShip activeShip = new LoadedShip();

                activeShip.setShip(target.GetComponent<ShipProperties>().getShip());
                activeShip.setPilot(target.GetComponent<ShipProperties>().getPilot());

                MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].setActiveShip(activeShip);
            }
        }
    }
}
