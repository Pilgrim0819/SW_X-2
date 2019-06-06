using ShipsXMLCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MatchHandlerService {

    private bool levitateShipsUpwards = true;

	public void instantiateShips()
    {
        foreach (Player player in MatchDatas.getPlayers())
        {
            int loopIndex = 0;
            Vector3 startingPosition = MatchHandlerUtil.getShipCollectionHolderPosition(player.getPlayerID());

            foreach (LoadedShip loadedShip in player.getSquadron())
            {
                Ship ship = loadedShip.getShip();
                string shipType = ship.ShipId;
                GameObject shipHolderPrefab = (GameObject)Resources.Load(MatchHandlerConstants.PREFABS_FOLDER + "/SmallShipContainerPrefab", typeof(GameObject));
                GameObject shipPrefab = (GameObject)Resources.Load(MatchHandlerConstants.PREFABS_FOLDER + "/" + shipType, typeof(GameObject));

                //TODO Tweak these coordinates!
                float posX = startingPosition.x + (loopIndex * MatchHandlerConstants.offsetX);
                float posY = startingPosition.y;
                float posZ = startingPosition.z;

                GameObject shipHolderGameObject = (GameObject)GameObject.Instantiate(
                    shipHolderPrefab,
                    new Vector3(posX, posY, posZ),
                    Quaternion.identity
                );

                // TODO change models, so offset is not needed anymore!
                Vector3 shipOffset = new Vector3(2.81896f * 500.0f, 0.0f, 3.286796f * 500.0f);

                GameObject shipGameObject = (GameObject)GameObject.Instantiate(
                    shipPrefab,
                    shipHolderGameObject.GetComponent<Renderer>().bounds.center + shipOffset,
                    Quaternion.identity
                );

                shipGameObject.transform.SetParent(shipHolderGameObject.transform, true);

                shipHolderGameObject.GetComponent<ShipProperties>().setLoadedShip(loadedShip);

                /***********************************/
                FiringArc arc = new FiringArc();
                arc.leftBoundary = -45.0f;
                arc.rightBoundary = 45.0f;
                /***********************************/

                generateFiringArc(shipHolderGameObject, arc);

                loopIndex++;
            };
        }
    }

    public void generateFiringArc(GameObject target, FiringArc arc)
    {
        GameObject canvas = target.transform.Find("Canvas").gameObject;
        GameObject firingArc = canvas.transform.Find("FiringArc").gameObject;
        float angle = arc.rightBoundary - arc.leftBoundary;

        canvas.transform.localPosition = new Vector3(0.0f, 0.002f, 0.0f);
        firingArc.GetComponent<Image>().fillOrigin = (int)Image.Origin360.Top;
        firingArc.GetComponent<Image>().fillAmount = angle / 360.0f;
        firingArc.transform.Rotate(0.0f, 0.0f, angle/2, Space.Self);

        // Remove activators!! Handle toggle elsewhere....
        firingArc.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
    }

    public bool isReachable()
    {
        // TODO

        return false;
    }

    // Just the basic....
    public bool isInsideFiringArc(List<FiringArc> arcs, GameObject ship, GameObject target)
    {
        float angle = Vector3.Angle(ship.transform.forward, ship.transform.position - target.transform.position);

        foreach (FiringArc arc in arcs)
        {
            if (arc.leftBoundary <= angle && arc.rightBoundary >= angle)
            {
                return true;
            }
        }

        return false;
    }

    // Buggy!! Works, but keeps speeding up (like a ping pong ball....)
    public void levitateShips()
    {
        /*foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("SmallShipContainer"))
        {
            gameObj.transform.Translate(Vector3.up * (levitateShipsUpwards ? 1 : -1));

            if (gameObj.transform.position.y <= -550 || gameObj.transform.position.y >= -450)
            {
                levitateShipsUpwards = !levitateShipsUpwards;
            }
        }*/
    }

    // This is just an idea!!! Needs further development......
    public float getTargetDistance(GameObject origin, GameObject target)
    {
        float result = 0.0f;

        RaycastHit hit;
        Ray downRay = new Ray(origin.transform.position, -Vector3.up);
        
        // TODO check, if target is even inside the firing arc!!!

        // Hit should be the first target the ray touches...
        if (Physics.Raycast(downRay, out hit))
        {
            result = hit.distance;
        }

            return result;
    }
    // *****************************************************
}
