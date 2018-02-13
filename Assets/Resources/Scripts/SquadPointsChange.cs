using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SquadPointsChange : MonoBehaviour {

    public void changePlayerPointsToSpend(InputField gameObj)
    {
        string squadPoints = gameObj.text;
        int points = System.Convert.ToInt32(squadPoints);
        PlayerDatas.setPointsToSpend(points);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
