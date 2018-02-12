using UnityEngine;
using System.Collections;

public class DiceRollerBase : MonoBehaviour {

    public ForceMode forceMode;
    public float force = 10.0f;
    public string button = "Fire1";

	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * force, forceMode);
        }
	}
}
