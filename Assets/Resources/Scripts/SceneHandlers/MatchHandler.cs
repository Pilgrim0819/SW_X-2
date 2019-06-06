using UnityEngine;
using System.Collections;
using ShipsXMLCSharp;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/*Controls the whole match. (The server, basically...)*/
public class MatchHandler : MonoBehaviour {

    public GameObject initiativePanel;
    public GameObject PilotCardPanel;
    public GameObject GameInfoPanel;
    public GameObject[] asteroids = new GameObject[6];

    private static Mocker mocker = new Mocker();
    private static MatchHandlerService matchHandlerService = new MatchHandlerService();
    private static GUIHandler guiHandler = new GUIHandler();

    private static List<LoadedShip> availableShips = new List<LoadedShip>();

    private const string MULTIPLE_AVAILABLE_SHIPS_INFO = "Chose which of the available ships should execute its planned maneuver!";
    private const string PLANNING_INFO = "Please choose a valid maneuver for each of your ships.";
    private const string SQUADRON_PLACEMENT_INFO = "Please put an available ship into the highlighted setup area.";
    private const string EMPTY_TEXT = "";

    //FOR TESTING ONLY!!!
    private string[] keyCodes = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "v", "b", "n", "m", "g", "h", "j", "k", "t", "z", "u", "i"};
    //FOR TESTING ONLY!!!

    void Start() {
        DiceRollerBase.setUpDiceRollerBase(ForceMode.VelocityChange, 10.0f);
        MatchHandlerUtil.setInitiativePanel(initiativePanel);

        mocker.mockLocalPlayer();
        mocker.mockPlayerSquadrons();

        matchHandlerService.instantiateShips();

        MatchHandlerUtil.determineInitiative();
    }

    // TODO Can we make the method calls inside to run only ONCE(!), when necessary?? Custom eventhandling maybe????
	void Update() {
        matchHandlerService.levitateShips();

        if (Input.GetKey("escape"))
        {
            // TODO Not active player, LOCAL PLAYER!!! (Maybe simply hiding the panel on the client side is enough....)
            MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].setSelectedShip(null);
            //guiHandler.hideGameObject(PilotCardPanel);
        }

        //THIS IS ONLY FOR TESTING MOVEMENTS!!!!!! DELETE LATER ON!!!! (Also, ADD UNIQUE IDs during squad building to ships and pilots!!!! [use something like: playerIndex_shipIndex])
        Maneuver man = new Maneuver();
        man.Difficulty = "1";

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                int n;
                bool isNumeric = int.TryParse(keyCodes[i], out n);

                if (isNumeric)
                {
                    int s = Int32.Parse(keyCodes[i]);
                    if (s < 6) {
                        man.Bearing = "straight";
                        man.Speed = keyCodes[i];
                    } else
                    {
                        man.Bearing = "koiogran";
                        man.Speed = (s - 5).ToString();
                    }
                } else
                {
                    switch (keyCodes[i])
                    {
                        case "v":
                        case "g":
                        case "t":
                        case "i":
                        case "k":
                        case "m":
                            man.Bearing = "turn_left";
                            man.Speed = "1";

                            if (keyCodes[i].Equals("i") || keyCodes[i].Equals("k") || keyCodes[i].Equals("m"))
                            {
                                man.Bearing = "turn_right";
                            }

                            if (keyCodes[i].Equals("g") || keyCodes[i].Equals("k"))
                            {
                                man.Speed = "2";
                            }

                            if (keyCodes[i].Equals("t") || keyCodes[i].Equals("i"))
                            {
                                man.Speed = "3";
                            }

                            break;
                        case "b":
                        case "h":
                        case "z":
                        case "u":
                        case "j":
                        case "n":
                            man.Bearing = "bank_left";
                            man.Speed = "1";

                            if (keyCodes[i].Equals("u") || keyCodes[i].Equals("j") || keyCodes[i].Equals("n"))
                            {
                                man.Bearing = "bank_right";
                            }

                            if (keyCodes[i].Equals("h") || keyCodes[i].Equals("j"))
                            {
                                man.Speed = "2";
                            }

                            if (keyCodes[i].Equals("z") || keyCodes[i].Equals("u"))
                            {
                                man.Speed = "3";
                            }

                            break;
                    }
                }
            }
        }

        //THIS IS ONLY FOR TESTING MOVEMENTS!!!!!! DELETE LATER ON!!!!
        //StartCoroutine(GameObject.Find("ScriptHolder").GetComponent<CoroutineHandler>().MoveShipOverTime(GameObject.FindGameObjectsWithTag("SmallShipContainer")[1], man));
        //THIS IS ONLY FOR TESTING MOVEMENTS!!!!!! DELETE LATER ON!!!!

        if (MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getSelectedhip() != null)
        {
            //TODO Check if comparing pilot names is enough/the right way!!!!!!!
            if (!PilotCardPanel.transform.Find("PilotName").gameObject.GetComponent<UnityEngine.UI.Text>().text.Equals(MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getSelectedhip().getPilot().Name.ToLower()))
            {
                guiHandler.hideGameObject(PilotCardPanel);
            }

            if (!PilotCardPanel.activeSelf)
            {
                guiHandler.showPilotCard(PilotCardPanel);
                matchHandlerService.showFiringArc(matchHandlerService.getShipHolderForShip(MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getSelectedhip()));

                if (MatchDatas.getCurrentPhase() == MatchDatas.phases.PLANNING && MatchHandlerUtil.isPlayersOwnShip())
                {
                    guiHandler.showManeuverSelector(PilotCardPanel);
                }
            }
        } else
        {
            MatchHandlerUtil.hideActiveShipHighlighters();
            MatchDatas.setActiveShip(null);
            guiHandler.hideGameObject(PilotCardPanel);
            matchHandlerService.hideFiringArcs();
        }

        if (MatchDatas.getCurrentPhase() == MatchDatas.phases.SQUADRON_PLACEMENT)
        {
            updateInfoPanel(SQUADRON_PLACEMENT_INFO);
        }

        if (MatchDatas.getCurrentPhase() == MatchDatas.phases.PLANNING)
        {
            updateInfoPanel(PLANNING_INFO);

            if (MatchHandlerUtil.maneuversPlanned())
            {
                updateInfoPanel(EMPTY_TEXT);
                PhaseHandlerService.nextPhase();
            }
        }

        if (MatchDatas.getCurrentPhase() == MatchDatas.phases.ACTIVATION)
        {
            // TODO Only show this to the current player!!
            if (availableShips.Count > 1)
            {
                foreach (LoadedShip ship in availableShips)
                {
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("SmallShipContainer"))
                    {
                        if (go.transform.GetComponent<ShipProperties>().getLoadedShip().getShip().ShipId.Equals(ship.getShip().ShipId) && go.transform.GetComponent<ShipProperties>().getLoadedShip().getPilotId() == ship.getPilotId())
                        {
                            updateInfoPanel(MULTIPLE_AVAILABLE_SHIPS_INFO);
                            MatchHandlerUtil.setShipHighlighters(go, true);
                        }
                    }
                }
            }
            else if (availableShips.Count == 1)
            {
                // DUPLICATED IN GameObjectDragAndDrop!!
                foreach (GameObject go in GameObject.FindGameObjectsWithTag("SmallShipContainer"))
                {
                    if (
                        go.transform.GetComponent<ShipProperties>().getLoadedShip().getShip().ShipId.Equals(availableShips[0].getShip().ShipId)
                        && go.transform.GetComponent<ShipProperties>().getLoadedShip().getPilotId() == availableShips[0].getPilotId()
                        && !go.transform.GetComponent<ShipProperties>().getLoadedShip().isHasBeenActivatedThisRound()
                    )
                    {
                        go.transform.GetComponent<ShipProperties>().getLoadedShip().setHasBeenActivatedThisRound(true);
                        StartCoroutine(GameObject.Find("ScriptHolder").GetComponent<CoroutineHandler>().MoveShipOverTime(go, go.transform.GetComponent<ShipProperties>().getLoadedShip().getPlannedManeuver()));
                    }
                }
            }
            else
            {
                updateInfoPanel(EMPTY_TEXT);
                MatchHandlerUtil.hideActiveShipHighlighters();
                PhaseHandlerService.nextPhase();
            }
        }
    }

    private void updateInfoPanel(string infoText)
    {
        if (infoText.Equals(EMPTY_TEXT) || infoText == null)
        {
            if (GameInfoPanel.activeSelf)
            {
                guiHandler.hideGameObject(GameInfoPanel);
            }
        } else
        {
            GameInfoPanel.transform.Find("InfoText").gameObject.GetComponent<UnityEngine.UI.Text>().text = infoText;

            if (!GameInfoPanel.activeSelf)
            {
                guiHandler.showGameObject(GameInfoPanel);
            }
        }
    }

    public static void handleManeuverSelection(Maneuver selectedManeuver)
    {
        // Hide (all) previous highlight
        foreach (Transform maneuverHolder in GameObject.Find("ShipManeuvers").GetComponentsInChildren<Transform>())
        {
            if (maneuverHolder.Find("ManeuverOverlay") != null && maneuverHolder.Find("ManeuverOverlay").gameObject.activeSelf)
            {
                maneuverHolder.Find("ManeuverOverlay").gameObject.SetActive(false);
            }
        }

        // Highlight the selected maneuver
        foreach (Transform maneuverHolder in GameObject.Find("ShipManeuvers").GetComponentsInChildren<Transform>())
        {
            if (maneuverHolder.Find("Image") != null && maneuverHolder.Find("Image").gameObject.GetComponent<ManeuverSelectionEvent>() as ManeuverSelectionEvent != null)
            {
                Maneuver maneuver = maneuverHolder.Find("Image").gameObject.GetComponent<ManeuverSelectionEvent>().maneuver;

                if (maneuver.Speed.Equals(selectedManeuver.Speed) && maneuver.Bearing.Equals(selectedManeuver.Bearing))
                {
                    maneuverHolder.Find("ManeuverOverlay").gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    public static void collectUpcomingAvailableShips(bool ascending)
    {
        List<LoadedShip> ships1 = MatchDatas.getPlayers()[0].getNextShips(MatchDatas.getCurrentLevel(), ascending);
        List<LoadedShip> ships2 = MatchDatas.getPlayers()[1].getNextShips(MatchDatas.getCurrentLevel(), ascending);
        // TODO add this bit when 3 player rules are getting included!
        //LoadedShip ship3 = MatchDatas.getPlayers()[2].getNextShip(MatchDatas.getCurrentLevel(), ascending);
        
        bool ship1IsEmpty = ships1 == null || ships1.Capacity == 0;
        bool ship2IsEmpty = ships2 == null || ships2.Capacity == 0;

        if (ship1IsEmpty && ship2IsEmpty)
        {
            PhaseHandlerService.nextPhase();
        } else
        {
            if (ship1IsEmpty && !ship2IsEmpty)
            {
                availableShips = ships2;
                MatchDatas.setActivePlayerIndex(1);
            }

            if (ship2IsEmpty && !ship1IsEmpty)
            {
                availableShips = ships1;
                MatchDatas.setActivePlayerIndex(0);
            }

            if (!ship1IsEmpty && !ship2IsEmpty)
            {
                if (ships1[0].getPilot().Level == ships2[0].getPilot().Level)
                {
                    foreach (LoadedShip ship in ships1)
                    {
                        if (!ship.isHasBeenActivatedThisRound() && MatchDatas.getPlayers()[0].getHasInitiative())
                        {
                            availableShips = ships1;
                            MatchDatas.setActivePlayerIndex(0);
                            break;
                        }
                    }

                    foreach (LoadedShip ship in ships2)
                    {
                        if (!ship.isHasBeenActivatedThisRound() && MatchDatas.getPlayers()[1].getHasInitiative())
                        {
                            availableShips = ships2;
                            MatchDatas.setActivePlayerIndex(1);
                            break;
                        }
                    }
                }
                else
                {
                    if (ascending)
                    {
                        if (ships1[0].getPilot().Level < ships2[0].getPilot().Level)
                        {
                            availableShips = ships1;
                            MatchDatas.setActivePlayerIndex(0);
                        }
                        else
                        {
                            availableShips = ships2;
                            MatchDatas.setActivePlayerIndex(1);
                        }
                    }
                    else
                    {
                        if (ships1[0].getPilot().Level > ships2[0].getPilot().Level)
                        {
                            availableShips = ships1;
                            MatchDatas.setActivePlayerIndex(0);
                        }
                        else
                        {
                            availableShips = ships2;
                            MatchDatas.setActivePlayerIndex(1);
                        }
                    }
                }
            }

            if (MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].isAI() && MatchDatas.getCurrentPhase() == MatchDatas.phases.SQUADRON_PLACEMENT)
            {
                moveAIShips(ascending);
            }
        }
    }

    public static List<LoadedShip> getAvailableShips()
    {
        return availableShips;
    }

    private static void moveAIShips(bool ascending)
    {
        GameObject[] ships = GameObject.FindGameObjectsWithTag("SmallShipContainer");

        foreach (GameObject shipObject in ships)
        {
            foreach (LoadedShip ship in availableShips)
            {
                if (ship.getPilotId() == shipObject.GetComponent<ShipProperties>().getLoadedShip().getPilotId() && ship.getShip().ShipId.Equals(shipObject.GetComponent<ShipProperties>().getLoadedShip().getShip().ShipId))
                {
                    shipObject.transform.position = mocker.getNextMockPosition();
                    shipObject.GetComponent<ShipProperties>().setMovable(false);
                    shipObject.GetComponent<ShipProperties>().getLoadedShip().setHasBeenActivatedThisRound(true);
                }
            }
        }

        collectUpcomingAvailableShips(ascending);
    }
}
