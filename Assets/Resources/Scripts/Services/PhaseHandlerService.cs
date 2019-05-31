using UnityEngine;

public class PhaseHandlerService {
    
    // TODO Make this class handle all phase changes and the required checks, cleanups, inits, etc.....

    public static void nextPhase()
    {
        Debug.Log("Initiating next phase from " + MatchDatas.getCurrentPhase());
        switch (MatchDatas.getCurrentPhase())
        {
            case MatchDatas.phases.INITIATIVE_ROLL:
                startAsteroidPlacementPhase();
                break;
            case MatchDatas.phases.ASTEROIDS_PLACEMENT:
                startSquadronPlacementPhase();
                break;
            case MatchDatas.phases.SQUADRON_PLACEMENT:
                startPlanningPhase();
                break;
            case MatchDatas.phases.PLANNING:
                startActivationPhase();
                break;
            case MatchDatas.phases.ACTIVATION:
                startAttackPhase();
                break;
            case MatchDatas.phases.ATTACK:
                startEndPhase();
                break;
            case MatchDatas.phases.END:
                if (!isMatchOver())
                {
                    startPlanningPhase();
                } else
                {
                    // TODO Finish game!
                }
                break;
        }
    }

    private static bool isMatchOver()
    {
        foreach (Player player in MatchDatas.getPlayers())
        {
            if (player.isDefeated())
            {
                return true;
            }
        }

        return false;
    }

    private static void startAsteroidPlacementPhase()
    {
        MatchDatas.setCurrentPhase(MatchDatas.phases.ASTEROIDS_PLACEMENT);
    }

    private static void startSquadronPlacementPhase()
    {
        MatchDatas.setCurrentPhase(MatchDatas.phases.SQUADRON_PLACEMENT);
        MatchHandler.collectUpcomingAvailableShips(true);
    }

    private static void startPlanningPhase()
    {
        MatchDatas.setCurrentPhase(MatchDatas.phases.PLANNING);

        MatchHandlerUtil.hideForceFields();
        MatchHandlerUtil.deleteSelectedManeuvers();
        MatchHandlerUtil.setAIManeuvers();
    }

    private static void startActivationPhase()
    {
        MatchHandlerUtil.hideActiveShipHighlighters();
        MatchDatas.setCurrentPhase(MatchDatas.phases.ACTIVATION);
        MatchDatas.setCurrentLevel(0);

        foreach(Player player in MatchDatas.getPlayers())
        {
            foreach(LoadedShip ship in player.getSquadron())
            {
                ship.setHasBeenActivatedThisRound(false);
            }
        }

        MatchHandler.collectUpcomingAvailableShips(true);

        // TODO Active player index is not always right/sufficient!!
        MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].setActiveShip(null);
        MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].setSelectedShip(null);
    }

    private static void startAttackPhase()
    {
        MatchDatas.setCurrentPhase(MatchDatas.phases.ATTACK);
    }

    private static void startEndPhase()
    {
        MatchDatas.setCurrentPhase(MatchDatas.phases.END);
    }
}
