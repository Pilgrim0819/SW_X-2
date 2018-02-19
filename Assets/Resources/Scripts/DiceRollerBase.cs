using UnityEngine;
using System.Collections;

public class DiceRollerBase : MonoBehaviour {

    public ForceMode forceMode;
    public float force = 10.0f;
    public string button = "Fire1";
    private bool resultChecked = false;

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

}
