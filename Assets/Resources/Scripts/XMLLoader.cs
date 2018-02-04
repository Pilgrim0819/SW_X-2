using UnityEngine;
using System.Collections;
using ShipsXMLCSharp;
using PilotsXMLCSharp;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

public class XMLLoader {

	public static Ships getShips(string xmlToLoad)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, xmlToLoad);
        XmlSerializer serializer = new XmlSerializer(typeof(Ships));
        Ships result = (Ships)serializer.Deserialize(new XmlTextReader(filePath));

        return result;
    }

    public static Pilots getPilots(string xmlToLoad)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, xmlToLoad);
        XmlSerializer serializer = new XmlSerializer(typeof(Pilots));
        Pilots result = (Pilots)serializer.Deserialize(new XmlTextReader(filePath));

        return result;
    }

}
