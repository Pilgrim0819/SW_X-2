using System.Xml.Serialization;
using System.Collections.Generic;

namespace UpgradesXMLCSharp
{
    [XmlRoot(ElementName = "upgrade")]
    public class Upgrade
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "cost")]
        public int Cost { get; set; }
        [XmlElement(ElementName = "unique")]
        public bool Unique { get; set; }
        [XmlElement(ElementName = "size-restriction")]
        public string SizeRestriction { get; set; }
        [XmlElement(ElementName = "side-restriction")]
        public string SideRestriction { get; set; }
        [XmlElement(ElementName = "ship-restriction")]
        public string ShipRestriction { get; set; }
    }

    [XmlRoot(ElementName = "upgrades")]
    public class Upgrades
    {
        [XmlElement(ElementName = "upgrade")]
        public List<Upgrade> Upgrade { get; set; }
    }

}
