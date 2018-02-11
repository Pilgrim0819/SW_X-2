using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnMatchStart : MonoBehaviour {

	private Mocker mocker = new Mocker();

    private const string PREFABS_FOLDER = "Prefabs";

    private const int offsetX = 500;
    private const int offsetY = 500;
    private const int offsetZ = 500;

	void Start() {
		mocker.mockPlayerSquadrons();

		List<LoadedShip> ships = new List<LoadedShip>();
        ships = PlayerDatas.getSquadron();

        int loopIndex = 0;

        foreach (LoadedShip ls in ships)
        {
            string shipType = ls.getShip().ShipId;
            GameObject shipPrefab = (GameObject)Resources.Load(PREFABS_FOLDER + "/" + shipType, typeof(GameObject));

            //TODO Tweak these coordinates!
            int posX = 2000 + (loopIndex * offsetX);
            int posY = 0;
            int posZ = 2000;

            GameObject ship = (GameObject)GameObject.Instantiate(
                shipPrefab,
                new Vector3(posX, posY, posZ),
                Quaternion.identity
            );

            loopIndex++;
        }
	}

	void Update() {
	
	}
}
