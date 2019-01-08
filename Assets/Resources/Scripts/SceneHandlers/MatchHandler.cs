﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PilotsXMLCSharp;
using ShipsXMLCSharp;
using UnityEngine.UI;

public class MatchHandler : MonoBehaviour {

    public GameObject initiativePanel;
    public GameObject PilotCardPanel;

    private Mocker mocker = new Mocker();

    private const string PREFABS_FOLDER = "Prefabs";
    private const string INITIATIVE_BUTTON_PREFAB_NAME = "/InitiativeButtonPrefab";
    private const string EQUAL_SQUAD_SCORE_TEXT = "The two players' squad scores are equal.";
    private const string EQUAL_SQUAD_SCORE_TEXT_ENDING = " will throw a dice to determine who can choose which player will have initiative.";
    private const string NOT_EQUAL_SQUAD_SCORE_TEXT = " has lesser squad score so he can choose which player will have initiative.";

    private const int offsetX = 500;
    private const int offsetY = 500;
    private const int offsetZ = 500;

	void Start() {
        mocker.mockPlayerSquadrons();
        DiceRollerBase.setUpDiceRollerBase(ForceMode.VelocityChange, 10.0f);

        foreach (Player player in MatchDatas.getPlayers())
        {
            int loopIndex = 0;
            Vector3 startingPosition = getShipCollectionHolderPosition(player.getPlayerID());

            foreach (LoadedShip loadedShip in player.getSquadron())
            {
                Ship ship = loadedShip.getShip();
                string shipType = ship.ShipId;
                GameObject shipHolderPrefab = (GameObject)Resources.Load(PREFABS_FOLDER + "/SmallShipContainerPrefab", typeof(GameObject));
                GameObject shipPrefab = (GameObject)Resources.Load(PREFABS_FOLDER + "/" + shipType, typeof(GameObject));

                //TODO Tweak these coordinates!
                float posX = startingPosition.x + (loopIndex * offsetX);
                float posY = startingPosition.y;
                float posZ = startingPosition.z;

                GameObject shipHolderGameObject = (GameObject)GameObject.Instantiate(
                    shipHolderPrefab,
                    new Vector3(posX, posY, posZ),
                    Quaternion.identity
                );

                // TODO change models, so offset is not needed anymore!
                Vector3 shipOffset = new Vector3(2.81896f * 500.0f, 0.08181581f * 1000.0f, 3.286796f * 500.0f);

                GameObject shipGameObject = (GameObject)GameObject.Instantiate(
                    shipPrefab,
                    shipHolderGameObject.GetComponent<Renderer>().bounds.center + shipOffset,
                    Quaternion.identity
                );

                shipGameObject.transform.SetParent(shipHolderGameObject.transform, true);

                shipHolderGameObject.GetComponent<ShipProperties>().setPilot(loadedShip.getPilot());
                shipHolderGameObject.GetComponent<ShipProperties>().setShip(loadedShip.getShip());

                loopIndex++;
            };
        }

        determineInitiative();
    }

	void Update() {
        if (Input.GetKey("escape"))
        {
            MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].setSelectedShip(null);
        }

