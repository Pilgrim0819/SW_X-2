using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace ShipsXMLCSharp
{
    [XmlRoot(ElementName = "actions")]
    public class Actions
    {
        [XmlElement(ElementName = "action")]
        public List<string> Action { get; set; }
    }

    [XmlRoot(ElementName = "maneuver")]
    public class Maneuver
    {
        [XmlElement(ElementName = "speed")]
        public string Speed { get; set; }
        [XmlElement(ElementName = "bearing")]
        public string Bearing { get; set; }
        [XmlElement(ElementName = "difficulty")]
        public string Difficulty { get; set; }
    }

    [XmlRoot(ElementName = "maneuvers")]
    public class Maneuvers
    {
        [XmlElement(ElementName = "maneuver")]
        public List<Maneuver> Maneuver { get; set; }
    }

    [XmlRoot(ElementName = "ship")]
    public class Ship
    {
        [XmlElement(ElementName = "shipId")]
        public string ShipId { get; set; }
        [XmlElement(ElementName = "shipName")]
        public string ShipName { get; set; }
        [XmlElement(ElementName = "weapon")]
        public int Weapon { get; set; }
        [XmlElement(ElementName = "agility")]
        public int Agility { get; set; }
        [XmlElement(ElementName = "hull")]
        public int Hull { get; set; }
        [XmlElement(ElementName = "shield")]
        public int Shield { get; set; }
        [XmlElement(ElementName = "size")]
        public string Size { get; set; }
        [XmlElement(ElementName = "actions")]
        public Actions Actions { get; set; }
        [XmlElement(ElementName = "maneuvers")]
        public Maneuvers Maneuvers { get; set; }
    }

    [XmlRoot(ElementName = "ships")]
    public class Ships
    {
        [XmlElement(ElementName = "ship")]
        public List<Ship> Ship { get; set; }
    }

}