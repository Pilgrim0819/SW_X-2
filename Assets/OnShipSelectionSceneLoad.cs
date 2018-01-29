using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnShipSelectionSceneLoad : MonoBehaviour {

    public Text testObj;

	// Use this for initialization
	void Start () {
        testObj.text = PlayerDatas.getChosenSide();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
