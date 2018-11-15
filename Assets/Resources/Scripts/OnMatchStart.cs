using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PilotsXMLCSharp;
using ShipsXMLCSharp;

public class OnMatchStart : MonoBehaviour {

	private Mocker mocker = new Mocker();

    private const string PREFABS_FOLDER = "Prefabs";

    private const int offsetX = 500;
    private const int offsetY = 500;
    private const int offsetZ = 500;

	void Start() {
        StartCoroutine(CheckObjectsHaveStopped());

        mocker.mockPlayerSquadrons();

        foreach (Player player in MatchDatas.getPlayers())
        {
                int loopIndex = 0;

                foreach (LoadedShip loadedShip in player.getSquadron())
                {
                    Ship ship = loadedShip.getShip();
                    string shipType = ship.ShipId;
                    GameObject shipHolderPrefab = (GameObject)Resources.Load(PREFABS_FOLDER + "/SmallShipContainerPrefab", typeof(GameObject));
                    GameObject shipPrefab = (GameObject)Resources.Load(PREFABS_FOLDER + "/" + shipType, typeof(GameObject));

                    //TODO Tweak these coordinates!
                    int posX = 2000 + (loopIndex * offsetX);
                    int posY = 500;
                    int posZ = 2000;

                    GameObject shipHolderGameObject = (GameObject)GameObject.Instantiate(
                        shipHolderPrefab,
                        new Vector3(posX, posY, posZ),
                        Quaternion.identity
                    );

                    GameObject shipGameObject = (GameObject)GameObject.Instantiate(
                        shipPrefab,
                        new Vector3(posX + 2.81896f, posY - 0.08181581f, posZ + 3.286796f),
                        Quaternion.identity
                    );

                    shipGameObject.transform.SetParent(shipHolderGameObject.transform, true);

                    shipGameObject.GetComponent<ShipProperties>().setPilot(loadedShip.getPilot());
                    shipGameObject.GetComponent<ShipProperties>().setShip(loadedShip.getShip());

                    loopIndex++;
                };
        }
	}

	void Update() {
	
	}

    IEnumerator CheckObjectsHaveStopped()
    {
        Rigidbody[] GOS = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];
        bool allSleeping = false;

        while (!allSleeping)
        {
            allSleeping = true;

            foreach (Rigidbody GO in GOS)
            {
                if (!GO.IsSleeping())
                {
                    allSleeping = false;
                    yield return null;
                    break;
                }
            }

        }
        
        DiceRollerBase.getDiceResults(GOS);
    }
}
