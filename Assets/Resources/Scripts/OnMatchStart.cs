using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

            /*GameObject ship = (GameObject)GameObject.Instantiate(
                shipPrefab,
                new Vector3(posX, posY, posZ),
                Quaternion.identity
            );*/

            loopIndex++;
        }
	}

	void Update() {
	
	}

    IEnumerator CheckObjectsHaveStopped()
    {
        print("checking... ");
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
        Debug.Log("All objects sleeping");
        getDiceResults(GOS);

        Debug.Log("Results size: " + PlayerDatas.getDiceResults().Capacity);

        foreach (int result in PlayerDatas.getDiceResults())
        {
            Debug.Log("Result: " + result);
        }
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
