using UnityEngine;
using System.Collections;

public class DiceRollerBase : MonoBehaviour {

    public ForceMode forceMode;
    public float force = 10.0f;
    public string button = "Fire1";
    private bool resultChecked = false;

    private void Start()
    {
        this.transform.rotation = Random.rotation;
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

        //Debug.Log("RC: "+resultChecked);
        //TODO Is there a better way of finding out if dice stopped after moving it...
        if (GetComponent<Rigidbody>().velocity.magnitude == 0.0 && PlayerDatas.getDeltaTime() > 0.3 && !resultChecked)
        {
            resultChecked = true;
            //Debug.Log("upv: " + this.transform.up + "; rightv: " + this.transform.right + "; forwardv: " + this.transform.forward);
            Vector3 up = this.transform.up;
            Vector3 right = this.transform.right;
            Vector3 forward = this.transform.forward;
            //Debug.Log("up: " + transform.eulerAngles.y + "; right: " + transform.eulerAngles.x + "; forward: " + transform.eulerAngles.z);
            Debug.Log("Adding result...");

            if (up.y > 0)
            {
                if (right.y < 0.1 && right.y > -0.1)
                {
                    if (forward.y > 0)
                    {
                        PlayerDatas.addDiceResult("focus");
                    }
                    else
                    {
                        PlayerDatas.addDiceResult("miss");
                    }
                } else
                {
                    if (right.y > 0)
                    {
                        PlayerDatas.addDiceResult("crit");
                    }
                    else
                    {
                        PlayerDatas.addDiceResult("hit");
                    }
                }
            } else
            {
                if (right.y < 0.1 && right.y > -0.1)
                {
                    if (forward.y > 0)
                    {
                        PlayerDatas.addDiceResult("miss");
                    }
                    else
                    {
                        PlayerDatas.addDiceResult("focus");
                    }
                }
                else
                {
                    if (right.y > 0)
                    {
                        PlayerDatas.addDiceResult("hit");
                    }
                    else
                    {
                        PlayerDatas.addDiceResult("hit");
                    }
                }
            }
        } else
        {
            PlayerDatas.addDeltaTime(Time.deltaTime);
        }

        Debug.Log("Results: " + PlayerDatas.getDiceResults().Capacity);
        if (PlayerDatas.getDiceResults().Capacity > PlayerDatas.numberOfDice-1)
        {
            foreach (string result in PlayerDatas.getDiceResults())
            {
                Debug.Log("Result: " + result);
            }
        }
    }

}
