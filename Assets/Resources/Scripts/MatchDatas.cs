using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchDatas {

    private enum phases {
        INITIATIVE_ROLL,
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
    };

    private static int currentPhase;
    private static int activePlayerIndex;
    private static List<Player> players;

    public static void setCurrentPhase(int phase)
    {
        currentPhase = phase;
    }

    public static int getCurrentPhase()
    {
        return currentPhase;
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

}
