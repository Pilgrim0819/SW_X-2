using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnMatchStart : MonoBehaviour {

	private Mocker mocker = new Mocker();

	void Start() {
		mocker.mockPlayerSquadrons();

		List<LoadedShip> ships = new List<LoadedShip>();

		foreach (LoadedShip ls in ships) {
			Debug.Log("Ship: " + ls.getShip().ShipName + ", Pilot: " + ls.getPilot().Name);
		}
	}

	void Update() {
	
	}
}
