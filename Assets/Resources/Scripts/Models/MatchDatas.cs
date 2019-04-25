using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchDatas {

    // These are not phases, but custom event indicators
    /*public enum phases {
        INITIATIVE_ROLL,
        ASTEROIDS_PLACEMENT,
        SQUADRON_PLACEMENT,
        PRE_PLANNING,
        POST_PLANNING,
        PRE_ACTIVATION,
        POST_ACTIVATION,
        PRE_ACTION,
        POST_ACTION,
        PRE_ATTACK,
        POST_ATTACK,
        PRE_DEFENSE,
        POST_DEFENSE,
        PRE_END,
        POST_END
    };*/

    public enum phases
    {
        INITIATIVE_ROLL,
        ASTEROIDS_PLACEMENT,
        SQUADRON_PLACEMENT,
        PLANNING,
        ACTIVATION,
        ATTACK,
        END
    };
    
    private static int activePlayerIndex = 0;
    private static int initiativeChoserPlayerIndex = 0;
    private static List<Player> players = new List<Player>();
    private static int round = 0;
    private static int currentLevel = 0;
    private static phases phase = phases.INITIATIVE_ROLL;
    // TODO Add missions later on! (This might change placement/rules/squadpoints/etc...)
    private static string mission = "";
    private static int totalSquadPoints = 100;
    private static GameObject activeShip = null;

    public static void setInitiativeChoserPlayerIndex(int index)
    {
        initiativeChoserPlayerIndex = index;
    }

    public static int getInitiativeChoserPlayerIndex()
    {
        return initiativeChoserPlayerIndex;
    }

    public static void setActiveShip(GameObject ship)
    {
        activeShip = ship;
    }

    public static GameObject getActiveShip()
    {
        return activeShip;
    }

    public static int getTotalSquadPoints()
    {
        return totalSquadPoints;
    }

    public static void setTotalSquadPoints(int total)
    {
        totalSquadPoints = total;
    }

    public static void setMission(string m)
    {
        mission = m;
    }

    public static string getMission()
    {
        return mission;
    }

    public static void setActivePlayerIndex(int playerIndex)
    {
        activePlayerIndex = playerIndex;
    }

    public static int getActivePlayerIndex()
    {
        return activePlayerIndex;
    }

    public static void changeActivePlayerIndex()
    {
        if (activePlayerIndex == (players.Capacity - 1))
        {
            activePlayerIndex = 0;
        } else
        {
            activePlayerIndex++;
        }
    }

    public static void addPlayer(Player player)
    {
        players.Add(player);
    }

    public static void removePlayer(Player player)
    {
        players.Remove(player);
    }

    public static List<Player> getPlayers()
    {
        return players;
    }

    public static void nextRound()
    {
        round++;
    }

    public static int getRound()
    {
        return round;
    }

    public static phases getCurrentPhase()
    {
        return phase;
    }

    public static void setCurrentPhase(phases p)
    {
        phase = p;
    }

    private static bool isMatchOver()
    {
        foreach (Player player in players)
        {
            if (player.isDefeated())
            {
                return true;
            }
        }

        return false;
    }

    public static int getCurrentLevel()
    {
        return currentLevel;
    }

    public static void setCurrentLevel(int level)
    {
        currentLevel = level;
    }
}
