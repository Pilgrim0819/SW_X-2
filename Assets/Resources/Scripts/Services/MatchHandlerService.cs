﻿using ShipsXMLCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                loopIndex++;
            };
        }
    }


    // Buggy!! Works, but keeps speeding up (like a ping pong ball....)
    public void levitateShips()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SmallShipContainer"))
        {
            go.transform.Translate(Vector3.up * (levitateShipsUpwards ? 1 : -1));

            if (go.transform.position.y <= -550 || go.transform.position.y >= -450)
            {
                levitateShipsUpwards = !levitateShipsUpwards;
            }
        }
    }
}
