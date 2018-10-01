using UnityEngine;
using System.Collections;

public class DiceRollerBase : MonoBehaviour {

    public ForceMode forceMode;
    public float force = 10.0f;
    public string button = "Fire1";
    private bool resultChecked = false;

    public const int DICE_RESULT_MISS = 0;
    public const int DICE_RESULT_FOCUS = 1;
    public const int DICE_RESULT_HIT_OR_EVADE = 2;
    public const int DICE_RESULT_CRIT = 3;

    private void Start()
    {
        Vector3 torque;

        torque.x = Random.Range(-200, 200) * 100;
        torque.y = Random.Range(-200, 200) * 100;
        torque.z = Random.Range(-200, 200) * 100;

        this.transform.rotation = Random.rotation;
        this.GetComponent<Rigidbody>().AddTorque(torque);
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
}
