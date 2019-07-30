using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchHandlerUtil {

    private static GameObject initiativePanel;

    private static string[] faces = { "FrontFace", "BackFace", "LeftFace", "RightFace" };

    public static void setInitiativePanel(GameObject panel)
    {
        initiativePanel = panel;
    }

    public static GameObject getInitiativePanel()
    {
        return initiativePanel;
    }

    public static Vector3 getShipCollectionHolderPosition(int playerID)
    {
        return GameObject.Find("ShipCollection" + playerID).transform.position;
    }

    public static void determineInitiative()
    {
        if (squadScoresAreEqual())
        {
            PointerClickCallback callback = GameObject.Find("ScriptHolder").GetComponent<CoroutineHandler>().rollAttackDiceCallback;
            SystemMessageService.showErrorMsg(MatchHandlerConstants.EQUAL_SQUAD_SCORE_TEXT + " " + MatchDatas.getPlayers()[0].getPlayerName() + MatchHandlerConstants.EQUAL_SQUAD_SCORE_TEXT_ENDING, GameObject.Find("SystemMessagePanel"), 1, callback);
        }
        else
        {
            PointerClickCallback callback = displayInitiativeChooserCallback;
            SystemMessageService.showErrorMsg(getPlayerWithLowestSquadScore().getPlayerName() + MatchHandlerConstants.NOT_EQUAL_SQUAD_SCORE_TEXT, GameObject.Find("SystemMessagePanel"), 1, callback);
        }
    }

    public static void displayInitiativeChooserCallback()
    {
        displayInitiativeChoser(getPlayerWithLowestSquadScore());
    }

    public static bool squadScoresAreEqual()
    {
        if (MatchDatas.getPlayers().Count == 2)
        {
            if (MatchDatas.getPlayers()[0].getCumulatedSquadPoints() == MatchDatas.getPlayers()[1].getCumulatedSquadPoints())
            {
                return true;
            }
        }
        else if (MatchDatas.getPlayers().Count == 3)
        {
            if (MatchDatas.getPlayers()[0].getCumulatedSquadPoints() == MatchDatas.getPlayers()[1].getCumulatedSquadPoints() && MatchDatas.getPlayers()[0].getCumulatedSquadPoints() == MatchDatas.getPlayers()[2].getCumulatedSquadPoints())
            {
                return true;
            }
        }

        return false;
    }

    public static Player getPlayerWithLowestSquadScore()
    {
        Player result = null;

        foreach (Player player in MatchDatas.getPlayers())
        {
            if (result == null)
            {
                result = player;
            }
            else
            {
                result = result.getCumulatedSquadPoints() > player.getCumulatedSquadPoints() ? player : result;
            }
        }

        return result;
    }

    public static void displayInitiativeChoser(Player player)
    {
        int playerIndex = 0;

        if (LocalDataWrapper.getPlayer().getPlayerName().Equals(player.getPlayerName()))
        {
            foreach (Player currentPlayer in MatchDatas.getPlayers())
            {
                Transform InitiativeButtonPrefab = Resources.Load<Transform>(MatchHandlerConstants.PREFABS_FOLDER + MatchHandlerConstants.INITIATIVE_BUTTON_PREFAB_NAME);
                Transform InitiativeButton = (Transform)GameObject.Instantiate(
                    InitiativeButtonPrefab,
                    new Vector3(InitiativeButtonPrefab.position.x, InitiativeButtonPrefab.position.y - playerIndex * 85, InitiativeButtonPrefab.position.z),
                    Quaternion.identity
                );

                InitiativeButton.GetComponentInChildren<Text>().text = currentPlayer.getPlayerName();
                InitiativeButton.transform.SetParent(initiativePanel.transform, false);

                playerIndex++;
            }

            initiativePanel.SetActive(true);
        }
    }

    public static void setShipHighlighters(GameObject target, bool active)
    {
        for (int i = 0; i < faces.Length; i++)
        {
            target.transform.Find(faces[i]).gameObject.SetActive(active);
        }
    }

    public static void applyManeuverBonus(GameObject ship, string difficulty)
    {
        if (difficulty.Equals("2"))
        {
            ship.transform.GetComponent<ShipProperties>().getLoadedShip().addToken(new StressToken());

            // TODO handle stress indicator elsewhere...
            ship.transform.Find("StressIndicator").gameObject.SetActive(true);
        }
        else if (difficulty.Equals("0"))
        {
            ship.transform.GetComponent<ShipProperties>().getLoadedShip().removeTokenById(
                typeof(StressToken),
                ship.transform.GetComponent<ShipProperties>().getLoadedShip().getTokenIdByType(typeof(StressToken))
            );
        }
    }

    // This one hides ALL ship highlighters!!!
    public static void hideActiveShipHighlighters()
    {
        for (int i = 0; i < faces.Length; i++)
        {
            foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>())
            {
                if (go.name.Equals(faces[i]))
                {
                    go.SetActive(false);
                }
            }
        }
    }

    public static void hideFiringArcs()
    {
        GameObject[] shipHolders = GameObject.FindGameObjectsWithTag("SmallShipContainer");

        foreach (GameObject shipHolder in shipHolders)
        {
            shipHolder.transform.Find("Canvas").gameObject.SetActive(false);
            shipHolder.transform.Find("Canvas/FiringArc").gameObject.SetActive(false);
        }
    }

    // DUPLICATED FRAGMENT!!!
    public static bool isPlayersOwnShip()
    {
        foreach (LoadedShip ship in LocalDataWrapper.getPlayer().getSquadron())
        {
            if (ship.getPilotId() == MatchDatas.getActiveShip().GetComponent<ShipProperties>().getLoadedShip().getPilotId())
            {
                return true;
            }
        }

        return false;
    }

    public static bool maneuversPlanned()
    {
        bool result = true;

        foreach (Player player in MatchDatas.getPlayers())
        {
            foreach (LoadedShip ship in player.getSquadron())
            {
                if (ship.getPlannedManeuver() == null)
                {
                    result = false;
                    break;
                }
            }
        }

        return result;
    }

    public static void setAIManeuvers()
    {
        foreach (Player player in MatchDatas.getPlayers())
        {
            if (player.isAI())
            {
                foreach (LoadedShip ship in player.getSquadron())
                {
                    ship.setPlannedManeuver(ship.getShip().Maneuvers.Maneuver[0]);
                }
            }
        }
    }

    public static void deleteSelectedManeuvers()
    {
        foreach (Player player in MatchDatas.getPlayers())
        {
            foreach (LoadedShip ship in player.getSquadron())
            {
                ship.setPlannedManeuver(null);
            }
        }
    }

    public static void hideForceFields()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("ForceField"))
        {
            o.SetActive(false);
        }
    }
}
