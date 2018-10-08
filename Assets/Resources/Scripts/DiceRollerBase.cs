using UnityEngine;
using System.Collections;

public class DiceRollerBase : MonoBehaviour {

    private const string ATTACK_DYE_PREFAB_NAME = "Prefabs/attackDye";
    private const string DEFENSE_DYE_PREFAB_NAME = "Prefabs/defenseDye";
    public const int DICE_RESULT_MISS = 0;
    public const int DICE_RESULT_FOCUS = 1;
    public const int DICE_RESULT_HIT_OR_EVADE = 2;
    public const int DICE_RESULT_CRIT = 3;

    private static GameObject diceAreaHolder;

    private static ForceMode forceMode;
    private static float force = 10.0f;
    private static string button = "Fire1";
    private static bool resultChecked = false;

    public static void setUpDiceRollerBase(ForceMode pforceMode, float pforce, string pbutton)
    {
        diceAreaHolder = GameObject.Find("DiceAreaHolder");
        diceAreaHolder.SetActive(false);

        forceMode = pforceMode;
        force = pforce;
        button = pbutton;
    }

    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            resultChecked = false;
            PlayerDatas.resetDeltaTime();
            GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * force, forceMode);

            if (PlayerDatas.getDiceResults().Capacity == PlayerDatas.numberOfDice)
            {
                PlayerDatas.deleteDiceResults();
            }
        }
    }

    public static void getDiceResults(Rigidbody[] dice)
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

        Debug.Log("Dice results recorded!");
    }

    public static void showDiceArea(int numOfDice, bool attack)
    {
        Transform dyePrefab = null;

        if (attack)
        {
            dyePrefab = Resources.Load<Transform>(ATTACK_DYE_PREFAB_NAME);
        } else
        {
            dyePrefab = Resources.Load<Transform>(DEFENSE_DYE_PREFAB_NAME);
        }

        diceAreaHolder.SetActive(true);

        for(int i=0; i < numOfDice; i++)
        {
            Transform dye = (Transform)GameObject.Instantiate(dyePrefab, new Vector3(470 + i*30, 160, 425), Quaternion.identity);
            dye.transform.SetParent(diceAreaHolder.transform, false);

            Vector3 torque;

            torque.x = Random.Range(-200, 200) * 100;
            torque.y = Random.Range(-200, 200) * 100;
            torque.z = Random.Range(-200, 200) * 100;

            dye.transform.rotation = Random.rotation;
            dye.GetComponent<Rigidbody>().AddTorque(torque);
        }
    }

    public static void hideDiceArea()
    {
        diceAreaHolder.SetActive(false);
    }

    public static void clearDiceArea()
    {
        GameObject[] dice = GameObject.FindGameObjectsWithTag("attackDye");

        foreach (GameObject dye in dice)
        {
            Destroy(dye);
        }
    }

}
