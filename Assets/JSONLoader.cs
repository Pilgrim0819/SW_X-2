using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class JSONLoader {

	public Ships getShips(string jsonToLoad)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonToLoad);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            Ships ships = JsonUtility.FromJson<Ships>(dataAsJson);

            return ships;
        } else
        {
            return null;
        }
    }

}
