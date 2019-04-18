using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour {

	void Start () {
        Player player = new Player();

        // TODO set player parameters (like name, squadpoints, etc..) from config?

        LocalDataWrapper.setPlayer(player);
	}
}
