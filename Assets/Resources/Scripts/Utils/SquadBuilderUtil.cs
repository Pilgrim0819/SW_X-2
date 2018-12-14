using UnityEngine;
using UnityEngine.UI;
using ShipsXMLCSharp;
using PilotsXMLCSharp;
using UpgradesXMLCSharp;

public class SquadBuilderUtil {

    public static Ships loadShipsForChosenSide()
    {
        string chosenSide = PlayerDatas.getChosenSide();

        /*********************************TODO remove when testing is done!!*/
        if (chosenSide == null || chosenSide.Equals(""))
        {
            chosenSide = "Rebels";
        }
        /*********************************TODO remove when testing is done!!*/

        Ships ships = new Ships();

        switch (chosenSide)
        {
            case "Rebels":
                ships = XMLLoader.getShips("rebel_ships.xml");
                break;
            case "Empire":
                ships = XMLLoader.getShips("imperial_ships.xml");
                break;
        }

        return ships;
    }

    public static Pilots loadPilotsForEachShip(Ships ships)
    {
        Pilots pilots = new Pilots();

        foreach (Ship ship in ships.Ship)
        {
            if (pilots == null || pilots.Pilot == null || pilots.Pilot.Capacity == 0)
            {
                pilots = XMLLoader.getPilots(ship.ShipId.ToString() + "_pilots.xml");
            }
            else
            {
                pilots.Pilot.AddRange(XMLLoader.getPilots(ship.ShipId.ToString() + "_pilots.xml").Pilot);
            }
        }

        return pilots;
    }

    public static Upgrades loadUpgrades(string side, string size)
    {
        Upgrades tempUpgrades = XMLLoader.getUpgrades();
        Upgrades upgrades = new Upgrades();
        upgrades.Upgrade = new System.Collections.Generic.List<UpgradesXMLCSharp.Upgrade>();

        foreach (UpgradesXMLCSharp.Upgrade upgrade in tempUpgrades.Upgrade)
        {
            bool available = true;

            if (upgrade.SideRestriction != null && !upgrade.SideRestriction.Equals(""))
            {
                if (!upgrade.SideRestriction.Equals(PlayerDatas.getChosenSide()))
                {
                    available = false;
                }
            }

            if (available)
            {
                if (upgrade.SizeRestriction != null && !upgrade.SizeRestriction.Equals(""))
                {
                    if (!upgrade.SizeRestriction.Equals(PlayerDatas.getChosenSize()))
                    {
                        available = false;
                    }
                }
            }

            if (available)
            {
                upgrades.Upgrade.Add(upgrade);
            }
        }

        return upgrades;
    }

    public static void resetImagesInGameObject(GameObject target, string path)
    {
        foreach (Transform child in target.transform.Find(path).GetComponentsInChildren<Transform>())
        {
            if (child.GetComponent<Image>() != null)
            {
                Image image = child.GetComponent<Image>();

                if (image != null)
                {
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0.0f);
                }
            }
        }
    }

    public static void resetScrollView(GameObject target)
    {
        foreach (Transform child in target.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
