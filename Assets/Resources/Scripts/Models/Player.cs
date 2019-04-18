using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PilotsXMLCSharp;
using ShipsXMLCSharp;
using UpgradesXMLCSharp;

public class Player {

    /* ---------------------------------------ONLY NEEDED FOR SQUADRON BUILDER SCENE-------------------------------------- */
    private Ship selectedEmptyShip;
    private Pilot selectedPilot;
    private string chosenSide;
    private string chosenSize = "small";
    private bool loadingSquadrons = false;
    // Needed to know which ship's which upgrade slot to be modified
    private LoadedShip chosenLoadedShip = null;
    private string chosenUpgradeType = "";
    private int chosenSlotId = 0;
    // Needed to know which ship's which upgrade slot to be modified
    /* ------------------------------------------------------------------------------------------------------------------- */

    private string playerName;
    private List<LoadedShip> squadron = new List<LoadedShip>();
    private List<int> lastDiceResults = new List<int>();
    private LoadedShip activeShip;
    private LoadedShip selectedShip;
    private bool hasInitiative = false;
    private int playerID;
    private bool AI;
    private int pointsToSpend = 100;
    private int currentPilotId = 1;
    private int numberOfDice = 5;

    //What was this used for?? Is it needed????
    private float dt = 0.0f;

    public void setPLayerID(int id)
    {
        this.playerID = id;
    }

    public int getPlayerID()
    {
        return this.playerID;
    }

    public void setInitiative()
    {
        this.hasInitiative = true;
    }

    public bool getHasInitiative()
    {
        return this.hasInitiative;
    }

    public void setPlayerName(string name)
    {
        this.playerName = name;
    }

    public string getPlayerName()
    {
        return this.playerName;
    }

    public void setChosenSide(string side)
    {
        this.chosenSide = side;
    }

    public string getChosenSide()
    {
        return this.chosenSide;
    }

    public void setSquadron(List<LoadedShip> ships)
    {
        this.squadron = ships;
    }

    public List<LoadedShip> getSquadron()
    {
        return this.squadron;
    }

    public void deleteSquadron()
    {
        this.squadron.Clear();
    }

    public void addDiceResult(int result)
    {
        this.lastDiceResults.Add(result);
    }

    public void setLastDiceResults(List<int> results)
    {
        this.lastDiceResults = results;
    }

    public List<int> getLastDiceResults()
    {
        return this.lastDiceResults;
    }

    public void setActiveShip(LoadedShip ship)
    {
        this.activeShip = ship;
    }

    public LoadedShip getActiveShip()
    {
        return this.activeShip;
    }

    public void setSelectedShip(LoadedShip ship)
    {
        this.selectedShip = ship;
    }

    public LoadedShip getSelectedhip()
    {
        return this.selectedShip;
    }

    public void setAI(bool isAI)
    {
        this.AI = isAI;
    }

    public bool isAI()
    {
        return this.AI;
    }

    public void setChosenLoadedShip(LoadedShip ship)
    {
        this.chosenLoadedShip = ship;
    }

    public LoadedShip getChosenLoadedShip()
    {
        return this.chosenLoadedShip;
    }

    public void setChosenUpgraType(string type)
    {
        this.chosenUpgradeType = type;
    }

    public string getChosenUpgradeType()
    {
        return this.chosenUpgradeType;
    }

    public void setChosenSlotId(int id)
    {
        this.chosenSlotId = id;
    }

    public int getChosenSlotId()
    {
        return this.chosenSlotId;
    }

    public void toggleLoadingSquadrons()
    {
        this.loadingSquadrons = !this.loadingSquadrons;
    }

    public bool isLoadingSquadrons()
    {
        return this.loadingSquadrons;
    }

    public void setChosenSize(string size)
    {
        this.chosenSize = size;
    }

    public string getChosenSize()
    {
        return this.chosenSize;
    }

    public void setSelectedEmptyShip(Ship ship)
    {
        this.selectedEmptyShip = ship;
    }

    public Ship getSelectedEmptyShip()
    {
        return this.selectedEmptyShip;
    }

    public void setSelectedPilot(Pilot pilot)
    {
        this.selectedPilot = pilot;
    }

    public Pilot getSelectedPilot()
    {
        return this.selectedPilot;
    }

    public void setPointsToSpend(int points)
    {
        this.pointsToSpend = points;
    }

