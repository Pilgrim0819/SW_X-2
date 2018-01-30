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
    public Actions actions { get; set; }
    public Maneuvers maneuvers { get; set; }

}
