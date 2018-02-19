using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SideSelectionStartup : MonoBehaviour {

    public InputField inputField;

    // Use this for initialization
    void Start()
    {
        if (PlayerDatas.getPointsToSpend() != null && PlayerDatas.getPointsToSpend() > 0)
        {
            inputField.text = PlayerDatas.getPointsToSpend().ToString();
        } else
        {
            inputField.text = "100";
        }

    }

    // Update is called once per frame
    void Update () {
	
	}
}
