using UnityEngine;
using System.Collections;
using PilotsXMLCSharp;
using ShipsXMLCSharp;
using UpgradesXMLCSharp;
using System.Collections.Generic;

public class PlayerDatas {
    /* ---------------------------------------ONLY NEEDED FOR SQUADRON BUILDER SCENE-------------------------------------- */
    private static Ship selectedShip;
    private static Pilot selectedPilot;
    private static string chosenSide;
    private static string chosenSize = "small";
    private static bool loadingSquadrons = false;
    // Needed to know which ship's which upgrade slot to be modified
    private static LoadedShip chosenLoadedShip = null;
    private static string chosenUpgradeType = "";
    private static int chosenSlotId = 0;
    // Needed to know which ship's which upgrade slot to be modified
    /* ------------------------------------------------------------------------------------------------------------------- */

    // Persistent player datas!!
    private static int pointsToSpend = 100;
    private static List<LoadedShip> squadron = new List<LoadedShip>();
    private static int currentPilotId = 1;
    private static List<int> lastDiceResults = new List<int>();
    private static float dt = 0.0f;
    private static string playerName;
    public static int numberOfDice = 5;

    // This holds the data, handled by tha MatchHandler for the local player!!!
    private static Player player;
    // Persistent player datas!!

    public static void setPlayer(Player p)
    {
        player = p;
    }

    public static Player getPlayer()
    {
        return player;
    }

    public static void toggleLoadingSquadrons()
    {
        loadingSquadrons = !loadingSquadrons;
    }

    public static bool isLoadingSquadrons()
    {
        return loadingSquadrons;
    }

    public static void setPlayerName(string name)
    {
        playerName = name;
    }

    public static string getPlayerName()
    {
        return playerName;
    }

    public static void setChosenSide(string side)
    {
        chosenSide = side;
    }

    public static string getChosenSide()
    {
        return chosenSide;
    }

    public static void setChosenSize(string size)
    {
        chosenSize = size;
    }

    public static string getChosenSize()
    {
        return chosenSize;
    }

    public static void setChosenLoadedShip(LoadedShip ship)
    {
        chosenLoadedShip = ship;
    }

    public static LoadedShip getChosenLoadedShip()
    {
        return chosenLoadedShip;
    }

    public static void setChosenUpgraType(string type)
    {
        chosenUpgradeType = type;
    }

    public static string getChosenUpgradeType()
    {
        return chosenUpgradeType;
    }

    public static void setChosenSlotId(int id)
    {
        chosenSlotId = id;
    }

    public static int getChosenSlotId()
    {
        return chosenSlotId;
    }

    public static void setSelectedShip(Ship ship)
    {
       selectedShip = ship;
    }

    public static Ship getSelectedShip()
    {
        return selectedShip;
    }

    public static void setSelectedPilot(Pilot pilot)
    {
        selectedPilot = pilot;
    }

    public static Pilot getSelectedPilot()
    {
        return selectedPilot;
    }

    public static void addPilotToSquadron(Pilot pilot)
    {
        bool canAddPilot = true;
		bool duplicate = false;
        string errorMsg = "";

		foreach (LoadedShip ls in squadron)
		{
			if (ls.getPilot().Name.Equals(pilot.Name))
			{
				duplicate = true;
			}
		}

		if (pilot.Unique && duplicate) {
			canAddPilot = false;
            errorMsg = "The selected pilot is unique and has already been added to your squadron!";

        }

        if ((getCumulatedSquadPoints() + pilot.Cost) > pointsToSpend)
        {
            canAddPilot = false;
            errorMsg = "The selected pilot's cost is too high to fit into your current squadron!";
        }

        if (canAddPilot)
        {
            LoadedShip ls = new LoadedShip();
            ls.setShip(selectedShip);
            ls.setPilot(pilot);
			ls.setPilotId (currentPilotId);

            squadron.Add(ls);

            currentPilotId++;
        } else
        {
            throw new System.ApplicationException(errorMsg);
        }
    }

	public static void removePilotFromSquadron(Pilot pilot, int pilotId)
    {
        //TODO Test if only one ship gets deleted when pilot is not unique!!
        LoadedShip shipToRemove = new LoadedShip();

        foreach (LoadedShip ls in squadron)
        {
			if (ls.getPilot().Name.Equals(pilot.Name) && ls.getPilotId() == pilotId)
            {
                shipToRemove = ls;
                break;
            }
        }

        squadron.Remove(shipToRemove);
    }

    // To remove an upgrade, make parameter "upgrade" null
    public static void addUpgradeToShip(LoadedShip ship, Upgrade upgrade, int slotId)
    {
        foreach (UpgradeSlot slot in ship.getPilot().UpgradeSlots.UpgradeSlot)
        {
            if (slot.upgradeSlotId == slotId)
            {
                slot.upgrade = upgrade;
            }
        }
    }

    public static void setPointsToSpend(int points)
    {
        pointsToSpend = points;
    }

    public static int getPointsToSpend()
    {
        return pointsToSpend;
    }

    public static int getCumulatedSquadPoints()
    {
        int total = 0;

        if (squadron != null && squadron.Capacity > 0)
        {
            foreach (LoadedShip ls in squadron)
            {
                total += System.Convert.ToInt32(ls.getPilot().Cost);

                foreach (UpgradeSlot slot in ls.getPilot().UpgradeSlots.UpgradeSlot)
                {
                    if (slot.upgrade != null)
                    {
                        total += System.Convert.ToInt32(slot.upgrade.Cost);
                    }
                }
            }
        }

        return total;
    }

    public static void setSquadron(List<LoadedShip> ships)
    {
        squadron = ships;
    }

    public static List<LoadedShip> getSquadron()
    {
        return squadron;
    }

    public static void deleteSquadron()
    {
        squadron.Clear();
    }

    public static void addDiceResult(int result)
    {
        lastDiceResults.Add(result);
    }

    public static List<int> getDiceResults()
    {
        return lastDiceResults;
    }

    public static void deleteDiceResults()
    {
        lastDiceResults = new List<int>();
    }

    public static void addDeltaTime(float time)
    {
        dt += time;
    }

    public static float getDeltaTime()
    {
        return dt;
    }

    public static void resetDeltaTime()
    {
        dt = 0.0f;
    }
}
