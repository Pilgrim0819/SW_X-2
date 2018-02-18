using UnityEngine;
using System.Collections;

public class DiceStateChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(CheckObjectsHaveStopped());
    }
	
	// Update is called once per frame
	void Update () {
	
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
    }
}
