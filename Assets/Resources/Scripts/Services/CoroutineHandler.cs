using ShipsXMLCSharp;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineHandler : MonoBehaviour
{
    private Maneuver maneuverToComplete = null;
    private GameObject target = null;

    public void rollAttackDiceCallback()
    {
        StartCoroutine(RollAttackDice(MatchHandlerUtil.displayInitiativeChoser));
    }

    public IEnumerator TurnShipAround(GameObject objectToMove, bool lastMovement)
    {
        float elapsedTime = 0.0f;
        float maneuverDurationInSeconds = 0.5f;
        float rotationDegree = 180.0f;

        Vector3 startingPos = objectToMove.transform.position;
        Vector3 center = startingPos + objectToMove.transform.up * 500.0f;

        while (elapsedTime < maneuverDurationInSeconds)
        {
            objectToMove.transform.RotateAround(center, -objectToMove.transform.right, (rotationDegree / maneuverDurationInSeconds) * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Vector3 angles = objectToMove.transform.rotation.eulerAngles;
        angles.z = rotationDegree;
        objectToMove.transform.rotation = Quaternion.Euler(angles);

        StartCoroutine(MoveAndRollShip(objectToMove, 180.0f, objectToMove.transform.up * 1000.0f, lastMovement));
    }

    public IEnumerator MoveAndRollShip(GameObject objectToMove, float rollDegree, Vector3 distance, bool lastMovement)
    {
        float elapsedTime = 0.0f;
        float maneuverDurationInSeconds = 0.5f;

        Vector3 startingPos = objectToMove.transform.position;
        Vector3 end = startingPos + distance;
        Vector3 angles = objectToMove.transform.rotation.eulerAngles;
        float endAngleZ = angles.z + rollDegree;

        while (elapsedTime < maneuverDurationInSeconds)
        {
            angles = objectToMove.transform.rotation.eulerAngles;
            angles.z += rollDegree * Time.deltaTime * 2;
            objectToMove.transform.rotation = Quaternion.Euler(angles);
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / maneuverDurationInSeconds));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //Not sure if needed...
        end.y = -500.0f;
        //--
        angles.x = 0.0f;
        angles.z = endAngleZ;

        objectToMove.transform.rotation = Quaternion.Euler(angles);
        objectToMove.transform.position = end;

        if (lastMovement)
        {
            afterManeuver();
        }
    }

    public IEnumerator MoveShipOverTime(GameObject objectToMove, Maneuver maneuver)
    {
        /*if (isMoving)
        {
            yield break; ///exit if this is still running
        }*/

        beforeManeuver(objectToMove, maneuver);

        float maneuverDurationInSeconds = 1.0f;
        float elapsedTime = 0.0f;
        int speed = Int32.Parse(maneuver.Speed == null ? "0" : maneuver.Speed);
        Vector3 startingPos = objectToMove.transform.position;

        switch (maneuver.Bearing)
        {
            case "koiogran":
            case "straight":
                Vector3 end = startingPos + objectToMove.transform.forward * (speed + 1) * 500;

                while (elapsedTime < maneuverDurationInSeconds)
                {
                    objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / maneuverDurationInSeconds));
                    elapsedTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                objectToMove.transform.position = end;

                if (maneuver.Bearing.Equals("koiogran"))
                {
                    StartCoroutine(this.TurnShipAround(objectToMove, true));
                } else
                {
                    afterManeuver();
                }

                break;
            case "bank_left":
            case "bank_right":
            case "turn_left":
            case "turn_right":
                float turnDegree = 90.0f;
                float turnMultiplier = 0.875f;
                float turnStep = 0.7f;

                if (maneuver.Bearing.Contains("bank"))
                {
                    turnDegree = 45.0f;
                    turnMultiplier = 2.0f;
                    turnStep = 1.25f;
                }

                if (maneuver.Bearing.Contains("left"))
                {
                    turnDegree = turnDegree * (-1);
                    turnMultiplier = turnMultiplier * (-1);
                }

                StartCoroutine(this.MoveAndRollShip(objectToMove, -turnDegree, objectToMove.transform.forward * 250.0f, false));

                float distance = 500 * (turnMultiplier + ((speed - 1) * turnStep));
                Vector3 center = startingPos + objectToMove.transform.right * distance;
                float finalRotation = objectToMove.transform.eulerAngles.y + turnDegree;

                if (finalRotation >= 360.0f)
                {
                    finalRotation = 0.0f;
                }

                while (elapsedTime < maneuverDurationInSeconds)
                {
                    objectToMove.transform.RotateAround(center, Vector3.up, turnDegree * Time.deltaTime);
                    elapsedTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                Vector3 angles = objectToMove.transform.rotation.eulerAngles;
                angles.y = finalRotation;
                objectToMove.transform.rotation = Quaternion.Euler(angles);

                StartCoroutine(this.MoveAndRollShip(objectToMove, turnDegree, objectToMove.transform.forward * 250.0f, true));
                
                break;
        }
    }

    public IEnumerator RollAttackDice(System.Action<Player> callBack)
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

        if (LocalDataWrapper.getPlayer().getLastDiceResults()[0] == DiceRollerBase.DICE_RESULT_HIT_OR_EVADE || LocalDataWrapper.getPlayer().getLastDiceResults()[0] == DiceRollerBase.DICE_RESULT_CRIT)
        {
            result = MatchDatas.getPlayers()[0];
        }
        else
        {
            result = MatchDatas.getPlayers()[1];
        }

        callBack(result);
    }

    private void beforeManeuver(GameObject objectToMove, Maneuver maneuver)
    {
        if (target == null || maneuver == null)
        {
            target = objectToMove;
            maneuverToComplete = maneuver;
        }
    }

    private void afterManeuver()
    {
        MatchHandlerUtil.applyManeuverBonus(target, maneuverToComplete.Difficulty);

        // TODO Make player chosen an action (if available)
        if (target.transform.GetComponent<ShipProperties>().getLoadedShip().getTokenIdByType(typeof(StressToken)) == 0)
        {
            target.transform.GetComponent<ShipProperties>().getLoadedShip().setNumOfActions(1);
            //Action choser.....
            //target.transform.GetComponent<ShipProperties>().getLoadedShip().setBeforeAction(true);

            GameObject actionChoserPopup = null;
            int actionIndex = 0;

            foreach(GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
            {
                if (go.name.Equals("ActionChoserPopup"))
                {
                    actionChoserPopup = go;
                }
            }

            if (actionChoserPopup != null)
            {
                // TODO outsource this fragment to UI handler!!!!!!!
                foreach (String action in MatchDatas.getPlayers()[MatchDatas.getActivePlayerIndex()].getActiveShip().getShip().Actions.Action)
                {
                    Image image = null;
                    Sprite sprite = Resources.Load<Sprite>(SquadBuilderConstants.IMAGE_FOLDER_NAME + "/" + action.Replace(" ", "-"));

                    Transform actionIconPrefab = Resources.Load<Transform>(SquadBuilderConstants.PREFABS_FOLDER_NAME + "/" + SquadBuilderConstants.ACTION_ICON);
                    RectTransform rt = (RectTransform)actionIconPrefab;
                    float actionIconWidth = rt.rect.width;

                    Transform actionIcon = (Transform)GameObject.Instantiate(
                        actionIconPrefab,
                        new Vector3((actionIndex * actionIconWidth) + SquadBuilderConstants.UPGRADE_IMAGE_X_OFFSET + 15, SquadBuilderConstants.UPGRADE_IMAGE_Y_OFFSET - 30, SquadBuilderConstants.UPGRADE_IMAGE_Z_OFFSET),
                        Quaternion.identity
                    );

                    Image actionIconImage = actionIcon.gameObject.GetComponent<Image>();

                    actionIcon.gameObject.AddComponent<ActionSelectorEvents>();
                    actionIcon.gameObject.GetComponent<ActionSelectorEvents>().setActionName(action);
                    actionIcon.transform.SetParent(actionChoserPopup.transform, false);
                    actionIconImage.sprite = sprite;
                    actionIconImage.color = new Color(actionIconImage.color.r, actionIconImage.color.g, actionIconImage.color.b, 1.0f);

                    actionIndex++;
                }

                actionChoserPopup.SetActive(true);
                PhaseHandlerService.initActionPhase();
            }
        }

        maneuverToComplete = null;
        target = null;
    }
}
