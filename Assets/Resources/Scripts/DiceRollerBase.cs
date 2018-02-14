using UnityEngine;
using System.Collections;

public class DiceRollerBase : MonoBehaviour {

    public ForceMode forceMode;
    public float force = 10.0f;
    public string button = "Fire1";

    private void Start()
    {
        this.transform.rotation = Random.rotation;
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            PlayerDatas.resetDeltaTime();
            GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * force, forceMode);

            if (PlayerDatas.getDiceResults().Capacity > 0)
            {
                PlayerDatas.deleteDiceResults();
            }
        }

        //TODO Is there a better way of finding out if dice stopped after moving it...
        if (GetComponent<Rigidbody>().velocity.magnitude == 0.0 && PlayerDatas.getDeltaTime() > 0.3)
        {
            //Debug.Log("upv: " + this.transform.up + "; rightv: " + this.transform.right + "; forwardv: " + this.transform.forward);
            Vector3 up = this.transform.up;
            Vector3 right = this.transform.right;
            Vector3 forward = this.transform.forward;
            //Debug.Log("up: " + transform.eulerAngles.y + "; right: " + transform.eulerAngles.x + "; forward: " + transform.eulerAngles.z);

            if (up.y > 0)
            {
                if (right.y < 0.1 && right.y > -0.1)
                {
                    if (forward.y > 0)
                    {
                        Debug.Log("focus");
                    }
                    else
                    {
                        Debug.Log("miss");
                    }
                } else
                {
                    if (right.y > 0)
                    {
                        Debug.Log("crit");
                    }
                    else
                    {
                        Debug.Log("hit");
                    }
                }
            } else
            {
                if (right.y < 0.1 && right.y > -0.1)
                {
                    if (forward.y > 0)
                    {
                        Debug.Log("miss");
                    }
                    else
                    {
                        Debug.Log("focus");
                    }
                }
                else
                {
                    if (right.y > 0)
                    {
                        Debug.Log("hit");
                    }
                    else
                    {
                        Debug.Log("hit");
                    }
                }
            }
        } else
        {
            PlayerDatas.addDeltaTime(Time.deltaTime);
        }
	}
}
