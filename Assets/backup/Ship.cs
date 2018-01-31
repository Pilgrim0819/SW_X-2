using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship {

    public string shipName { get; set; }
    public int weapon { get; set; }
    public int agility { get; set; }
    public int hull { get; set; }
    public int shield { get; set; }
    public string size { get; set; }
    public List<string> actions { get; set; }
    public List<Maneuver> maneuvers { get; set; }

}
