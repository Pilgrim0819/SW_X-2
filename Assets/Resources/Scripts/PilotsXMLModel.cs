﻿using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace PilotsXMLCSharp
{
    [XmlRoot(ElementName = "slots")]
    public class Slots
    {
        [XmlElement(ElementName = "slot")]
        public List<string> Slot { get; set; }
    }

    [XmlRoot(ElementName = "ability")]
    public class Ability
    {
        [XmlElement(ElementName = "phase")]
        public string Phase { get; set; }
        [XmlElement(ElementName = "target")]
        public string Target { get; set; }
        [XmlElement(ElementName = "targetScope")]
        public string TargetScope { get; set; }
        [XmlElement(ElementName = "targetRangeMin")]
        public string TargetRangeMin { get; set; }
        [XmlElement(ElementName = "targetRangeMax")]
        public string TargetRangeMax { get; set; }
        [XmlElement(ElementName = "valueToModify")]
        public string ValueToModify { get; set; }
        [XmlElement(ElementName = "modifier")]
        public string Modifier { get; set; }
        [XmlElement(ElementName = "bonusAction")]
        public string BonusAction { get; set; }
        [XmlElement(ElementName = "valueToCheck")]
        public string ValueToCheck { get; set; }
        [XmlElement(ElementName = "conditionValue")]
        public string ConditionValue { get; set; }
        [XmlElement(ElementName = "conditionWay")]
        public string ConditionWay { get; set; }
        [XmlElement(ElementName = "conditionCheckPhase")]
        public string ConditionCheckPhase { get; set; }
    }

    [XmlRoot(ElementName = "pilot")]
    public class Pilot
    {
        [XmlElement(ElementName = "shipId")]
        public string ShipId { get; set; }
        [XmlElement(ElementName = "unique")]
        public bool Unique { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "cost")]
        public int Cost { get; set; }
        [XmlElement(ElementName = "level")]
        public int Level { get; set; }
        [XmlElement(ElementName = "text")]
        public string Text { get; set; }
        [XmlElement(ElementName = "slots")]
        public Slots Slots { get; set; }
        [XmlElement(ElementName = "ability")]
        public Ability Ability { get; set; }
    }

    [XmlRoot(ElementName = "pilots")]
    public class Pilots
    {
        [XmlElement(ElementName = "pilot")]
        public List<Pilot> Pilot { get; set; }
    }

}