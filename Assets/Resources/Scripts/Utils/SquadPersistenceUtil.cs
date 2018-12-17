using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SquadPersistenceUtil {

    private const string SAVED_DATA_FOLDER = "Saves/";
    private const string SAVED_DATA_EXTENSION = ".dat";
    private const string DEFAULT_SQUADRON_NAME = "default";

    public static void saveSquadron(string squadronName)
    {
        if (squadronName == null || squadronName.Equals(""))
        {
            squadronName = DEFAULT_SQUADRON_NAME;
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(Path.Combine(Application.streamingAssetsPath, SAVED_DATA_FOLDER + squadronName + SAVED_DATA_EXTENSION), FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize(fileStream, PlayerDatas.getSquadron());
        }

        Debug.Log("Ending save....");
    }

    public static void loadSquadron(string squadronName) {
        if (squadronName == null || squadronName.Equals(""))
        {
            throw new System.ApplicationException("Cannot load squadron without a valid name! Parameter was null or empty string!!!");
        }

        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open(Path.Combine(Application.streamingAssetsPath, SAVED_DATA_FOLDER + squadronName + SAVED_DATA_EXTENSION), FileMode.Open))
        {
            List<LoadedShip> squadron = (List<LoadedShip>)binaryFormatter.Deserialize(fileStream);

            PlayerDatas.setSquadron(squadron);
        }
    }

    public static List<string> getSquadronNames()
    {
        List<string> names = new List<string>();
        DirectoryInfo dir = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, "Saves/"));
        FileInfo[] info = dir.GetFiles("*.dat");

        foreach (FileInfo f in info)
        {
            names.Add(f.Name.Split('.')[0]);
        }

        return names;
    }
}
