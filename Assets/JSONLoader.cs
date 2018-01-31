using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class JSONLoader {

	public Ship[] getShips(string jsonToLoad)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonToLoad);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);

            Ship[] ships = JSONHelper.FromJson<Ship>(dataAsJson);

            return ships;
        } else
        {
            return null;
        }
    }

}
