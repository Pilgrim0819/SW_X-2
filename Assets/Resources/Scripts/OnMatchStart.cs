using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PilotsXMLCSharp;
using ShipsXMLCSharp;

public class OnMatchStart : MonoBehaviour {

	private Mocker mocker = new Mocker();

    private const string PREFABS_FOLDER = "Prefabs";
    private const int DICE_RESULT_MISS = 0;
    private const int DICE_RESULT_FOCUS = 1;
    private const int DICE_RESULT_HIT_OR_EVADE = 2;
    private const int DICE_RESULT_CRIT = 3;

    private const int offsetX = 500;
    private const int offsetY = 500;
    private const int offsetZ = 500;

	void Start() {
        StartCoroutine(CheckObjectsHaveStopped());

        mocker.mockPlayerSquadrons();

        foreach (Player player in MatchDatas.getPlayers())
        {
            if (player.getPlayerName().Equals("Player Number 1"))
            {
                int loopIndex = 0;

                foreach (LoadedShip loadedShip in player.getSquadron())
                {
                    Ship ship = loadedShip.getShip();
                    string shipType = ship.ShipId;
                    GameObject shipPrefab = (GameObject)Resources.Load(PREFABS_FOLDER + "/" + shipType, typeof(GameObject));

                    //TODO Tweak these coordinates!
                    int posX = 2000 + (loopIndex * offsetX);
                    int posY = 0;
                    int posZ = 2000;

                    GameObject shipGameObject = (GameObject)GameObject.Instantiate(
                        shipPrefab,
                        new Vector3(posX, posY, posZ),
                        Quaternion.identity
                    );

                    loopIndex++;
                };
            }
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
        getDiceResults(GOS);
    }

    private void getDiceResults(Rigidbody[] dice)
    {
        foreach (Rigidbody dye in dice)
        {
            Vector3 up = dye.transform.up;
            Vector3 right = dye.transform.right;
            Vector3 forward = dye.transform.forward;

            if (up.y > 0)
            {
                if (right.y < 0.1 && right.y > -0.1)
                {
                    if (forward.y > 0)
                    {
                        PlayerDatas.addDiceResult(DICE_RESULT_FOCUS);
                    }
                    else
                    {
                        PlayerDatas.addDiceResult(DICE_RESULT_MISS);
                    }
                }
                else
                {
                    if (right.y > 0)
                    {
                        PlayerDatas.addDiceResult(DICE_RESULT_CRIT);
                    }
                    else
                    {
                        PlayerDatas.addDiceResult(DICE_RESULT_HIT_OR_EVADE);
                    }
                }
            }
            else
            {
                if (right.y < 0.1 && right.y > -0.1)
                {
                    if (forward.y > 0)
                    {
                        PlayerDatas.addDiceResult(DICE_RESULT_MISS);
                    }
                    else
                    {
                        PlayerDatas.addDiceResult(DICE_RESULT_FOCUS);
                    }
                }
                else
                {
                    if (right.y > 0)
                    {
                        PlayerDatas.addDiceResult(DICE_RESULT_HIT_OR_EVADE);
                    }
                    else
                    {
                        PlayerDatas.addDiceResult(DICE_RESULT_HIT_OR_EVADE);
                    }
                }
            }
        }
    }
}