    public int getPointsToSpend()
    {
        return this.pointsToSpend;
    }

    /***********************************************************/
    public void addDeltaTime(float time)
    {
        this.dt += time;
    }

    public float getDeltaTime()
    {
        return this.dt;
    }

    public void resetDeltaTime()
    {
        this.dt = 0.0f;
    }
    /***********************************************************/

    public void setNumberOfDice(int num)
    {
        this.numberOfDice = num;
    }

    public int getNumberOfDice()
    {
        return this.numberOfDice;
    }

    public int getCumulatedSquadPoints()
    {
        int total = 0;

        if (this.squadron != null && this.squadron.Capacity > 0)
        {
            foreach (LoadedShip ls in this.squadron)
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

    public bool isDefeated()
    {
        bool result = true;

        foreach (LoadedShip ship in this.squadron)
        {
            // TODO DO NOT USE the hull value!!! Make another variable for ships which holds the CURRENT hull value!!
            if (ship.getShip().Hull != 0)
            {
                result = false;
                break;
            }
        }

        return result;
    }

    //TODO make it a List, so every ship with the same level (if not yet used!) will be returned
    public List<LoadedShip> getNextShips(int currentLevel, bool ascending)
    {
        int nextLevel = getNextPilotLevel(currentLevel, ascending);
        List<LoadedShip> result = new List<LoadedShip>();

        foreach (LoadedShip ship in this.squadron)
        {
            if (ascending)
            {
                if (ship.getPilot().Level == nextLevel && !ship.isHasBeenActivatedThisRound())
                {
                    result.Add(ship);
                }
            } else
            {
                if (ship.getPilot().Level == nextLevel && !ship.isHasBeenActivatedThisRound())
                {
                    result.Add(ship);
                }
            }
        }

        return result;
    }

    private int getNextPilotLevel(int currentLevel, bool ascending)
    {
        int result = ascending ? 12 : 0;

        foreach (LoadedShip ship in this.squadron)
        {
            if (ascending)
            {
                if (ship.getPilot().Level >= currentLevel && ship.getPilot().Level <= result && !ship.isHasBeenActivatedThisRound())
                {
                    result = ship.getPilot().Level;
                }
            } else
            {
                if (ship.getPilot().Level <= currentLevel && ship.getPilot().Level >= result && !ship.isHasBeenActivatedThisRound())
                {
                    result = ship.getPilot().Level;
                }
            }
        }

            return result;
    }

    public void addPilotToSquadron(Pilot pilot)
    {
        bool canAddPilot = true;
        bool duplicate = false;
        string errorMsg = "";

        foreach (LoadedShip ls in this.squadron)
        {
            if (ls.getPilot().Name.Equals(pilot.Name))
            {
                duplicate = true;
            }
        }

        if (pilot.Unique && duplicate)
        {
            canAddPilot = false;
            errorMsg = "The selected pilot is unique and has already been added to your squadron!";

        }

        if ((getCumulatedSquadPoints() + pilot.Cost) > this.pointsToSpend)
        {
            canAddPilot = false;
            errorMsg = "The selected pilot's cost is too high to fit into your current squadron!";
        }

        if (canAddPilot)
        {
            LoadedShip ls = new LoadedShip();
            ls.setShip(this.selectedEmptyShip);
            ls.setPilot(pilot);
            ls.setPilotId(this.currentPilotId);

            squadron.Add(ls);

            currentPilotId++;
        }
        else
        {
            throw new System.ApplicationException(errorMsg);
        }
    }

    public void removePilotFromSquadron(Pilot pilot, int pilotId)
    {
        //TODO Test if only one ship gets deleted when pilot is not unique!!
        LoadedShip shipToRemove = new LoadedShip();

        foreach (LoadedShip ls in this.squadron)
        {
            if (ls.getPilot().Name.Equals(pilot.Name) && ls.getPilotId() == pilotId)
            {
                shipToRemove = ls;
                break;
            }
        }

        this.squadron.Remove(shipToRemove);
    }

    // To remove an upgrade, make parameter "upgrade" null
    public void addUpgradeToShip(LoadedShip ship, Upgrade upgrade, int slotId)
    {
        foreach (UpgradeSlot slot in ship.getPilot().UpgradeSlots.UpgradeSlot)
        {
            if (slot.upgradeSlotId == slotId)
            {
                slot.upgrade = upgrade;
            }
        }
    }
}
