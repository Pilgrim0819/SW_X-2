using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PilotsXMLCSharp;
using ShipsXMLCSharp;
using UnityEngine.UI;

public class SummaryBeforeMatch : MonoBehaviour {

    private Mocker mocker = new Mocker();
    public GameObject cardsHolder;

    private const string PREFABS_FOLDER = "Prefabs";
    private const string IMAGE_FOLDER_NAME = "images";
    private const string PREFAB_FOLDER_NAME = "Prefabs/PilotCardPrefab";
    private const string VS_PREFAB_NAME = "Prefabs/VSPrefab";
    private const string SUMMARY_UUPER_TEXT_PREFAB = "Prefabs/SummaryUpperTextPrefab";
    private const string PILOT_LEVEL_TEXT = "Level: ";
    private const string PILOT_COST_TEXT = "Cost: ";
    
    private const int offsetX = 150;
    private const int offsetY = -900;
    private const int shipCardWidth = 200;

    private const float iterationSleepTime = 0.75f;

    void Start()
    {
        StartCoroutine(checkObjectsHaveStopped());
        StartCoroutine(populateSummaryView());
        determineInitiative();
    }

    void Update()
    {

    }

    IEnumerator checkObjectsHaveStopped()
    {
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
    }

    IEnumerator populateSummaryView()
    {
        mocker.mockPlayerSquadrons();

        cardsHolder = GameObject.Find("Ships Scroll");
        int sideIndex = 0;
        int pilotIndex = 0;

        foreach (Player player in MatchDatas.getPlayers())
        {
            foreach (LoadedShip loadedShip in player.getSquadron())
            {
                Transform pilotCardPrefab = Resources.Load<Transform>(PREFAB_FOLDER_NAME);
                Sprite pilotSprite = Resources.Load<Sprite>(IMAGE_FOLDER_NAME + "/" + loadedShip.getPilot().Name);
                Vector3 position = cardsHolder.transform.position;

                Transform shipCard = (Transform)GameObject.Instantiate(
                    pilotCardPrefab,
                    new Vector3((position.x - 1000) + (shipCardWidth * pilotIndex) + offsetX, position.y + offsetY, position.z),
                    Quaternion.identity
                );

                shipCard.transform.SetParent(cardsHolder.transform, false);
                shipCard.transform.Find("Pilot Name").gameObject.GetComponent<UnityEngine.UI.Text>().text = loadedShip.getPilot().Name.ToString();
                shipCard.transform.Find("Pilot Image").gameObject.GetComponent<Image>().sprite = pilotSprite;
                shipCard.transform.Find("Pilot Level Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = PILOT_LEVEL_TEXT + loadedShip.getPilot().Level;
                shipCard.transform.Find("Pilot Cost Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = PILOT_COST_TEXT + loadedShip.getPilot().Cost;
                shipCard.transform.Find("Pilot Description Text").gameObject.GetComponent<UnityEngine.UI.Text>().text = loadedShip.getPilot().Text;

                AddPilotToSquardon shipcardPilotScript = shipCard.transform.GetComponent<AddPilotToSquardon>();
                shipcardPilotScript.setPilot(loadedShip.getPilot());

                pilotIndex++;

                yield return new WaitForSeconds(iterationSleepTime);
            };

            Transform UpperTextPrefab = Resources.Load<Transform>(SUMMARY_UUPER_TEXT_PREFAB);
            float UpperTextX = sideIndex == 0 ? -460 : 460;
            Transform UpperText = (Transform)GameObject.Instantiate(
                UpperTextPrefab,
                new Vector3(UpperTextX, 360, cardsHolder.transform.position.z),
                Quaternion.identity
            );

            UpperText.GetComponent<Text>().text = player.getChosenSide() + " points: " + player.getCumulatedSquadPoints();
            UpperText.transform.SetParent(cardsHolder.transform, false);

            yield return new WaitForSeconds(iterationSleepTime);

            sideIndex++;

            if (pilotIndex < MatchDatas.getPlayers().Capacity - 1)
            {
                Transform VSPrefab = Resources.Load<Transform>(VS_PREFAB_NAME);
                Transform VSText = (Transform)GameObject.Instantiate(
                    VSPrefab,
                    new Vector3((cardsHolder.transform.position.x - 1000) + (shipCardWidth * pilotIndex) + offsetX, cardsHolder.transform.position.y + offsetY, cardsHolder.transform.position.z),
                    Quaternion.identity
                );
                
                VSText.transform.SetParent(cardsHolder.transform, false);

                pilotIndex++;

                yield return new WaitForSeconds(iterationSleepTime);
            }
        }
    }

    /* INITIATIVE DETERMINATION PART */
    private void determineInitiative()
    {
        if (squadScoresAreEqual())
        {
            Player player = rollForInitiative();
            chooseInitiative(player);
        }
        else
        {
            chooseInitiative(getPlayerWithLowestSquadScore());
        }

        /********************************************TODO remove when tesing is done!*/
        if (MatchDatas.getPlayers()[0].getHasInitiative())
        {
            Debug.Log("Player " + MatchDatas.getPlayers()[0].getPlayerName() + " has initiative!");
        } else
        {
            Debug.Log("Player " + MatchDatas.getPlayers()[1].getPlayerName() + " has initiative!");
        }
        /********************************************TODO remove when tesing is done!*/
    }

    private Player rollForInitiative()
    {
        Player result = null;

        //TODO Roll dye with player #0!!!
        if (PlayerDatas.getDiceResults()[0] == DiceRollerBase.DICE_RESULT_HIT_OR_EVADE || PlayerDatas.getDiceResults()[0] == DiceRollerBase.DICE_RESULT_CRIT)
        {
            result = MatchDatas.getPlayers()[0];
        }
        else
        {
            result = MatchDatas.getPlayers()[1];
        }

        return result;
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

    private void chooseInitiative(Player player)
    {
        //TODO implement actual functionality, NOT RANDOM!!
        int randomIndex = Random.Range(0, 1);

        MatchDatas.getPlayers()[randomIndex].setInitiative();
    }
    /* INITIATIVE DETERMINATION PART */
}