	    if (MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getSelectedhip() != null)
        {
            //TODO Check if comparing pilot names is enough/the right way!!!!!!!
            if (!PilotCardPanel.transform.Find("PilotName").gameObject.GetComponent<UnityEngine.UI.Text>().text.Equals(MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getSelectedhip().getPilot().Name))
            {
                hidePilotCard();
            }

            if (!PilotCardPanel.activeSelf)
            {
                showPilotCard();
            }
        } else
        {
            hidePilotCard();
        }
	}

    IEnumerator RollAttackDice()
    {
        DiceRollerBase.showDiceArea(1, true);
        Rigidbody[] GOS = FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];
        bool allSleeping = false;

        while (!allSleeping)
        {
            allSleeping = true;

            foreach (Rigidbody GO in GOS)
            {
                if (!GO.IsSleeping())
                {
                    allSleeping = false;
                    yield return null;
                    break;
                }
            }

        }
        
        DiceRollerBase.getDiceResults(GOS);

        Player result = null;

        if (PlayerDatas.getDiceResults()[0] == DiceRollerBase.DICE_RESULT_HIT_OR_EVADE || PlayerDatas.getDiceResults()[0] == DiceRollerBase.DICE_RESULT_CRIT)
        {
            result = MatchDatas.getPlayers()[0];
        }
        else
        {
            result = MatchDatas.getPlayers()[1];
        }
        
        displayInitiativeChoser(result);
    }

    private void showPilotCard()
    {
        LoadedShip ship = MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getSelectedhip();
        string side = "";

        foreach (Player player in MatchDatas.getPlayers())
        {
            foreach (LoadedShip ls in player.getSquadron())
            {
                if (ls.getShip().ShipId == ship.getShip().ShipId && ls.getPilot().Name.Equals(ship.getPilot().Name))
                {
                    side = player.getChosenSide();
                    break;
                }

                if (!side.Equals(""))
                {
                    break;
                }
            }
        }

        PilotCardPanel.transform.Find("PilotName").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getPilot().Name.ToLower();
        PilotCardPanel.transform.Find("ShipType").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().ShipName.ToLower();
        PilotCardPanel.transform.Find("Description").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getPilot().Text.ToLower();
        PilotCardPanel.transform.Find("BaseCostHolder/BaseCost").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getPilot().Cost.ToString();
        PilotCardPanel.transform.Find("ShieldValueHolder/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Shield.ToString();
        PilotCardPanel.transform.Find("HullValueHolder/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Hull.ToString();
        PilotCardPanel.transform.Find("AgilityValueHolder/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Agility.ToString();
        PilotCardPanel.transform.Find("PowerValueHolder/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getShip().Weapon.ToString();
        PilotCardPanel.transform.Find("PilotLevelHolder/Value").gameObject.GetComponent<UnityEngine.UI.Text>().text = ship.getPilot().Level.ToString();

        Sprite sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + side + "_pilot_card");
        PilotCardPanel.transform.Find("CardImage").gameObject.GetComponent<Image>().sprite = sprite;
        Image image = PilotCardPanel.transform.Find("CardImage").gameObject.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1.0f);

        Sprite sprite1 = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + ship.getShip().ShipId);
        PilotCardPanel.transform.Find("CardImage/ShipImage").gameObject.GetComponent<Image>().sprite = sprite1;
        Image image1 = PilotCardPanel.transform.Find("CardImage/ShipImage").gameObject.GetComponent<Image>();
        image1.color = new Color(image1.color.r, image1.color.g, image1.color.b, 1.0f);

        PilotCardPanel.SetActive(true);
    }

    private void hidePilotCard()
    {
        PilotCardPanel.SetActive(false);
    }

    private Vector3 getShipCollectionHolderPosition(int playerID)
    {
        return GameObject.Find("ShipCollection" + playerID).transform.position;
    }

    /* INITIATIVE DETERMINATION PART */
    private void determineInitiative()
    {
        if (squadScoresAreEqual())
        {
            PointerClickCallback callback = rollAttackDiceCallback;
            SystemMessageService.showErrorMsg(EQUAL_SQUAD_SCORE_TEXT + " " + MatchDatas.getPlayers()[0].getPlayerName() + EQUAL_SQUAD_SCORE_TEXT_ENDING, GameObject.Find("SystemMessagePanel"), 1, callback);

            //StartCoroutine(RollAttackDice());
        }
        else
        {
            PointerClickCallback callback = displayInitiativeChooserCallback;
            SystemMessageService.showErrorMsg(getPlayerWithLowestSquadScore().getPlayerName() + NOT_EQUAL_SQUAD_SCORE_TEXT, GameObject.Find("SystemMessagePanel"), 1, callback);

            //StartCoroutine(RollAttackDice());
            //displayInitiativeChoser(getPlayerWithLowestSquadScore());
        }
    }

    public void rollAttackDiceCallback()
    {
        StartCoroutine(RollAttackDice());
    }

    public void displayInitiativeChooserCallback()
    {
        displayInitiativeChoser(getPlayerWithLowestSquadScore());
    }

    private bool squadScoresAreEqual()
    {
        if (MatchDatas.getPlayers().Capacity == 2)
        {
            if (MatchDatas.getPlayers()[0].getCumulatedSquadPoints() == MatchDatas.getPlayers()[1].getCumulatedSquadPoints())
            {
                return true;
            }
        }
        else if (MatchDatas.getPlayers().Capacity == 3)
        {
            if (MatchDatas.getPlayers()[0].getCumulatedSquadPoints() == MatchDatas.getPlayers()[1].getCumulatedSquadPoints() && MatchDatas.getPlayers()[0].getCumulatedSquadPoints() == MatchDatas.getPlayers()[2].getCumulatedSquadPoints())
            {
                return true;
            }
        }

        return false;
    }

    private Player getPlayerWithLowestSquadScore()
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

    private void displayInitiativeChoser(Player player)
    {
        Debug.Log(player.getPlayerName() + " can chose who gets initiative!");
        int playerIndex = 0;

        if (PlayerDatas.getPlayerName().Equals(player.getPlayerName()))
        {
            foreach (Player currentPlayer in MatchDatas.getPlayers())
            {
                Transform InitiativeButtonPrefab = Resources.Load<Transform>(PREFABS_FOLDER + INITIATIVE_BUTTON_PREFAB_NAME);
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
    /* INITIATIVE DETERMINATION PART */

    private LoadedShip getNextShip(bool ascending)
    {
        LoadedShip ship1 = MatchDatas.getPlayers()[0].getNextShip(MatchDatas.getCurrentLevel(), ascending);
        LoadedShip ship2 = MatchDatas.getPlayers()[1].getNextShip(MatchDatas.getCurrentLevel(), ascending);
        // TODO add this bit when 3 player rules are getting included!
        //LoadedShip ship3 = MatchDatas.getPlayers()[2].getNextShip(MatchDatas.getCurrentLevel(), ascending);

        if (ship1 == null && ship2 != null)
        {
            return ship2;
        }

        if (ship2 == null && ship1 != null)
        {
            return ship1;
        }

        if (ship1 != null && ship2 != null)
        {
            if (ship1.getPilot().Level == ship2.getPilot().Level)
            {
                if (ship1.isHasBeenActivatedThisRound())
                {
                    return ship2;
                } else if (ship2.isHasBeenActivatedThisRound())
                {
                    return ship1;
                } else
                {
                    return MatchDatas.getPlayers()[0].getHasInitiative() ? ship1 : ship2;
                }
            } else
            {
                if (ascending)
                {
                    return ship1.getPilot().Level < ship2.getPilot().Level ? ship1 : ship2;
                } else
                {
                    return ship1.getPilot().Level > ship2.getPilot().Level ? ship1 : ship2;
                }
            }
        }

        return null;
    }
}
